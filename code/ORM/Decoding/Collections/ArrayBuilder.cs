using Swisstalk.Foundation.Metadata.Reflection;
using System;
using System.Collections;

namespace Swisstalk.ORM.Decoding.Collections
{
    public class ArrayBuilder : ICollectionBuilder
    {
        private readonly IList array;
        private readonly Type elementType;
        private int currentIndex;

        public ArrayBuilder(Type arrayType, int targetElementCount)
        {
            elementType = arrayType.GetArrayElementType();
            array = Array.CreateInstance(elementType, targetElementCount);
            currentIndex = 0;
        }

        public Type ElementType
        {
            get
            {
                return elementType;
            }
        }

        public ICollectionBuilder AddElement(object element)
        {
            array[currentIndex] = element;
            currentIndex++;

            return this;
        }

        public ICollectionBuilder Reset()
        {
            currentIndex = 0;

            return this;
        }

        public object Build()
        {
            return array;
        }
    }
}
