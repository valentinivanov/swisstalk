using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Validation;
using Swisstalk.Foundation.Metadata.Reflection.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction
{
    public class DirectPropertyExtractionStrategy : DirectMemberExtractionStrategy
    {
        private readonly IMemberExtractionPredicate extractionPredicate;

        public DirectPropertyExtractionStrategy(IMemberExtractionPredicate extractionPredicate,
                                                IMemberNameSelector nameSelector, 
                                                IEnumerable<IMemberValidator> validators) : base(nameSelector, validators)
        {
            this.extractionPredicate = extractionPredicate;
        }

        protected override ITypeElement CreateTypeElement(string name, MemberInfo memberInfo)
        {
            return new PropertyTypeElement(name, (PropertyInfo)memberInfo);
        }

        protected override IEnumerable<MemberInfo> FilterMembers(Type typeInfo)
        {
            return typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                           .Where(p => extractionPredicate.IsEligible(p))
                           .Select(p => (MemberInfo)p);
        }
    }
}
