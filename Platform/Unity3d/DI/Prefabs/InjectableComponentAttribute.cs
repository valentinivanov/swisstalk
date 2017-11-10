using System;

namespace Swisstalk.Platform.Unity3d.DI.Prefabs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class InjectableComponentAttribute : Attribute
    {
    }
}
