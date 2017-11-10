using Swisstalk.Foundation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swisstalk.Foundation.Composition
{
    public class CompositeObject<T> : IAppendable<T>, IRemovable<T>
    {
        private readonly List<T> composite;

        public CompositeObject()
        {
            composite = new List<T>();
        }

        public CompositeObject(IEnumerable<T> objects)
        {
            composite = objects.ToList();
        }

        public CompositeObject(params T[] objects)
        {
            composite = objects.ToList();
        }

        public void Append(T item)
        {
            composite.Add(item);
        }

        public bool Remove(T item)
        {
            return composite.Remove(item);
        }

        public void RemoveAll(Predicate<T> match)
        {
            composite.RemoveAll(match);
        }

        public void Clear()
        {
            composite.Clear();
        }

        protected IEnumerable<T> Composite
        {
            get
            {
                return composite;
            }
        }
    }
}