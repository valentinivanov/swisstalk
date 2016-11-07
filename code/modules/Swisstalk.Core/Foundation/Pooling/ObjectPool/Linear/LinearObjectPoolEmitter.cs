namespace Swisstalk.Foundation.Pooling.ObjectPool.Linear
{
    public static class LinearObjectPoolEmitter<T, U>
        where U : struct
        where T : class, IPooledObject<U>, new()
    {
        public static IObjectPool<T, U> NewStaticPool(int maxItemCount)
        {
            return new StaticLinearObjectPool<T, U>(maxItemCount);
        }

        public static IObjectPool<T, U> NewDynamicPool(int initialCapacity)
        {
            return new DynamicLinearObjectPool<T, U>(initialCapacity);
        }
    }
}
