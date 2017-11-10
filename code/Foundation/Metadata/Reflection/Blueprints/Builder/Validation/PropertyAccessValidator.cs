using Swisstalk.Foundation.Utils;
using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Validation
{
    public class PropertyAccessValidator : IMemberValidator
    {
        public bool CanValidate(MemberInfo memberInfo)
        {
            return memberInfo is PropertyInfo;
        }

        public void Validate(MemberInfo memberInfo)
        {
            ValidateProperty((PropertyInfo)memberInfo);
        }

        private void ValidateProperty(PropertyInfo propertyInfo)
        {
            RaiseException.WhenFalse(propertyInfo.CanWrite,
                                "Property {0}.{1} must have setter defined!",
                                propertyInfo.DeclaringType.FullName,
                                propertyInfo.Name);
        }
    }
}
