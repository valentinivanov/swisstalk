using System;
using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Elements
{
    public class PropertyTypeElement : ITypeElement
    {
        private readonly string name;
        private readonly PropertyInfo propertyInfo;

        public PropertyTypeElement(string name, PropertyInfo propertyInfo)
        {
            this.name = name;
            this.propertyInfo = propertyInfo;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Type ElementType
        {
            get
            {
                return propertyInfo.PropertyType;
            }
        }

        public object GetValue(object target)
        {
            return propertyInfo.GetValue(target, null);
        }

        public void SetValue(object target, object value)
        {
            propertyInfo.SetValue(target, value, null);
        }
    }
}
