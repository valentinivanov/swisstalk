namespace Swisstalk.Foundation.Metadata
{
    public class IdentifierObject
    {
        private string _description;

        public IdentifierObject(string description)
        {
            _description = description;
        }

        public int Id
        {
            get
            {
                return GetHashCode();
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }
    }
}
