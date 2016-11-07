namespace Swisstalk.Foundation.Pooling.ObjectPool
{
    internal class ConcurrentObjectPool<T, U> : ObjectPoolBase<T, U>
        where U : struct
        where T : class, IPooledObject<U>, new()
    {
        private object _syncRoot;
        private ObjectPoolBase<T, U> _target;

        public ConcurrentObjectPool(ObjectPoolBase<T, U> target)
        {
            _syncRoot = new object();
            _target = target;
        }

        public override IPooledRef<T, U> Get(U state)
        {
            lock(_syncRoot)
            {
                return _target.Get(state);
            }
        }

        public override IPooledRef<T, U> Get()
        {
            lock (_syncRoot)
            {
                return _target.Get();
            }
        }

        public override void Return(IPooledRef<T, U> pooledRef)
        {
            lock(_syncRoot)
            {
                _target.Return(pooledRef);
            }
        }

        public override void Dispose()
        {
            lock (_syncRoot)
            {
                _target.Dispose();
            }
        }
    }
}
