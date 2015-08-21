using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SerializationLib;

namespace SerializationLibTests
{
    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void TestByteEnum()
        {
            testEnum<ByteEnum>();
        }

        [TestMethod]
        public void TestShortEnum()
        {
            testEnum<ShortEnum>();
        }

        [TestMethod]
        public void TestIntEnum()
        {
            testEnum<IntEnum>();
        }

        [TestMethod]
        public void TestLongEnum()
        {
            testEnum<LongEnum>();
        }

        private void testEnum<T>()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                string[] names = Enum.GetNames(typeof(T));
                T[] values = new T[names.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    values[i] = (T)Enum.Parse(typeof(T), names[i]);
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < names.Length; i++)
                    Assert.AreEqual(values[i], (T)sr.Read<T>());
            }
        }

        [Flags]
        public enum ByteEnum : byte
        {
            a = 0,
            b = 1 << 0,
            c = 1 << 1,
            d = 1 << 2,
            e = 1 << 3,
            f = 1 << 4,
            g = 1 << 5,
            h = 1 << 6
        }

        [Flags]
        private enum ShortEnum : short
        {
            a = 0,
            b = 1 << 0,
            c = 1 << 1,
            d = 1 << 2,
            e = 1 << 3,
            f = 1 << 4,
            g = 1 << 5,
            h = 1 << 6,
            i = 1 << 7,
            j = 1 << 8,
            k = 1 << 9,
            l = 1 << 10,
            m = 1 << 11,
            n = 1 << 12,
            o = 1 << 13,
            p = 1 << 14
        }

        [Flags]
        private enum IntEnum : int
        {
            a = 0,
            b = 1 << 0,
            c = 1 << 1,
            d = 1 << 2,
            e = 1 << 3,
            f = 1 << 4,
            g = 1 << 5,
            h = 1 << 6,
            i = 1 << 7,
            j = 1 << 8,
            k = 1 << 9,
            l = 1 << 10,
            m = 1 << 11,
            n = 1 << 12,
            o = 1 << 13,
            p = 1 << 14,
            q = 1 << 15,
            r = 1 << 16,
            s = 1 << 17,
            t = 1 << 18,
            u = 1 << 19,
            v = 1 << 20,
            w = 1 << 21,
            x = 1 << 22,
            y = 1 << 23,
            z = 1 << 24,
            aa = 1 << 25,
            ab = 1 << 26,
            ac = 1 << 27,
            ad = 1 << 28,
            ae = 1 << 29,
            af = 1 << 30,
            ag = 1 << 31
        }

        [Flags]
        private enum LongEnum : long
        {
            a = 0,
            b = 1 << 0,
            c = 1 << 1,
            d = 1 << 2,
            e = 1 << 3,
            f = 1 << 4,
            g = 1 << 5,
            h = 1 << 6,
            i = 1 << 7,
            j = 1 << 8,
            k = 1 << 9,
            l = 1 << 10,
            m = 1 << 11,
            n = 1 << 12,
            o = 1 << 13,
            p = 1 << 14,
            q = 1 << 15,
            r = 1 << 16,
            s = 1 << 17,
            t = 1 << 18,
            u = 1 << 19,
            v = 1 << 20,
            w = 1 << 21,
            x = 1 << 22,
            y = 1 << 23,
            z = 1 << 24,
            aa = 1 << 25,
            ab = 1 << 26,
            ac = 1 << 27,
            ad = 1 << 28,
            ae = 1 << 29,
            af = 1 << 30,
            ag = 1 << 31,
            ah = 1 << 32,
            ai = 1 << 33,
            aj = 1 << 34,
            ak = 1 << 35,
            al = 1 << 36,
            am = 1 << 37,
            an = 1 << 38,
            ao = 1 << 39,
            ap = 1 << 40,
            aq = 1 << 41,
            ar = 1 << 42,
            at = 1 << 43,
            au = 1 << 44,
            av = 1 << 45,
            aw = 1 << 46,
            ax = 1 << 47,
            ay = 1 << 48,
            az = 1 << 49,
            ba = 1 << 50,
            bb = 1 << 51,
            bc = 1 << 52,
            bd = 1 << 53,
            be = 1 << 54,
            bf = 1 << 55,
            bg = 1 << 56,
            bh = 1 << 57,
            bi = 1 << 58,
            bj = 1 << 59,
            bk = 1 << 60,
            bl = 1 << 61,
            bm = 1 << 62,
            bn = 1 << 63
        }
    }
}
