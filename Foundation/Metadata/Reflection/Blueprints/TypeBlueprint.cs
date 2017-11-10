using Swisstalk.Foundation.Metadata.Reflection.Elements;
using Swisstalk.Foundation.Utils;
using System;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints
{
    public class TypeBlueprint : ITypeBlueprint
    {
        private readonly Type _type;
        private readonly List<ITypeElement> _elements;

        public TypeBlueprint(Type t)
        {
            RaiseException.WhenTrue(null == t, "t cannot be null!");

            _type = t;
            _elements = new List<ITypeElement>();
        }

        public void AddElement(ITypeElement element)
        {
            RaiseException.WhenTrue(null == element, "element cannot be null!");

            _elements.Add(element);
        }

        public Type Type
        {
            get
            {
                return _type;
            }
        }

        public IEnumerable<ITypeElement> AllElements
        {
            get
            {
                return _elements;
            }
        }

        public ITypeElement this[string name]
        {
            get
            {
                ITypeElement element = FindByName(name);

                RaiseException.WhenTrue(null == element, "Field {0} cannot be found in type {1}!", name, _type.FullName);

                return element;
            }
        }

        private ITypeElement FindByName(string name)
        {
            return _elements.Find(element => element.Name == name);
        }
    }
}
