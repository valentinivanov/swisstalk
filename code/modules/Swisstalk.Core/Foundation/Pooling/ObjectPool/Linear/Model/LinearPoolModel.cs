using System;
using System.Collections.Generic;
using Swisstalk.Foundation.Algorithms;

namespace Swisstalk.Foundation.Pooling.ObjectPool.Linear.Model
{
    internal class LinearPoolModel<T, U> : IPoolModel<T, U>
       where U : struct
       where T : class, IPooledObject<U>, new()
    {
        private readonly ILinearPoolStorageSource<T, U> _storageSource;

        private readonly LinearSearchMethod<bool> _stateSearch;
        private readonly LinearSearchMethod<PooledRef<T, U>> _poolSearch;

        private readonly IList<PooledRef<T, U>> _objects;
        private readonly IList<bool> _states;

        public LinearPoolModel(ILinearPoolStorageSource<T, U> storageSource)
        {
            _storageSource = storageSource;

            _stateSearch = new LinearSearchMethod<bool>();
            _poolSearch = new LinearSearchMethod<PooledRef<T, U>>();
            
            _objects = _storageSource.GetPooledRefStorage();
            _states = _storageSource.GetPooledRefStateStorage();
        }

        public PooledRef<T, U> Acquire()
        {
            PooledRef<T, U> item = null;
            int index = _stateSearch.FindElementIndex(_states, true);

            if (index >= 0)
            {
                item = Reserve(index);
            }
            else
            {
                item = Extend();
            }

            return item;
        }

        public void Return(IPooledRef<T, U> element)
        {
            int index = _poolSearch.FindElementIndex(_objects, element);

            if (index < 0)
            {
                throw new InvalidOperationException("Element doesn't belong to the current pool!");
            }

            if (_states[index])
            {
                throw new InvalidOperationException("Trying to free element that is already free!");
            }

            _states[index] = true;
        }

        private PooledRef<T, U> Reserve(int index)
        {
            _states[index] = false;
            return _objects[index];
        }

        private PooledRef<T, U> Extend()
        {
            PooledRef<T, U> item = _storageSource.NewRef();
            _objects.Add(item);
            _states.Add(false);

            return item;
        }

        public void Dispose()
        {
            foreach(PooledRef<T, U> pooledRef in _objects)
            {
                pooledRef.DisposeInstance();
            }
        }
    }
}
