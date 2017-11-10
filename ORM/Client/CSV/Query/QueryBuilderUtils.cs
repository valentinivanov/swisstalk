using System.Collections.Generic;
using System.Linq;

namespace Swisstalk.ORM.Client.CSV.Query
{
    public static class QueryBuilderUtils
    {
        public static HeaderAlias[] ToHeaderAliases(IEnumerable<QueryBuilder.FieldAlias> fieldAliases)
        {
            return (fieldAliases.Count() > 0) ? fieldAliases.Select(e => new HeaderAlias(e.Name, e.Alias)).ToArray() : null;
        }

        public static List<QueryBuilder.FieldFilter> CombineFilters(List<QueryBuilder.FieldFilter> filters, List<QueryBuilder.FieldAlias> aliases)
        {
            if (aliases.Count > 0)
            {
                List<QueryBuilder.FieldFilter> combinedFilters = new List<QueryBuilder.FieldFilter>(filters);

                foreach (QueryBuilder.FieldAlias alias in aliases)
                {
                    if (combinedFilters.Find(e => e.Name == alias.Name).IsEmpty())
                    {
                        combinedFilters.Add(new QueryBuilder.FieldFilter(alias.Name));
                    }
                }

                return combinedFilters;
            }
            else
            {
                return filters;
            }
        }

        public static string[] ToHeaderNames(IEnumerable<QueryBuilder.FieldFilter> filters)
        {
            return (filters.Count() > 0) ? filters.Select(f => f.Name).ToArray() : null;
        }
    }
}
