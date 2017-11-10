using Swisstalk.Foundation.Utils;

namespace Swisstalk.ORM.Client.CSV
{
    public class DataRowFilter : IDataRow
    {
        private readonly IDataRow underlyingRecord;
        private readonly int[] activeIndices;

        public DataRowFilter(IDataRow underlyingRecord, int[] activeIndices)
        {
            RaiseException.WhenTrue(underlyingRecord == null, "underlyingRecord must not be null!");
            RaiseException.WhenTrue(activeIndices == null, "activeIndices must not be null!");
            RaiseException.WhenTrue(activeIndices.Length == 0, "activeIndices must not be empty!");

            this.underlyingRecord = underlyingRecord;
            this.activeIndices = activeIndices;
        }

        public int FieldCount
        {
            get
            {
                return activeIndices.Length;
            }
        }

        public object FetchField(int fragmentIndex)
        {
            return underlyingRecord.FetchField(activeIndices[fragmentIndex]);
        }
    }
}
