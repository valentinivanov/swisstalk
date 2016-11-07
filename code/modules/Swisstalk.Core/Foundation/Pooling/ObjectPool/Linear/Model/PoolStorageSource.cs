using System;
using System.Collections.Generic;
using Swisstalk.Foundation.Factory.Collection;

namespace Swisstalk.Foundation.Pooling.ObjectPool.Linear.Model
{
    internal interface ILinearPoolStorageSource<T, U>
       where U : struct
       where T : class, IPooledObject<U>, new()
    {
        IList<PooledRef<T, U>> GetPooledRefStorage();
        IList<bool> GetPooledRefStateStorage();
        PooledRef<T, U> NewRef();
    }

    internal class DynamicLinearPoolStorageSource<T, U> : ILinearPoolStorageSource<T, U>
       where U : struct
       where T : class, IPooledObject<U>, new()
    {
        private readonly int _initialCapacity;
        private readonly PooledRefFactory<T, U> _elementFactory;

        public DynamicLinearPoolStorageSource(int initialCapacity,
                                              PooledRefFactory<T, U> elementFactory)
        {
            _initialCapacity = initialCapacity;
            _elementFactory = elementFactory;
        }

        public IList<bool> GetPooledRefStateStorage()
        {
            return ListEmitter<bool>.Emit(_initialCapacity, true);
        }

        public IList<PooledRef<T, U>> GetPooledRefStorage()
        {
            return ListEmitter<PooledRef<T, U>>.Emit(_initialCapacity, 
                                                     _elementFactory);
        }

        public PooledRef<T, U> NewRef()
        {
            return _elementFactory.Execute();
        }
    }

    internal class StaticLinearPoolStorageSource<T, U> : ILinearPoolStorageSource<T, U>
       where U : struct
       where T : class, IPooledObject<U>, new()
    {
        private readonly int _initialCapacity;
        private readonly PooledRefFactory<T, U> _elementFactory;

        public StaticLinearPoolStorageSource(int initialCapacity,
                                             PooledRefFactory<T, U> elementFactory)
        {
            _initialCapacity = initialCapacity;
            _elementFactory = elementFactory;
        }

        public IList<bool> GetPooledRefStateStorage()
        {
            return ArrayEmitter<bool>.Emit(_initialCapacity, true);
        }

        public IList<PooledRef<T, U>> GetPooledRefStorage()
        {
            return ArrayEmitter<PooledRef<T, U>>.Emit(_initialCapacity,
                                                      _elementFactory);
        }

        public PooledRef<T, U> NewRef()
        {
            throw new InvalidOperationException("StaticLinearPoolStorageSource cannot create new refereneces! Use DynamicLinearPoolStorageSource.");
        }
    }
}
