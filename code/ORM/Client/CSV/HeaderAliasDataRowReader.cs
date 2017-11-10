using Swisstalk.Foundation.Utils;
using System.Linq;

namespace Swisstalk.ORM.Client.CSV
{
    public struct HeaderAlias
    {
        private readonly string name;
        private readonly string alias;

        public HeaderAlias(string name, string alias)
        {
            RaiseException.WhenTrue(name == null, "name must not be null!");
            RaiseException.WhenTrue(alias == null, "alias must not be null!");

            this.name = name;
            this.alias = alias;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Alias
        {
            get
            {
                return alias;
            }
        }

        public bool IsDefault
        {
            get
            {
                return name == null && alias == null;
            }
        }
    }

    public class HeaderAliasDataRowReader : IDataRowReader
    {
        private readonly IDataRowReader underlyingReader;
        private readonly HeaderAlias[] aliases;

        private string[] renamedHeaders;

        public HeaderAliasDataRowReader(IDataRowReader underlyingReader, HeaderAlias[] aliases)
        {
            RaiseException.WhenTrue(underlyingReader == null, "underlyingReader must not be null!");
            RaiseException.WhenTrue(aliases == null, "aliases must not be null!");
            RaiseException.WhenTrue(aliases.Length == 0, "aliases must not be empty!");

            this.underlyingReader = underlyingReader;
            this.aliases = aliases;
        }

        public string[] Headers
        {
            get
            {
                return renamedHeaders;
            }
        }

        public void Open()
        {
            underlyingReader.Open();
            renamedHeaders = RenameHeaders(underlyingReader.Headers, aliases);
        }

        public void Close()
        {
            underlyingReader.Close();
        }

        public void Dispose()
        {
            underlyingReader.Dispose();
        }

        public IDataRow ReadNext()
        {
            return underlyingReader.ReadNext();
        }

        public bool HasNext()
        {
            return underlyingReader.HasNext();
        }

        private static string[] RenameHeaders(string[] originalHeaders, HeaderAlias[] rules)
        {
            return originalHeaders.Select(h => Substitute(h, rules)).ToArray();
        }

        private static string Substitute(string header, HeaderAlias[] rules)
        {
            HeaderAlias headerAlias = rules.Where(r => header == r.Name).SingleOrDefault();

            return (headerAlias.IsDefault) ? header : headerAlias.Alias;
        }
    }
}
