using System.Collections;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Utils
{
    public struct OneItemDependencyEnumerator<T> : IEnumerator<T>
    {
        private readonly T item;
        private bool atEnd;

        public OneItemDependencyEnumerator(T item)
        {
            this.item = item;
            atEnd = false;
        }

        public T Current
        {
            get
            {
                return item;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return item;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            bool hasMoreElements = !atEnd;
            atEnd = true;

            return hasMoreElements;
        }

        public void Reset()
        {
            atEnd = false;
        }
    }

    public struct OneItemDependencyEnumerable<T> : IEnumerable<T>
    {
        private readonly T item;

        public OneItemDependencyEnumerable(T item)
        {
            this.item = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new OneItemDependencyEnumerator<T>(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new OneItemDependencyEnumerator<T>(item);
        }
    }
}
