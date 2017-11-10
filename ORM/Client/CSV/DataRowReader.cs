using CSVFile;
using Swisstalk.Foundation.Utils;
using System.IO;

namespace Swisstalk.ORM.Client.CSV
{
    public class DataRowReader : IDataRowReader
    {
        private readonly string path;

        private CSVReader csvReader;
        private StreamReader streamReader;

        private string[] headers;
        private string[] nextLine;

        public DataRowReader(string path)
        {
            RaiseException.WhenTrue(path == null, "Trying create FileSystemRecordReader with null path");
            RaiseException.WhenTrue(path.Length == 0, "Trying create FileSystemRecordReader with empty path");

            this.path = path;
        }

        public string[] Headers
        {
            get
            {
                return headers;
            }
        }

        public bool HasNext()
        {
            RaiseException.WhenFalse(IsActive(), "Trying to check stream that is closed! Path: {0}", path);
            return nextLine != null;
        }

        public void Open()
        {
            RaiseException.WhenTrue(IsActive(), "Trying to open stream that is opened already! Path: {0}", path);

            streamReader = new StreamReader(path);
            csvReader = new CSVReader(streamReader, CSVFile.CSV.DEFAULT_DELIMITER, CSVFile.CSV.DEFAULT_QUALIFIER, true);

            headers = csvReader.Headers;
            nextLine = csvReader.NextLine();
        }

        public void Close()
        {
            RaiseException.WhenFalse(IsActive(), "Trying to close stream that is closed already! Path: {0}", path);

            streamReader.Dispose();

            streamReader = null;
            csvReader = null;
            headers = null;
            nextLine = null;
        }

        public IDataRow ReadNext()
        {
            RaiseException.WhenTrue(nextLine == null, "Trying to read past end of stream! Path: {0}", path);

            IDataRow current = new DataRow(nextLine);

            nextLine = csvReader.NextLine();

            return current;
        }

        public void Dispose()
        {
            if (IsActive())
            {
                Close();
            }
        }

        private bool IsActive()
        {
            return streamReader != null;
        }
    }
}
