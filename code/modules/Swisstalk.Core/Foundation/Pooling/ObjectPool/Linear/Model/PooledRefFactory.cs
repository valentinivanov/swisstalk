using Swisstalk.Foundation.Factory;

namespace Swisstalk.Foundation.Pooling.ObjectPool.Linear.Model
{
    internal class PooledRefFactory<T, U> : IFactory<PooledRef<T, U>>
        where U : struct
        where T : class, IPooledObject<U>, new()
    {
        private readonly ObjectPoolBase<T, U> _owner;

        public PooledRefFactory(ObjectPoolBase<T, U> owner)
        {
            _owner = owner;
        }

        public PooledRef<T, U> Execute()
        {
            return new PooledRef<T, U>(_owner);
        }
    }
}
