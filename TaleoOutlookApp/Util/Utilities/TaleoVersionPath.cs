using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Win32;

namespace Util.Utilities
{
    public class TaleoVersionPath
    {
        #region Property
        public string taleoVersion { get; set; }
        public string osVersion { get; set; }
        public string outlookVersion { get; set; }
        public string locale { get; set; } 
        #endregion

        #region Constructor
        public TaleoVersionPath()
        {
            osVersion = Environment.OSVersion.ToString();
            outlookVersion = GetOutLookVersion();
            locale = CultureInfo.CurrentCulture.Name;
            taleoVersion = typeof(TaleoVersionPath).Assembly.GetName().Version.ToString();
        } 
        #endregion

        #region Private Methods
        private string GetOutLookPath()
        {
            const string regKey = @"Software\Microsoft\Windows\CurrentVersion\App Paths";
            string toReturn = string.Empty;
            string key = "outlook.exe";

            //looks inside CURRENT_USER:
            RegistryKey mainKey = Registry.CurrentUser;
            try
            {
                mainKey = mainKey.OpenSubKey(regKey + "\\" + key, false);
                if (mainKey != null)
                {
                    toReturn = mainKey.GetValue(string.Empty).ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //if not found, looks inside LOCAL_MACHINE:
            mainKey = Registry.LocalMachine;
            if (string.IsNullOrEmpty(toReturn))
            {
                try
                {
                    mainKey = mainKey.OpenSubKey(regKey + "\\" + key, false);
                    if (mainKey != null)
                    {
                        toReturn = mainKey.GetValue(string.Empty).ToString();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //closing the handle:
            if (mainKey != null)
                mainKey.Close();

            return toReturn;
        }

        private string GetOutLookVersion()
        {
            string toReturn = "";
            string path = GetOutLookPath();

            if (File.Exists(path))
            {
                try
                {
                    FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(path);
                    toReturn = fileVersion.ProductVersion;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return toReturn;
        } 
        #endregion
    }
}
