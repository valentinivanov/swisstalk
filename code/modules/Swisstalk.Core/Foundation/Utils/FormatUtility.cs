namespace Swisstalk.Foundation.Utils
{
    public class FormatUtility
    {
        public static string SafeToString(object obj)
        {
            return (obj != null) ? obj.ToString() : "(null)";
        }
    }
}
