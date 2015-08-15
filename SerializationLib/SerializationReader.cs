using BinaryBitLib;
using SerializationLib.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SerializationLib
{
    public class SerializationReader : IDisposable
    {
        private BinaryBitReader reader;

        public Stream BaseStream { get { return reader.BaseStream; } }

        public SerializationReader(Stream stream)
            : this(stream, new UTF8Encoding())
        { }

        public SerializationReader(Stream stream, Encoding encoding)
        {
            reader = new BinaryBitReader(stream, encoding);
        }

        /// <summary>
        /// Reads a value from the stream.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <returns>The value.</returns>
        public T Read<T>()
        {
            SerializationTypes serType = (SerializationTypes)reader.ReadByte();

            if (serType == SerializationTypes.None)
                return default(T);

            if (serType == SerializationTypes.SerializableType)
            {
                T instance = (T)Activator.CreateInstance<T>();
                ((ISerializable)instance).ReadFromStream(this);
                return instance;
            }

            return (T)read(typeof(T), serType);
        }

        private object read(Type type, SerializationTypes serType)
        {
            switch (serType)
            {
                case SerializationTypes.BoolType:
                    return reader.ReadBool();
                case SerializationTypes.ByteType:
                    return reader.ReadByte();
                case SerializationTypes.CharType:
                    return reader.ReadChar();
                case SerializationTypes.ShortType:
                    return (short)reader.ReadInt(16);
                case SerializationTypes.IntType:
                    return reader.ReadInt();
                case SerializationTypes.LongType:
                    return reader.ReadLong();
                case SerializationTypes.FloatType:
                    return reader.ReadFloat();
                case SerializationTypes.DoubleType:
                    return reader.ReadDouble();
                case SerializationTypes.DecimalType:
                    return reader.ReadDecimal();
                case SerializationTypes.DateTimeType:
                        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(reader.ReadLong());
                case SerializationTypes.StringType:
                    return reader.ReadString();
                case SerializationTypes.EnumType:
                    {
                        int size = SizeOfHelper.SizeOf(type);
                        if (size > 4)
                            return Enum.ToObject(type, reader.ReadLong(size * 8));
                        else
                            return Enum.ToObject(type, reader.ReadInt(size * 8));
                    }
                case SerializationTypes.ArrayType:
                    {
                        int length = read7BitEncodedInt();
                        SerializationTypes vType = (SerializationTypes)reader.ReadByte();

                        Type tvType = type.GetElementType();

                        Array arrayInstance = Array.CreateInstance(tvType, length);

                        for (int i = 0; i < length; i++)
                        {
                            if (vType == SerializationTypes.SerializableType)
                                arrayInstance.SetValue(readSerializable(tvType), i);
                            else
                                arrayInstance.SetValue(read(tvType, vType), i);
                        }

                        return arrayInstance;
                    }
                case SerializationTypes.ListType:
                    {
                        int length = read7BitEncodedInt();
                        SerializationTypes vType = (SerializationTypes)reader.ReadByte();

                        Type tvType = type.GetGenericArguments()[0];

                        IList listInstance = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(tvType));

                        for (int i = 0; i < length; i++)
                        {
                            if (vType == SerializationTypes.SerializableType)
                                listInstance.Add(readSerializable(tvType));
                            else
                                listInstance.Add(read(tvType, vType));
                        }

                        return listInstance;
                    }
                case SerializationTypes.DictionaryType:
                    {
                        int length = read7BitEncodedInt();
                        SerializationTypes kType = (SerializationTypes)reader.ReadByte();
                        SerializationTypes vType = (SerializationTypes)reader.ReadByte();

                        Type tkType = type.GetGenericArguments()[0];
                        Type tvType = type.GetGenericArguments()[1];

                        IDictionary dictInstance = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(tkType, tvType));

                        for (int i = 0; i < length; i++)
                        {
                            object newKey;
                            object newValue;

                            if (kType == SerializationTypes.SerializableType)
                                newKey = readSerializable(tkType);
                            else
                                newKey = read(tkType, kType);

                            if (vType == SerializationTypes.SerializableType)
                                newValue = readSerializable(tvType);
                            else
                                newValue = read(tvType, vType);

                            dictInstance.Add(newKey, newValue);
                        }

                        return dictInstance;
                    }
            }

            return null;
        }

        private object readSerializable(Type type)
        {
            object newInstance = Activator.CreateInstance(type);
            ((ISerializable)newInstance).ReadFromStream(this);

            return newInstance;
        }

        /// <summary>
        /// Exposed from BinaryBitLib/BinaryBitReader/
        /// </summary>
        private int read7BitEncodedInt()
        {
            int count = 0;
            int shift = 0;
            byte b;
            do
            {
                b = Read<byte>();
                count |= (b & 0x7F) << shift;
                shift += 7;
            } while ((b & 0x80) != 0);

            return count;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (reader != null)
                reader.Dispose();

            reader = null;
        }

        ~SerializationReader()
        {
            Dispose(false);
        }
    }
}
