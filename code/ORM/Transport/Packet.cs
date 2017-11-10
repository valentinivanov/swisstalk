namespace Swisstalk.ORM.Transport
{
    public enum PacketType
    {
        None = 0,
        Atomic,
        Array,
        Dataset
    }

    public struct Packet
    {
        private readonly object payload;
        private readonly PacketType payloadType;

        public Packet(object payload, PacketType payloadType)
        {
            this.payload = payload;
            this.payloadType = payloadType;
        }

        public object Payload
        {
            get
            {
                return payload;
            }
        }

        public PacketType PayloadType
        {
            get
            {
                return payloadType;
            }
        }

        public T GetPayloadAs<T>()
        {
            return (T)payload;
        }
    }
}
