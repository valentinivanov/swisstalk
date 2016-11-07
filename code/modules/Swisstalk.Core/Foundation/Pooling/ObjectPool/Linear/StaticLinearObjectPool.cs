using Swisstalk.Foundation.Pooling.ObjectPool.Linear.Model;

namespace Swisstalk.Foundation.Pooling.ObjectPool.Linear
{
    internal class StaticLinearObjectPool<T, U> : LinearObjectPool<T, U>
        where U : struct
        where T : class, IPooledObject<U>, new()
    {
        public StaticLinearObjectPool(int maxItemCount)
        {
            StaticLinearPoolStorageSource<T, U> source = new StaticLinearPoolStorageSource<T, U>(maxItemCount,
                                                                                                 new PooledRefFactory<T, U>(this));
            InitializeModel(source);
        }
    }
}
