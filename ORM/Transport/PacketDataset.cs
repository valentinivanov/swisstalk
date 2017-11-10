using System.Collections.Generic;

namespace Swisstalk.ORM.Transport
{
    public class PacketDataset
    {
        private readonly Dictionary<string, Packet> entries;

        public PacketDataset()
        {
            entries = new Dictionary<string, Packet>();
        }

        public Packet this[string fieldName]
        {
            get
            {
                return entries[fieldName];
            }

            set
            {
                entries[fieldName] = value;
            }
        }

        public int Count
        {
            get
            {
                return entries.Count;
            }
        }

        public IEnumerable<string> AllKeys
        {
            get
            {
                return entries.Keys;
            }
        }

        public IEnumerable<Packet> AllValues
        {
            get
            {
                return entries.Values;
            }
        }

        public bool HasKey(string key)
        {
            return entries.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return entries.Remove(key);
        }

        public void Clear()
        {
            entries.Clear();
        }
    }
}
