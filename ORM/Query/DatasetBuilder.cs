using Swisstalk.ORM.Transport;
using System.Collections.Generic;

namespace Swisstalk.ORM.Query
{
    public class DatasetBuilder : IQuery
    {
        private struct DatasetFragment
        {
            private readonly IQuery query;
            private readonly string fieldName;

            public DatasetFragment(IQuery query, string fieldName)
            {
                this.query = query;
                this.fieldName = fieldName;
            }

            public IQuery Query
            {
                get
                {
                    return query;
                }
            }

            public string FieldName
            {
                get
                {
                    return fieldName;
                }
            }
        }

        private readonly List<DatasetFragment> fragments;

        public DatasetBuilder()
        {
            fragments = new List<DatasetFragment>();
        }

        public DatasetBuilder RequireField(string fieldName, IQuery query)
        {
            fragments.Add(new DatasetFragment(query, fieldName));
            return this;
        }

        public Packet Execute()
        {
            PacketDataset dataset = new PacketDataset();

            foreach (DatasetFragment fragment in fragments)
            {
                dataset[fragment.FieldName] = fragment.Query.Execute();
            }

            return new Packet(dataset, PacketType.Dataset);
        }

        public void Dispose()
        {
            foreach (DatasetFragment fragment in fragments)
            {
                fragment.Query.Dispose();
            }
        }
    }
}
