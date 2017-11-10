using System;
using System.Reflection;

namespace Swisstalk.Foundation.Metadata.Reflection.Elements
{
    public class FieldTypeElement : ITypeElement
    {
        private readonly string name;
        private readonly FieldInfo fieldInfo;

        public FieldTypeElement(string name, FieldInfo fieldInfo)
        {
            this.name = name;
            this.fieldInfo = fieldInfo;
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
                return fieldInfo.FieldType;
            }
        }

        public object GetValue(object target)
        {
            return fieldInfo.GetValue(target);
        }

        public void SetValue(object target, object value)
        {
            fieldInfo.SetValue(target, value);
        }
    }
}
