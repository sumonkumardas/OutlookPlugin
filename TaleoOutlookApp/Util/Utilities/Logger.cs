using System;
using System.IO;

namespace Util.Utilities
{
    public static class Logger
    {
        #region Property
        private static string path = @"C:\Users\ASNazrul\Downloads\Taleo\log1.txt";
        public static int LogLabel = 0;
        public static string FilePath
        {
            get { return path; }
            set { path = value; }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Write Log Information with string array
        /// </summary>
        /// <param name="className">classname</param>
        /// <param name="functionName">method name</param>
        /// <param name="threadName">thread name</param>
        /// <param name="logInformations">string array of information</param>
        /// <returns>true if successfully written</returns>
        public static bool WriteLogInformation(string className, string functionName, string threadName, string[] logInformations)
        {
            try
            {
                foreach (var logInformation in logInformations)
                {
                    WriteLoggerinformationInFile("[" + DateTime.Now + "][" + className + "][" + functionName + "][" + threadName + "][" + logInformation + "]");
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// Write Log Information with string value
        /// </summary>
        /// <param name="className">classname</param>
        /// <param name="functionName">method name</param>
        /// <param name="threadName">thread name</param>
        /// <param name="logInformation">string value of information</param>
        /// <returns>true if successfully written</returns>
        public static bool WriteLogInformation(string className, string functionName, string threadName, string logInformation)
        {
            try
            {
                var newLogInformation = "[" + DateTime.Now + "][" + className + "][" + functionName + "][" + threadName + "][" +
                                       logInformation + "]";
                WriteLoggerinformationInFile(newLogInformation);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        /// <summary>
        /// Write Log Information with Exception
        /// </summary>
        /// <param name="className">classname</param>
        /// <param name="functionName">method name</param>
        /// <param name="threadName">thread name</param>
        /// <param name="e">exception</param>
        /// <returns>true if successfully written</returns>
        public static bool WriteLogInformation(string className, string functionName, string threadName, Exception e)
        {
            try
            {
                string newLogInformation = "[" + DateTime.Now + "][" + className + "][" + functionName + "][" + threadName + "][" +
                                       e + "]";
                WriteLoggerinformationInFile(newLogInformation);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        #endregion

        #region Miscellaneous
        private static void WriteLoggerinformationInFile(string information)
        {
            if (!File.Exists(path))
            {
                using (var streamWriter = File.CreateText(path))
                {
                    streamWriter.WriteLine(information);
                }
            }
            else
            {
                using (var streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(information);
                }
            }
        } 
        #endregion
    }
}