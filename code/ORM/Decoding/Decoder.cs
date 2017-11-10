using Swisstalk.Foundation.Utils;
using Swisstalk.ORM.Transport;
using System;

namespace Swisstalk.ORM.Decoding
{
    public static class Decoder
    {
        public static T DecodeAs<T>(Packet p)
        {
            return (T)Decode(p, typeof(T));
        }

        public static object Decode(Packet p, Type t)
        {
            if (p.PayloadType == PacketType.Atomic)
            {
                return AtomicDecoder.Decode(p, t);
            }
            else if (p.PayloadType == PacketType.Dataset)
            {
                return DatasetDecoder.Decode(p.GetPayloadAs<PacketDataset>(), t);
            }
            else if (p.PayloadType == PacketType.Array)
            {
                return ArrayDecoder.Decode(p.GetPayloadAs<PacketArray>(), t);
            }
            else
            {
                RaiseException.WhenTrue(true, "Invalid packet type {0}", p.PayloadType);
                return null;
            }
        }
    }
}
