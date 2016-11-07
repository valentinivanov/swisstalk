using System.Collections.Generic;

namespace Swisstalk.Foundation.Factory.Collection
{
    public class ListEmitDefaultFactory<T> : IFactory<List<T>>
    {
        private readonly int _count;

        public ListEmitDefaultFactory(int count)
        {
            _count = count;
        }

        public List<T> Execute()
        {
            List<T> list = new List<T>(_count);

            for (int i = 0; i < _count; ++i)
            {
                list.Add(default(T));
            }

            return list;
        }
    }
}
