/*
 * 2006 - 2016 Ted Spence, http://tedspence.com
 * License: http://www.apache.org/licenses/LICENSE-2.0 
 * Home page: https://github.com/tspence/csharp-csv-reader
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace CSVFile
{
    public class CSVReader : IEnumerable<string[]>, IDisposable
    {
        protected char _delimiter, _text_qualifier;

        protected StreamReader _instream;

        #region Public Variables
        /// <summary>
        /// If the first row in the file is a header row, this will be populated
        /// </summary>
        public string[] Headers = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a new CSV reader off a streamed source
        /// </summary>
        public CSVReader(StreamReader source, char delim = CSV.DEFAULT_DELIMITER, char qual = CSV.DEFAULT_QUALIFIER, bool first_row_are_headers = false)
        {
            _instream = source;
            _delimiter = delim;
            _text_qualifier = qual;
            if (first_row_are_headers)
            {
                Headers = NextLine();
            }
        }

        /// <summary>
        /// Construct a new CSV reader off a streamed source
        /// </summary>
        public CSVReader(Stream source, char delim = CSV.DEFAULT_DELIMITER, char qual = CSV.DEFAULT_QUALIFIER, bool first_row_are_headers = false)
        {
            _instream = new StreamReader(source);
            _delimiter = delim;
            _text_qualifier = qual;
            if (first_row_are_headers)
            {
                Headers = NextLine();
            }
        }

        #endregion

        #region Iterate through a CSV File
        /// <summary>
        /// Iterate through all lines in this CSV file
        /// </summary>
        /// <returns>An array of all data columns in the line</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Lines().GetEnumerator();
        }

        /// <summary>
        /// Iterate through all lines in this CSV file
        /// </summary>
        /// <returns>An array of all data columns in the line</returns>
        IEnumerator<string[]> System.Collections.Generic.IEnumerable<string[]>.GetEnumerator()
        {
            return Lines().GetEnumerator();
        }

        /// <summary>
        /// Iterate through all lines in this CSV file
        /// </summary>
        /// <returns>An array of all data columns in the line</returns>
        public IEnumerable<string[]> Lines()
        {
            while (true)
            {

                // Attempt to parse the line successfully
                string[] line = NextLine();

                // If we were unable to parse the line successfully, that's all the file has
                if (line == null) break;

                // We got something - give the caller an object
                yield return line;
            }
        }

        /// <summary>
        /// Retrieve the next line from the file.
        /// </summary>
        /// <returns>One line from the file.</returns>
        public string[] NextLine()
        {
            return CSV.ParseMultiLine(_instream, _delimiter, _text_qualifier);
        }
        #endregion

        #region Disposables
        /// <summary>
        /// Close our resources - specifically, the stream reader
        /// </summary>
        public void Dispose()
        {
            _instream.Dispose();
        }
        #endregion
    }
}
