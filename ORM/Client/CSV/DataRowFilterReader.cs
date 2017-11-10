using Swisstalk.Foundation.Behaviors;
using Swisstalk.Foundation.Utils;

namespace Swisstalk.ORM.Client.CSV
{
    public class DataRowFilterReader : IDataRowReader
    {
        private readonly IDataRowReader underlyingReader;
        private readonly string[] headerFilter;

        private int[] activeIndices;

        public DataRowFilterReader(IDataRowReader underlyingReader, string[] headerFilter)
        {
            RaiseException.WhenTrue(underlyingReader == null, "underlyingReader must not be null!");
            RaiseException.WhenTrue(headerFilter == null, "headerFilter must not be null!");
            RaiseException.WhenTrue(headerFilter.Length == 0, "headerFilter must not be empty!");

            this.underlyingReader = underlyingReader;
            this.headerFilter = headerFilter;
        }

        public string[] Headers
        {
            get
            {
                return (underlyingReader.Headers != null) ? headerFilter : null;
            }
        }

        public void Open()
        {
            underlyingReader.Open();

            activeIndices = RemaActiveIndices(underlyingReader.Headers, headerFilter);
        }

        public void Close()
        {
            underlyingReader.Close();

            activeIndices = null;
        }

        public IDataRow ReadNext()
        {
            IDataRow record = underlyingReader.ReadNext();
            return new DataRowFilter(record, activeIndices);
        }

        public bool HasNext()
        {
            return underlyingReader.HasNext();
        }

        public void Dispose()
        {
            underlyingReader.Dispose();

            activeIndices = null;
        }

        private static int[] RemaActiveIndices(string[] headers, string[] headerFilter)
        {
            int[] indices = new int[headerFilter.Length];

            for (int i = 0; i < headerFilter.Length; ++i)
            {
                indices[i] = headers.IndexOf(headerFilter[i]);
                RaiseException.WhenTrue(indices[i] == -1, "Cannot find index for '{0}' header!", headerFilter[i]);
            }

            return indices;
        }
    }
}
