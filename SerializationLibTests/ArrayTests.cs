using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SerializationLib;
using System.Collections.Generic;

namespace SerializationLibTests
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        public void TestBasicArray()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                int[] array = new int[Config.MULTI_TEST_COUNT];
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    array[i] = rand.Next(int.MinValue, int.MaxValue);
                sw.Write(array);

                sw.Flush();
                ms.Position = 0;

                int[] ret = sr.Read<int[]>();
                for (int i = 0; i < ret.Length; i++)
                    Assert.AreEqual(array[i], ret[i]);
            }
        }

        [TestMethod]
        public void TestBasicList()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                List<int> list = new List<int>(Config.MULTI_TEST_COUNT);
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    list.Add(rand.Next(int.MinValue, int.MaxValue));
                sw.Write(list);

                sw.Flush();
                ms.Position = 0;

                List<int> ret = sr.Read<List<int>>();
                for (int i = 0; i < list.Count; i++)
                    Assert.AreEqual(list[i], ret[i]);
            }
        }

        [TestMethod]
        public void TestBasicDictionary()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                Dictionary<int, float> dict = new Dictionary<int, float>(Config.MULTI_TEST_COUNT);
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                    dict[rand.Next(int.MinValue, int.MaxValue)] = (float)rand.NextDouble();
                sw.Write(dict);

                sw.Flush();
                ms.Position = 0;

                Dictionary<int, float> ret = sr.Read<Dictionary<int, float>>();
                foreach (KeyValuePair<int, float> kvp in dict)
                    Assert.AreEqual(kvp.Value, ret[kvp.Key]);
            }
        }

        [TestMethod]
        public void TestComplexArray()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                SIBFClass[] array = new SIBFClass[Config.MULTI_TEST_COUNT];
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    array[i] = new SIBFClass()
                    {
                        S = "test" + i,
                        IBF = new IBFClass()
                        {
                            I = rand.Next(int.MinValue, int.MaxValue),
                            B = rand.Next(2) == 1,
                            F = (float)rand.NextDouble()
                        }
                    };
                }
                sw.Write(array);

                sw.Flush();
                ms.Position = 0;

                SIBFClass[] ret = sr.Read<SIBFClass[]>();
                for (int i = 0; i < ret.Length; i++)
                {
                    Assert.AreEqual(array[i].S, ret[i].S);
                    Assert.AreEqual(array[i].IBF.I, ret[i].IBF.I);
                    Assert.AreEqual(array[i].IBF.B, ret[i].IBF.B);
                    Assert.AreEqual(array[i].IBF.F, ret[i].IBF.F);
                }
            }
        }

        [TestMethod]
        public void TestComplexList()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                List<SIBFClass> list = new List<SIBFClass>(Config.MULTI_TEST_COUNT);
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    list.Add(new SIBFClass()
                    {
                        S = "test" + i,
                        IBF = new IBFClass()
                        {
                            I = rand.Next(int.MinValue, int.MaxValue),
                            B = rand.Next(2) == 1,
                            F = (float)rand.NextDouble()
                        }
                    });
                }
                sw.Write(list);

                sw.Flush();
                ms.Position = 0;

                List<SIBFClass> ret = sr.Read<List<SIBFClass>>();
                for (int i = 0; i < list.Count; i++)
                {
                    Assert.AreEqual(list[i].S, ret[i].S);
                    Assert.AreEqual(list[i].IBF.I, ret[i].IBF.I);
                    Assert.AreEqual(list[i].IBF.B, ret[i].IBF.B);
                    Assert.AreEqual(list[i].IBF.F, ret[i].IBF.F);
                }
            }
        }

        [TestMethod]
        public void TestComplexDictionary()
        {
            Random rand = new Random();

            using (MemoryStream ms = new MemoryStream())
            using (SerializationWriter sw = new SerializationWriter(ms))
            using (SerializationReader sr = new SerializationReader(ms))
            {
                Dictionary<int, SIBFClass> dict = new Dictionary<int, SIBFClass>(Config.MULTI_TEST_COUNT);
                for (int i = 0; i < Config.MULTI_TEST_COUNT; i++)
                {
                    dict[rand.Next(int.MinValue, int.MaxValue)] = new SIBFClass()
                    {
                        S = "test" + i,
                        IBF = new IBFClass()
                        {
                            I = rand.Next(int.MinValue, int.MaxValue),
                            B = rand.Next(2) == 1,
                            F = (float)rand.NextDouble()
                        }
                    };
                }
                sw.Write(dict);

                sw.Flush();
                ms.Position = 0;

                Dictionary<int, SIBFClass> ret = sr.Read<Dictionary<int, SIBFClass>>();
                foreach (KeyValuePair<int, SIBFClass> kvp in dict)
                {
                    Assert.AreEqual(kvp.Value.S, ret[kvp.Key].S);
                    Assert.AreEqual(kvp.Value.IBF.I, ret[kvp.Key].IBF.I);
                    Assert.AreEqual(kvp.Value.IBF.B, ret[kvp.Key].IBF.B);
                    Assert.AreEqual(kvp.Value.IBF.F, ret[kvp.Key].IBF.F);
                }
            }
        }

        private class IBFClass : ISerializable
        {
            public int I;
            public bool B;
            public float F;

            public void ReadFromStream(SerializationReader sr)
            {
                I = sr.Read<int>();
                B = sr.Read<bool>();
                F = sr.Read<float>();
            }

            public void WriteToStream(SerializationWriter sw)
            {
                sw.Write(I);
                sw.Write(B);
                sw.Write(F);
            }
        }

        private class SIBFClass : ISerializable
        {
            public string S;
            public IBFClass IBF;

            public void ReadFromStream(SerializationReader sr)
            {
                S = sr.Read<string>();
                IBF = sr.Read<IBFClass>();
            }

            public void WriteToStream(SerializationWriter sw)
            {
                sw.Write(S);
                sw.Write(IBF);
            }
        }
    }
}
