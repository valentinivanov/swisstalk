using Swisstalk.Foundation.Metadata.Reflection.Blueprints;
using Swisstalk.Foundation.Metadata.Reflection.Elements;
using Swisstalk.Foundation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swisstalk.DI.Injection
{
    public class NamedReflectionInjector : IDependencyInjector
    {
        private readonly string name;
        private readonly object targetValue;

        public NamedReflectionInjector(string name, object targetValue)
        {
            this.name = name;
            this.targetValue = targetValue;
        }

        public void InjectTo(object o)
        {
            ITypeBlueprint blueprint = DIBlueprintRepository.Get(o.GetType());

            ITypeElement typeElement = FindElementByName(blueprint);

            RaiseException.WhenTrue(typeElement == null, "Blueprint '{0}' has no AutoInject field/property with name '{1}'!", blueprint.Type, name);
            RaiseException.WhenFalse(targetValue == null || typeElement.ElementType.IsAssignableFrom(targetValue.GetType()), "Element '{0}' in blueprint '{1}' is not assignable from {2}", name, blueprint.Type, targetValue.GetType());

            typeElement.SetValue(o, targetValue);
        }

        private ITypeElement FindElementByName(ITypeBlueprint blueprint)
        {
            return blueprint.AllElements.FirstOrDefault(te => te.Name == name);
        }
    }
}
