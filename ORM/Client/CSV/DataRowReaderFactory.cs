namespace Swisstalk.ORM.Client.CSV
{
    public static class DataRowReaderFactory
    {
        public static IDataRowReader FetchAll (string path)
        {
            return new DataRowReader(path);
        }

        public static IDataRowReader FilterWith(this IDataRowReader reader, string[] headers)
        {
            if (headers != null && headers.Length > 0)
            {
                return new DataRowFilterReader(reader, headers);
            }
            else
            {
                return reader;
            }
        }

        public static IDataRowReader AliasWith(this IDataRowReader reader, HeaderAlias[] aliases)
        {
            if (aliases != null && aliases.Length > 0)
            {
                return new HeaderAliasDataRowReader(reader, aliases);
            }
            else
            {
                return reader;
            }
        }
    }
}
