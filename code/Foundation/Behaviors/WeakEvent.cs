using Swisstalk.Foundation.Composition;
using System;

namespace Swisstalk.Foundation.Behaviors
{
    public interface IWeakEvent<TDelegate>
    {
        void Add(TDelegate weakHandler);
        void Remove(TDelegate weakHandler);
    }

    public class WeakEvent<TDelegate> : IWeakEvent<TDelegate>
    {
        private class WeakDelegateCollection : CompositeObject<WeakReference<TDelegate>>
        {
            public void Invoke(Action<TDelegate> invokeAction)
            {
                RemoveAll(d => !d.IsAlive);

                foreach (WeakReference<TDelegate> weakDelegate in Composite)
                {
                    if (weakDelegate.IsAlive)
                    {
                        invokeAction(weakDelegate.Reference);
                    }
                }
            }
        }

        private readonly WeakDelegateCollection delegates = new WeakDelegateCollection();

        public void Add(TDelegate weakHandler)
        {
            delegates.Append(new WeakReference<TDelegate>(weakHandler));
        }

        public void Remove(TDelegate weakHandler)
        {
            delegates.RemoveAll(d => d.Target.Equals(weakHandler));
        }

        public void Invoke(Action<TDelegate> invokeAction)
        {
            delegates.Invoke(invokeAction);
        }
    }
}
