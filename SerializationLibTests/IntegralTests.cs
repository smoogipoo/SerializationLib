using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SerializationLib;

namespace SerializationLibTests
{
    [TestClass]
    public class IntegralTests
    {
        [TestMethod]
        public void TestBools()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                bool[] values = new bool[Config.MULTI_TEST_COUNT];
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    values[i] = rand.Next(0, 2) == 1;
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    Assert.AreEqual(values[i], sr.Read<bool>());
            }
        }

        [TestMethod]
        public void TestBytes()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                byte[] values = new byte[Config.MULTI_TEST_COUNT];
                rand.NextBytes(values);
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    sw.Write(values[i]);

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    Assert.AreEqual(values[i], sr.Read<byte>());
            }
        }

        [TestMethod]
        public void TestShorts()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                short[] values = new short[Config.MULTI_TEST_COUNT];
                byte[] bytes = new byte[2];

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    rand.NextBytes(bytes);
                    values[i] = BitConverter.ToInt16(bytes, 0);
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    Assert.AreEqual(values[i], sr.Read<short>());
            }
        }

        [TestMethod]
        public void TestInts()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                int[] values = new int[Config.MULTI_TEST_COUNT];
                byte[] bytes = new byte[4];

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    rand.NextBytes(bytes);
                    values[i] = BitConverter.ToInt32(bytes, 0);
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    Assert.AreEqual(values[i], sr.Read<int>());
            }
        }

        [TestMethod]
        public void TestLongs()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                long[] values = new long[Config.MULTI_TEST_COUNT];
                byte[] bytes = new byte[8];
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    rand.NextBytes(bytes);
                    values[i] = BitConverter.ToInt64(bytes, 0);
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    Assert.AreEqual(values[i], sr.Read<long>());
            }
        }

        [TestMethod]
        public void TestFloats()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                float[] values = new float[Config.MULTI_TEST_COUNT];
                byte[] bytes = new byte[4];
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    rand.NextBytes(bytes);
                    values[i] = BitConverter.ToSingle(bytes, 0);
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    float result = sr.Read<float>();
                    Assert.IsTrue(values[i] != values[i] || result == values[i]);
                }
            }
        }

        [TestMethod]
        public void TestDoubles()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                double[] values = new double[Config.MULTI_TEST_COUNT];
                byte[] bytes = new byte[8];
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    rand.NextBytes(bytes);
                    values[i] = BitConverter.ToDouble(bytes, 0);
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    double result = sr.Read<double>();
                    Assert.IsTrue(values[i] != values[i] || result == values[i]);
                }
            }
        }

        [TestMethod]
        public void TestDecimals()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                decimal[] values = new decimal[Config.MULTI_TEST_COUNT];

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    values[i] = new decimal(rand.Next(0, int.MaxValue), rand.Next(0, int.MaxValue), rand.Next(0, int.MaxValue), rand.Next(0, 2) == 1, (byte)rand.Next(0, 29));
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    Assert.AreEqual(values[i], sr.Read<decimal>());
            }
        }

        [TestMethod]
        public void TestDateTimes()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                DateTime[] values = new DateTime[Config.MULTI_TEST_COUNT];

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    values[i] = new DateTime(rand.Next(1970, 5623), rand.Next(1, 13), rand.Next(1, 29), rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60), DateTimeKind.Utc);
                    sw.Write(values[i]);
                }

                sw.Flush();
                ms.Position = 0;

                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    Assert.AreEqual(values[i], sr.Read<DateTime>());
            }
        }
    }
}
