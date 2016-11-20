/* MidiUtils

LICENSE - The MIT License (MIT)

Copyright (c) 2013-2015 Tomona Nanase

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

namespace MidiUtils.IO
{
    public class MidiIn : IDisposable
    {
        #region -- Private Fields --

        private const int BufferSize = 256;

        private readonly NativeMethods.MidiInProc midiInProc;
        private NativeMethods.MIDIHDR midiHeader;
        private IntPtr ptrHeader, handle;
        private readonly uint headerSize;
        private readonly Queue<byte[]> exclusiveQueue;
        private bool isClosing;

        #endregion

        #region -- Public Properties --

        public bool IsPlaying { get; private set; }

        /// <summary>
        /// オブジェクトが破棄されたかを表す真偽値を取得します。
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// MIDI-IN デバイスの数を取得します。
        /// </summary>
        public static int InputCount
        {
            get
            {
                CheckPlatform();

                return NativeMethods.midiInGetNumDevs();
            }
        }

        /// <summary>
        /// MIDI-IN デバイス名を格納した配列を取得します。
        /// </summary>
        public static string[] InputDeviceNames
        {
            get
            {
                CheckPlatform();

                var count = InputCount;
                var result = new NativeMethods.MIDIINCAPS();
                var names = new string[count];

                for (var i = 0; i < count; i++)
                {
                    NativeMethods.midiInGetDevCaps((UIntPtr)i,
                                                   ref result,
                                                   (uint)Marshal.SizeOf(typeof(NativeMethods.MIDIINCAPS)));
                    names[i] = result.szPname;
                }

                return names;
            }
        }

        #endregion

        #region -- Public Events --

        public event EventHandler<ReceivedMidiEventEventArgs> ReceivedMidiEvent;

        public event EventHandler<ReceivedExclusiveMessageEventArgs> ReceivedExclusiveMessage;

        /// <summary>
        /// デバイスが開かれた時に発生します。
        /// </summary>
        public event EventHandler Opened;

        /// <summary>
        /// デバイスが閉じられた時に発生します。
        /// </summary>
        public event EventHandler Closed;

        #endregion

        #region -- Constructors --
        /// <summary>
        /// サンプリング周波数とデバイス ID を指定して新しい MidiInConnector クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="id">オープンされる MIDI-IN デバイスの ID。</param>
        public MidiIn(int id)
        {
            CheckPlatform();

            midiInProc = MidiProc;
            handle = IntPtr.Zero;
            exclusiveQueue = new Queue<byte[]>();

            if (!Open(id))
                throw new InvalidOperationException();

            headerSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MIDIHDR));
            midiHeader = new NativeMethods.MIDIHDR
            {
                data = Marshal.AllocHGlobal(BufferSize),
                bufferLength = BufferSize
            };
            Marshal.Copy(new byte[BufferSize], 0, midiHeader.data, BufferSize);

            ptrHeader = Marshal.AllocHGlobal((int)headerSize);
            Marshal.StructureToPtr(midiHeader, ptrHeader, true);

            NativeMethods.midiInPrepareHeader(handle, ptrHeader, headerSize);
            NativeMethods.midiInAddBuffer(handle, ptrHeader, headerSize);
        }
        #endregion

        #region -- Public Methods --
        /// <summary>
        /// MIDI-IN からの入力を開始します。
        /// </summary>
        public void Start()
        {
            if (IsDisposed)
                throw new ObjectDisposedException("this");

            if (IsPlaying)
                return;

            NativeMethods.midiInStart(handle);
            IsPlaying = true;
        }

        /// <summary>
        /// MIDI-IN からの入力を停止します。
        /// </summary>
        public void Stop()
        {
            if (IsDisposed)
                throw new ObjectDisposedException("this");

            if (!IsPlaying)
                return;

            NativeMethods.midiInStop(handle);
            IsPlaying = false;
        }

        /// <summary>
        /// MIDI-IN デバイスとの接続を閉じ、リソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region -- Protected Methods --
        /// <summary>
        /// このオブジェクトによって使用されているアンマネージリソースを解放し、オプションでマネージリソースも解放します。
        /// </summary>
        /// <param name="disposing">マネージリソースとアンマネージリソースの両方を解放する場合は true。
        /// アンマネージリソースだけを解放する場合は false。</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                Stop();
                Close();
            }

            IsDisposed = true;
        }
        #endregion

        #region -- Private Methods --

        private static void CheckPlatform()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                throw new PlatformNotSupportedException();
        }

        private void Close()
        {
            if (IsDisposed)
                return;

            if (handle == IntPtr.Zero)
                return;

            isClosing = true;

            NativeMethods.midiInReset(handle);
            NativeMethods.midiInUnprepareHeader(handle, ptrHeader, headerSize);
            NativeMethods.midiInClose(handle); // == NativeMethods.MMSYSERR_NOERROR;

            if (midiHeader.data != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(midiHeader.data);
                midiHeader.data = IntPtr.Zero;
            }

            if (ptrHeader != IntPtr.Zero)
            {
                Marshal.DestroyStructure(ptrHeader, typeof(NativeMethods.MIDIHDR));
                Marshal.FreeHGlobal(ptrHeader);
                ptrHeader = IntPtr.Zero;
            }

            handle = IntPtr.Zero;
        }

        private bool Open(int id)
        {
            return NativeMethods.midiInOpen(out handle,
                                            id,
                                            midiInProc,
                                            IntPtr.Zero,
                                            NativeMethods.CALLBACK_FUNCTION) == NativeMethods.MMSYSERR_NOERROR;
        }

        private void MidiProc(IntPtr hMidiIn, int wMsg, IntPtr dwInstance, int dwParam1, int dwParam2)
        {
            if (wMsg == NativeMethods.MIM_DATA)
            {
                if (IsDisposed || !IsPlaying)
                    return;

                if (ReceivedMidiEvent == null)
                    return;

                var @event = new MidiEvent((EventType)(dwParam1 & 0xf0),
                    dwParam1 & 0x0f,
                    dwParam1 >> 8 & 0xff,
                    dwParam1 >> 16 & 0xff);
                ReceivedMidiEvent(this, new ReceivedMidiEventEventArgs(@event, this));
            }
            else if (wMsg == NativeMethods.MIM_LONGDATA)
            {
                if (IsDisposed || !IsPlaying)
                    return;

                midiHeader = (NativeMethods.MIDIHDR)Marshal.PtrToStructure(ptrHeader,
                    typeof(NativeMethods.MIDIHDR));

                var buffer = new byte[BufferSize];
                Marshal.Copy(midiHeader.data, buffer, 0, BufferSize);
                var length = Array.IndexOf<byte>(buffer, 0xf7, 0, BufferSize);

                if (length == -1)
                {
                    exclusiveQueue.Enqueue(buffer);
                }
                else
                {
                    if (ReceivedExclusiveMessage != null)
                    {
                        var data = exclusiveQueue.SelectMany(a => a).Concat(buffer.Take(length)).Skip(1);
                        ReceivedExclusiveMessage(this, new ReceivedExclusiveMessageEventArgs(data.ToArray(), this));
                    }

                    exclusiveQueue.Clear();
                }

                if (!isClosing)
                    NativeMethods.midiInAddBuffer(handle, ptrHeader, headerSize);
            }
            else if (wMsg == NativeMethods.MIM_OPEN)
            {
                Opened?.Invoke(this, new EventArgs());
            }
            else if (wMsg == NativeMethods.MIM_CLOSE)
            {
                Closed?.Invoke(this, new EventArgs());
            }
        }

        #endregion

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        private static class NativeMethods
        {
            internal const int MMSYSERR_NOERROR = 0;
            internal const int CALLBACK_FUNCTION = 0x00030000;

            internal const int MIM_OPEN = 961;
            internal const int MIM_CLOSE = 962;
            internal const int MIM_DATA = 963;
            internal const int MIM_LONGDATA = 964;

            internal delegate void MidiInProc(IntPtr hMidiIn, int wMsg, IntPtr dwInstance, int dwParam1, int dwParam2);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern int midiInGetNumDevs();

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern int midiInClose(IntPtr hMidiIn);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern int midiInOpen(out IntPtr lphMidiIn,
                                                  int uDeviceID,
                                                  MidiInProc dwCallback,
                                                  IntPtr dwCallbackInstance,
                                                  int dwFlags);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern int midiInStart(IntPtr hMidiIn);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern int midiInStop(IntPtr hMidiIn);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern MMRESULT midiInGetDevCaps(UIntPtr uDeviceID, ref MIDIINCAPS caps, uint cbMidiInCaps);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern MMRESULT midiInPrepareHeader(IntPtr hMidiIn, IntPtr lpMidiInHdr, uint cbMidiInHdr);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern MMRESULT midiInAddBuffer(IntPtr hMidiIn, IntPtr lpMidiInHdr, uint cbMidiInHdr);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern MMRESULT midiInUnprepareHeader(IntPtr hMidiIn, IntPtr lpMidiInHdr, uint cbMidiInHdr);

            [DllImport("winmm.dll", SetLastError = true)]
            internal static extern MMRESULT midiInReset(IntPtr hMidiIn);

            [StructLayout(LayoutKind.Sequential)]
            public struct MIDIINCAPS
            {
                public ushort wMid;
                public ushort wPid;
                public uint vDriverVersion;     // MMVERSION
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
                public string szPname;
                public uint dwSupport;
            }

            internal enum MMRESULT : uint
            {
                MMSYSERR_NOERROR = 0,
                MMSYSERR_ERROR = 1,
                MMSYSERR_BADDEVICEID = 2,
                MMSYSERR_NOTENABLED = 3,
                MMSYSERR_ALLOCATED = 4,
                MMSYSERR_INVALHANDLE = 5,
                MMSYSERR_NODRIVER = 6,
                MMSYSERR_NOMEM = 7,
                MMSYSERR_NOTSUPPORTED = 8,
                MMSYSERR_BADERRNUM = 9,
                MMSYSERR_INVALFLAG = 10,
                MMSYSERR_INVALPARAM = 11,
                MMSYSERR_HANDLEBUSY = 12,
                MMSYSERR_INVALIDALIAS = 13,
                MMSYSERR_BADDB = 14,
                MMSYSERR_KEYNOTFOUND = 15,
                MMSYSERR_READERROR = 16,
                MMSYSERR_WRITEERROR = 17,
                MMSYSERR_DELETEERROR = 18,
                MMSYSERR_VALNOTFOUND = 19,
                MMSYSERR_NODRIVERCB = 20,
                WAVERR_BADFORMAT = 32,
                WAVERR_STILLPLAYING = 33,
                WAVERR_UNPREPARED = 34
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MIDIHDR
            {
                public IntPtr data;
                public uint bufferLength;
                public uint bytesRecorded;
                public IntPtr user;
                public uint flags;
                public IntPtr next;
                public IntPtr reserved;
                public uint offset;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                public IntPtr[] reservedArray;
            }
        }
    }

    public class ReceivedMidiEventEventArgs : EventArgs
    {
        #region -- Public Properties --

        public MidiEvent Event { get; private set; }

        public MidiIn MidiIn { get; private set; }

        #endregion

        #region -- Constructors --

        public ReceivedMidiEventEventArgs(MidiEvent @event, MidiIn midiIn)
        {
            Event = @event;
            MidiIn = midiIn;
        }

        #endregion
    }

    public class ReceivedExclusiveMessageEventArgs : EventArgs
    {
        #region -- Public Properties --

        public IEnumerable<byte> Message { get; private set; }

        public MidiIn MidiIn { get; private set; }

        #endregion

        #region -- Constructors --

        public ReceivedExclusiveMessageEventArgs(IEnumerable<byte> message, MidiIn midiIn)
        {
            Message = message;
            MidiIn = midiIn;
        }

        #endregion
    }
}
