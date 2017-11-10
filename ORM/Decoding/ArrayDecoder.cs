using Swisstalk.Foundation.Metadata.Reflection;
using Swisstalk.Foundation.Utils;
using Swisstalk.ORM.Decoding.Collections;
using Swisstalk.ORM.Transport;
using System;

namespace Swisstalk.ORM.Decoding
{
    public static class ArrayDecoder
    { 
        public static object Decode(PacketArray pa, Type t)
        {
            RaiseException.WhenTrue(t.IsArrayOf<object>(), "Cannot deserialize array of objects '{0}'!", t);
            RaiseException.WhenTrue(t.IsGenericListOf<object>(), "Cannot deserialize list of objects '{0}'!", t);

            ICollectionBuilder collectionBuilder = CreateCollectionBuilder(pa, t);

            foreach (Packet p in pa.Values)
            {
                object element = Decoder.Decode(p, collectionBuilder.ElementType);
                collectionBuilder.AddElement(element);
            }

            return collectionBuilder.Build();
        }

        private static ICollectionBuilder CreateCollectionBuilder(PacketArray pa, Type t)
        {
            //TODO: think about pluggable extensibility for collection types
            //TODO: think about builder pooling
            if (t.IsArray)
            {
                return new ArrayBuilder(t, pa.Count);
            }
            else if (t.IsGenericList())
            {
                return new GenericListBuilder(t);
            }
            else
            {
                RaiseException.WhenTrue(true, "Unknown collection type: '{0}'! Only arrays and List<> are supported.", t.FullName);
                return null;
            }
        }
    }
}
