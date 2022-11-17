using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Util.Utilities
{
    public class Settings
    {
        #region Property
        private string FileName = @"";
        //private string _SettingsPath = @"C:\Users\Sumon\Downloads\Taleo\Settings.txt";
        public List<string[]> allSettings = null; 
        #endregion

        #region Constructor
        public Settings(string SettingsPath)
        {
            FileName = SettingsPath;
            allSettings = Get();
        }

        public Settings(string SettingsPath, String content)
        {
            FileName = SettingsPath;
            File.WriteAllText(SettingsPath, content);
            allSettings = Get();
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// Set the the value of a key in setting file. If the key does not exist, it is added.
        /// </summary>
        /// <param name="key">name of settings key</param>
        /// <param name="value">value of settings key</param>
        /// <returns>Is successfully set the new value of key</returns>
        public bool Set(string key, string value)
        {
            //Trimming Parameters
            key = key.Trim();
            value = value.Trim();

            //File and Key Validation
            if (!File.Exists(FileName))
            {
                var fs = File.Create(FileName);
                fs.Close();
            }
            if (key.Contains("=") || key.Contains(" ") || key.Contains(";") || String.IsNullOrEmpty(key) || String.IsNullOrEmpty(value))
            {
                Console.WriteLine("Setting doesn't accept key with ' ', ';', '=' or emty key, value.");
                return false;
            }

            bool hasEmptyLine = false;
            string[] lines = File.ReadAllLines(FileName);
            int linesLength = lines.Length;

            //Checking comments , empty line and searching key
            for (int i = 0; i < linesLength; i++)
            {
                lines[i] = lines[i].Trim();
                if (!String.IsNullOrEmpty(lines[i]))
                {
                    string firstChar = lines[i].Substring(0, 1);
                    if (firstChar == ";") continue;
                }
                else
                {
                    hasEmptyLine = true;
                    continue;
                }

                //Skip line without '='
                if (!lines[i].Contains("=")) continue;

                string[] lineContent = lines[i].Split('=');
                string lineKey = lineContent[0].Trim();

                //Setting Existing Value
                if (lineKey == key)
                {
                    lines[i] = key + "=" + value;
                    if (hasEmptyLine)
                    {
                        lines = lines.Where(val => !String.IsNullOrEmpty(val)).ToArray();
                    }
                    File.WriteAllLines(FileName, lines);
                    return true;
                }
            }

            //Setting New Key and Value
            Array.Resize(ref lines, ++linesLength);
            int lastIndex = linesLength - 1;
            lines[lastIndex] = key + "=" + value;
            if (hasEmptyLine)
            {
                lines = lines.Where(val => !String.IsNullOrEmpty(val)).ToArray();
            }

            //Writing in File
            File.WriteAllLines(FileName, lines);
            return true;
        }

        /// <summary>
        /// Reads and returns all the keys with their value.
        /// </summary>
        /// <returns>All keys with their values</returns>
        public List<string[]> Get()
        {
            if (!File.Exists(FileName))
            {
                var fs = File.Create(FileName);
                fs.Close();
            }

            string[] lines = File.ReadAllLines(FileName);
            int linesLength = lines.Length;

            List<string[]> keyValueList = new List<string[]>();

            for (int i = 0; i < linesLength; i++)
            {
                //Accepting only valid lines
                lines[i] = lines[i].Trim();

                if (String.IsNullOrEmpty(lines[i]))
                    continue;
                string firstChar = lines[i].Substring(0, 1);
                if (String.IsNullOrEmpty(lines[i]) || firstChar == ";") continue;

                //Getting key and value from line
                string[] lineContent = lines[i].Split('=');
                string lineKey = "";
                string lineValue = "";

                if (lineContent == null || lineContent.Length < 2)
                {
                    continue;
                }
                else if (lineContent.Length == 2)
                {
                    lineKey = lineContent[0];
                    lineValue = lineContent[1];
                }
                else
                {
                    lineKey = lineContent[0];
                    try
                    {
                        lineValue = lines[i].Substring(lines[i].IndexOf("=") + 1, lines[i].Length - lines[i].IndexOf("=") - 1);
                    }
                    catch (Exception)
                    {
                    }
                }

                //Setting key and value to List
                string[] keyValuePair = new string[2];
                keyValuePair[0] = lineKey.Trim();
                keyValuePair[1] = lineValue.Trim();
                keyValueList.Add(keyValuePair);
            }
            return keyValueList;
        }

        /// <summary>
        /// Return the value of a settings key. If it is not found, empty string is returned
        /// </summary>
        /// <param name="key">name of the key</param>
        /// <returns>value of the key</returns>
        public string getValue(string key)
        {
            int l = allSettings == null ? -1 : allSettings.Count;
            for (int i = l - 1; i >= 0; i--)
            {
                if (allSettings[i][0] == key)
                    return allSettings[i][1];
            }
            return "";
        } 
        #endregion
    }
}
