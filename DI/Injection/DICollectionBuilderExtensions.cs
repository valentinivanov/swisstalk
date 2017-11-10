using System;

namespace Swisstalk.DI.Injection
{
    public static class DICollectionBuilderExtensions
    {
        public static DICollectionBuilder InjectAnonimous(this DICollectionBuilder builder, object targetValue)
        {
            return builder.UseInjector(new AnonimousReflectionInjector(targetValue));
        }

        public static DICollectionBuilder InjectNamed(this DICollectionBuilder builder, string name, object targetValue)
        {
            return builder.UseInjector(new NamedReflectionInjector(name, targetValue));
        }
    }
}

