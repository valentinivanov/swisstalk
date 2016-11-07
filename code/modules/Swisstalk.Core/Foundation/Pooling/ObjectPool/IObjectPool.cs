using Swisstalk.Foundation.Pooling.Scope;

namespace Swisstalk.Foundation.Pooling.ObjectPool
{    
    public interface IObjectPool<T, U> : IScopeFrame
        where U : struct
        where T : class, IPooledObject<U>
    {
        IPooledRef<T, U> Get(U state);
        IPooledRef<T, U> Get();
    }
}
