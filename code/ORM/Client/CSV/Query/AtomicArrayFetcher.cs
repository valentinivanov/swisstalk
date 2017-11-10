using Swisstalk.Foundation.Utils;
using Swisstalk.ORM.Query;
using Swisstalk.ORM.Transport;

namespace Swisstalk.ORM.Client.CSV.Query
{
    public class AtomicArrayFetcher : IQuery
    {
        private readonly IDataRowReader reader;

        public AtomicArrayFetcher(IDataRowReader reader)
        {
            RaiseException.WhenTrue(reader == null, "reader cannot be null!");

            this.reader = reader;
        }

        public Packet Execute()
        {
            PacketArray array = DataRowConverter.AtomicArrayFrom(reader);
            return new Packet(array, PacketType.Array);
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
