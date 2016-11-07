using System.Collections.Generic;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Composition
{
    public class Composition<T> : IAppendable<T>
    {
        private readonly List<T> _composite;

        public Composition()
        {
            _composite = new List<T>();
        }

        public void Append(T item)
        {
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
