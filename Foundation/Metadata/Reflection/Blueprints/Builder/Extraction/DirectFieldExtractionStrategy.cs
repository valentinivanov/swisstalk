using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Validation;
using Swisstalk.Foundation.Metadata.Reflection.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction
{
    public class DirectFieldExtractionStrategy : DirectMemberExtractionStrategy
    {
        private readonly IMemberExtractionPredicate extractionPredicate;

        public DirectFieldExtractionStrategy(IMemberExtractionPredicate extractionPredicate,
                                             IMemberNameSelector nameSelector, 
                                             IEnumerable<IMemberValidator> validators) : base(nameSelector, validators)
        {
            this.extractionPredicate = extractionPredicate;
        }

        protected override ITypeElement CreateTypeElement(string name, MemberInfo memberInfo)
        {
            return new FieldTypeElement(name, (FieldInfo)memberInfo);
        }

        protected override IEnumerable<MemberInfo> FilterMembers(Type typeInfo)
        {
            return typeInfo.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                           .Where(f => extractionPredicate.IsEligible(f))
                           .Select(f => (MemberInfo)f);
        }
    }
}
