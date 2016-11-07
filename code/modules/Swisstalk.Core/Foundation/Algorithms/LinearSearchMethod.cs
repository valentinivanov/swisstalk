using Swisstalk.Foundation.Runnable;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Algorithms
{
    public class LinearSearchMethod<T>
    {
        public T FindFirst(IList<T> source, IPredicate<T> condition)
        {
            for(var i = 0; i < source.Count; ++i)
            {
                T element = source[i];
                if (condition.Evaluate(element))
                {
                    return element;
                }
            }

            return default(T);
        }

        public IList<T> FindAll(IList<T> source, IPredicate<T> condition)
        {
            List<T> result = new List<T>();

            for (var i = 0; i < source.Count; ++i)
            {
                T element = source[i];
                if (condition.Evaluate(element))
                {
                    result.Add(element);
                }
            }

            return result;
        }

        public int FindElementIndex(IList<T> list, T element)
        {
            int index = -1;
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].Equals(element))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public int FindElementIndex(IList<T> list, object element)
        {
            int index = -1;
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].Equals(element))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}
