using Swisstalk.Foundation.Metadata.Reflection;
using System;
using System.Collections;

namespace Swisstalk.ORM.Decoding.Collections
{
    public class GenericListBuilder : ICollectionBuilder
    {
        private readonly IList list;
        private readonly Type elementType;

        public GenericListBuilder(Type listType)
        {
            elementType = listType.GetListElementType();
            list = listType.CreateGenericList();
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
            list.Add(element);

            return this;
        }

        public ICollectionBuilder Reset()
        {
            list.Clear();

            return this;
        }

        public object Build()
        {
            return list;
        }
    }
}
