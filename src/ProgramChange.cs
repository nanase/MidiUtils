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
        /// <summary>
        /// アコースティックピアノ。
        /// </summary>
        AcousticGrandPiano,

        /// <summary>
        /// ブライトピアノ。
        /// </summary>
        BrightAcousticPiano,
        
        /// <summary>
        /// エレクトリックグランドピアノ。
        /// </summary>
        ElectricGrandPiano,

        /// <summary>
        /// ホンキートンクピアノ。
        /// </summary>
        HonkyTonkPiano,

        /// <summary>
        /// エレクトリックピアノ1。
        /// </summary>
        ElectricPiano1,

        /// <summary>
        /// エレクトリックピアノ2。
        /// </summary>
        ElectricPiano2,

        /// <summary>
        /// ハープシコード。
        /// </summary>
        Harpsichord,

        /// <summary>
        /// クラビネット。
        /// </summary>
        Clavinet,

        /// <summary>
        /// チェレスタ。
        /// </summary>
        Celesta,

        /// <summary>
        /// グロッケンシュピール。
        /// </summary>
        Glockenspiel,

        /// <summary>
        /// オルゴール。
        /// </summary>
        MusicBox,

        /// <summary>
        /// ビブラフォン。
        /// </summary>
        Vibraphone,

        /// <summary>
        /// マリンバ。
        /// </summary>
        Marimba,

        /// <summary>
        /// シロフォン。
        /// </summary>
        Xylophone,

        /// <summary>
        /// チューブラーベル。
        /// </summary>
        TubularBells,

        /// <summary>
        /// ダルシマー。
        /// </summary>
        Dulcimer,

        /// <summary>
        /// ドローバーオルガン。
        /// </summary>
        DrawbarOrgan,

        /// <summary>
        /// パーカッシブオルガン。
        /// </summary>
        PercussiveOrgan,

        /// <summary>
        /// ロックオルガン。
        /// </summary>
        RockOrgan,

        /// <summary>
        /// チャーチオルガン。
        /// </summary>
        ChurchOrgan,

        /// <summary>
        /// リードオルガン。
        /// </summary>
        ReedOrgan,

        /// <summary>
        /// アコーディオン。
        /// </summary>
        Accordion,

        /// <summary>
        /// ハーモニカ。
        /// </summary>
        Harmonica,

        /// <summary>
        /// タンゴアコーディオン。
        /// </summary>
        TangoAccordion,

        /// <summary>
        /// アコースティックギター(ナイロン弦)。
        /// </summary>
        AcousticGuitarNylon,

        /// <summary>
        /// アコースティックギター(スチール弦)。
        /// </summary>
        AcousticGuitarSteel,

        /// <summary>
        /// ジャズギター。
        /// </summary>
        ElectricGuitarJazz,

        /// <summary>
        /// クリーンギター。
        /// </summary>
        ElectricGuitarClean,

        /// <summary>
        /// ミュートギター。
        /// </summary>
        ElectricGuitarMuted,

        /// <summary>
        /// オーバードライブギター。
        /// </summary>
        OverdrivenGuitar,

        /// <summary>
        /// ディストーションギター。
        /// </summary>
        DistortionGuitar,

        /// <summary>
        /// ギターハーモニクス。
        /// </summary>
        GuitarHarmonics,

        /// <summary>
        /// アコースティックベース。
        /// </summary>
        AcousticBass,

        /// <summary>
        /// フィンガーベース。
        /// </summary>
        ElectricBassFinger,

        /// <summary>
        /// ピックベース。
        /// </summary>
        ElectricBassPick,

        /// <summary>
        /// フレットレスベース。
        /// </summary>
        FretlessBass,

        /// <summary>
        /// スラップベース1。
        /// </summary>
        SlapBass1,

        /// <summary>
        /// スラップベース2。
        /// </summary>
        SlapBass2,

        /// <summary>
        /// シンセベース1。
        /// </summary>
        SynthBass1,

        /// <summary>
        /// シンセベース2。
        /// </summary>
        SynthBass2,

        /// <summary>
        /// ヴァイオリン。
        /// </summary>
        Violin,

        /// <summary>
        /// ヴィオラ。
        /// </summary>
        Viola,

        /// <summary>
        /// チェロ。
        /// </summary>
        Cello,

        /// <summary>
        /// コントラバス。
        /// </summary>
        Contrabass,

        /// <summary>
        /// トレモロストリングス。
        /// </summary>
        TremoloStrings,

        /// <summary>
        /// ピッチカートストリングス。
        /// </summary>
        PizzicatoStrings,

        /// <summary>
        /// ハープ。
        /// </summary>
        OrchestralHarp,

        /// <summary>
        /// ティンパニ。
        /// </summary>
        Timpani,

        /// <summary>
        /// ストリングアンサンブル1。
        /// </summary>
        StringEnsemble1,
        
        /// <summary>
        /// ストリングアンサンブル2。
        /// </summary>
        StringEnsemble2,

        /// <summary>
        /// シンセストリングス1。
        /// </summary>
        SynthStrings1,

        /// <summary>
        /// シンセストリングス2。
        /// </summary>
        SynthStrings2,

        /// <summary>
        /// クワイア(アー)。
        /// </summary>
        ChoirAahs,

        /// <summary>
        /// ボイス(オー)。
        /// </summary>
        VoiceOohs,

        /// <summary>
        /// シンセボイス。
        /// </summary>
        SynthVoice,

        /// <summary>
        /// オーケストラヒット
        /// </summary>
        OrchestraHit,

        /// <summary>
        /// トランペット。
        /// </summary>
        Trumpet,

        /// <summary>
        /// トロンボーン。
        /// </summary>
        Trombone,

        /// <summary>
        /// チューバ。
        /// </summary>
        Tuba,

        /// <summary>
        /// ミュートトランペット。
        /// </summary>
        MutedTrumpet,

        /// <summary>
        /// フレンチホルン。
        /// </summary>
        FrenchHorn,

        /// <summary>
        /// ブラスセレクション。
        /// </summary>
        BrassSection,

        /// <summary>
        /// シンセブラス1。
        /// </summary>
        SynthBrass1,

        /// <summary>
        /// シンセブラス2。
        /// </summary>
        SynthBrass2,

        /// <summary>
        /// ソプラノサックス。
        /// </summary>
        SopranoSax,

        /// <summary>
        /// ソプラノサックス。
        /// </summary>
        AltoSax,

        /// <summary>
        /// テナーサックス。
        /// </summary>
        TenorSax,

        /// <summary>
        /// バリトンサックス。
        /// </summary>
        BaritoneSax,

        /// <summary>
        /// オーボエ。
        /// </summary>
        Oboe,

        /// <summary>
        /// イングリッシュホルン。
        /// </summary>
        EnglishHorn,

        /// <summary>
        /// ファゴット。
        /// </summary>
        Bassoon,

        /// <summary>
        /// クラリネット。
        /// </summary>
        Clarinet,

        /// <summary>
        /// ピッコロ。
        /// </summary>
        Piccolo,

        /// <summary>
        /// フルート。
        /// </summary>
        Flute,

        /// <summary>
        /// リコーダー。
        /// </summary>
        Recorder,

        /// <summary>
        /// パンフルート。
        /// </summary>
        PanFlute,

        /// <summary>
        /// 茶瓶。
        /// </summary>
        Blownbottle,

        /// <summary>
        /// 尺八。
        /// </summary>
        Shakuhachi,

        /// <summary>
        /// 口笛。
        /// </summary>
        Whistle,

        /// <summary>
        /// オカリナ。
        /// </summary>
        Ocarina,

        /// <summary>
        /// 矩形波。
        /// </summary>
        Lead1Square,

        /// <summary>
        /// ノコギリ波。
        /// </summary>
        Lead2Sawtooth,

        /// <summary>
        /// カリオペ。
        /// </summary>
        Lead3Calliope,

        /// <summary>
        /// チフ。
        /// </summary>
        Lead4Chiff,

        /// <summary>
        /// チャランゴ。
        /// </summary>
        Lead5Charang,

        /// <summary>
        /// ボイス。
        /// </summary>
        Lead6Voice,

        /// <summary>
        /// フィフスズ。
        /// </summary>
        Lead7Fifths,

        /// <summary>
        /// バス+リード。
        /// </summary>
        Lead8BassLead,

        /// <summary>
        /// ファンタジア。
        /// </summary>
        Pad1Newage,

        /// <summary>
        /// ウォーム。
        /// </summary>
        Pad2Warm,

        /// <summary>
        /// ポリシンセ。
        /// </summary>
        Pad3Polysynth,

        /// <summary>
        /// クワイア。
        /// </summary>
        Pad4Choir,

        /// <summary>
        /// ボウ。
        /// </summary>
        Pad5Bowed,

        /// <summary>
        /// メタリック。
        /// </summary>
        Pad6Metallic,

        /// <summary>
        /// ハロー。
        /// </summary>
        Pad7Halo,

        /// <summary>
        /// スウィープ。
        /// </summary>
        Pad8Sweep,

        /// <summary>
        /// レイン。
        /// </summary>
        FX1Rain,

        /// <summary>
        /// サウンドトラック。
        /// </summary>
        FX2Soundtrack,

        /// <summary>
        /// クリスタル。
        /// </summary>
        FX3Crystal,

        /// <summary>
        /// アトモスフィア。
        /// </summary>
        FX4Atmosphere,

        /// <summary>
        /// ブライトネス。
        /// </summary>
        FX5Brightness,

        /// <summary>
        /// ゴブリン。
        /// </summary>
        FX6Goblins,

        /// <summary>
        /// エコー。
        /// </summary>
        FX7Echoes,

        /// <summary>
        /// サイファイ。
        /// </summary>
        FX8SciFi,

        /// <summary>
        /// シタール。
        /// </summary>
        Sitar,

        /// <summary>
        /// バンジョー。
        /// </summary>
        Banjo,

        /// <summary>
        /// 三味線。
        /// </summary>
        Shamisen,

        /// <summary>
        /// 琴。
        /// </summary>
        Koto,

        /// <summary>
        /// カリンバ。
        /// </summary>
        Kalimba,

        /// <summary>
        /// バグパイプ。
        /// </summary>
        Bagpipe,

        /// <summary>
        /// フィドル。
        /// </summary>
        Fiddle,

        /// <summary>
        /// シャハナーイ。
        /// </summary>
        Shanai,

        /// <summary>
        /// ティンクルベル。
        /// </summary>
        TinkleBell,

        /// <summary>
        /// アゴゴ。
        /// </summary>
        Agogo,

        /// <summary>
        /// スチールドラム。
        /// </summary>
        SteelDrums,

        /// <summary>
        /// ウッドブロック。
        /// </summary>
        Woodblock,

        /// <summary>
        /// 太鼓。
        /// </summary>
        TaikoDrum,

        /// <summary>
        /// メロディックタム。
        /// </summary>
        MelodicTom,

        /// <summary>
        /// シンセドラム。
        /// </summary>
        SynthDrum,

        /// <summary>
        /// リバースシンバル。
        /// </summary>
        ReverseCymbal,

        /// <summary>
        /// ギターフレットノイズ。
        /// </summary>
        GuitarFretNoise,

        /// <summary>
        /// ブレスノイズ。
        /// </summary>
        BreathNoise,

        /// <summary>
        /// 海岸。
        /// </summary>
        Seashore,

        /// <summary>
        /// さえずり。
        /// </summary>
        BirdTweet,

        /// <summary>
        /// 電話ベル。
        /// </summary>
        TelephoneRing,

        /// <summary>
        /// ヘリコプター。
        /// </summary>
        Helicopter,

        /// <summary>
        /// 拍手。
        /// </summary>
        Applause,

        /// <summary>
        /// 銃声。
        /// </summary>
        Gunshot
    }
}
