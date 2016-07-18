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
    public class SequenceTest
    {
        [Test]
        [TestCase("mini.mid")]
        [TestCase("empty.mid")]
        public void CtorTest1(string filename)
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

        [Test]
        [TestCase("mini.mid")]
        [TestCase("empty.mid")]
        public void CtorTest2(string filename)
        {
            var inputFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "midi", filename);

            using (var stream = new FileStream(inputFilepath, FileMode.Open))
            {
                var sequence = new Sequence(stream);

                Assert.That(sequence.EventCount, Is.GreaterThan(0));
                Assert.That(sequence.Format, Is.EqualTo(0).Or.EqualTo(1));
                Assert.That(sequence.LoopBeginTick, Is.GreaterThanOrEqualTo(0));
                Assert.That(sequence.MaxTick, Is.GreaterThanOrEqualTo(0).And.GreaterThanOrEqualTo(sequence.LoopBeginTick));
                Assert.That(sequence.Resolution, Is.GreaterThan(0));
                Assert.That(sequence.Tracks, Is.Not.Null);
            }
        }

        [Test]
        public void CtorError1()
        {
            Assert.Throws<ArgumentNullException>(() => new Sequence((string)null));
            Assert.Throws<ArgumentNullException>(() => new Sequence(string.Empty));
            Assert.Throws<FileNotFoundException>(() => new Sequence("???"));
        }

        [Test]
        public void CtorError2()
        {
            Assert.Throws<ArgumentNullException>(() => new Sequence((Stream)null));
            Assert.Throws<InvalidDataException>(() => new Sequence(new WriteOnlyStream()));
        }

        [Test]
        public void LoadFileError()
        {
            // WIP

            Assert.Throws<EndOfStreamException>(() => new Sequence(new MemoryStream(new byte[] { })));
            Assert.Throws<InvalidDataException>(() => new Sequence(new MemoryStream(new byte[] { 0, 0, 0, 0 })));
            Assert.Throws<InvalidDataException>(() => new Sequence(new MemoryStream(new byte[] { 0x4d, 0x54, 0x68, 0x64, 0, 0, 0, 0 })));
            Assert.Throws<InvalidDataException>(() => new Sequence(new MemoryStream(new byte[] { 0x4d, 0x54, 0x68, 0x64, 0, 0, 0, 6, 0, 2 })));
            Assert.Throws<InvalidDataException>(() => new Sequence(new MemoryStream(new byte[] { 0x4d, 0x54, 0x68, 0x64, 0, 0, 0, 6, 0, 1, 0, 1, 0, 0 })));
            Assert.Throws<InvalidDataException>(() => new Sequence(new MemoryStream(new byte[] { 0x4d, 0x54, 0x68, 0x64, 0, 0, 0, 6, 0, 1, 0, 1, 0, 1 })));
        }

        private class WriteOnlyStream : MemoryStream
        {
            public override bool CanRead => false;
        }
    }
}
