using Swisstalk.DI;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Swisstalk.Platform.Unity3d.DI.Prefabs
{
    public class PrefabDIFactory : DIFactory<string, GameObject>
    {
        private struct InstanceSelectionContext
        {
            public readonly Component injectee;
            public readonly Type injecteeType;
            public readonly PrefabComponentKey componentKey;

            public InstanceSelectionContext(Component injectee,
                                            Type injecteeType,
                                            PrefabComponentKey componentKey)
            {
                this.injectee = injectee;
                this.injecteeType = injecteeType;
                this.componentKey = componentKey;
            }
        }
        
        private readonly Dictionary<PrefabComponentKey, DICollection> injectionConfiguration;
        private readonly Dictionary<string, GameObject> prefabs;

        public PrefabDIFactory(Dictionary<PrefabComponentKey, DICollection> injectionConfiguration, 
                                           Dictionary<string, GameObject> prefabs)
        {
            this.injectionConfiguration = injectionConfiguration;
            this.prefabs = prefabs;
        }

        protected override IEnumerable<InstanceDependencyMapping> MapInstance(string key, object instance)
        {
            GameObject gameObject = (GameObject)instance;

            return gameObject.GetComponentsInChildren<Component>(true)
                             .Where(component => component != null)
                             .Select(component => new InstanceSelectionContext(component, component.GetType(), new PrefabComponentKey(key, component.GetType())))
                             .Where(context => IsInjectable(context) && HasInjectionConfigured(context))
                             .Select(context => new InstanceDependencyMapping(context.injectee, GetInjectorsFor(context)));
        }

        protected override GameObject NewInstance(string key)
        {
            GameObject prefab = prefabs[key]; //allow Dictionary to throw an exception if no prefab configured
            return GameObject.Instantiate(prefab);
        }

        private bool IsInjectable(InstanceSelectionContext context)
        {
            //TODO: add type cache
            return context.injecteeType.IsDefined(typeof(InjectableComponentAttribute), true);
        }

        private bool HasInjectionConfigured(InstanceSelectionContext context)
        {
            return injectionConfiguration.ContainsKey(context.componentKey) &&
                   injectionConfiguration[context.componentKey].Count > 0;
        }

        private DICollection GetInjectorsFor(InstanceSelectionContext context)
        {
            return injectionConfiguration[context.componentKey];
        }
    }
}
