using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace SerializationLib.Helpers
{
    internal static class SizeOfHelper
    {
        private static Dictionary<Type, int> sizeCache = new Dictionary<Type, int>();

        internal static int SizeOf<T>(T obj)
        {
            return SizeOf(typeof(T));
        }

        internal static int SizeOf(Type type)
        {
            if (!sizeCache.ContainsKey(type))
            {
                var dm = new DynamicMethod("func", typeof(int), Type.EmptyTypes, typeof(SizeOfHelper));

                ILGenerator il = dm.GetILGenerator();
                il.Emit(OpCodes.Sizeof, type);
                il.Emit(OpCodes.Ret);

                Func<int> func = (Func<int>)dm.CreateDelegate(typeof(Func<int>));
                sizeCache[type] = func();
            }

            return sizeCache[type];
        }
    }
}
