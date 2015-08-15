using System;
using System.Collections.Generic;
using System.Text;

namespace SerializationLib
{
    internal enum SerializationTypes : byte
    {
        BoolType,
        ByteType,
        CharType,
        ShortType,
        IntType,
        LongType,
        FloatType,
        DoubleType,
        DecimalType,
        DateTimeType,
        StringType,
        EnumType,
        ArrayType,
        ListType,
        DictionaryType,

        SerializableType
    }
}
