using Swisstalk.DI;
using System.Collections.Generic;
using UnityEngine;

namespace Swisstalk.Platform.Unity3d.DI.Prefabs
{
    /// <summary>
    /// See example:
    /// prefabsFactoryBuilder.ForComponentInPrefab<InjectableBehavior>(nameMapping.CurrentLevel)
    ///            .InjectAnonimous(gameplayInputMethod)
    ///            .InjectNamed("InputExecutor", gameplayExecutors.Input)
    ///            .InjectNamed("LogicExecutor", gameplayExecutors.Logic);
    ///
    /// </summary>
    public class PrefabDIFactoryBuilder : DIFactoryBuilder<PrefabComponentKey, PrefabDIFactory>
    {
        private readonly Dictionary<string, GameObject> prefabs;
 
        public PrefabDIFactoryBuilder()
        {
            prefabs = new Dictionary<string, GameObject>();
        }

        public PrefabDIFactoryBuilder WithPrefab(string prefabName, GameObject prefab)
        {
            prefabs.Add(prefabName, prefab);
            return this;
        }

        protected override PrefabDIFactory NewFactory(Dictionary<PrefabComponentKey, DICollection> factoryConfiguration)
        {
            return new PrefabDIFactory(factoryConfiguration, prefabs);
        }
    }
}
