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
            var inputFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "midi", filename);
            var sequence = new Sequence(inputFilepath);

            Assert.That(sequence.EventCount, Is.GreaterThan(0));
            Assert.That(sequence.Format, Is.EqualTo(0).Or.EqualTo(1));
            Assert.That(sequence.LoopBeginTick, Is.GreaterThanOrEqualTo(0));
            Assert.That(sequence.MaxTick, Is.GreaterThanOrEqualTo(0).And.GreaterThanOrEqualTo(sequence.LoopBeginTick));
            Assert.That(sequence.Resolution, Is.GreaterThan(0));
            Assert.That(sequence.Tracks, Is.Not.Null);
        }
    }
}
