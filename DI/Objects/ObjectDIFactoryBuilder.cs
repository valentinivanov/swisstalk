using System;
using System.Collections.Generic;

namespace Swisstalk.DI.Objects
{
    public class ObjectDIFactoryBuilder : DIFactoryBuilder<Type, Object>
    {
        protected override object NewFactory(Dictionary<Type, DICollection> factoryConfiguration)
        {
            return new ObjectDIFactory(factoryConfiguration);
        }
    }
}
