namespace Swisstalk.Foundation.Factory.Collection
{
    public class ArrayEmitCreateFactory<T> : IFactory<T[]>
    {
        private readonly int _count;
        private readonly IFactory<T> _elementFactory;

        public ArrayEmitCreateFactory(int count, IFactory<T> elementFactory)
        {
            _count = count;
            _elementFactory = elementFactory;
        }

        public T[] Execute()
        {
            T[] array = new T[_count];

            for (int i = 0; i < _count; ++i)
            {
                array[i] = _elementFactory.Execute();
            }

            return array;
        }
    }
}
