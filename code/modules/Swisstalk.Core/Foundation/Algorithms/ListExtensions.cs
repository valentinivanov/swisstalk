using System.Collections.Generic;

namespace Swisstalk.Foundation.Algorithms
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
    }
}

