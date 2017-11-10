using Swisstalk.Foundation.Metadata.Reflection.Blueprints;
using Swisstalk.Foundation.Metadata.Reflection.Elements;
using Swisstalk.ORM.Transport;
using System;

namespace Swisstalk.ORM.Decoding
{
    public static class DatasetDecoder
    {
        public static object Decode(PacketDataset dataset, Type t)
        {
            object result = Activator.CreateInstance(t);
            ITypeBlueprint blueprint = DecodeBlueprintRepository.Get(t);

            foreach (ITypeElement typeElement in blueprint.AllElements)
            {
                Packet p = dataset[typeElement.Name];

                object decodedValue = Decoder.Decode(p, typeElement.ElementType);

                typeElement.SetValue(result, decodedValue);
            }

            return result;
        }
    }
}
