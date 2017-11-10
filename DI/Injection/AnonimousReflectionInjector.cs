using Swisstalk.Foundation.Metadata.Reflection.Blueprints;
using Swisstalk.Foundation.Metadata.Reflection.Elements;
using Swisstalk.Foundation.Utils;
using System;
using System.Linq;

namespace Swisstalk.DI.Injection
{
    public class AnonimousReflectionInjector : IDependencyInjector
    {
        private readonly object targetValue;

        public AnonimousReflectionInjector(object targetValue)
        {
            this.targetValue = targetValue;
        }

        public void InjectTo(object o)
        {
            ITypeBlueprint blueprint = DIBlueprintRepository.Get(o.GetType());

            ITypeElement typeElement = FindElementByType(blueprint, targetValue.GetType());

            RaiseException.WhenTrue(typeElement == null, "Blueprint '{0}' has no AutoInject field/property assignable from '{1}'!", blueprint.Type, targetValue.GetType());

            typeElement.SetValue(o, targetValue);
        }

        private ITypeElement FindElementByType(ITypeBlueprint blueprint, Type valueType)
        {
            return blueprint.AllElements.FirstOrDefault(te => te.ElementType.IsAssignableFrom(valueType));
        }
    }
}
