using Swisstalk.Foundation.Runnable;
using System.Collections.Generic;
using Swisstalk.Foundation.Factory;

namespace Swisstalk.Foundation.Algorithms
{
    public class IterationMethod<T>
    {
        public void ForEachItem(IList<T> source, ISingleArgStatement<T> statement)
        {
            for (var i = 0; i < source.Count; ++i)
            {
                T item = source[i];
                statement.Execute(item);
            }
        }

        public void ForEachIndex(IList<T> source, IDoubleArgStatement<int, IList<T>> statement)
        {
            for (var i = 0; i < source.Count; ++i)
            {
                statement.Execute(i, source);
            }
        }

        public void ForEachInitialize(IList<T> source, T value)
        {
            for (var i = 0; i < source.Count; ++i)
            {
                source[i] = value;
            }
        }

        public void ForEachCreate(IList<T> source, IFactory<T> factory)
        {
            for (var i = 0; i < source.Count; ++i)
            {
                source[i] = factory.Execute();
            }
        }
    }
}
