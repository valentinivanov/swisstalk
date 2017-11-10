using System;
using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction
{
    public class AttributeMemberExtractionPredicate<T> : IMemberExtractionPredicate where T : Attribute
    {
        public bool IsEligible(MemberInfo memberInfo)
        {
            object[] attributes = memberInfo.GetCustomAttributes(typeof(T), true);
            return attributes != null && attributes.Length > 0; //has attibute
        }
    }
}
