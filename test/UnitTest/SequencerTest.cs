using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using MidiUtils.Sequencer;
using NUnit.Framework;

// ReSharper disable All
// ReSharper disable UnusedVariable

namespace UnitTest
{
    [TestFixture]
    public class SequencerTest
    {
        [Test]
        [TestCase("mini.mid")]
        [TestCase("empty.mid")]
        public void CtorTest(string filename)
        {
            var inputFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "midi", filename);
            var sequence = new Sequence(inputFilepath);
            var sequencer = new Sequencer(sequence);

            Assert.That(sequencer.Interval, Is.GreaterThan(0));
            Assert.That(sequencer.LoopBeginTick, Is.GreaterThanOrEqualTo(0));
            Assert.That(sequencer.Sequence, Is.EqualTo(sequence));
            Assert.That(sequencer.Tempo, Is.Not.Negative);
            Assert.That(sequencer.TempoFactor, Is.GreaterThanOrEqualTo(0));
        }
    }
}
