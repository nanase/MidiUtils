using MidiUtils.IO;
using NUnit.Framework;

// ReSharper disable UnusedVariable

namespace UnitTest
{
    [TestFixture]
    public class MidiEventTest
    {
        [Test]
        public void CtorTest()
        {
            var midiEvent = new MidiEvent(EventType.NoteOn, 0, 0, 0);
        }

        [Test]
        public void ToStringTest()
        {
            const EventType type = EventType.NoteOn;
            const int channel = 3;
            const int data1 = 5;

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
