using System;

namespace Swisstalk.Foundation.Pooling.ObjectPool
{
    internal class PooledRef<T, U> : IPooledRef<T, U>
        where U : struct
        where T : class, IPooledObject<U>, new() 
    {
        private T _instance;
        private ObjectPoolBase<T, U> _parent;
        private bool _instanceDisposed;

        public PooledRef(ObjectPoolBase<T, U> parent)
        {
            _instance = new T();
            _instance.Reset();

            _parent = parent;
            _instanceDisposed = false;
        }

        public T Instance
        {
            get
            {
                if (_instanceDisposed)
                {
                    throw new InvalidOperationException("Cannot use disposed instance!");
                }

                return _instance;
            }
        }

        public void Dispose()
        {
            _instance.Reset();
            _parent.Return(this);
        }

        public void DisposeInstance()
        {
            IDisposable disposableInstance = _instance as IDisposable;
            if (disposableInstance != null)
            {
                disposableInstance.Dispose();
            }

            _instanceDisposed = true;
        }
    }
}
