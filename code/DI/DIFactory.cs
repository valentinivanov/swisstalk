using System.Collections.Generic;
using Swisstalk.DI.Injection;

namespace Swisstalk.DI
{
    public abstract class DIFactory<TKey, TProduct>
    {
        protected struct InstanceDependencyMapping
        {
            private readonly object instance;
            private readonly DICollection dependencies;

            public InstanceDependencyMapping(object instance,
                                             DICollection dependencies)
            {
                this.instance = instance;
                this.dependencies = dependencies;
            }

            public object Instance
            {
                get
                {
                    return instance;
                }
            }

            public DICollection Dependencies
            {
                get
                {
                    return dependencies;
                }
            }
        }

        public TProduct Create(TKey key)
        {
            TProduct instance = NewInstance(key);

            foreach (InstanceDependencyMapping dependencyMapping in MapInstance(key, instance))
            {
                TryInjectDependenciesAndConstruct(dependencyMapping.Instance, dependencyMapping.Dependencies);
            }

            return instance;
        }

        protected void TryInjectDependenciesAndConstruct(object instance,
                                                         DICollection instanceInjectors)
        {
            if (instanceInjectors != null)
            {
                foreach (IDependencyInjector injector in instanceInjectors)
                {
                    injector.InjectTo(instance);
                }
            }

            IConstructAware constructAware = instance as IConstructAware;
            if (constructAware != null)
            {
                constructAware.Construct();
            }
        }

        protected abstract TProduct NewInstance(TKey key);

        //Need to map single instance because some objects are composite. 
        //I.e. Unity GameObject is actually a collection of Components 
        //and dependencies are injected into Components but not GameObject itself
        protected abstract IEnumerable<InstanceDependencyMapping> MapInstance(TKey key, object instance);
    }
}
