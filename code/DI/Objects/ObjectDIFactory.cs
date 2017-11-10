using Swisstalk.Foundation.Utils;
using System;
using System.Collections.Generic;

namespace Swisstalk.DI.Objects
{
    public class ObjectDIFactory : DIFactory<Type, object>
    {
        private readonly Dictionary<Type, DICollection> configuration;

        public ObjectDIFactory(Dictionary<Type, DICollection> configuration)
        {
            this.configuration = configuration;
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        protected override object NewInstance(Type key)
        {
            return Activator.CreateInstance(key);
        }

        protected override IEnumerable<InstanceDependencyMapping> MapInstance(Type key, object instance)
        {
            DICollection dependencies;
            configuration.TryGetValue(instance.GetType(), out dependencies);

            return new OneItemDependencyEnumerable<InstanceDependencyMapping>(new InstanceDependencyMapping(instance, dependencies));
        }
    }
}
