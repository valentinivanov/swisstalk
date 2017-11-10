using Swisstalk.Foundation.Metadata.Reflection.Blueprints;
using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder;
using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction;
using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Validation;

namespace Swisstalk.ORM.Decoding
{
    public class DecodeBlueprintBuilderFactory : IBlueprintBuilderFactory
    {
        public ITypeBlueprintBuilder Create()
        {
            return new TypeBlueprintBuilder(new TypeBlueprintBuilderConfiguration(new ITypeElementExtractionStrategy[]
                {
                    new DirectFieldExtractionStrategy(new AttributeMemberExtractionPredicate<DecodeableAttribute>(), new DecodeAliasMemberNameSelector(), new IMemberValidator[] { }),
                    new DirectPropertyExtractionStrategy(new AttributeMemberExtractionPredicate<DecodeableAttribute>(), new DecodeAliasMemberNameSelector(), new IMemberValidator[] { new PropertyAccessValidator()})
                })
            );
        }
    }

    public class DecodeBlueprintRepository : BlueprintRepository<DecodeBlueprintBuilderFactory>
    {
    }
}
