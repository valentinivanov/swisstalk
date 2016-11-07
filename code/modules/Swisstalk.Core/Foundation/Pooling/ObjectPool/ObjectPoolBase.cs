namespace Swisstalk.Foundation.Pooling.ObjectPool
{
    internal abstract class ObjectPoolBase<T, U> : IObjectPool<T, U>
        where U : struct
        where T : class, IPooledObject<U>, new()
    {
        public abstract IPooledRef<T, U> Get(U state);
        public abstract IPooledRef<T, U> Get();
        public abstract void Return(IPooledRef<T, U> pooledRef);

        public abstract void Dispose();
    }
}
