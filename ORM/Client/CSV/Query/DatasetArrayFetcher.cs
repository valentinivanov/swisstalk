using Swisstalk.Foundation.Utils;
using Swisstalk.ORM.Query;
using Swisstalk.ORM.Transport;

namespace Swisstalk.ORM.Client.CSV.Query
{
    public class DatasetArrayFetcher : IQuery
    {
        private readonly IDataRowReader reader;

        public DatasetArrayFetcher(IDataRowReader reader)
        {
            RaiseException.WhenTrue(reader == null, "reader cannot be null!");

            this.reader = reader;
        }

        public Packet Execute()
        {
            PacketArray array = DataRowConverter.DatasetArrayFrom(reader);
            return new Packet(array, PacketType.Array);
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
