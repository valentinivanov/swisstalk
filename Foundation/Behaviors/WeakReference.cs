using System;
using System.Runtime.Serialization;

namespace Swisstalk.Foundation.Behaviors
{
    public class WeakReference<T> : WeakReference
    {
        public WeakReference(T reference) : base(reference)
        {
        }

        public WeakReference(T reference, bool trackResurrection) : base(reference, trackResurrection)
        {
        }

        public WeakReference(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public T Reference
        {
            get
            {
                return (T)base.Target;
            }
        }
    }
}
