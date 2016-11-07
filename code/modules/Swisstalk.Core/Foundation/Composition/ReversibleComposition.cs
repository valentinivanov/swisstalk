using System.Collections.Generic;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Composition
{
    public class ReversibleComposition<T> : IAppendable<T>, IRemoveable<T>
    {
        private readonly List<T> _composite;

        public ReversibleComposition()
        {
            _composite = new List<T>();
        }

        public void Append(T item)
        {
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