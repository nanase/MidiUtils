/* MidiUtils

LICENSE - The MIT License (MIT)

Copyright (c) 2013-2014 Tomona Nanase

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
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MidiUtils.IO;

namespace MidiUtils.Sequencer
{
    /// <summary>
    /// イベントをスケジューリングし、決められた時間に送出するシーケンサを提供します。
    /// </summary>
    public class Sequencer
    {
        #region -- Private Fields --
        private long tick;
        private int interval = 5;
        private readonly long endOfTick;
        private double tempoFactor = 1.0;
        private double tickTime;
        private long loopBeginTick;
        private double progressTick;

        private int eventIndex;

        private Task sequenceTask;
        private readonly List<Event> eventList;
        private readonly object syncObject = new object();

        private volatile bool reqEnd;
        private volatile bool reqRewind;
        #endregion

        #region -- Public Properties --
        /// <summary>
        /// 一連のイベントを格納したシーケンスを取得します。
        /// </summary>
        public Sequence Sequence { get; }

        /// <summary>
        /// シーケンサの現在のテンポ (BPM) を取得します。
        /// </summary>
        public double Tempo { get; private set; } = 120.0;

        /// <summary>
        /// シーケンサの現在のティックを取得または設定します。
        /// </summary>
        public long Tick
        {
            get { return tick; }
            set
            {
                if (value < 0)
                    throw new ArgumentException();

                eventIndex = 0;

                if (sequenceTask == null)
                {
                    tick = value;
                }
                else
                {
                    lock (syncObject)
                    {
                        tick = value;
                        reqRewind = true;
                    }
                }
            }
        }

        /// <summary>
        /// シーケンサがスケジューリングのために割り込む間隔をミリ秒単位で取得または設定します。
        /// </summary>
        public int Interval
        {
            get { return interval; }
            set
            {
                if (value < 1)
                    throw new ArgumentException();

                interval = value;
            }
        }

        /// <summary>
        /// シーケンサのテンポに応じて乗算される係数を取得または設定します。
        /// </summary>
        public double TempoFactor
        {
            get { return tempoFactor; }
            set
            {
                if (value <= 0.0)
                    throw new ArgumentException();

                tempoFactor = value;
                RecalcTickTime();
            }
        }

        /// <summary>
        /// シーケンサのループが開始されるティック位置を取得または設定します。
        /// </summary>
        public long LoopBeginTick
        {
            get { return loopBeginTick; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                loopBeginTick = value;
            }
        }

        /// <summary>
        /// シーケンサが指定位置でループされるかの真偽値を取得または設定します。
        /// </summary>
        public bool Looping { get; set; }
        #endregion

        #region -- Public Events --
        /// <summary>
        /// シーケンサによってスケジュールされたイベントが送出される時に発生します。
        /// </summary>
        public event EventHandler<TrackEventArgs> OnTrackEvent;

        /// <summary>
        /// イベントによってテンポが変更された時に発生します。このイベントは TempoFactor の影響を受けません。
        /// </summary>
        public event EventHandler<TempoChangedEventArgs> TempoChanged;

        /// <summary>
        /// シーケンサが開始された時に発生します。
        /// </summary>
        public event EventHandler SequenceStarted;

        /// <summary>
        /// シーケンサが停止した時に発生します。
        /// </summary>
        public event EventHandler SequenceStopped;

        /// <summary>
        /// シーケンサがスケジュールされたイベントの最後を処理し、シーケンスの最後に達した時に発生します。
        /// </summary>
        public event EventHandler SequenceEnd;
        #endregion

        #region -- Constructors --
        /// <summary>
        /// シーケンスを指定して新しい <see cref="Sequencer"/> クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="sequence">一連のイベントが格納されたシーケンス。</param>
        public Sequencer(Sequence sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException();

            Sequence = sequence;
            eventList = new List<Event>(sequence.Tracks.SelectMany(t => t.Events).OrderBy(e => e.Tick));
            endOfTick = sequence.MaxTick;

            tick = -(long)(sequence.Resolution * 1.0);
            loopBeginTick = sequence.LoopBeginTick;

            RecalcTickTime();
        }
        #endregion

        #region -- Public Methods --
        /// <summary>
        /// シーケンサを開始します。
        /// </summary>
        public void Start()
        {
            if (sequenceTask != null && !sequenceTask.IsCompleted)
                return;

            SequenceStarted?.Invoke(this, new EventArgs());

            reqEnd = false;
            progressTick = 0.0;
            sequenceTask = Task.Factory.StartNew(Update);
        }

        /// <summary>
        /// シーケンサを停止します。
        /// </summary>
        public void Stop()
        {
            if (sequenceTask == null)
                return;

            SequenceStopped?.Invoke(this, new EventArgs());

            reqEnd = true;

            if (Task.CurrentId.HasValue && Task.CurrentId.Value == sequenceTask.Id)
                return;

            sequenceTask.Wait();
            sequenceTask.Dispose();
            sequenceTask = null;
        }

        public void Progress(double seconds)
        {
            var tickTimeDelta = 1.0 / ((60.0 / (Tempo * tempoFactor)) / Sequence.Resolution);

            progressTick += (seconds * tickTimeDelta);

            if (Math.Abs(progressTick) < double.Epsilon)
                return;

            var processTick = (long)progressTick;
            progressTick -= processTick;

            var startTick = tick;
            var endTick = startTick + processTick;

            OutputEvents(SelectEvents(startTick, endTick).ToList());

            tick += processTick;

            if (tick < endOfTick)
                return;

            if (Looping)
                Tick = loopBeginTick;
            else
                SequenceEnd?.Invoke(this, new EventArgs());
        }
        #endregion

        #region -- Private Methods --
        private void Update()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var oldTick = 0L;

            while (!reqEnd)
            {
                Thread.Sleep(interval);

                if (reqEnd)
                    break;

                lock (syncObject)
                {
                    if (reqRewind)
                    {
                        oldTick = 0L;
                        reqRewind = false;
                        stopwatch.Restart();
                        continue;
                    }

                    var nowTick = stopwatch.ElapsedTicks;
                    progressTick += (nowTick - oldTick) * tickTime;

                    if (Math.Abs(progressTick) < double.Epsilon)
                        continue;

                    var processTick = (long)progressTick;
                    progressTick -= processTick;

                    var startTick = tick;
                    var endTick = startTick + processTick;

                    OutputEvents(SelectEvents(startTick, endTick).ToList());

                    oldTick = nowTick;
                    tick += processTick;

                    if (tick < endOfTick)
                        continue;

                    if (Looping)
                        Tick = loopBeginTick;
                    else
                        SequenceEnd?.Invoke(this, new EventArgs());
                }
            }

            tick = -(long)(Sequence.Resolution * 1.0);

            stopwatch.Stop();
        }

        private IEnumerable<Event> SelectEvents(long start, long end)
        {
            if (eventIndex < 0)
                eventIndex = 0;

            eventIndex = eventList.FindIndex(eventIndex, e => e.Tick >= start);

            while (eventIndex >= 0 &&
                   eventIndex < eventList.Count &&
                   eventList[eventIndex].Tick < end)
            {
                var @event = eventList[eventIndex++];

                var tempoEvent = @event as MetaEvent;
                if (tempoEvent?.MetaType == MetaType.Tempo)
                    ChangeTempo(tempoEvent.GetTempo());

                yield return @event;
            }
        }

        private void ChangeTempo(double newTempo)
        {
            if (Math.Abs(Tempo - newTempo) < double.Epsilon)
                return;

            var oldTempo = Tempo;

            TempoChanged?.Invoke(this, new TempoChangedEventArgs(oldTempo, newTempo));

            Tempo = newTempo;
            RecalcTickTime();
        }

        private void OutputEvents(IEnumerable<Event> events) => OnTrackEvent?.Invoke(this, new TrackEventArgs(events));

        private void RecalcTickTime() => tickTime = 1.0 / (Stopwatch.Frequency *
                                                                ((60.0 / (Tempo * tempoFactor)) / Sequence.Resolution));

        #endregion
    }

    /// <summary>
    /// イベントの送出イベントに用いられるデータを格納したクラスです。
    /// </summary>
    public class TrackEventArgs : EventArgs
    {
        #region -- Public Properties --
        /// <summary>
        /// イベントの列挙子を取得します。
        /// </summary>
        public IEnumerable<Event> Events { get; }
        #endregion

        #region -- Constructors --
        /// <summary>
        /// イベントの列挙子を指定して新しい TrackEventArgs クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="events">イベントの列挙子。</param>
        public TrackEventArgs(IEnumerable<Event> events)
        {
            Events = events;
        }
        #endregion
    }

    /// <summary>
    /// テンポ変更イベントに用いられるデータを格納したクラスです。
    /// </summary>
    public class TempoChangedEventArgs : EventArgs
    {
        #region -- Public Properties --
        /// <summary>
        /// 変更前のテンポを取得します。
        /// </summary>
        public double OldTempo { get; }

        /// <summary>
        /// 変更後のテンポを取得します。
        /// </summary>
        public double NewTempo { get; }
        #endregion

        #region -- Constructors --
        /// <summary>
        /// 引数を指定して新しい <see cref="TempoChangedEventArgs"/> クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="oldTempo">変更前のテンポ。</param>
        /// <param name="newTempo">変更後のテンポ。</param>
        public TempoChangedEventArgs(double oldTempo, double newTempo)
        {
            OldTempo = oldTempo;
            NewTempo = newTempo;
        }
        #endregion
    }
}
