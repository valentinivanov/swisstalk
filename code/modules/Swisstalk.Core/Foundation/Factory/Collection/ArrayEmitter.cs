namespace Swisstalk.Foundation.Factory.Collection
{
    public static class ArrayEmitter<T>
    {
        public static T[] Emit(int count, IFactory<T> elementFactory)
        {
            ArrayEmitCreateFactory<T> factory = new ArrayEmitCreateFactory<T>(count, elementFactory);
            
            return factory.Execute();
        }

        public static T[] Emit(int count)
        {
            ArrayEmitDefaultFactory<T> factory = new ArrayEmitDefaultFactory<T>(count);

            return factory.Execute();
        }

        public static T[] Emit(int count, T defaultValue)
        {
            ArrayEmitValueFactory<T> factory = new ArrayEmitValueFactory<T>(count, defaultValue);
            
            return factory.Execute();
        }
    }
}
