using System;

namespace Swisstalk.ORM.Decoding
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DecodeAliasAttribute : Attribute
    {
        private readonly string name;

        public DecodeAliasAttribute(string name)
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
    }
}
