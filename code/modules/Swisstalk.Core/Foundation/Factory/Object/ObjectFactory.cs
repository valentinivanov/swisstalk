namespace Swisstalk.Foundation.Factory.Object
{
    public class ObjectFactory<T> : IFactory<T>
        where T : class, new()
    {
        public T Execute()
        {
            return new T();
        }
    }
}
