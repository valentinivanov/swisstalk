using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction
{
    public interface IMemberExtractionPredicate
    {
        bool IsEligible(MemberInfo memberInfo);
    }
}
