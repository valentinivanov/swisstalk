using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder
{
    public class TypeBlueprintBuilderConfiguration
    {
        private readonly IEnumerable<ITypeElementExtractionStrategy> extraction;

        public TypeBlueprintBuilderConfiguration(IEnumerable<ITypeElementExtractionStrategy> extraction)
        {
            this.extraction = extraction;
        }

        public IEnumerable<ITypeElementExtractionStrategy> Extraction
        {
            get
            {
                return extraction;
            }
        }
    }
}
