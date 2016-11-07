using System;
using System.Collections.Generic;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Composition
{
    public class UniqueComposition<T> : IAppendable<T>
    {
        private readonly List<T> _composite;

        public UniqueComposition()
        {
            _composite = new List<T>();
        }

        public void Append(T item)
        {
            if (_composite.Contains(item))
            {
                throw new InvalidOperationException("Adding duplicates is not allowed for UniqueComposition!");
            }

            _composite.Add(item);
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

