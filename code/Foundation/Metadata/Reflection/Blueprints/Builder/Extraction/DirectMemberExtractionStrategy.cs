using Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Validation;
using Swisstalk.Foundation.Metadata.Reflection.Elements;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints.Builder.Extraction
{
    public abstract class DirectMemberExtractionStrategy : ITypeElementExtractionStrategy
    {
        private readonly IMemberNameSelector nameSelector;
        private readonly IEnumerable<IMemberValidator> validators;

        public DirectMemberExtractionStrategy(IMemberNameSelector nameSelector,
                                              IEnumerable<IMemberValidator> validators)
        {
            this.nameSelector = nameSelector;
            this.validators = validators;
        }

        public void ExtractTo(Type t, TypeBlueprint blueprint)
        {
            foreach (MemberInfo memberInfo in FilterMembers(t))
            {
                foreach (IMemberValidator memberValidator in validators)
                {
                    memberValidator.TryValidate(memberInfo);
                }

                string memberName = nameSelector.Select(memberInfo);
                ITypeElement element = CreateTypeElement(memberName,
                                                         memberInfo);
                blueprint.AddElement(element);
            }
        }

        protected abstract IEnumerable<MemberInfo> FilterMembers(Type typeInfo);
        protected abstract ITypeElement CreateTypeElement(string name, MemberInfo memberInfo);
    }
}
