using System.Collections.Generic;

namespace Swisstalk.Foundation.Factory.Collection
{
    public static class ListEmitter<T>
    {
        public static List<T> Emit(int count, IFactory<T> elementFactory)
        {
            ListEmitCreateFactory<T> factory = new ListEmitCreateFactory<T>(count, elementFactory);
            
            return factory.Execute();
        }

        public static List<T> Emit(int count)
        {
            ListEmitDefaultFactory<T> factory = new ListEmitDefaultFactory<T>(count);

            return factory.Execute();
        }

        public static List<T> Emit(int count, T defaultValue)
        {
            ListEmitValueFactory<T> factory = new ListEmitValueFactory<T>(count, defaultValue);

            return factory.Execute();
        }
    }
}
