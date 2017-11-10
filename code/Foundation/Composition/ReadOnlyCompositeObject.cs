using System.Collections.Generic;
using System.Linq;

namespace Swisstalk.Foundation.Composition
{
    public class ReadOnlyCompositeObject<T>
    {
        private readonly T[] composite;

        public ReadOnlyCompositeObject(IEnumerable<T> objects)
        {
            composite = objects.ToArray();
        }

        public ReadOnlyCompositeObject(params T[] objects)
        {
            composite = objects;
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
