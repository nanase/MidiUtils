using MidiUtils;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class ExtensionTest
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(0x3412, 0x1234)]
        [TestCase(0x7fff, unchecked ((short)0xff7f))]
        public void ToLittleEndianInt16Test(short input, short output)
        {
            Assert.AreEqual(output, input.ToLittleEndian());
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(unchecked ((int)0xffffffff), -1)]
        [TestCase(0x78563412, 0x12345678)]
        [TestCase(0x7fffffff, unchecked((int)0xffffff7f))]
        public void ToLittleEndianInt32Test(int input, int output)
        {
            Assert.AreEqual(output, input.ToLittleEndian());
        }

        [Test]
        [TestCase(0U, 0U)]
        [TestCase(0xffffffffU, 0xffffffffU)]
        [TestCase(0x78563412U, 0x12345678U)]
        [TestCase(0x7fffffffU, 0xffffff7fU)]
        public void ToLittleEndianUInt32Test(uint input, uint output)
        {
            Assert.AreEqual(output, input.ToLittleEndian());
        }
    }
}
