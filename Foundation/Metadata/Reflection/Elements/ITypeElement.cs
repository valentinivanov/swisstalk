using System;

namespace Swisstalk.Foundation.Metadata.Reflection.Elements
{
    public interface ITypeElement
    {
        string Name
        {
            get;
        }

        Type ElementType
        {
            get;
        }

        object GetValue(object target);
        void SetValue(object target, object value);
    }
}
