using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiUtils.IO;
// ReSharper disable UnusedVariable

namespace UnitTest
{
    [TestClass]
    public class MidiEventTest
    {
        [TestMethod]
        public void CtorTest()
        {
            var midiEvent = new MidiEvent(EventType.NoteOn, 0, 0, 0);
        }

        [TestMethod]
        public void ToStringTest()
        {
            const EventType type = EventType.NoteOn;
            const int channel = 3;
            const int data1 = 5;

            var midiEvent = new MidiEvent(type, channel, data1, 0);
            var str = midiEvent.ToString();

            Assert.IsNotNull(str);
            Assert.IsFalse(string.IsNullOrWhiteSpace(str));

            StringAssert.Contains(str, type.ToString());
            StringAssert.Contains(str, channel.ToString());
            StringAssert.Contains(str, data1.ToString());

            // data2 is not contained in MidiEvent#ToString
        }
    }
}
