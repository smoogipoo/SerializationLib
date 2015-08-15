using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SerializationLib;
using System.Text;

namespace SerializationLibTests
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void TestASCIIChars()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms, new ASCIIEncoding()))
            using (SerializationReader sr = new SerializationReader(ms, new ASCIIEncoding()))
            {
                for (int i = 0; i < 128; i++)
                    sw.Write((char)i);

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < 128; i++)
                    Assert.AreEqual((char)i, sr.Read<char>());
            }
        }

        [TestMethod]
        public void TestUTF8Chars()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms, new UTF8Encoding()))
            using (SerializationReader sr = new SerializationReader(ms, new UTF8Encoding()))
            {
                for (int i = 0; i < 55296; i++)
                    sw.Write((char)i);

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < 55296; i++)
                    Assert.AreEqual((char)i, sr.Read<char>());
            }
        }

        [TestMethod]
        public void TestUTF16Chars()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms, new UnicodeEncoding()))
            using (SerializationReader sr = new SerializationReader(ms, new UnicodeEncoding()))
            {
                for (int i = 0; i < 55296; i++)
                    sw.Write((char)i);

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < 55296; i++)
                    Assert.AreEqual((char)i, sr.Read<char>());
            }
        }

        [TestMethod]
        public void TestUTF32Chars()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms, new UTF32Encoding()))
            using (SerializationReader sr = new SerializationReader(ms, new UTF32Encoding()))
            {
                for (int i = 0; i < 55296; i++)
                    sw.Write((char)i);

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < 55296; i++)
                    Assert.AreEqual((char)i, sr.Read<char>());
            }
        }

        [TestMethod]
        public void TestASCIIString()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms, new ASCIIEncoding()))
            using (SerializationReader sr = new SerializationReader(ms, new ASCIIEncoding()))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 128; i++)
                    sb.Append((char)i);
                string res = sb.ToString();
                sw.Write(res);

                sw.Flush();
                ms.Position = 0;

                Assert.AreEqual(res, sr.Read<string>());
            }
        }

        [TestMethod]
        public void TestUTF8String()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms, new UTF8Encoding()))
            using (SerializationReader sr = new SerializationReader(ms, new UTF8Encoding()))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 55296; i++)
                    sb.Append((char)i);
                string res = sb.ToString();
                sw.Write(res);

                sw.Flush();
                ms.Position = 0;

                Assert.AreEqual(res, sr.Read<string>());
            }
        }

        [TestMethod]
        public void TestUTF16String()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms, new UnicodeEncoding()))
            using (SerializationReader sr = new SerializationReader(ms, new UnicodeEncoding()))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 55296; i++)
                    sb.Append((char)i);
                string res = sb.ToString();
                sw.Write(res);

                sw.Flush();
                ms.Position = 0;

                Assert.AreEqual(res, sr.Read<string>());
            }
        }

        [TestMethod]
        public void TestUTF32String()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms, new UTF32Encoding()))
            using (SerializationReader sr = new SerializationReader(ms, new UTF32Encoding()))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 55296; i++)
                    sb.Append((char)i);
                string res = sb.ToString();
                sw.Write(res);

                sw.Flush();
                ms.Position = 0;

                Assert.AreEqual(res, sr.Read<string>());
            }
        }
    }
}
