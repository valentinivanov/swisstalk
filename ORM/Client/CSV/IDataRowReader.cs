using System;

namespace Swisstalk.ORM.Client.CSV
{
    public interface IDataRowReader : IDisposable
    {
        void Open();
        void Close();

        string[] Headers
        {
            get;
        }

        IDataRow ReadNext();
        bool HasNext();
    }
}
