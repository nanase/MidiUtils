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

namespace MidiUtils
{
    /// <summary>
    /// MIDI で使用されるプログラムチェンジ (インストゥルメント) の列挙体です。
    /// </summary>
    public enum ProgramChange
    {
        AcousticGrandPiano,
        BrightAcousticPiano,
        ElectricGrandPiano,
        HonkyTonkPiano,
        ElectricPiano1,
        ElectricPiano2,
        Harpsichord,
        Clavinet,

        Celesta,
        Glockenspiel,
        MusicBox,
        Vibraphone,
        Marimba,
        Xylophone,
        TubularBells,
        Dulcimer,

        DrawbarOrgan,
        PercussiveOrgan,
        RockOrgan,
        ChurchOrgan,
        ReedOrgan,
        Accordion,
        Harmonica,
        TangoAccordion,

        AcousticGuitarNylon,
        AcousticGuitarSteel,
        ElectricGuitarJazz,
        ElectricGuitarClean,
        ElectricGuitarMuted,
        OverdrivenGuitar,
        DistortionGuitar,
        GuitarHarmonics,

        AcousticBass,
        ElectricBassFinger,
        ElectricBassPick,
        FretlessBass,
        SlapBass1,
        SlapBass2,
        SynthBass1,
        SynthBass2,

        Violin,
        Viola,
        Cello,
        Contrabass,
        TremoloStrings,
        PizzicatoStrings,
        OrchestralHarp,
        Timpani,

        StringEnsemble1,
        StringEnsemble2,
        SynthStrings1,
        SynthStrings2,
        ChoirAahs,
        VoiceOohs,
        SynthChoir,
        OrchestraHit,

        Trumpet,
        Trombone,
        Tuba,
        MutedTrumpet,
        FrenchHorn,
        BrassSection,
        SynthBrass1,
        SynthBrass2,

        SopranoSax,
        AltoSax,
        TenorSax,
        BaritoneSax,
        Oboe,
        EnglishHorn,
        Bassoon,
        Clarinet,

        Piccolo,
        Flute,
        Recorder,
        PanFlute,
        Blownbottle,
        Shakuhachi,
        Whistle,
        Ocarina,

        Lead1Square,
        Lead2Sawtooth,
        Lead3Calliope,
        Lead4Chiff,
        Lead5Charang,
        Lead6Voice,
        Lead7Fifths,
        Lead8BassLead,

        Pad1Newage,
        Pad2Warm,
        Pad3Polysynth,
        Pad4Choir,
        Pad5Bowed,
        Pad6Metallic,
        Pad7Halo,
        Pad8Sweep,

        FX1Rain,
        FX2Soundtrack,
        FX3Crystal,
        FX4Atmosphere,
        FX5Brightness,
        FX6Goblins,
        FX7Echoes,
        FX8SciFi,

        Sitar,
        Banjo,
        Shamisen,
        Koto,
        Kalimba,
        Bagpipe,
        Fiddle,
        Shanai,

        TinkleBell,
        Agogo,
        SteelDrums,
        Woodblock,
        TaikoDrum,
        MelodicTom,
        SynthDrum,
        ReverseCymbal,

        GuitarFretNoise,
        BreathNoise,
        Seashore,
        BirdTweet,
        TelephoneRing,
        Helicopter,
        Applause,
        Gunshot
    }
}
