using System.Collections.Generic;

namespace Swisstalk.DI
{
    public abstract class DIFactoryBuilder<TKey, TFactory>
    {
        private readonly Dictionary<TKey, DICollectionBuilder> configuration;

        protected DIFactoryBuilder()
        {
            configuration = new Dictionary<TKey, DICollectionBuilder>();
        }

        public DICollectionBuilder ForKey(TKey key)
        {
            DICollectionBuilder collectionBuilder;
            if (!configuration.TryGetValue(key, out collectionBuilder))
            {
                collectionBuilder = new DICollectionBuilder();
                configuration[key] = collectionBuilder;
            }

            return collectionBuilder;
        }

        public TFactory Build()
        {
            Dictionary<TKey, DICollection> factoryConfiguration = new Dictionary<TKey, DICollection>();
            foreach(KeyValuePair<TKey, DICollectionBuilder> entry in configuration)
            {
                factoryConfiguration[entry.Key] = entry.Value.Build();
            }

            return NewFactory(factoryConfiguration);
        }

        protected abstract TFactory NewFactory(Dictionary<TKey, DICollection> factoryConfiguration);
    }
}
