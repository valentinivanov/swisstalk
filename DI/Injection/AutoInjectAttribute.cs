using System;

namespace Swisstalk.DI.Injection
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AutoInjectAttribute : Attribute
    {
        private readonly string name;

        public AutoInjectAttribute()
        {
            name = null;
        }

        public AutoInjectAttribute(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public bool IsAnonimous
        {
            get
            {
                return name == null || name.Length == 0;
            }
        }
    }
}
