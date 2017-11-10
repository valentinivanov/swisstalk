using Swisstalk.Foundation.Utils;
using Swisstalk.ORM.Query;
using Swisstalk.ORM.Transport;

namespace Swisstalk.ORM.Client.CSV.Query
{
    public class DatasetFetcher : IQuery
    {
        private readonly IDataRowReader reader;

        public DatasetFetcher(IDataRowReader reader)
        {
            RaiseException.WhenTrue(reader == null, "reader cannot be null!");

            this.reader = reader;
        }

        public Packet Execute()
        {
            PacketDataset dataset = DataRowConverter.DatasetFrom(reader);
            return new Packet(dataset, PacketType.Dataset);
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
