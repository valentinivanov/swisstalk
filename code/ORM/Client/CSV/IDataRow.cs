namespace Swisstalk.ORM.Client.CSV
{
    public interface IDataRow
    {
        int FieldCount
        {
            get;
        }

        object FetchField(int fragmentIndex);
    }
}
