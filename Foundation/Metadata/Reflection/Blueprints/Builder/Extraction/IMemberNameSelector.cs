using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction
{
    public interface IMemberNameSelector
    {
        string Select(MemberInfo memberInfo);
    }
}
