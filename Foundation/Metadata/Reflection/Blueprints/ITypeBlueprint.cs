using Swisstalk.Foundation.Metadata.Reflection.Elements;
using System;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Metadata.Reflection.Blueprints
{
    public interface ITypeBlueprint
    {
        Type Type
        {
            get;
        }

        IEnumerable<ITypeElement> AllElements
        {
            get;
        }

        ITypeElement this[string name]
        {
            get;
        }
    }
}