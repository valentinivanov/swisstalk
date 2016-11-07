using System.Collections.Generic;

namespace Swisstalk.Foundation.Factory.Collection
{
    public class ListEmitCreateFactory<T> : IFactory<List<T>>
    {
        private readonly int _count;
        private readonly IFactory<T> _elementFactory;

        public ListEmitCreateFactory(int count, IFactory<T> elementFactory)
        {
            _count = count;
            _elementFactory = elementFactory;
        }

        public List<T> Execute()
        {
            List<T> list = new List<T>(_count);

            for (int i = 0; i < _count; ++i)
            {
                T element = _elementFactory.Execute();

                list.Add(element);
            }

            return list;
        }
    }
}
