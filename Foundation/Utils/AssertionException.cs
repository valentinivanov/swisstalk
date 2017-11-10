using System;

namespace Swisstalk.Foundation.Utils
{
    public class AssertionException : Exception
    {
        public AssertionException(string template, params object[] args) : base(string.Format(template, args))
        {
        }
    }
}
