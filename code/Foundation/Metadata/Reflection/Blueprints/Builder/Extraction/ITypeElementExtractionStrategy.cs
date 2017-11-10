using System;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction
{
    public interface ITypeElementExtractionStrategy
    {
        void ExtractTo(Type t, TypeBlueprint blueprint);
    }
}
