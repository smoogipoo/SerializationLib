using BinaryBitLib;
using SerializationLib.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SerializationLib
{
    public class SerializationWriter : IDisposable
    {
        private BinaryBitWriter writer;

        public Stream BaseStream { get { return writer.BaseStream; } }

        public SerializationWriter()
            : this(new MemoryStream())
        { }

        public SerializationWriter(Stream stream)
            : this(stream, new UTF8Encoding())
        { }

        public SerializationWriter(Stream stream, Encoding encoding)
        {
            writer = new BinaryBitWriter(stream, encoding);
        }

        /// <summary>
        /// Writes a value to the stream.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value to write.</param>
        public void Write<T>(T value)
        {
            if (value == null)
                return;

            writeType(typeof(T));
            write(typeof(T), value);
        }

        private void writeType(Type type)
        {
            if (typeof(ISerializable).IsAssignableFrom(type))
                writer.WriteByte((byte)SerializationTypes.SerializableType);
            else if (type == typeof(bool))
                writer.WriteByte((byte)SerializationTypes.BoolType);
            else if (type == typeof(byte))
                writer.WriteByte((byte)SerializationTypes.ByteType);
            else if (type == typeof(char))
                writer.WriteByte((byte)SerializationTypes.CharType);
            else if (type == typeof(short))
                writer.WriteByte((byte)SerializationTypes.ShortType);
            else if (type == typeof(int))
                writer.WriteByte((byte)SerializationTypes.IntType);
            else if (type == typeof(long))
                writer.WriteByte((byte)SerializationTypes.LongType);
            else if (type == typeof(float))
                writer.WriteByte((byte)SerializationTypes.FloatType);
            else if (type == typeof(double))
                writer.WriteByte((byte)SerializationTypes.DoubleType);
            else if (type == typeof(decimal))
                writer.WriteByte((byte)SerializationTypes.DecimalType);
            else if (type == typeof(DateTime))
                writer.WriteByte((byte)SerializationTypes.DateTimeType);
            else if (type == typeof(string))
                writer.WriteByte((byte)SerializationTypes.StringType);
            else if (type.IsEnum)
                writer.WriteByte((byte)SerializationTypes.EnumType);
        }

        private void write(Type type, object value)
        {
            if (typeof(ISerializable).IsAssignableFrom(type))
                ((ISerializable)value).WriteToStream(this);
            else if (type == typeof(bool))
                writer.WriteBool((bool)value);
            else if (type == typeof(byte))
                writer.WriteByte((byte)value);
            else if (type == typeof(char))
                writer.WriteChar((char)value);
            else if (type == typeof(short))
                writer.WriteInt((short)value, 16);
            else if (type == typeof(int))
                writer.WriteInt((int)value);
            else if (type == typeof(long))
                writer.WriteLong((long)value);
            else if (type == typeof(float))
                writer.WriteFloat((float)value);
            else if (type == typeof(double))
                writer.WriteDouble((double)value);
            else if (type == typeof(decimal))
                writer.WriteDecimal((decimal)value);
            else if (type == typeof(DateTime))
                writer.WriteLong((long)((DateTime)value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
            else if (type == typeof(string))
                writer.WriteString(value.ToString());
            else if (type.IsEnum)
            {
                int size = SizeOfHelper.SizeOf(type);
                if (size > 4)
                    writer.WriteLong(Convert.ToInt64(value), size * 8);
                else
                    writer.WriteInt(Convert.ToInt32(value), size * 8);
            }
            else if (type.IsArray)
            {
                //Written as (arrayType)(count)(vType)[(value)][(value)]

                writer.WriteByte((byte)SerializationTypes.ArrayType);
                write7BitEncodedInt(((Array)value).Length);

                Type vType = ((Array)value).GetType().GetElementType();
                writeType(vType);

                for (int i = 0; i < ((Array)value).Length; i++)
                    write(vType, ((Array)value).GetValue(i));
            }
            else if (typeof(IList).IsAssignableFrom(type))
            {
                //Written as (listType)(count)(vType)[(value)][(value)]

                writer.WriteByte((byte)SerializationTypes.ListType);
                write7BitEncodedInt(((IList)value).Count);

                Type vType = value.GetType().GetGenericArguments()[0];
                writeType(vType);

                for (int i = 0; i < ((IList)value).Count; i++)
                    write(vType, ((IList)value)[i]);
            }
            else if (typeof(IDictionary).IsAssignableFrom(type))
            {
                //Written as (dictType)(count)(kType)(vType)[(key)(value)][(key)(value)]

                writer.WriteByte((byte)SerializationTypes.DictionaryType);
                write7BitEncodedInt(((IDictionary)value).Count);

                Type kType = value.GetType().GetGenericArguments()[0];
                Type vType = value.GetType().GetGenericArguments()[1];
                writeType(kType);
                writeType(vType);

                IDictionaryEnumerator e = ((IDictionary)value).GetEnumerator();
                while (e.MoveNext())
                {
                    write(kType, e.Key);
                    write(vType, e.Value);
                };
            }
        }

        public void Flush()
        {
            writer.Flush();
        }

        /// <summary>
        /// Exposed from BinaryBitLib/BinaryBitWriter
        /// </summary>
        /// <param name="value">The value to write.</param>
        private void write7BitEncodedInt(int value)
        {
            uint v = (uint)value;
            while (v >= 0x80)
            {
                Write((byte)(v | 0x80));
                v >>= 7;
            }
            Write((byte)v);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (writer != null)
                writer.Dispose();

            writer = null;
        }

        ~SerializationWriter()
        {
            Dispose(false);
        }
    }
}
