using System;
using System.Collections.Generic;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Composition
{
    public class UniqueReversibleComposition<T> : IAppendable<T>, IRemoveable<T>
    {
        private readonly List<T> _composite;

        public UniqueReversibleComposition()
        {
            _composite = new List<T>();
        }

        public void Append(T item)
        {
            if (_composite.Contains(item))
            {
                throw new InvalidOperationException("Adding duplicates is not allowed for UniqueReversibleComposition!");
            }

            _composite.Add(item);
        }

        public void Remove(T item)
        {
            _composite.Remove(item);
        }

        protected IEnumerable<T> Composite
        {
            get
            {
                return _composite;
            }
        }
    }
}