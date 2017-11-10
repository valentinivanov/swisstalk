using System;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder
{
    public interface ITypeBlueprintBuilder
    {
        ITypeBlueprint Build(Type t);
    }
}
