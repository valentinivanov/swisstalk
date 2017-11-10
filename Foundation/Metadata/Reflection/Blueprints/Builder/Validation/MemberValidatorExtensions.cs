using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Validation
{
    public static class MemberValidatorExtensions
    {
        public static void TryValidate(this IMemberValidator memberValidator,
                                       MemberInfo memberInfo)
        {
            if (memberValidator.CanValidate(memberInfo))
            {
                memberValidator.Validate(memberInfo);
            }
        }
    }
}
