using System;
using UnityEditor;

namespace Swisstalk.Platform.Unity3d.DI.Prefabs
{
    public struct PrefabComponentKey
    {
        private readonly string prefabKey;
        private readonly Type componentType;
        private readonly int hashCode;

        public PrefabComponentKey(string prefabKey, Type componentType)
        {
            this.prefabKey = prefabKey;
            this.componentType = componentType;
            this.hashCode = string.Format("{0}__{1}", prefabKey, componentType.FullName).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is PrefabComponentKey)
            {
                PrefabComponentKey other = (PrefabComponentKey)obj;
                return prefabKey == other.prefabKey && componentType == other.componentType;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}

