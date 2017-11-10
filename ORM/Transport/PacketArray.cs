using System.Collections.Generic;

namespace Swisstalk.ORM.Transport
{
    public class PacketArray
    {
        private readonly List<Packet> entries;

        public PacketArray()
        {
            entries = new List<Packet>();
        }

        public Packet this[int index]
        {
            get
            {
                return entries[index];
            }

            set
            {
                entries[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return entries.Count;
            }
        }

        public IEnumerable<Packet> Values
        {
            get
            {
                return entries;
            }
        }

        public void Add(Packet v)
        {
            entries.Add(v);
        }

        public bool Remove(Packet v)
        {
            return entries.Remove(v);
        }

        public void RemoveAt(int index)
        {
            entries.RemoveAt(index);
        }

        public void Clear()
        {
            entries.Clear();
        }
    }
}
