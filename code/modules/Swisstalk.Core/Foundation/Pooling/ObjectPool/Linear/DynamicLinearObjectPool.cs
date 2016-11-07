using Swisstalk.Foundation.Pooling.ObjectPool.Linear.Model;

namespace Swisstalk.Foundation.Pooling.ObjectPool.Linear
{
    internal class DynamicLinearObjectPool<T, U> : LinearObjectPool<T, U>
        where U : struct
        where T : class, IPooledObject<U>, new()
    {
        public DynamicLinearObjectPool(int initialCapacity)
        {
            DynamicLinearPoolStorageSource<T, U> source = new DynamicLinearPoolStorageSource<T, U>(initialCapacity, 
                                                                                                   new PooledRefFactory<T, U>(this));
            InitializeModel(source);
        }
    }
}
