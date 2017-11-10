using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction;
using System.Reflection;

namespace Swisstalk.DI.Injection
{
    public class DIMemberNameSelector : IMemberNameSelector
    {
        public string Select(MemberInfo memberInfo)
        {
            AutoInjectAttribute injectAttribute = GetAutoInjectAttribute(memberInfo);

            if (injectAttribute.IsAnonimous)
            {
                return memberInfo.Name;
            }
            else
            {
                return injectAttribute.Name;
            }
        }

        private AutoInjectAttribute GetAutoInjectAttribute(MemberInfo memberInfo)
        {
            object[] attributes = memberInfo.GetCustomAttributes(typeof(AutoInjectAttribute), true);
            return (AutoInjectAttribute)attributes[0];
        }
    }
}
