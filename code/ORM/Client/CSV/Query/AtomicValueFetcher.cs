using Swisstalk.Foundation.Utils;
using Swisstalk.ORM.Query;
using Swisstalk.ORM.Transport;

namespace Swisstalk.ORM.Client.CSV.Query
{
    public class AtomicValueFetcher : IQuery
    {
        private readonly IDataRowReader reader;

        public AtomicValueFetcher(IDataRowReader reader)
        {
            RaiseException.WhenTrue(reader == null, "reader cannot be null!");

            this.reader = reader;
        }

        public Packet Execute()
        {
            return DataRowConverter.AtomicValueFrom(reader);
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
