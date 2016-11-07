using Swisstalk.Foundation.Pooling.ObjectPool.Linear.Model;

namespace Swisstalk.Foundation.Pooling.ObjectPool.Linear
{
    internal class LinearObjectPool<T, U> : ObjectPoolBase<T, U>
        where U : struct
        where T : class, IPooledObject<U>, new()
    {
        protected LinearPoolModel<T, U> _model;

        protected LinearObjectPool()
        {
        }

        protected void InitializeModel(ILinearPoolStorageSource<T, U> storageSource)
        {
            _model = new LinearPoolModel<T, U>(storageSource);
        }

        public override IPooledRef<T, U> Get(U state)
        {
            IPooledRef<T, U> pooledObj = _model.Acquire();
            pooledObj.Instance.Construct(state);

            return pooledObj;
        }

        public override IPooledRef<T, U> Get()
        {
            IPooledRef<T, U> pooledObj = _model.Acquire();
            pooledObj.Instance.Construct();

            return pooledObj;
        }

        public override void Return(IPooledRef<T, U> pooledRef)
        {
            _model.Return(pooledRef);
        }

        public override void Dispose()
        {
            _model.Dispose();
        }
    }
}
