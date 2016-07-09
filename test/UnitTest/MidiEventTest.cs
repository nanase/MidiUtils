using MidiUtils.IO;
using NUnit.Framework;

// ReSharper disable UnusedVariable

namespace UnitTest
{
    [TestFixture]
    public class MidiEventTest
    {
        [Test]
        [TestCase((EventType)0, 0, 0, 0)]
        [TestCase(EventType.NoteOn, 1, 3, 5)]
        [TestCase((EventType)(-1), -1, -1, -1)]
        [TestCase((EventType)(int.MaxValue), int.MaxValue, int.MaxValue, int.MaxValue)]
        public void CtorTest(EventType type, int channel, int data1, int data2)
        {
            var midiEvent = new MidiEvent(type, channel, data1, data2);

            Assert.AreEqual(type, midiEvent.Type);
            Assert.AreEqual(channel, midiEvent.Channel);
            Assert.AreEqual(data1, midiEvent.Data1);
            Assert.AreEqual(data2, midiEvent.Data2);
        }

        [Test]
        [TestCase((EventType)0, 0, 0)]
        [TestCase(EventType.NoteOn, 1, 3)]
        [TestCase((EventType)(-1), -1, -1)]
        [TestCase((EventType)(int.MaxValue), int.MaxValue, int.MaxValue)]
        public void ToStringTest(EventType type, int channel, int data1)
        {
            var midiEvent = new MidiEvent(type, channel, data1, 0);
            var str = midiEvent.ToString();

            Assert.IsNotNull(str);
            Assert.IsFalse(string.IsNullOrWhiteSpace(str));

            StringAssert.Contains(type.ToString(), str);
            StringAssert.Contains(channel.ToString(), str);
            StringAssert.Contains(data1.ToString(), str);

            // data2 is not contained in MidiEvent#ToString
        }
    }
}
