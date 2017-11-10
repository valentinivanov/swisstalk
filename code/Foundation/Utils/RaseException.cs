namespace Swisstalk.Foundation.Utils
{
    public static class RaiseException
    {
        public static void WhenTrue(bool value, string message, params object[] messageArgs)
        {
            if (value)
            {
                throw new AssertionException(message, messageArgs);
            }
        }

        public static void WhenFalse(bool value, string message, params object[] messageArgs)
        {
            if (!value)
            {
                throw new AssertionException(message, messageArgs);
            }
        }
    }
}
