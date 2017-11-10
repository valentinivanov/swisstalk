using System;

namespace Swisstalk.ORM.Decoding
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    class DecodeableAttribute : Attribute
    {
    }
}
