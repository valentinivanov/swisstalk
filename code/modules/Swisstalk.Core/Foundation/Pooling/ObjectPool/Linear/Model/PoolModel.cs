using System;

namespace Swisstalk.Foundation.Pooling.ObjectPool.Linear
{
    internal interface IPoolModel<T, U> : IDisposable
       where U : struct
       where T : class, IPooledObject<U>, new()
    {
        PooledRef<T, U> Acquire();

        void Return(IPooledRef<T, U> element);
    }
}
