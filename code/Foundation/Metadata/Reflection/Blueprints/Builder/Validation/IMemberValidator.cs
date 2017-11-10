using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Validation
{
    public interface IMemberValidator
    {
        bool CanValidate(MemberInfo memberInfo);
        void Validate(MemberInfo memberInfo);
    }
}
