using System.Collections.Generic;

namespace Swisstalk.Foundation.Factory.Collection
{
    class ListEmitValueFactory<T> : IFactory<List<T>>
    {
        private readonly int _count;
        private readonly T _value;

        public ListEmitValueFactory(int count, T defaultValue)
        {
            _count = count;
            _value = defaultValue;
        }

        public List<T> Execute()
        {
            List<T> list = new List<T>(_count);

            for (int i = 0; i < _count; ++i)
            {
                list.Add(_value);
            }

            return list;
        }
    }
 }
