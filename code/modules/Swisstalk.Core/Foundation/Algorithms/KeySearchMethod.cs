using Swisstalk.Foundation.Factory;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Algorithms
{
    public class KeySearchMethod<K, T>
    {
        public T GetOrCreate(K key, IDictionary<K, T> source, IFactory<T> factory)
        {
            T result = default(T);

            if (!source.TryGetValue(key, out result))
            {
                result = factory.Execute();

                source[key] = result;
            }

            return result;
        }
    }
}
