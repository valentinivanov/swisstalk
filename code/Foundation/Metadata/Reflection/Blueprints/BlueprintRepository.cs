using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder;
using System;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints
{
    public class BlueprintRepository<BlueprintBuilderFactoryType> where BlueprintBuilderFactoryType : IBlueprintBuilderFactory, new()
    {
        private readonly Dictionary<Type, ITypeBlueprint> blueprintCache;
        private readonly ITypeBlueprintBuilder blueprintBuilder;

        private static BlueprintRepository<BlueprintBuilderFactoryType> instance = new BlueprintRepository<BlueprintBuilderFactoryType>();

        protected BlueprintRepository()
        {
            blueprintCache = new Dictionary<Type, ITypeBlueprint>();

            BlueprintBuilderFactoryType builderFactory = new BlueprintBuilderFactoryType();
            blueprintBuilder = builderFactory.Create();
        }

        public static ITypeBlueprint Get(Type type)
        {
            ITypeBlueprint blueprint;

            if (!instance.blueprintCache.TryGetValue(type, out blueprint))
            {
                blueprint = Create(type);
                instance.blueprintCache.Add(type, blueprint);
            }

            return blueprint;
        }

        private static ITypeBlueprint Create(Type type)
        {
            return instance.blueprintBuilder.Build(type);
        }
    }
}
