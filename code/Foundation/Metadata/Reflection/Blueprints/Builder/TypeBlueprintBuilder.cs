using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction;
using System;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder
{
    public class TypeBlueprintBuilder : ITypeBlueprintBuilder
    {
        private readonly TypeBlueprintBuilderConfiguration _configuration;

        public TypeBlueprintBuilder(TypeBlueprintBuilderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ITypeBlueprint Build(Type t)
        {
            TypeBlueprint blueprint = new TypeBlueprint(t);

            foreach (ITypeElementExtractionStrategy elementExtractor in _configuration.Extraction)
            {
                elementExtractor.ExtractTo(t, blueprint);
            }

            return blueprint;
        }
    }
}
