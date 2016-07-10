using System;
using System.IO;
using MidiUtils.Sequencer;
using NUnit.Framework;

// ReSharper disable UnusedVariable

namespace UnitTest
{
    [TestFixture]
    public class SequenceTest
    {
        [Test]
        [TestCase("mini.mid")]
        [TestCase("empty.mid")]
        public void CtorTest(string filename)
        {
            var input_filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "midi", filename);
            var sequence = new Sequence(input_filepath);
        }
    }
}
