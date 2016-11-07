namespace Swisstalk.Foundation.Pooling.ObjectPool
{
    public struct NullObjectState
    {
    }

    public interface IPooledObject<U>
        where U : struct
    {
        void Construct(U state);
        void Construct();
        void Reset();
    }
}
