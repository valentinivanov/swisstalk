using Swisstalk.Foundation.Utils;
using Swisstalk.ORM.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swisstalk.ORM.Client.CSV.Query
{
    public class QueryBuilder
    {
        public struct FieldFilter
        {
            private readonly string name;

            public FieldFilter(string name)
            {
                this.name = name;
            }

            public string Name
            {
                get
                {
                    return name;
                }
            }

            public bool IsEmpty()
            {
                return name == null;
            }
        }

        public struct FieldAlias
        {
            private readonly string name;
            private readonly string alias;

            public FieldAlias(string name, string alias)
            {
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

            public bool IsEmpty()
            {
                return name == null && alias == null;
            }
        }

        [Flags]
        private enum SelectVerb
        {
            None = 0,
            All = 1,
            Atomic = 2,
            Filter = 4,
            Alias = 8
        }

        private enum SelectLayout
        {
            None,
            Atomic,
            Composite
        }

        private enum SelectSequence
        {
            None,
            Single,
            Array
        }

        private readonly List<FieldFilter> filters = new List<FieldFilter>();
        private readonly List<FieldAlias> aliases = new List<FieldAlias>();

        private string fromPath;
        private SelectLayout layout = SelectLayout.Composite;
        private SelectSequence sequence = SelectSequence.Array;

        public QueryBuilder Select(string fieldName)
        {
            RaiseException.WhenTrue(fieldName == null, "fieldName cannot be null!");
            RaiseException.WhenTrue(fieldName.Length == 0, "fieldName cannot be empty!");

            filters.Add(new FieldFilter(fieldName));
            return this;
        }

        public QueryBuilder SelectAs(string fieldName, string fieldNameAlias)
        {
            RaiseException.WhenTrue(fieldName == null, "fieldName cannot be null!");
            RaiseException.WhenTrue(fieldName.Length == 0, "fieldName cannot be empty!");
            RaiseException.WhenTrue(fieldNameAlias == null, "fieldNameAlias cannot be null!");
            RaiseException.WhenTrue(fieldNameAlias.Length == 0, "fieldNameAlias cannot be empty!");

            aliases.Add(new FieldAlias(fieldName, fieldNameAlias));
            return this;
        }

        public QueryBuilder SelectAll()
        {
            filters.Clear();

            return this;
        }

        public QueryBuilder Atomic()
        {
            layout = SelectLayout.Atomic;

            return this;
        }

        public QueryBuilder Composite()
        {
            layout = SelectLayout.Composite;

            return this;
        }

        public QueryBuilder Single()
        {
            sequence = SelectSequence.Single;

            return this;
        }

        public QueryBuilder Array()
        {
            sequence = SelectSequence.Array;

            return this;
        }

        public QueryBuilder From(string path)
        {
            RaiseException.WhenTrue(path == null, "path cannot be null!");
            RaiseException.WhenTrue(path.Length == 0, "path cannot be empty!");

            fromPath = path;
            return this;
        }

        public IQuery Build()
        {
            if (layout == SelectLayout.Atomic && sequence == SelectSequence.Single)
            {
                return new AtomicValueFetcher(CreateAtomicReader());
            }
            else if (layout == SelectLayout.Atomic && sequence == SelectSequence.Array)
            {
                return new AtomicArrayFetcher(CreateAtomicReader());
            }
            else if (layout == SelectLayout.Composite && sequence == SelectSequence.Single)
            {
                return new DatasetFetcher(CreateCompositeReader());
            }
            else if (layout == SelectLayout.Composite && sequence == SelectSequence.Array)
            {
                return new DatasetArrayFetcher(CreateCompositeReader());
            }
            else
            {
                RaiseException.WhenTrue(true, "Cannot build query with the following attributes: {0}/{1}", layout, sequence);
                return null;
            }
        }

        private IDataRowReader CreateAtomicReader()
        {
            return DataRowReaderFactory.FetchAll(fromPath)
                                       .FilterWith(QueryBuilderUtils.ToHeaderNames(filters));
        }

        private IDataRowReader CreateCompositeReader()
        {
            return (filters.Count > 0) ? CreateFilteredReader() : CreateUnfilteredReader();
        }

        private IDataRowReader CreateFilteredReader()
        {
            List<FieldFilter> effectiveFilters = QueryBuilderUtils.CombineFilters(filters, aliases);

            return DataRowReaderFactory.FetchAll(fromPath)
                                       .FilterWith(QueryBuilderUtils.ToHeaderNames(effectiveFilters))
                                       .AliasWith(QueryBuilderUtils.ToHeaderAliases(aliases));
        }

        private IDataRowReader CreateUnfilteredReader()
        {
            return DataRowReaderFactory.FetchAll(fromPath)
                                       .AliasWith(QueryBuilderUtils.ToHeaderAliases(aliases));
        }
    }
}
