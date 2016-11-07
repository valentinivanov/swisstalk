using System;

namespace Swisstalk.Foundation.Pooling.ObjectPool
{
    public interface IPooledRef<T, U> : IDisposable
        where U : struct
        where T : class, IPooledObject<U>
    {
        T Instance { get; }
    }
}
