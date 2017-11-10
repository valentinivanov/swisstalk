using System;
using Swisstalk.DI;

namespace Swisstalk.Platform.Unity3d.DI.Prefabs
{
    public static class PrefabDIFactoryBuilderExtensions
    {
        public static DICollectionBuilder ForComponentInPrefab<T>(this PrefabDIFactoryBuilder builder, string prefabKey)
        {
            return builder.ForKey(new PrefabComponentKey(prefabKey, typeof(T)));
        }
    }
}

