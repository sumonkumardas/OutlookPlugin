using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Model.AuthenticateData;
using Model.Response;
using Service.AddIn;
using Service.Authentication;
using Service.HTTP;
using TaleoOutlookAddin.Forms.CustomeMessage;
using TaleoOutlookAddin.Forms.Login;
using TaleoOutlookAddin.Forms.TaleoFormHelper;
using TaleoOutlookAddin.TaleoWebControl.Common;
using Util.ApplicationGlobal;
using Util.Enums;
using Util.Utilities;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Reflection;
using System.Net;
using System.IO;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Win32;
using Exception = System.Exception;

namespace TaleoOutlookAddin
{
    public partial class TaleoAddIn
    {
        #region Property

//        public static String authToken = "";
        public static String loginToken = "";
        public static String jSessionId = "";
        public static String languageFileName = "";
        public static AddInServices addInService;
        public static LogInData lastLogInData = null;
        public static SessionData sessionData = null;
        public static string outLookVersion = null;
        public static String serviceURL = "";
        private static TaleoAddIn taleoAddIn = null;

        private static Microsoft.Office.Interop.Outlook.Application app = null;

        public static EnableServiceResponse OrgSettingsResponse { get; set; }

        public static bool USE_REST;
        //public static bool USE_REST { get; set; }
        public bool LogEmailClicked { get; set; }
        public bool LogEmailSent { get; set; }
        public Office.IRibbonUI AddinRibbon { get; set; }
        public static Ribbon ARibbon { get; set; }

        public static Microsoft.Office.Tools.CustomTaskPane myViewTaskPane;
        private TaleoButtonAction taleoButtonAction = null;

        /// <summary>
        /// TO enable disable filefeedback button
        /// </summary>
        private Explorer filefeedBackExplorer = null;
        #endregion

        #region property for outlook 2007

        private Office.CommandBar menuBar = null;
        private Office.CommandBarPopup newTaleoMenuBar;
        private Office.CommandBarButton btnTaleo;
        private Office.CommandBarButton btnLogin;
        private Office.CommandBarButton btnDashBoard;
        private Office.CommandBarButton btnRequestFeedBack;
        private Office.CommandBarButton btnFileFeedBack;
        private Office.CommandBarButton btnAddToTaleo;
        private Office.CommandBarButton btnAddComments;
        private Office.CommandBarButton btnPreference;
        private Office.CommandBarButton btnCheckForUpdates;
        private Office.CommandBarButton btnAbout;
        private string menuTag = "Taleo";

        Office.CommandBar taleoToolBar;
        private Office.CommandBarButton btnTaleoToolBar;
        private Office.CommandBarButton btnLoginToolBar;
        private Office.CommandBarButton btnDashBoardToolBar;
        private Office.CommandBarButton btnRequestFeedBackToolBar;
        private Office.CommandBarButton btnFileFeedBackToolBar;
        private Office.CommandBarButton btnAddToTaleoToolBar;
        private Office.CommandBarButton btnAddCommentsToolBar;
        private Office.CommandBarButton btnPreferenceToolBar;
        private Office.CommandBarButton btnCheckForUpdatesToolBar;
        private Office.CommandBarButton btnAboutToolBar;
        #endregion
        private void TaleoAddIn_Startup(object sender, System.EventArgs e)
        {
            USE_REST = SetCommonSettings.IsUseREST();
            ApplicationGlobal.USE_REST = USE_REST;
            ApplicationGlobal.QA_Environment = SetCommonSettings.IsUseQAEnvironment();

            languageFileName = TaleoFormHelper.GetValueFromSettingByKey("LanguageFileName", "ApplicationSettings.ini");
            if (!String.IsNullOrEmpty(TaleoFormHelper.GetValueFromSettingByKey("LogLabel", "ApplicationSettings.ini")))
                Logger.LogLabel = Convert.ToInt16(TaleoFormHelper.GetValueFromSettingByKey("LogLabel", "ApplicationSettings.ini"));
            //if (string.IsNullOrEmpty(languageFileName))
            //{
            //    languageFileName = "Taleo.en-1.resx";
            //}
            TaleoAddIn.outLookVersion = Globals.TaleoAddIn.Application.Version.Split(new char[] { '.' })[0];
            TaleoButtonAction.RegistryKeyURL = TaleoFormHelper.GetRegistryKeyURL(TaleoAddIn.outLookVersion);
            if (TaleoAddIn.outLookVersion == "12")
            {
                taleoButtonAction = new TaleoButtonAction();
                ApplicationGlobal.isOutlookVersion2007 = true;
                RemoveTaleoMenubar();
                AddTaleoMenuBar();
                AddTaleoToolbar();
            }
            addInService = AddInServices.getInstance();
            addInService.SetLoggerPath(ApplicationGlobal.FinalPath + "taleo.log");
            addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 1");
            Application.ItemSend += new ApplicationEvents_11_ItemSendEventHandler(SendingLogEmailAction);
           // Application.ActiveExplorer().SelectionChange += new ExplorerEvents_10_SelectionChangeEventHandler(SelectionChange);
            filefeedBackExplorer = Globals.TaleoAddIn.Application.ActiveExplorer();
            filefeedBackExplorer.SelectionChange += SelectionChange;
            try
            {
                addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 2");
                app = Application;
                Microsoft.Office.Interop.Outlook.NameSpace nSpace = app.GetNamespace("MAPI");
                MAPIFolder mapiFolder = nSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);
                mapiFolder.Folders.FolderChange += new Microsoft.Office.Interop.Outlook.FoldersEvents_FolderChangeEventHandler(Folders_FolderChange);
                addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 3");
            }
            catch (System.Exception)
            {
                addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 4");
            }
            //			mapiFolder.Folders.FolderChange += new Microsoft.Office.Interop.Outlook.FoldersEvents_FolderChangeEventHandler(Folders_FolderChange);

            taleoAddIn = this;
            try
            {
                LoadTaleoStarterInfo();
                //Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, "Load Basic Stater Info",null);
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, "MainThread", ex);
            }

            try
            {
                String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
                String oraclePath = globalPath + "\\Oracle";
                String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
                String dummyHTMLFile = taleoPath + "\\colt.htm";
                RegistryUtilities.SetValue(TaleoButtonAction.RegistryKeyURL, "URLx", dummyHTMLFile);

                //Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, "Set up registry and setting file's path.", null);
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, "MainThread", ex);
            }
        }


        /// <summary>
        ///  Active explorer selection change event handler function
        /// </summary>
        public void SelectionChange()
        {
            try
            {
                var ol = new Outlook.Application();
                Selection selections = ol.ActiveExplorer().Selection;
                if (selections.Count <= 0)
                {
                    if (ARibbon != null)
                        ARibbon.fileFeedbackButton.Enabled = false;
                        ARibbon.addToTaleoButton.Enabled = false;
                    if (outLookVersion == "12")
                    {
                        btnFileFeedBack.Enabled = false;
                        btnFileFeedBackToolBar.Enabled = false;
                        btnAddToTaleo.Enabled = false;
                        btnAddCommentsToolBar.Enabled = false;
                    }
                }
                else
                {
                    if (ARibbon != null)
                        ARibbon.fileFeedbackButton.Enabled = true;
                        ARibbon.addToTaleoButton.Enabled = true;
                    if (outLookVersion == "12")
                    {
                        btnFileFeedBack.Enabled = true;
                        btnFileFeedBackToolBar.Enabled = true;
                        btnAddToTaleo.Enabled = true;
                        btnAddCommentsToolBar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ARibbon != null)
                    ARibbon.fileFeedbackButton.Enabled = false;
                Logger.WriteLogInformation("TaleoAddIn", MethodBase.GetCurrentMethod().Name, "MainThread", ex);
            }

        }
        /// <summary>
        /// To initaialize taleo addin
        /// </summary>
        public static void LoadTaleoStarterInfo()
        {
            addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 5");
            init();
            addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 6");
            Boolean autoLogIn = false;
            if (!String.IsNullOrEmpty(serviceURL))
            {
                addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 7");
                AuthenticationService authUtil = new AuthenticationService(serviceURL, ApplicationGlobal.USE_REST);
                sessionData = new SessionData(lastLogInData, string.Empty, string.Empty, string.Empty, new List<Cookie>(), false, false, AccessDepth.Upto_Cookies);
                sessionData = authUtil.checkAuthentication(sessionData, AccessDepth.Only_AuthToken, false, false);
                //Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, "Populate Session Data. New Auth-token is "+sessionData.authToken, null);
//                authToken = sessionData.authToken;
                //authUtil.checkAuthenticationNavigate(
                autoLogIn = TaleoFormHelper.IsAutoLoogedIn();
                sessionData.Language = "en";
                addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 8");
                OrgSettingsResponse = TaleoAddIn.addInService.GetEnableServiceResponse_REST(TaleoFormHelper.GetAuthTokenForRest());
                //Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, "Setup Organization settings.", null);
            }
            if (String.IsNullOrEmpty(serviceURL)
                    || String.IsNullOrEmpty(sessionData.authToken)
                )
            {
                sessionData = new SessionData(lastLogInData, string.Empty, string.Empty, string.Empty, new List<Cookie>(), false, false, AccessDepth.Upto_Cookies);
                addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 9");
            }
            else
            {
                addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 10");
//               autoLoginWebViewActivity(autoLogIn);
//               postLogInActivity(true);
                addInService.WriteLoggerInformation("TaleoAddIn", "startup", "main", "step 11");
            }
        }

        private void Folders_FolderChange(MAPIFolder Folder)
        {

        }
        public static TaleoAddIn getInstance()
        {
            return taleoAddIn;
        }
        public static void autoLoginWebViewActivity(Boolean autoLogIn)
        {
        }
        /// <summary>
        /// To store login token and also used auto logged out if service url null.
        /// </summary>
        /// <param name="autoLogOut"></param>
        public static void postLogInActivity(bool autoLogOut)
        {
            LoginTokenResponse loginTokenResponse = addInService.logInTokenSOAP(serviceURL, sessionData.authToken);

            if (loginTokenResponse.successResult)
            {
                loginToken = loginTokenResponse.response.loginToken;
            }
            jSessionId = addInService.getJSessionResponse(serviceURL, loginToken, lastLogInData.companyCode).jSessionID;
            if (autoLogOut)
            {
                LogoutResponse los = addInService.logOutSOAP(serviceURL, sessionData.authToken);
                if (los.successResult)
                {
                }

            }
        }
        /// <summary>
        /// Basic initialization of taleo outlook addin
        /// </summary>
        public static void init()
        {
            //Directory check
            if (!Directory.Exists(ApplicationGlobal.FinalPath))
            {
                string userNameLine = "username=";
                string passwordLine = "password=";
                string companyCodeLine = "companyCode=";
                string checkBoxLine = "remember=";

                //Save these code and continue
                String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
                String oraclePath = globalPath + "\\Oracle";
                String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
                String userSettingsFile = taleoPath + "\\userSettings.ini";

                if (!Directory.Exists(oraclePath))
                {
                    Directory.CreateDirectory(oraclePath);
                }

                if (!Directory.Exists(taleoPath))
                {
                    Directory.CreateDirectory(taleoPath);
                }

                File.WriteAllText(userSettingsFile, userNameLine + "\n" + passwordLine + "\n" + companyCodeLine + "\n" + checkBoxLine + "\n");
            }

            addInService = AddInServices.getInstance();
            addInService.SetCurrentApplicationSettings(ApplicationGlobal.FinalPath + "ApplicationSettings.ini");
            //Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, "Setup application variable.", null);

            addInService.SetCurrentUserSettings(ApplicationGlobal.FinalPath + "UserSettings.ini");
            addInService.SetCurrentApplicationSettings(ApplicationGlobal.FinalPath + "ApplicationSettings.ini");
            addInService.getSetTaleoSettings(null);

            //addInService.SetCurrentUserSettings(ApplicationGlobal.FinalPath + "UserSettings.ini");
            //addInService.SetCurrentApplicationSettings(ApplicationGlobal.FinalPath + "ApplicationSettings.ini");
            //addInService.SetCurrentApplicationSettings(ApplicationGlobal.FinalPath + "Signeture.ini");
            if (!File.Exists(ApplicationGlobal.FinalPath + "Signeture.ini"))
            {
                File.Create(ApplicationGlobal.FinalPath + "Signeture.ini").Dispose();
            }
            HttpBaseUrl.SetDispatcherForREST();
            

            lastLogInData = getLastLogInData();

            serviceURL = addInService.getServiceURL(lastLogInData.companyCode);

            addInService.SetLoggerPath(ApplicationGlobal.FinalPath + "taleo.log");
        }
        /// <summary>
        /// To get last logged in data
        /// </summary>
        /// <returns></returns>
        public static LogInData getLastLogInData()
        {
            LogInData lastLogInData = new LogInData();

            List<string[]> lastLogInDataFromFile = addInService.GetUserSettingsKeyAndValue();
            
            string timeStamp = "";

            if (lastLogInDataFromFile != null && lastLogInDataFromFile.Count != 0)
            {
                foreach (var data in lastLogInDataFromFile)
                {
                    if (data[0] == "username")
                    {
                        lastLogInData.userName = data[1];
                    }

                    if (data[0] == "password")
                    {
                        lastLogInData.password = data[1];
                    }

                    if (data[0].ToLower() == "companyCode".ToLower())
                    {
                        lastLogInData.companyCode = data[1];
                    }

                    if (data[0] == "remember")
                    {
                        lastLogInData.remember = data[1];
                    }

                    if (data[0] == "TimeStamp")
                    {
                        timeStamp = data[1];
                    }
                }
                EncryptDecrypt encryptDecrypt = new EncryptDecrypt(ApplicationGlobal.EncryptionKey + timeStamp);
                lastLogInData.userName = encryptDecrypt.Decrypt(lastLogInData.userName);
                lastLogInData.password = encryptDecrypt.Decrypt(lastLogInData.password);
                lastLogInData.companyCode = encryptDecrypt.Decrypt(lastLogInData.companyCode).ToUpper();

            }
            else
            {
                lastLogInData.userName = "";
                lastLogInData.password = "";
                lastLogInData.companyCode = "";
            }
            return lastLogInData;
        }
        /// <summary>
        /// To check is user logged in or not
        /// </summary>
        /// <param name="lastLogInData">Nee last last logged in info</param>
        public static bool checkLogIn(LogInData lastLogInData)
        {
            //string serviceURL = addInService.getServiceURL(logInDataObj.companyCode);
            if (String.IsNullOrEmpty(serviceURL) && !String.IsNullOrEmpty(lastLogInData.companyCode))
                serviceURL = addInService.getServiceURL(lastLogInData.companyCode);
            addInService.setBaseURL(serviceURL);
            LoginResponse loginResponse = null;
            //if (USE_REST)
            //    loginResponse = addInService.LogInREST(lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
            //else
            //    loginResponse = addInService.logInSOAP(serviceURL, lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
            //if (loginResponse.successResult)
            //    authToken = loginResponse.response.authToken;
            //else
            //    authToken = "";

            loginResponse = logInServiceCaller(lastLogInData, null);
            if (loginResponse == null || !loginResponse.successResult)
            {
                return false;
            }
            return (!String.IsNullOrEmpty(loginResponse.response.authToken));
        }

        private static LoginResponse logInServiceCaller(LogInData lastLogInData, LoginResponse loginResponse)
        {
            if (sessionData.authToken == "" && USE_REST)
            {
                addInService.setBaseURL(serviceURL);
                addInService.logout(sessionData.authToken);
                loginResponse = addInService.LogInREST(lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
                sessionData.authToken = loginResponse.successResult ? loginResponse.response.authToken : "";
            }

            if (sessionData.authToken == "" && (serviceURL.Contains("servlet/rpcrouter") || serviceURL.Contains("services/rpcrouter")) && !USE_REST)
            {
                addInService.setBaseURL(serviceURL);
                addInService.logOutSOAP(serviceURL, sessionData.authToken);
                loginResponse = addInService.logInSOAP(serviceURL, lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
                sessionData.authToken = loginResponse.successResult ? loginResponse.response.authToken : "";

            }
            if (sessionData.authToken == "" && serviceURL.Contains("api/v1") && !USE_REST)
            {
                serviceURL = serviceURL.Replace("api/v1", "services/rpcrouter");

                if (serviceURL.EndsWith("/"))
                    serviceURL = serviceURL.Substring(0, serviceURL.Length - 1);

                addInService.setBaseURL(serviceURL);
                loginResponse = addInService.logInSOAP(serviceURL, lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
                sessionData.authToken = loginResponse.successResult ? loginResponse.response.authToken : "";

            }
            return loginResponse;
        }

        //public static bool checkLogIn(LogInData lastLogInData, String serviceUrl)
        //{
        //    serviceURL = serviceUrl;
        //    addInService.setBaseURL(serviceUrl);
        //    LoginResponse loginResponse = null;
        //    if (USE_REST)
        //        loginResponse = addInService.LogInREST(lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
        //    else
        //        loginResponse = addInService.logInSOAP(serviceURL, lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
        //    if (loginResponse.successResult)
        //        authToken = loginResponse.response.authToken;
        //    else
        //        authToken = "";

        //    loginResponse = logInServiceCaller(lastLogInData, loginResponse);
        //    return (!String.IsNullOrEmpty(authToken));
        //}

        private void TaleoAddIn_Shutdown(object sender, System.EventArgs e)
        {
            if (!USE_REST)
                addInService.logOutSOAP(serviceURL, sessionData.authToken);
            else
                addInService.logout(sessionData.authToken);
        }

        #region VSTO generated code

        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(TaleoAddIn_Startup);
            this.Shutdown += new System.EventHandler(TaleoAddIn_Shutdown);
        }

        #endregion
        /// <summary>
        /// To show taleo, dashboard or my view page.
        /// </summary>
        /// <param name="controlIndex">Decide which url need to execute</param>
        public void showLink(int controlIndex)
        {
            try
            {
                switch (controlIndex)
                {
                    case PagePaneIndex.RESOURCE_CENTER:
                        showCustomOutlookToday(getResourceCenterURL());
                        break;
                    case PagePaneIndex.MY_VIEW:
                        showMyViewInNewTab();
                        break;
                    case PagePaneIndex.DASHBOARD:
                        showCustomOutlookToday(getMyViewURLOutlook());
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name,MethodBase.GetCurrentMethod().Name,Thread.CurrentThread.Name,ex.Message);
                TaleoMessageBox.Show(TaleoMsg.TaleoUnreachableMessage);
            }
            //			File.Copy(ApplicationGlobal.FinalPath + ApplicationGlobal.OriginalOutlookTodayFileName, ApplicationGlobal.FinalPath + ApplicationGlobal.CustomOutlookTodayFileName, true);
        }

        private void showMyViewInNewTab()
        {
            string myViewUrl = getMyViewURL();
            System.Diagnostics.Process.Start(myViewUrl);
        }

        private static string getMyViewURL()
        {
            String currentUrl = null;
            if (!TaleoAddIn.USE_REST)
            {
                currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("DASHBOARD").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
                currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            }
            else
            {
                currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/myview/main.jsp?org=" + TaleoAddIn.sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>&isBrowser=true");

                //implementation of main site link
                currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("TALEO_LOGIN");
                if (String.IsNullOrEmpty(currentUrl))
                    currentUrl = "<BASE_URL>/ats/outlook/myview/main.jsp?org=<COMPANY_CODE>&pword=<LOGIN_TOKEN>&redirectPath=../../myView/MyView.jsp&isBrowser=true&User-Agent="+ApplicationGlobal.GetUserAgent();
                currentUrl = currentUrl.Replace("<BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));

                currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            }

            String loginToken = sessionData.loginToken;
            AuthenticationService authUtil = AuthenticationService.getInstance(serviceURL, ApplicationGlobal.USE_REST);

            TaleoAddIn.sessionData = authUtil.checkAuthentication(sessionData, AccessDepth.Only_LogInToken, false, true);
            loginToken = sessionData.loginToken;

            //loginToken = USE_REST ? ((LoginTokenResponse)(addInService.LogInTokenREST(sessionData.authToken))).response.loginToken : ((LoginTokenResponse)(addInService.logInTokenSOAP(serviceURL, sessionData.authToken))).response.loginToken;
            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
            return currentUrl;
        }

        private static string getMyViewURLOutlook()
        {
            String currentUrl = null;
            if (!TaleoAddIn.USE_REST)
            {
                currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("DASHBOARD").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
                currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            }
            else
            {
                currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/myview/main.jsp?org=" + TaleoAddIn.sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>&User-Agent=" + HttpUtility.UrlEncode(ApplicationGlobal.GetUserAgent()));

                //implementation of main site link
//                currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("TALEO_LOGIN");
//                if (String.IsNullOrEmpty(currentUrl))
//                    currentUrl = "<BASE_URL>/ats/outlook/myview/main.jsp?org=<COMPANY_CODE>&pword=<LOGIN_TOKEN>&redirectPath=../../myView/MyView.jsp";
                currentUrl = currentUrl.Replace("<BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));

                currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
            }

            String loginToken = sessionData.loginToken;
            AuthenticationService authUtil = AuthenticationService.getInstance(serviceURL, ApplicationGlobal.USE_REST);

            TaleoAddIn.sessionData = authUtil.checkAuthentication(sessionData, AccessDepth.Only_LogInToken, false, true);
            loginToken = sessionData.loginToken;

            //loginToken = USE_REST ? ((LoginTokenResponse)(addInService.LogInTokenREST(sessionData.authToken))).response.loginToken : ((LoginTokenResponse)(addInService.logInTokenSOAP(serviceURL, sessionData.authToken))).response.loginToken;
            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
            return currentUrl;
        }

        private void showCustomOutlookToday(String currentURL)
        {
            ApplicationGlobal.writeCustomHTM(currentURL);
            Outlook.Application objOutlook = Application;
            Outlook.NameSpace objNS = Application.Session;
            Outlook.MAPIFolder objFolder = objNS.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            objOutlook.ActiveExplorer().CurrentFolder = objFolder;
            objFolder = objFolder.Parent == null ? objFolder : objFolder.Parent;
            objOutlook.ActiveExplorer().CurrentFolder = objFolder;
        }
        private static string getResourceCenterURL()
        {
            String currentUrl = null;
            if (!TaleoAddIn.USE_REST)
            {
                currentUrl = addInService.GetTaleoSettingsValue("RESOURCE_CENTER").Replace("<SOAP_BASE_URL>", serviceURL.Substring(0, serviceURL.IndexOf("/ats")));
                currentUrl = currentUrl.Replace("<COMPANY_CODE>", sessionData.lastLoginData.companyCode);
            }
            else
            {
                currentUrl = serviceURL.Replace(@"api/v1/", "outlook/resource/main.jsp?org=" + sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>");
            }

            //loginToken = USE_REST ? ((LoginTokenResponse)(addInService.LogInTokenREST(sessionData.authToken))).response.loginToken : ((LoginTokenResponse)(addInService.logInTokenSOAP(serviceURL, sessionData.authToken))).response.loginToken;

            AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
            TaleoAddIn.sessionData = authUtil.checkAuthentication(sessionData, AccessDepth.Only_LogInToken, false, true);
            String loginToken = TaleoAddIn.sessionData.loginToken;

            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
            return currentUrl;
        }

        private void SendingLogEmailAction(object item, ref bool cancel)
        {
            LogEmailMsgForm logEmailMsgForm = new LogEmailMsgForm();
            EmailWindowRibbon emailWindowRibbonObj= new EmailWindowRibbon();
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    TaleoMessageBox.Show(TaleoMsg.TaleoOffLineMsg);
                    return;
                }
                if (!LogEmailClicked) return;
                if (!TaleoFormHelper.IsAutoLoogedIn())
                {
                    LoginForm loginFormObj = new LoginForm();
                    loginFormObj.ShowDialog();
                    DialogResult result = loginFormObj.DialogResult;
                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                }

                bool accessResponse = false;

                if (!TaleoAddIn.USE_REST)
                    accessResponse = emailWindowRibbonObj.GetAccessPermisionResponse("Recruit");
                else
                {
                    accessResponse = emailWindowRibbonObj.GetAccessPermisionResponse(Modules.LogEmail.ToString());
                }
                if (!accessResponse) return;

                var emailItem = item as MailItem;
                string emailId = "", subject = "", body = "";
                if (emailItem != null)
                {
                    //emailId = emailItem.To;
                    const string PR_SMTP_ADDRESS = TaleoMsg.PR_SMTP_ADDRESS;
                    Outlook.Recipients recips1 = emailItem.Recipients;
                    foreach (Outlook.Recipient recip in recips1)
                    {
                        Outlook.PropertyAccessor pa = recip.PropertyAccessor;
                        emailId = pa.GetProperty(PR_SMTP_ADDRESS).ToString();
                    }
                    subject = emailItem.Subject;
                    body = emailItem.Body;
                }
                logEmailMsgForm.Show();
                System.Windows.Forms.Application.DoEvents();

                if (!USE_REST)
                {
                    EmailSentLogResponse sendEmailLogResponse = new EmailSentLogResponse(false, null, null);

                    sendEmailLogResponse = addInService.createEmailSentLogSOAP(serviceURL, sessionData.authToken, emailId, subject, HttpUtility.UrlEncode(body), DateTime.Now.ToString("o"));

                    if (sendEmailLogResponse.isSuccess)
                    {
                        logEmailMsgForm.Close();
                        LogEmailClicked = false;
                    }
                    else
                    {
                        logEmailMsgForm.Close();
                        TaleoMessageBox.Show(@"Failed ""Log Email""! This user may not exist");
                    }
                }
                else
                {
                    int candidateId = GetCandIDForREST(emailId);
                    const int typeNo = 3;
                    CreateEmailLogResponse aCreateEmailLogResponse = new CreateEmailLogResponse(false, null, 0);
                    aCreateEmailLogResponse = addInService.CreateEmailSentLogREST(candidateId, "CTCT", DateTime.Now, HttpUtility.UrlEncode(body), HttpUtility.UrlEncode(subject), typeNo, sessionData.authToken);
                    if (aCreateEmailLogResponse.SuccessResult)
                    {
                        logEmailMsgForm.Close();
                        LogEmailClicked = false;
                    }
                    else
                    {
                        logEmailMsgForm.Close();
                        TaleoMessageBox.Show(TaleoMsg.LoggedEmailUserNotExist);
                    }
                }
            }
            catch (Exception ex)
            {
                logEmailMsgForm.Close();
                TaleoMessageBox.Show(TaleoMsg.LoggedEmailError);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name,
                    ex);
            }


        }

        private int GetCandIDForREST(string email)
        {
            return addInService.CandidateResponse_REST(sessionData.authToken, email).CandidateID;
        }

        /// <summary>
        /// This method perform Log email action for inbox mail
        /// </summary>
        /// <param name="selections">Outlook email selection object that contains selected email property</param>
        public void InboxLogEmailAction(Selection selections)
        {
            MailItem emailItem = null;
            foreach (var selection in selections)
            {
                emailItem = selection as MailItem;
            }
            if (emailItem != null)
            {
                string email = emailItem.SenderEmailAddress;
                string subject = emailItem.Subject;
                string body = emailItem.Body;

                LogEmailMsgForm logEmailMsgForm = new LogEmailMsgForm();
                logEmailMsgForm.Show();
                System.Windows.Forms.Application.DoEvents();
                CreateEmailLogResponse incommingEmailLogResponse = new CreateEmailLogResponse(false, null, 0);
                incommingEmailLogResponse = addInService.getCreateEmailLogResponseSOAP(serviceURL, sessionData.authToken, email, subject, HttpUtility.UrlEncode(body), DateTime.Now);

                if (incommingEmailLogResponse.SuccessResult)
                {
                    logEmailMsgForm.Close();
                }
                else
                {
                    logEmailMsgForm.Close();
                    TaleoMessageBox.Show(TaleoMsg.LoggedEmailException);
                }
            }
        }

        /// <summary>
        /// This method perform Log email action for sent item mail
        /// </summary>
        /// <param name="selections">Outlook email selection object that contains selected email property</param>
        public void SentItemLogEmailAction(Selection selections)
        {
            MailItem emailItem = null;
            foreach (var selection in selections)
            {
                emailItem = selection as MailItem;
            }
            if (emailItem != null)
            {
                string email = emailItem.To;
                string subject = emailItem.Subject;
                string body = emailItem.Body;

                LogEmailMsgForm logEmailMsgForm = new LogEmailMsgForm();
                logEmailMsgForm.Show();
                System.Windows.Forms.Application.DoEvents();
                EmailSentLogResponse sendEmailLogResponse = new EmailSentLogResponse(false, null, null);
                sendEmailLogResponse = addInService.createEmailSentLogSOAP(TaleoAddIn.serviceURL, TaleoAddIn.sessionData.authToken, email, subject, HttpUtility.UrlEncode(body), DateTime.Now.ToString("o"));

                if (sendEmailLogResponse.isSuccess)
                {
                    logEmailMsgForm.Close();
                }
                else
                {
                    logEmailMsgForm.Close();
                    TaleoMessageBox.Show(TaleoMsg.LoggedEmailException);

                }
            }
        }

        /// <summary>
        /// Check internet connection
        /// </summary>
        /// <returns></returns>
        public static bool CheckForInternetConnection()
        {
            try
            {
                //HTTPService serviceHTTP = new HTTPService(USE_REST,null);
                //SettingsFileResponse response = (SettingsFileResponse)serviceHTTP.makeHTTPRequest_GetSettingsFile("http://tbe.taleo.net/outlook/Toolbar.settings");
                return true;
            }
            catch
            {
                return true;
            }
        }
        #region method for outlook 2007
        private void RemoveTaleoMenubar()
        {
            // If the menu already exists, remove it. 
            try
            {
                Office.CommandBarPopup foundMenu = (Office.CommandBarPopup)
                    this.Application.ActiveExplorer().CommandBars.ActiveMenuBar.
                    FindControl(Office.MsoControlType.msoControlPopup,
                    missing, menuTag, true, true);
                if (foundMenu != null)
                {
                    foundMenu.Delete(true);
                }
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
            }
        }
        private void AddTaleoMenuBar()
        {
            try
            {
                menuBar = this.Application.ActiveExplorer().CommandBars.ActiveMenuBar;
                newTaleoMenuBar = (Office.CommandBarPopup)menuBar.Controls.Add(
                    Office.MsoControlType.msoControlPopup, missing,
                    missing, missing, false);
                if (newTaleoMenuBar != null)
                {

                    btnAbout = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                         Add(Office.MsoControlType.msoControlButton, missing,
                             missing, 1, true);
                    InitializeAboutButton(btnAbout, true);
                    btnCheckForUpdates = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                           Add(Office.MsoControlType.msoControlButton, missing,
                               missing, 1, true);
                    InitializeUpdateButton(btnCheckForUpdates, true);
                    btnPreference = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                          Add(Office.MsoControlType.msoControlButton, missing,
                              missing, 1, true);
                    InitializePreferenceButton(btnPreference, true);
                    btnAddComments = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                         Add(Office.MsoControlType.msoControlButton, missing,
                             missing, 1, true);
                    InitializeAddCommentsButton(btnAddComments, true);
                    btnAddToTaleo = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                         Add(Office.MsoControlType.msoControlButton, missing,
                             missing, 1, true);
                    InitializeAddtoTaleoButton(btnAddToTaleo, true);
                    btnFileFeedBack = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                         Add(Office.MsoControlType.msoControlButton, missing,
                             missing, 1, true);
                    InitializeFileFeedbackButton(btnFileFeedBack, true);
                    btnRequestFeedBack = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                         Add(Office.MsoControlType.msoControlButton, missing,
                             missing, 1, true);
                    InitializeRequestFeedbackButton(btnRequestFeedBack, true);
                    btnLogin = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                         Add(Office.MsoControlType.msoControlButton, missing,
                             missing, 1, true);
                    InitializeLogInButton(btnLogin, true);
                    btnDashBoard = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                         Add(Office.MsoControlType.msoControlButton, missing,
                             missing, 1, true);
                    InitializeDashBoardButton(btnDashBoard, true);
                    btnTaleo = (Office.CommandBarButton)newTaleoMenuBar.Controls.
                         Add(Office.MsoControlType.msoControlButton, missing,
                             missing, 1, true);
                    InitializeTaleoButton(btnTaleo, true);
                    newTaleoMenuBar.Caption = "Taleo";
                    newTaleoMenuBar.Tag = menuTag;

                }
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
            }
        }
        private void AddTaleoToolbar()
        {

            if (taleoToolBar == null)
            {
                Office.CommandBars cmdBars =
                    this.Application.ActiveExplorer().CommandBars;
                taleoToolBar = cmdBars.Add("TaleoToolBar",
                    Office.MsoBarPosition.msoBarTop, false, true);

            }
            try
            {
                btnTaleoToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                 Add(1, missing, missing, missing, missing);
                InitializeTaleoButton(btnTaleoToolBar, false);
                btnDashBoardToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                 Add(1, missing, missing, missing, missing);
                InitializeDashBoardButton(btnDashBoardToolBar, false);
                btnLoginToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                 Add(1, missing, missing, missing, missing);
                InitializeLogInButton(btnLoginToolBar, false);
                btnRequestFeedBackToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                  Add(1, missing, missing, missing, missing);
                InitializeRequestFeedbackButton(btnRequestFeedBackToolBar, false);
                btnFileFeedBackToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                  Add(1, missing, missing, missing, missing);
                InitializeFileFeedbackButton(btnFileFeedBackToolBar, false);
                btnAddToTaleoToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                  Add(1, missing, missing, missing, missing);
                InitializeAddtoTaleoButton(btnAddToTaleoToolBar, false);
                btnAddCommentsToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                  Add(1, missing, missing, missing, missing);
                InitializeAddCommentsButton(btnAddCommentsToolBar, false);
                btnPreferenceToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                  Add(1, missing, missing, missing, missing);
                InitializePreferenceButton(btnPreferenceToolBar, false);
                btnCheckForUpdatesToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                  Add(1, missing, missing, missing, missing);
                InitializeUpdateButton(btnCheckForUpdatesToolBar, false);
                btnAboutToolBar = null;
                btnAboutToolBar = (Office.CommandBarButton)taleoToolBar.Controls.
                                   Add(1, missing, missing, missing, missing);
                InitializeAboutButton(btnAboutToolBar, false);
                taleoToolBar.Visible = true;
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
            }
        }
        private void InitializeTaleoButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "Taleo";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.taleo_ic);
            btn.Tag = isForMenuBar ? "TaleoMenubar" : "TaleoToolbar";
            btn.Click += new Office.
                _CommandBarButtonEvents_ClickEventHandler
                (TaleoMenuAndToolbarButton_Click);
        }
        private void TaleoMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            btnFileFeedBack.Enabled = false;
            btnFileFeedBackToolBar.Enabled = false;
            btnAddToTaleoToolBar.Enabled = false;
            btnAboutToolBar.Enabled = false;
            taleoButtonAction.GetTaleoButtoonAction();
        }
        private void InitializeDashBoardButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "Dashboard";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.dashboard_ic);
            btn.Tag = isForMenuBar ? "DashboardMenubar" : "DashboardToolbar";
            btn.Click += new Office.
                _CommandBarButtonEvents_ClickEventHandler
                (DashBoardMenuAndToolbarButton_Click);
        }
        private void DashBoardMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            btnFileFeedBack.Enabled = false;
            btnFileFeedBackToolBar.Enabled = false;
            btnAddToTaleoToolBar.Enabled = false;
            btnAboutToolBar.Enabled = false;
            taleoButtonAction.GetDashBoardButtoonAction();
        }
        private void InitializeLogInButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "Taleo Login";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.taleo_ic);
            btn.Tag = isForMenuBar ? "LoginMenubar" : "LoginToolbar";
            btn.Click += new Office.
                    _CommandBarButtonEvents_ClickEventHandler
                    (LoginMenuAndToolbarButton_Click);
        }
        private void LoginMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            taleoButtonAction.GetTaleoLoginButtoonAction();
        }
        private void InitializeRequestFeedbackButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "Request Feedback";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.request_bubble_ic);
            btn.Tag = isForMenuBar ? "RequestFeedBackMenubar" : "RequestFeedBackToolbar";
            btn.Click += new Office.
                    _CommandBarButtonEvents_ClickEventHandler
                    (RequestFeedBackMenuAndToolbarButton_Click);
        }
        private void RequestFeedBackMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            taleoButtonAction.GetRequestFeedBackButtoonAction();
        }
        private void InitializeFileFeedbackButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "File Feedback";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.file_bubble_ic);
            btn.Tag = isForMenuBar ? "FileFeedBackMenubar" : "FileFeedBackToolbar";
            btn.Click += new Office.
                   _CommandBarButtonEvents_ClickEventHandler
                   (FileFeedBackMenuAndToolbarButton_Click);
        }
        private void FileFeedBackMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            taleoButtonAction.GetFileFeedbackButtoonAction();
        }
        private void InitializeAddtoTaleoButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "Add to Taleo";
            btn.FaceId = 610; btn.Picture = TaleoFormHelper.getImage(Properties.Resources.add_to_taleo_ic);
            btn.Tag = isForMenuBar ? "AddToTaleoMenubar" : "AddToTaleoToolbar";
            btn.Click += new Office.
                   _CommandBarButtonEvents_ClickEventHandler
                   (AddToTaleoMenuAndToolbarButton_Click);
        }
        private void AddToTaleoMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            taleoButtonAction.GetAddToTaleoButtoonAction();
        }
        private void InitializeAddCommentsButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "Add Comments";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.AddCommentRBic);
            btn.Tag = isForMenuBar ? "AddCommentsMenubar" : "AddCommentsToolbar";
            btn.Click += new Office.
                   _CommandBarButtonEvents_ClickEventHandler
                   (AddCommentsMenuAndToolbarButton_Click);
        }
        private void AddCommentsMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            taleoButtonAction.GetAddCommentsButtonAction();
        }
        private void InitializePreferenceButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "Preferences";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.PreferenceRBic);
            btn.Tag = isForMenuBar ? "PreferencesMenubar" : "PreferencesToolbar";
            btn.Click += new Office.
                    _CommandBarButtonEvents_ClickEventHandler
                    (PreferenceMenuAndToolbarButton_Click);
        }
        private void PreferenceMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            taleoButtonAction.GetPreferencesButtonAction();
        }
        private void InitializeUpdateButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "Check for Updates";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.updates_ic);
            btn.Tag = isForMenuBar ? "CheckforUpdatesMenubar" : "CheckforUpdatesToolbar";
            btn.Click += new Office.
                    _CommandBarButtonEvents_ClickEventHandler
                    (CheckForUpdatesMenuAndToolbarButton_Click);
        }
        private void CheckForUpdatesMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            taleoButtonAction.GetCheckForUpdatesButtonAction();
        }
        private void InitializeAboutButton(Office.CommandBarButton btn, bool isForMenuBar)
        {
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = "About";
            btn.FaceId = 610;
            btn.Picture = TaleoFormHelper.getImage(Properties.Resources.AboutRBic);
            btn.Tag = isForMenuBar ? "AboutMenuBar" : "AboutToolBar";
            btn.Click += new Office.
                    _CommandBarButtonEvents_ClickEventHandler
                    (AboutButtonMenuAndToolbarButton_Click);
        }
        private void AboutButtonMenuAndToolbarButton_Click(Office.CommandBarButton ctrl, ref bool cancel)
        {
            taleoButtonAction.GetAboutButtonAction();
        }
        #endregion
    }
}
