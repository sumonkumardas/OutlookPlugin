using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Model.AuthenticateData;
using Model.Response;
using Service.Authentication;
using TaleoOutlookAddin.Forms.CustomeMessage;
using TaleoOutlookAddin.Forms.Login;
using TaleoOutlookAddin.Forms.RequestFeedback;
using Util.Enums;
using Util.Utilities;
using System.Reflection;
using System.Threading;
using Util.ApplicationGlobal;

namespace TaleoOutlookAddin.Forms.TaleoFormHelper
{


    /// <summary>
    /// Helping Methods for Taleo Forms
    /// </summary>
    public static class TaleoFormHelper
    {
        private static ResourceManager _resourceManager;
        private static CultureInfo _cultureInfo;
        static TaleoFormHelper()
        {
            ResourceInitialize();
        }

        private static void ResourceInitialize()
        {
            try
            {
                _resourceManager = new ResourceManager(TaleoMsg.ResourceLanguagePath, typeof(TaleoFormHelper).Assembly);
                _cultureInfo = CultureInfo.CreateSpecificCulture("en");
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name,
                    Thread.CurrentThread.Name, ex);
            }
        }
        /// <summary>
        /// To get application root path
        /// </summary>
        public static string RootPath
        {
            get
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                var idx = path.LastIndexOf("bin", StringComparison.Ordinal);
                path = path.Substring(0, idx);
                return path;
            }
        }

        #region Public Methods
        /// <summary>
        /// Authenticates login. If not logged in, shows login window and then check login if it fails, returns false.
        /// </summary>
        /// <returns>Is successfully logged in</returns>
        public static bool AuthenticateLogin()
        {
            Boolean autoLogIn = false;
            AuthenticationMessageForm authenticationMessageForm = new AuthenticationMessageForm();
            authenticationMessageForm.Show();
            Application.DoEvents();
            AuthenticationService authenticationUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, TaleoAddIn.USE_REST);
            SessionData sessionData = authenticationUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_AuthToken, false, true);
            if (!String.IsNullOrEmpty(TaleoAddIn.serviceURL))
                autoLogIn = sessionData.isLoggedIn;
            authenticationMessageForm.Close();
            if (autoLogIn)
            {
                return true;
            }
            else
            {
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
                DialogResult result = loginForm.DialogResult;
                return (result == DialogResult.OK);
            }
        }

        /// <summary>
        /// Encodes from string to Base64String
        /// </summary>
        /// <param name="plainText">Plain string</param>
        /// <returns>Base64String</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decodes from Base64String to string
        /// </summary>
        /// <param name="base64EncodedData">Base64String</param>
        /// <returns>Plain string</returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Encode all text of a file to Base64String
        /// </summary>
        /// <param name="filePath">name of file</param>
        /// <returns>Base64String</returns>
        public static string EncodeFileTextToBase64(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Get cookie string
        /// </summary>
        /// <returns></returns>
        public static string GetCookieString()
        {
            String cookieString = String.Empty;
            try
            {
                for (int i = 0; TaleoAddIn.sessionData.Cookies != null && i < TaleoAddIn.sessionData.Cookies.Count; i++)
                    cookieString = cookieString + TaleoAddIn.sessionData.Cookies[i].ToString() + ";";
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "Get Cookie String Failed");
                throw ex;
            }
            return cookieString;
        }

        /// <summary>
        /// This method get resource value by name
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static string GetResourceValueByName(string resourceName)
        {
            try
            {
                return _resourceManager.GetString(resourceName, _cultureInfo);
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                return "";
            }
        }
        public static string GetResourceValueByKey(string resourceKey)
        {
            try
            {
                ResourceManager _resourceManager1 = new ResourceManager("TalaeoLanguage", typeof(TaleoFormHelper).Assembly);
                //CultureInfo _cultureInfo1 = CultureInfo.CreateSpecificCulture(TaleoAddIn.sessionData.Language);
                return _resourceManager1.GetString(resourceKey);
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                return "";
            }
        }
        /// <summary>
        /// Gets the url for Selecting Candidate With Soap
        /// </summary>
        /// <param name="loginToken">Log In Token</param>
        /// <returns>SOAP Candidate Selection url</returns>
        public static string GetUrlToSelectCandidateSoap(string loginToken)
        {
            String currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("CONTACT_LOG_SELECT_CANDIDATE").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
            currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);

            return currentUrl;
        }
        /// <summary>
        /// Gets the url for Selecting Candidate With Rest
        /// </summary>
        /// <param name="loginToken">Log In Token</param>
        /// <returns>REST Candidate Selection url</returns>
        public static string GetUrlToSelectCandidateRest(string loginToken)
        {
            String currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/ats/calllog/main.jsp?org=" + TaleoAddIn.sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>");
            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);

            return currentUrl;
        }

        /// <summary>
        /// Gets the url to create candidate with soap
        /// </summary>
        /// <param name="loginToken">Log In Token</param>
        /// <param name="result">Soap Response Result</param>
        /// <returns>Create Candidate url</returns>
        public static string GetUrlToCreateCandidate(ParseResumeIntoCandidateResponse result)
        {
            String currentUrl = null;
            if (!TaleoAddIn.USE_REST)
            {
                currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("CREATE_CANDIDATE").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
                currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            }
            else
            {
                currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/ats/candidates/main.jsp?org=<COMPANY_CODE>&pword=<LOGIN_TOKEN>&id=<CANDIDATE_ID>&dup=<CANDIDATE_DUP>");
            }

            AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);

            TaleoAddIn.sessionData = authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_LogInToken, false, true);

            currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            currentUrl = currentUrl.Replace("<CANDIDATE_ID>", result.candidateId + "");
            currentUrl = currentUrl.Replace("<CANDIDATE_DUP>", result.dup + "");
            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", TaleoAddIn.sessionData.loginToken);

            return currentUrl;
        }



        public static string GetUrlToAddContact(int contactId,List<string> paramlist=null )
        {
            String currentUrl = null;

            currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("CREATE_CONTACT");
            if (String.IsNullOrEmpty(currentUrl))
                currentUrl = "<BASE_URL>/ats/outlook/ats/contacts/main.jsp?org=<COMPANY_CODE>&pword=<LOGIN_TOKEN>";

            if (!currentUrl.Contains("&email")&& paramlist !=null)
            {
                currentUrl += "&email=<EMAIL>&lastName=<LAST_NAME>&firstName=<FIRST_NAME>";
                currentUrl = currentUrl.Replace("<EMAIL>", paramlist[1]);
                if (!string.IsNullOrEmpty(paramlist[0]))
                {
                    currentUrl = currentUrl.Replace("<FIRST_NAME>", paramlist[0].Split(' ')[0]);
                    currentUrl = currentUrl.Replace("<LAST_NAME>", paramlist[0].Split(' ')[1]);
                }
                else
                {
                    currentUrl = currentUrl.Replace("<FIRST_NAME>", "");
                    currentUrl = currentUrl.Replace("<LAST_NAME>", "");
                }

            }
            AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);

            TaleoAddIn.sessionData = authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_LogInToken, false, true);

            try
            {
                currentUrl = currentUrl.Replace("<BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
                currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
                currentUrl = currentUrl.Replace("<CONTACT_ID>", contactId + "");
                //           currentUrl = currentUrl.Replace("<CANDIDATE_DUP>", result.dup + "");
                currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", TaleoAddIn.sessionData.loginToken);

            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
            }
            return currentUrl;
        }



        /// <summary>
        /// Gets Log Entry Confirmation Url
        /// </summary>
        /// <param name="loginToken">Log In Token</param>
        /// <returns>Confirmation Url</returns>
        /// 
        public static string GetUrlForLogConfirmation(string loginToken)
        {
            String currentUrl = null;
            if (!TaleoAddIn.USE_REST)
            {
                currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("CONTACT_LOG_CONFIRMATION").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
            }
            else
            {
                currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/",
                    "outlook/ats/calllog/confirmation.jsp?org=<COMPANY_CODE>&pword=<LOGIN_TOKEN>");
            }
            currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
            return currentUrl;
        }



        /// <summary>
        /// This function can update existing value of given key or add new key and value
        /// </summary>
        /// <param name="resourceKey">key</param>
        /// <param name="resourceValue"> value</param>
        /// <param name="resourcePath"> file path</param>
        /// <returns></returns>
        public static bool AddOrUpdateResourceFile(string resourceKey, String resourceValue, string resourcePath)
        {
            try
            {
                Hashtable resourceEntries = new Hashtable();
                ResXResourceReader reader = new ResXResourceReader(resourcePath);
                ResXResourceWriter resourceWriter = new ResXResourceWriter(resourcePath);

                if (reader != null)
                {
                    IDictionaryEnumerator id = reader.GetEnumerator();
                    foreach (DictionaryEntry d in reader)
                    {
                        string val = "";
                        if (d.Value == null)
                            resourceEntries.Add(d.Key.ToString(), "");
                        else
                        {
                            resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                            val = d.Value.ToString();
                        }
                        if (d.Key.ToString() == resourceKey)  // update
                            val = resourceValue;

                        //Write (with read to keep xml file order)
                        resourceWriter.AddResource(d.Key.ToString(), val);

                    }
                    reader.Close();
                }

                //Add new data (at the end of the file):
                if (!resourceEntries.ContainsKey(resourceKey))
                {
                    resourceWriter.AddResource(resourceKey, resourceValue);
                }
                resourceWriter.Generate();
                resourceWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                return false;
            }
        }
        /// <summary>
        /// Get application settings value of request
        /// </summary>
        /// <param name="key">key of application settings</param>
        /// <returns>value of specified key</returns>
        public static string GetApplicationSettingsValue(string key)
        {
            string className = "TaleoFormHelper";
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                Settings applicationSetting = new Settings(ApplicationGlobal.FinalPath + "ApplicationSettings.ini");
                String value = applicationSetting.getValue(key);
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return value;
            }
            catch (Exception e)
            {
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }
        }
        public static string GetApplicationCurrentVersion()
        {

            try
            {
                string major = GetApplicationSettingsValue("MAJOR");
                if (major == "") major = "0";
                string minor = GetApplicationSettingsValue("MINOR");
                if (minor == "") minor = "0";
                string revision = GetApplicationSettingsValue("REVISION");
                if (revision == "") revision = "0";
                string currentVersion = major + "." + minor + "." + revision;
                return currentVersion;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                throw ex;
            }
        }
        /// <summary>
        /// Get Register URL
        /// </summary>
        /// <returns></returns>
        public static RegistryKey GetRegistryKeyURL(string outlookVersion)
        {

            try
            {
                if (outlookVersion == "12")
                {
                    return Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\12.0\Outlook\Today\", true);
                }
                else if (outlookVersion == "14")
                {
                    return Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\14.0\Outlook\Today\", true);
                }
                else if (outlookVersion == "15")
                {
                    return Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\15.0\Outlook\Today\", true);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name,
                    Thread.CurrentThread.Name, ex.Message);
                throw ex;
            }
            return null;
        }
        /// <summary>
        /// Get candidate list URL
        /// </summary>
        /// <returns></returns>
        public static string GetCandidateListURL()
        {
            String currentUrl = null;
            if (!TaleoAddIn.USE_REST)
            {
                currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("CONTACT_LOG_SELECT_CANDIDATE").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
                currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            }
            else
            {
                currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/ats/calllog/main.jsp?org=" + TaleoAddIn.sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>");
            }

            AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
            TaleoAddIn.sessionData = authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_LogInToken, false, true);
            String loginToken = TaleoAddIn.sessionData.loginToken;

            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
            return currentUrl;
        }
        /// <summary>
        /// Get candidate list URL
        /// </summary>
        /// <returns></returns>
        public static string GetContactListURL()
        {
            String currentUrl = null;
            if (!TaleoAddIn.USE_REST)
            {
                currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("CONTACT_LOG");
                if (String.IsNullOrEmpty(currentUrl))
                {
                    currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("CONTACT_LOG_SELECT_CANDIDATE");
                    currentUrl
                        .Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")))
                        .Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode)
                        .Replace("<CONTACT_LOG_MODULE>", "Contacts")
                    ;
                }
                else
                {
                    currentUrl
                        .Replace("<BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")))
                        .Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode)
                        .Replace("<CONTACT_LOG_MODULE>", "Contacts")
                    ;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(currentUrl))
                {
                    currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("CONTACT_LOG_SELECT_CANDIDATE");
                    currentUrl = currentUrl
                        .Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")))
                        .Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
                        currentUrl = currentUrl + "&szModule=" + "Contacts";
                    ;
                }
                else
                {
                    currentUrl = currentUrl
                        .Replace("<BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")))
                        .Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode)
                        .Replace("<CONTACT_LOG_MODULE>", "Contacts")
                    ;
                }
 //               currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/ats/calllog/main.jsp?org=" + TaleoAddIn.sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>");
            }

            AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
            TaleoAddIn.sessionData = authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_LogInToken, false, true);
            String loginToken = TaleoAddIn.sessionData.loginToken;

            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
            Logger.WriteLogInformation("TaleoFormHelper", "GetContactListURL", Thread.CurrentThread.Name, "currentUrl=" + currentUrl);

            return currentUrl;
        }
        /// <summary>
        /// get .cio to stdole.IPictureDisp type image for toolbar and menubar icon
        /// </summary>
        /// <param name="menuIconIcon"></param>
        /// <returns>bitmap image</returns>
        public static stdole.IPictureDisp getImage(Icon menuIconIcon)
        {
            stdole.IPictureDisp tempImage = null;
            try
            {
                Icon newIcon = menuIconIcon;
                System.Windows.Forms.ImageList newImageList =
                    new System.Windows.Forms.ImageList();
                newImageList.Images.Add(newIcon);
                tempImage = ConvertImage.Convert(newImageList.Images[0]);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return tempImage;
        }
        /// <summary>
        /// Gets the MIME type using the extension of the file
        /// </summary>
        /// <param name="ext">extension</param>
        /// <returns>MIME type</returns>
        public static string GetMimeType(string ext)
        {
            string mimeType = "application/unknown";

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(
                ext.ToLower()
                );

            if (regKey != null)
            {
                object contentType = regKey.GetValue("Content Type");

                if (contentType != null)
                    mimeType = contentType.ToString();
            }

            return mimeType;
        }

        /// <summary>
        ///  check login info
        /// </summary>
        /// <returns></returns>
        public static bool IsAutoLoogedIn()
        {
            if (!String.IsNullOrEmpty(TaleoAddIn.serviceURL) && TaleoAddIn.sessionData.isLoggedIn)
                return true;
            else
                return TaleoAddIn.checkLogIn(TaleoAddIn.lastLogInData);
        }

        /// <summary>
        /// Gets the Authentication token for REST
        /// </summary>
        public static string GetAuthTokenForRest()
        {
            AuthenticationService authenticationService = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);

            try
            {
                if (!ApplicationGlobal.USE_REST)
                    TaleoAddIn.addInService.logOutSOAP(TaleoAddIn.serviceURL, TaleoAddIn.sessionData.authToken);
                else
                    TaleoAddIn.addInService.logout(TaleoAddIn.sessionData.authToken);
            }
            catch (Exception)
            {

            }

            TaleoAddIn.sessionData = authenticationService.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_AuthToken, false, false);

            return TaleoAddIn.sessionData.authToken;
        }
        /// <summary>
        /// Gets the User signeture from Signeture.ini file
        /// </summary>
        public static string GetSignature()
        {
            try
            {
                string signature = "";
                String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                String oraclePath = globalPath + "\\Oracle";
                String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
                String signatureSettingsFile = taleoPath + "\\Signeture.ini";
                if (File.Exists(signatureSettingsFile))
                    signature = File.ReadAllText(signatureSettingsFile);
                return signature;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
            return "";
        }
        #endregion

        public static string GetResourceValueByName2(string resourceName)
        {

            try
            {
//                string signature = "";
                String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                String oraclePath = globalPath + "\\Oracle";
                String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
                String filePath = taleoPath + "\\Language\\" + TaleoAddIn.languageFileName;
                if (File.Exists(filePath))
                {
                    //Create a new ResXResource reader and set the resx path to resxPathName
                    ResXResourceReader resReader = new ResXResourceReader(filePath);
                    //Enumerate the elements within the resx file and dispaly them

                    foreach (DictionaryEntry d in resReader)
                    {
                        if (d.Key.ToString() == resourceName)
                        {
                            resReader.Close();
                            return d.Value.ToString();
                        }
                        //MessageBox.Show(d.Key.ToString() + ": " + d.Value.ToString());
                    }

                    //Close the resxReader
                    resReader.Close();
                }
            }

       //If the resx file represents some incoherences

            catch (ArgumentException caught)
            {
                TaleoMessageBox.Show("Source: " + caught.Source + "Message: " + caught.Message);
                //  Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, caught.Message);
            }
            return null;
        }
        public static string GetValueFromSettingByKey(string key, string fileName)
        {
//            string signature = "";
            String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            String oraclePath = globalPath + "\\Oracle";
            String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
            String filePath = taleoPath + "\\" + fileName;
            try
            {
                if (!Directory.Exists(taleoPath))
                    Directory.CreateDirectory(taleoPath);
                if (!File.Exists(filePath))
                    File.Create(filePath);

                string[] lines = File.ReadAllLines(filePath);
                int linesLength = lines.Length;
                List<string[]> keyValueList = new List<string[]>();
                for (int i = 0; i < linesLength; i++)
                {
                    //Accepting only valid lines
                    lines[i] = lines[i].Trim();

                    if (String.IsNullOrEmpty(lines[i]))
                        continue;
                    string firstChar = lines[i].Substring(0, 1);
                    if (String.IsNullOrEmpty(lines[i])) continue;

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
                int l = keyValueList == null ? -1 : keyValueList.Count;
                for (int i = l - 1; i >= 0; i--)
                {
                    if (keyValueList[i][0] == key)
                        return keyValueList[i][1];
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static void SetValueFromSettingByKey(string key, string value, string fileName)
        {
//            string signature = "";
            String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            String oraclePath = globalPath + "\\Oracle";
            String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
            String userSettingsFile = taleoPath + "\\" + fileName;
            key = key.Trim();
            value = value.Trim();

            if (key.Contains("=") || key.Contains(" ") || key.Contains(";") || String.IsNullOrEmpty(key) || String.IsNullOrEmpty(value))
            {
                return;
            }
            bool hasEmptyLine = false;
            string[] lines = File.ReadAllLines(userSettingsFile);
            int linesLength = lines.Length;
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
                    File.WriteAllLines(userSettingsFile, lines);
                    return;
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
            File.WriteAllLines(userSettingsFile, lines);
            return ;
        }
    }
}
sealed public class ConvertImage : System.Windows.Forms.AxHost
{
    /// <summary>
    /// this class is used for convert ico type to bit type image to load toobar and menu bar icon outlook 2007
    /// </summary>
    private ConvertImage()
        : base(null)
    {
    }
    /// <summary>
    /// convert to stdole.IPictureDisp type to load toobar and menu bar icon outlook 2007
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public static stdole.IPictureDisp Convert
        (System.Drawing.Image image)
    {
        return (stdole.IPictureDisp)System.
            Windows.Forms.AxHost
            .GetIPictureDispFromPicture(image);
    }
}