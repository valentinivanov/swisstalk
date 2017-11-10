using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction;
using System.Reflection;

namespace Swisstalk.ORM.Decoding
{
    public class DecodeAliasMemberNameSelector : IMemberNameSelector
    {
        public string Select(MemberInfo memberInfo)
        {
            DecodeAliasAttribute aliasAttribute = GetAliasAttribute(memberInfo);

            if (aliasAttribute == null)
            {
                return memberInfo.Name;
            }
            else
            {
                return aliasAttribute.Name;
            }
        }

        private DecodeAliasAttribute GetAliasAttribute(MemberInfo memberInfo)
        {
            object[] attributes = memberInfo.GetCustomAttributes(typeof(DecodeAliasAttribute), true);
            return (attributes != null && attributes.Length > 0) ? (DecodeAliasAttribute)attributes[0] : null;
        }
    }
}
