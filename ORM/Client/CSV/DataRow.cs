using Swisstalk.Foundation.Utils;

namespace Swisstalk.ORM.Client.CSV
{
    public class DataRow : IDataRow
    {
        private readonly string[] fragments;

        public DataRow(string[] fragments)
        {
            RaiseException.WhenTrue(fragments == null, "fragments must not be null!");
            RaiseException.WhenTrue(fragments.Length == 0, "fragments must not be empty!");

            this.fragments = fragments;
        }

        public int FieldCount
        {
            get
            {
                return fragments.Length;
            }
        }

        public object FetchField(int fragmentIndex)
        {
            return fragments[fragmentIndex];
        }

        //TODO: move this code to parser
        /*private void PatchFragmentsAccordingToFormat()
        {
            for (int i = 0; i < fragments.Length; ++i)
            {
                if (formats[i] == FieldFormat.Numeric)
                {
                    fragments[i] = fragments[i].Replace("\"", string.Empty).Replace(',', '.');
                }
            }
        }
        */
    }
}
