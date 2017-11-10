using Swisstalk.Foundation.Metadata.Reflection.Blueprints;
using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder;
using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction;
using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Validation;

namespace Swisstalk.DI.Injection
{
    public class DIBlueprintBuilderFactory : IBlueprintBuilderFactory
    {
        public ITypeBlueprintBuilder Create()
        {
            return new TypeBlueprintBuilder(new TypeBlueprintBuilderConfiguration(new ITypeElementExtractionStrategy[]
                {
                    new DirectFieldExtractionStrategy(new AttributeMemberExtractionPredicate<AutoInjectAttribute>(), new DIMemberNameSelector(), new IMemberValidator[] { }),
                    new DirectPropertyExtractionStrategy(new AttributeMemberExtractionPredicate<AutoInjectAttribute>(), new DIMemberNameSelector(), new IMemberValidator[] { new PropertyAccessValidator()})
                })
            );
        }
    }

    public class DIBlueprintRepository : BlueprintRepository<DIBlueprintBuilderFactory>
    {
    }
}
