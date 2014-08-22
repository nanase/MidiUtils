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

// 規格出典:
// http://www.geocities.co.jp/Hollywood-Miyuki/9008/basic/b_02.html
// http://ja.wikipedia.org/wiki/%E3%82%B3%E3%83%B3%E3%83%88%E3%83%AD%E3%83%BC%E3%83%AB%E3%83%81%E3%82%A7%E3%83%B3%E3%82%B8

namespace MidiUtils.IO
{
    /// <summary>
    /// パラメータのコントロールを行うためのコントロールチェンジを表した列挙体です。
    /// </summary>
    public enum ControlChange
    {
        BankSelectMSB = 0,
        Modulation = 1,
        BreathControl = 2,
        FootControl = 4,
        PortamentTime = 5,
        DataEntryMSB = 6,
        Volume = 7,
        BalanceControl = 8,
        Panpot = 10,
        Expression = 11,

        BankSelectLSB = 32,
        ModulationLSB = 33,
        BreathControlLSB = 34,
        FootControlLSB = 36,
        PortamentTimeLSB = 37,
        DataEntryLSB = 38,
        VolumeLSB = 39,
        BalanceControlLSB = 40,
        Hold1 = 64,

        Portament = 65,
        Sostenuto = 66,
        SoftPedal = 67,
        Hold2 = 69,
        MemoryPatchSelect = 70,

        DataIncrement = 96,
        DataDecrement = 97,
        NRPNLSB = 98,
        NRPNMSB = 99,
        RPNLSB = 100,
        RPNMSB = 101,
        AllSoundOff = 120,

        ResetAllController = 121,
        LocalControll = 122,
        AllNotesOff = 123,
        OmniOff = 124,
        OmniOn = 125,
        MonoOn = 126,
        PolyOn = 127,
    }
}
