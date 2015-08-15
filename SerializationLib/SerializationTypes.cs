using System;
using System.Collections.Generic;
using System.Text;

namespace SerializationLib
{
    internal enum SerializationTypes : byte
    {
        None,

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
