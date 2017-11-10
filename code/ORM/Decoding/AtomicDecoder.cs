using Swisstalk.ORM.Transport;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Swisstalk.ORM.Decoding
{
    public class AtomicDecoder
    {
        private static readonly Dictionary<Type, Func<object, object>> ConversionTable = new Dictionary<Type, Func<object, object>>()
        {
            { typeof(bool), ConvertBool },
            { typeof(sbyte), ConvertSByte },
            { typeof(byte), ConvertByte },
            { typeof(short), ConvertShort },
            { typeof(ushort), ConvertUShort },
            { typeof(int), ConvertInt },
            { typeof(uint), ConvertUInt },
            { typeof(long), ConvertLong },
            { typeof(ulong), ConvertULong },
            { typeof(float), ConvertFloat },
            { typeof(double), ConvertDouble },
            { typeof(string), ConvertString },
        };

        public static object Decode(Packet p, Type t)
        {
            if (p.Payload == null)
            {
                return null;
            }
            else if (t.IsEnum)
            {
                return Enum.Parse(t, p.Payload.ToString());
            }
            else
            {
                return ConversionTable[t](p.Payload);
            }
        }

        private static object ConvertBool(object obj)
        {
            return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
        }

        private static object ConvertSByte(object obj)
        {
            return Convert.ToSByte(obj, CultureInfo.InvariantCulture);
        }

        private static object ConvertByte(object obj)
        {
            return Convert.ToByte(obj, CultureInfo.InvariantCulture);
        }

        private static object ConvertShort(object obj)
        {
            return Convert.ToInt16(obj, CultureInfo.InvariantCulture);
        }

        private static object ConvertUShort(object obj)
        {
            return Convert.ToUInt16(obj, CultureInfo.InvariantCulture);
        }

        private static object ConvertInt(object obj)
        {
            return Convert.ToInt32(obj, CultureInfo.InvariantCulture);
        }

        private static object ConvertUInt(object obj)
        {
            return Convert.ToUInt32(obj, CultureInfo.InvariantCulture);
        }

        private static object ConvertLong(object obj)
        {
            return Convert.ToInt64(obj, CultureInfo.InvariantCulture); ;
        }

        private static object ConvertULong(object obj)
        {
            return Convert.ToUInt64(obj, CultureInfo.InvariantCulture);
        }

        private static object ConvertFloat(object obj)
        {
            return Convert.ToSingle(PatchFloatingPointToInvariantCulture(obj), CultureInfo.InvariantCulture);
        }

        private static object ConvertDouble(object obj)
        {
            return Convert.ToDouble(PatchFloatingPointToInvariantCulture(obj), CultureInfo.InvariantCulture);
        }

        private static object ConvertString(object obj)
        {
            return obj.ToString();
        }

        private static object PatchFloatingPointToInvariantCulture(object obj)
        {
            return obj.ToString().Replace("\"", string.Empty).Replace(',', '.');
        }
    }
}
