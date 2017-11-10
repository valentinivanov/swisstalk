using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder
{
    public interface IBlueprintBuilderFactory
    {
        ITypeBlueprintBuilder Create();
    }
}
