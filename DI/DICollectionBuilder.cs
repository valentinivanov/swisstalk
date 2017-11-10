using Swisstalk.DI.Injection;

namespace Swisstalk.DI
{
    public class DICollectionBuilder
    {
        private readonly DICollection collection;

        public DICollectionBuilder()
        {
            collection = new DICollection();
        }

        public DICollectionBuilder UseInjector(IDependencyInjector injector)
        {
            collection.Add(injector);
            return this;
        }

        public DICollection Build()
        {
            return collection;
        }
    }
}
