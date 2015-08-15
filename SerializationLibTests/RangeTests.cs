using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SerializationLib;
using System.Collections.Generic;

namespace SerializationLibTests
{
    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationReader sr = new SerializationReader(ms))
            {
                sr.Read<bool>();
                sr.Read<byte>();
                sr.Read<char>();
                sr.Read<short>();
                sr.Read<int>();
                sr.Read<long>();
                sr.Read<float>();
                sr.Read<double>();
                sr.Read<decimal>();
                sr.Read<DateTime>();
                sr.Read<string>();
                sr.Read<TestEnum>();

                sr.Read<string[]>();
                sr.Read<List<string>>();
                sr.Read<Dictionary<int, string>>();

                sr.Read<TestSerializable>();
            }
        }

        [TestMethod]
        public void TestNull()
        {
            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                sw.Write<object>(null);

                sw.Write<int[]>(null);
                sw.Write<List<int>>(null);
                sw.Write<Dictionary<int, string>>(null);

                sw.Write<TestSerializable>(null);
            }
        }
        
        private enum TestEnum
        {
            a,
            b
        }

        private class TestSerializable : ISerializable
        {

            public void ReadFromStream(SerializationReader sr)
            {
                throw new NotImplementedException();
            }

            public void WriteToStream(SerializationWriter sw)
            {
                throw new NotImplementedException();
            }
        }
    }
}
