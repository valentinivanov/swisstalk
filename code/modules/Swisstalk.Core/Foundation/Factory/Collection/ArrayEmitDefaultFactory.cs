namespace Swisstalk.Foundation.Factory.Collection
{
    public class ArrayEmitDefaultFactory<T> : IFactory<T[]>
    {
        private readonly int _count;

        public ArrayEmitDefaultFactory(int count)
        {
            _count = count;
        }

        public T[] Execute()
        {
            T[] array = new T[_count];

            for (int i = 0; i < _count; ++i)
            {
                array[i] = default(T);
            }

            return array;
        }
    }
}
