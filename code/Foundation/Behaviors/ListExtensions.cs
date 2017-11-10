using System;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Behaviors
{
    public static class ListExtensions
    {
        public static T Dequeue<T>(this List<T> list)
        {
            T result = default(T);
            if (list.Count > 0)
            {
                result = list[0];
                list.RemoveAt(0);
            }

            return result;
        }

        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

