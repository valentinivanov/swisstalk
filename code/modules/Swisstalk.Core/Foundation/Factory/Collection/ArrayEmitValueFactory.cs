namespace Swisstalk.Foundation.Factory.Collection
{
    public class ArrayEmitValueFactory<T> : IFactory<T[]>
    {
        private readonly int _count;
        private readonly T _value;

        public ArrayEmitValueFactory(int count, T defaultValue)
        {
            _count = count;
            _value = defaultValue;
        }

        public T[] Execute()
        {
            T[] array = new T[_count];

            for (int i = 0; i < _count; ++i)
            {
                array[i] = _value;
            }

            return array;
        }
    }
}
