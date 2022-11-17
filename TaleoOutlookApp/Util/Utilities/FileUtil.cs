using System;
using System.IO;
using System.Text;

namespace Util.Utilities
{
    public class FileUtil
    {
        /// <summary>
        /// change a file encoding system
        /// </summary>
        /// <param name="inputfilePath">input file path</param>
        /// <param name="outputfilePath">output file path</param>
        /// <param name="outputEncoding">Encoding system</param>
        #region Public Methods
        public void ChangeFileEncoding(string inputfilePath, string outputfilePath, Encoding outputEncoding)
        {
            //Check if it is valid path
            if (!File.Exists(inputfilePath))
            {
                Console.WriteLine("Invalid File Path.");
                return;
            }

            // Read the BOM & Find the input Encoding
            Encoding inputEncoding;
            var bom = new byte[4];
            using (var file = new FileStream(inputfilePath, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) inputEncoding = Encoding.UTF7;
            else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) inputEncoding = Encoding.UTF8;
            else if (bom[0] == 0xff && bom[1] == 0xfe) inputEncoding = Encoding.Unicode; //UTF-16LE
            else if (bom[0] == 0xfe && bom[1] == 0xff) inputEncoding = Encoding.BigEndianUnicode; //UTF-16BE
            else if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) inputEncoding = Encoding.UTF32;
            else inputEncoding = Encoding.ASCII;

            //Find the output & Write in the outputfilePath
            string input = File.ReadAllText(inputfilePath);
            string output = outputEncoding.GetString(Encoding.Convert(inputEncoding, outputEncoding, inputEncoding.GetBytes(input)));

            if (String.IsNullOrEmpty(outputfilePath))
            {
                using (var writer = new StreamWriter(inputfilePath, false, outputEncoding))
                {
                    writer.Write(output);
                }
            }
            else
            {
                if (File.Exists(outputfilePath))
                {
                    Console.WriteLine("Invalid File Path.");
                    return;
                }
                using (var writer = new StreamWriter(outputfilePath, false, outputEncoding))
                {
                    writer.Write(output);
                }
            }
        } 
        #endregion
    }
}
