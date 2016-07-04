using MidiUtils;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class ExtensionTest
    {
        [Test]
        public void ToLittleEndianInt16Test()
        {
            Assert.AreEqual((short)0, ((short)0).ToLittleEndian());
            Assert.AreEqual((short)-1, ((short)-1).ToLittleEndian());

            unchecked
            {
                Assert.AreEqual((short)0x1234, ((short)0x3412).ToLittleEndian());
                Assert.AreEqual((short)0xff7f, ((short)0x7fff).ToLittleEndian());
            }
        }

        [Test]
        public void ToLittleEndianInt32Test()
        {
            Assert.AreEqual(0, 0.ToLittleEndian());
            Assert.AreEqual(-1, (int)0xffffffff.ToLittleEndian());

            unchecked
            {
                Assert.AreEqual(0x12345678, 0x78563412.ToLittleEndian());
                Assert.AreEqual((int)0xffffff7f, 0x7fffffff.ToLittleEndian());
            }
        }

        [Test]
        public void ToLittleEndianUInt32Test()
        {
            Assert.AreEqual(0U, 0U.ToLittleEndian());
            Assert.AreEqual(0xffffffffU, 0xffffffffU.ToLittleEndian());

            Assert.AreEqual(0x12345678U, 0x78563412U.ToLittleEndian());
            Assert.AreEqual(0xffffff7fU, 0x7fffffffU.ToLittleEndian());
        }
    }
}
