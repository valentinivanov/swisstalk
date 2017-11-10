using System;

namespace Swisstalk.ORM.Decoding.Collections
{
    public interface ICollectionBuilder
    {
        ICollectionBuilder AddElement(object element);
        ICollectionBuilder Reset();
        
        Type ElementType
        {
            get;
        }

        object Build();
    }
}
