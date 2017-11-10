using System;
using System.Collections;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Metadata.Reflection
{
    public static class TypeExtensions
    {
        public static bool IsArray(this Type type)
        {
            return type.IsArray;
        }

        public static bool IsArrayOf<T>(this Type type)
        {
            return type == typeof(T[]);
        }

        public static bool IsGenericList(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>);
        }

        public static bool IsGenericListOf<T>(this Type t)
        {
            return typeof(T).MakeGenericListType() == t;
        }

        public static Type MakeGenericListType(this Type elementType)
        {
            return typeof(List<>).MakeGenericType(elementType);
        }

        public static IList CreateGenericListOfElements(this Type elementType)
        {
            Type listType = elementType.MakeGenericListType();
            return (IList)Activator.CreateInstance(listType);
        }

        public static IList CreateGenericList(this Type listType)
        {
            return (IList)Activator.CreateInstance(listType);
        }

        public static Type GetListElementType(this Type t)
        {
            if (!t.IsGenericList())
            {
                throw new InvalidOperationException(string.Format("Type '{0}' is not a List<>!", t.FullName));
            }

            return t.GetGenericArguments()[0];
        }

        public static Type GetArrayElementType(this Type t)
        {
            if (!t.IsArray())
            {
                throw new InvalidOperationException(string.Format("Type '{0}' is not an Array!", t.FullName));
            }

            return t.GetElementType();
        }

    }
}
