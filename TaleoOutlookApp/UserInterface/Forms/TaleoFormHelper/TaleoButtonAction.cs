using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Win32;
using Model.Response;
using Service.AddIn;
using Service.Authentication;
using TaleoOutlookAddin.Forms.AddComments;
using TaleoOutlookAddin.Forms.AddressBook;
using TaleoOutlookAddin.Forms.AddToTaleo;
using TaleoOutlookAddin.Forms.CustomeMessage;
using TaleoOutlookAddin.Forms.FileFeedBack;
using TaleoOutlookAddin.Forms.Login;
using TaleoOutlookAddin.Forms.Preferences;
using TaleoOutlookAddin.Forms.RequestFeedback;
using Util.ApplicationGlobal;
using Util.Enums;
using Util.Utilities;
using System.IO;
using Exception = System.Exception;

namespace TaleoOutlookAddin.Forms.TaleoFormHelper
{
    public class TaleoButtonAction
    {
        public Microsoft.Office.Core.IRibbonUI ribbon;

        /// <summary>
        /// This is used to get/set the registry key of Microsoft/Office/Version/Outlook/Today
        /// </summary>
        public static RegistryKey RegistryKeyURL { get; set; }
        /// <summary>
        /// This is used to get/set the registry value of Microsoft/Office/Version/Outlook/Today/customURL; customURL is sometimes used to show outlook today folder.
        /// </summary>
        private string CustomUrlValue { get; set; }
        /// <summary>
        /// This is used to get/set the registry value of Microsoft/Office/Version/Outlook/Today/customURL; customURL is sometimes used to show outlook today folder.
        /// </summary>
        private string UserdefinedurlValue { get; set; }
        /// <summary>
        /// This is used to get/set the registry value of Microsoft/Office/Version/Outlook/Today/customURL; customURL is sometimes used to show outlook today folder.
        /// </summary>
        private string UrlValue { get; set; }
        /// <summary>
        /// This is used to get/set the registry value of Microsoft/Office/Version/Outlook/Today/customURL; customURL is sometimes used to show outlook today folder.
        /// </summary>
        public string StampValue { get; set; }
        public TaleoButtonAction()
        {
        }
        /// <summary>
        /// This method is executed when Login ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetTaleoLoginButtoonAction()
        {
            /*            GetRegistryValue();
                        CopyUrlxValuetoOthers();
                        //RegistryUtilities.RenameSubKeyStringValue(RegistryKeyURL, "URLx", "URL");
                        LoginForm loginForm = new LoginForm(false);
                        loginForm.ShowDialog();
                        DialogResult result = loginForm.DialogResult;
                        if (result == DialogResult.OK)
                        {
                            TaleoAddIn.getInstance().showLink(PagePaneIndex.MY_VIEW);
                        }
                        //RegistryUtilities.RenameSubKeyStringValue(RegistryKeyURL, "URL", "URLx");
                        SetOldRegValueBack();
            */
            AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
            authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_LogInToken, false, false);
            GetRegistryValue();
            CopyUrlxValuetoOthers();
            //RegistryUtilities.RenameSubKeyStringValue(RegistryKeyURL, "URLx", "URL");
            if (!IsAutoLoogedIn())
            {
                LoginForm loginFormObj = new LoginForm(false);
                loginFormObj.ShowDialog();
                DialogResult result = loginFormObj.DialogResult;
                if (result == DialogResult.OK)
                {
                    TaleoAddIn.getInstance().showLink(PagePaneIndex.MY_VIEW);
                }
            }
            else
                TaleoAddIn.getInstance().showLink(PagePaneIndex.MY_VIEW);
            //RegistryUtilities.RenameSubKeyStringValue(RegistryKeyURL, "URL", "URLx");
            SetOldRegValueBack();

        }
        /// <summary>
        /// This method is executed when Taleo ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetTaleoButtoonAction()
        {
            AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
            authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_LogInToken, false, false);
            GetRegistryValue();
            CopyUrlxValuetoOthers();
            //RegistryUtilities.RenameSubKeyStringValue(RegistryKeyURL, "URLx", "URL");
            if (!IsAutoLoogedIn())
            {
                LoginForm loginFormObj = new LoginForm(false);
                loginFormObj.ShowDialog();
                DialogResult result = loginFormObj.DialogResult;
                if (result == DialogResult.OK)
                {
                    TaleoAddIn.getInstance().showLink(PagePaneIndex.RESOURCE_CENTER);
                }
            }
            else
                TaleoAddIn.getInstance().showLink(PagePaneIndex.RESOURCE_CENTER);
            //RegistryUtilities.RenameSubKeyStringValue(RegistryKeyURL, "URL", "URLx");
            SetOldRegValueBack();
        }
        /// <summary>
        /// This method is executed when DashBoard ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetDashBoardButtoonAction()
        {
            AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
            authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_LogInToken, false, false);
            GetRegistryValue();
            CopyUrlxValuetoOthers();
            // RegistryUtilities.RenameSubKeyStringValue(RegistryKeyURL, "URLx", "URL");
            if (!IsAutoLoogedIn())
            {
                LoginForm loginFormObj = new LoginForm(false);
                loginFormObj.ShowDialog();
                DialogResult result = loginFormObj.DialogResult;
                if (result == DialogResult.OK)
                {
                    TaleoAddIn.getInstance().showLink(PagePaneIndex.DASHBOARD);
                }
            }
            else
                TaleoAddIn.getInstance().showLink(PagePaneIndex.DASHBOARD);
            //RegistryUtilities.RenameSubKeyStringValue(RegistryKeyURL, "URL", "URLx");
            SetOldRegValueBack();
        }
        /// <summary>
        /// This method is executed when RequestFeedBack ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetRequestFeedBackButtoonAction()
        {
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable() || !TaleoAddIn.CheckForInternetConnection())
                {
                    MessageBox.Show(TaleoMsg.TaleoOffLineMsg);
                    return;
                }
                AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
                TaleoAddIn.sessionData = authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_AuthToken, false, false);
                if (!IsAutoLoogedIn())
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
                    accessResponse = GetAccessPermisionResponse("Perform");
                else
                {
                    accessResponse = GetAccessPermisionResponse(Modules.RequestFeedback.ToString());
                }
                if (!accessResponse) return;
                AuthenticationMessageForm authenticationMessageForm = new AuthenticationMessageForm();
                authenticationMessageForm.Show();
                System.Windows.Forms.Application.DoEvents();

                authenticationMessageForm.Close();

                ShowRequestFeedbackPage();
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name,
                    ex);
            }

        }
        /// <summary>
        /// This method is executed when FileFeedback ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetFileFeedbackButtoonAction()
        {
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable() || !TaleoAddIn.CheckForInternetConnection())
                {
                    TaleoMessageBox.Show(TaleoMsg.TaleoOffLineMsg);
                    return;
                }

                if (!IsAutoLoogedIn())
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
                    accessResponse = GetAccessPermisionResponse("Perform");
                else
                {
                    accessResponse = GetAccessPermisionResponse(Modules.FileFeedback.ToString());
                }
                if (!accessResponse) return;

                var ol = new Microsoft.Office.Interop.Outlook.Application();
                Selection selections = ol.ActiveExplorer().Selection;
                if (selections.Count <= 0)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoSelectEmailAnEmailMsg);
                }
                else if (selections.Count > 1)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoSelectEmailOnlyOneEmailMsg);
                }
                else
                {
                    MailItem item = GetFileFeedbackSelectedItem(selections);
                    string selectedText = GetFileFeedbackSelectedEmailBody(item);

                    AuthenticationMessageForm authenticationMessageForm = new AuthenticationMessageForm();
                    authenticationMessageForm.Show();
                    System.Windows.Forms.Application.DoEvents();

                    authenticationMessageForm.Close();

                    ShowFileFeedbackPage(item, selectedText);
                }
            }
            catch (System.Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
        }
        /// <summary>
        /// This method is executed when AddToTaleo ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetAddToTaleoButtoonAction()
        {
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable() || !TaleoAddIn.CheckForInternetConnection())
                {
                    TaleoMessageBox.Show(TaleoMsg.TaleoOffLineMsg);
                    return;
                }

                if (!IsAutoLoogedIn())
                {
                    LoginForm loginFormObj = new LoginForm();
                    loginFormObj.ShowDialog();
                    DialogResult result = loginFormObj.DialogResult;
                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                }

                if (TaleoAddIn.sessionData == null)
                    TaleoAddIn.LoadTaleoStarterInfo();

                bool accessResponse = false;

                if (!TaleoAddIn.USE_REST)
                {
                    EnableServiceResponse enableServiceResponse =
                        TaleoAddIn.addInService.GetEnableServiceResponseSOAP(TaleoAddIn.serviceURL,
                            TaleoAddIn.sessionData.authToken);

                    accessResponse = TaleoAddIn.addInService.CheckAccessPermission("Recruit",
                        enableServiceResponse.Response);
                }
                else
                {
                    accessResponse = GetAccessPermisionResponse(Modules.AddtoTaleo.ToString());
                }
                if (!accessResponse) return;

                var ol = new Microsoft.Office.Interop.Outlook.Application();
                Selection selections = ol.ActiveExplorer().Selection;
                if (selections.Count == 0)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoSelectEmailMsg);
                }
                else
                {
                    if (IsAutoLoogedIn())
                    {
                        AddToTaleoStarter.ShowFormCheckingSelection(selections);
                    }
                    else
                    {
                        LoginForm loginFormObj = new LoginForm();
                        loginFormObj.ShowDialog();
                        DialogResult result = loginFormObj.DialogResult;
                        if (result == DialogResult.OK)
                        {
                            AddToTaleoStarter.ShowFormCheckingSelection(selections);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
        }

        /// <summary>
        /// This method is executed when AddComments ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetAddCommentsButtonAction()
        {
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable() || !TaleoAddIn.CheckForInternetConnection())
                {
                    TaleoMessageBox.Show(TaleoMsg.TaleoOffLineMsg);
                    return;
                }
                AddCommentsForm addCommentsForm = new AddCommentsForm();
                if (!IsAutoLoogedIn())
                {
                    LoginForm loginFormObj = new LoginForm();
                    loginFormObj.ShowDialog();
                    DialogResult result = loginFormObj.DialogResult;
                    if (result == DialogResult.OK)
                    {
                        addCommentsForm.ShowDialog();
                    }
                }
                else
                    addCommentsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
        }
        /// <summary>
        /// This method is executed when Preferences ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetPreferencesButtonAction()
        {
            try
            {
                PreferencesForm preferencesForm = new PreferencesForm();
                preferencesForm.ShowDialog();
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
        }
        /// <summary>
        /// This method is executed when CheckForUpdates ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetCheckForUpdatesButtonAction()
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable() && TaleoAddIn.CheckForInternetConnection())
                {
                    if (TaleoAddIn.sessionData == null)
                        TaleoAddIn.LoadTaleoStarterInfo();
                    UpdateCheckingMessageForm updateCheckingMessage = new UpdateCheckingMessageForm();
                    updateCheckingMessage.Show();
                    System.Windows.Forms.Application.DoEvents();
                    AddInServices addInServices = AddInServices.getInstance();

                    //New Version : Read Taleo Settings
                    string major = addInServices.GetTaleoSettingsValue("MAJOR");
                    if (major == "") major = "0";
                    string minor = addInServices.GetTaleoSettingsValue("MINOR");
                    if (minor == "") minor = "0";
                    string revision = addInServices.GetTaleoSettingsValue("REVISION");
                    if (revision == "") revision = "0";
                    string newVersion = major + "." + minor + "." + revision;

                    //Current Version : Read App Settings
                    major = addInServices.GetApplicationSettingsValue("MAJOR");
                    if (major == "") major = "0";
                    minor = addInServices.GetApplicationSettingsValue("MINOR");
                    if (minor == "") minor = "0";
                    revision = addInServices.GetApplicationSettingsValue("REVISION");
                    if (revision == "") revision = "0";
                    string currentVersion = major + "." + minor + "." + revision;
                    updateCheckingMessage.Close();

                    //Checking and Showing
                    bool hasUpdatedVersion = newVersion == currentVersion;
                    string message;
                    if (!hasUpdatedVersion)
                    {
                        //                        message = "The current version of Taleo is " + currentVersion + " and a new version of Taleo (" + newVersion +
                        //                                         ") is available. Do you want to update?";

                        message = AddInServices.getInstance().GetTaleoSettingsValue("UPDATE_PROMPT").Replace("<PRODUCT_NAME>", "Oracle Taleo Outlook Plugin").Replace("<NEW_PRODUCT_VERSION>", newVersion).Replace("<BR><BR>", "\n\n");

                        DialogResult dr = MessageBox.Show(message, "Taleo Outlook Toolbar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            String updateURL = AddInServices.getInstance().GetTaleoSettingsValue("INSTALLER");
                            System.Diagnostics.Process.Start(updateURL);
                        }
                    }
                    else
                    {
                        message = "You are running the latest (" + currentVersion + ") version of Taleo Outlook Toolbar.";
                        TaleoMessageBox.Show(message, "Taleo Outlook Toolbar", TaleoMsgBoxIcon.Information);
                    }
                }
                else
                {
                    TaleoMessageBox.Show(TaleoMsg.TaleoOffLineMsg);
                }
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
        }
        /// <summary>
        /// This method is executed when About ribbon/menu/toolbar is clicked
        /// </summary>
        public void GetAboutButtonAction()
        {
            try
            {
                AboutOracleTaleoForm.AboutOracleTaleoForm aboutOracleTaleoForm = new AboutOracleTaleoForm.AboutOracleTaleoForm();
                aboutOracleTaleoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
        }
        #region Ribbon helper Function
        private void SetOldRegValueBack()
        {
            RegistryKeyURL = TaleoFormHelper.GetRegistryKeyURL(TaleoAddIn.outLookVersion);
            RegistryUtilities.SetValue(RegistryKeyURL, "CustomUrl", CustomUrlValue);
            RegistryUtilities.SetValue(RegistryKeyURL, "Userdefinedurl", UserdefinedurlValue);
            RegistryUtilities.SetValue(RegistryKeyURL, "Url", UrlValue);
            RegistryUtilities.SetValue(RegistryKeyURL, "Stamp", StampValue);
        }

        private void CopyUrlxValuetoOthers()
        {
            RegistryUtilities.SetValue(RegistryKeyURL, "CustomUrl", RegistryUtilities.GetValue(RegistryKeyURL, "URLx"));
            RegistryUtilities.SetValue(RegistryKeyURL, "Userdefinedurl", RegistryUtilities.GetValue(RegistryKeyURL, "URLx"));
            RegistryUtilities.SetValue(RegistryKeyURL, "Url", RegistryUtilities.GetValue(RegistryKeyURL, "URLx"));
        }

        private void GetRegistryValue()
        {
            String globalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            String oraclePath = globalPath + "\\Oracle";
            String taleoPath = oraclePath + "\\HCM\\SMBOutlookPlugin";
            String dummyHTMLFile = taleoPath + "\\colt.htm";

            CustomUrlValue = RegistryUtilities.GetValue(RegistryKeyURL, "CustomUrl");
            if (string.IsNullOrEmpty(CustomUrlValue))
            {
                RegistryUtilities.SetValue(RegistryKeyURL, "CustomUrl", @"outlook.htm");
                CustomUrlValue = @"outlook.htm";
            }
            UserdefinedurlValue = RegistryUtilities.GetValue(RegistryKeyURL, "Userdefinedurl");
            if (string.IsNullOrEmpty(UserdefinedurlValue))
            {
                RegistryUtilities.SetValue(RegistryKeyURL, "Userdefinedurl", dummyHTMLFile);
                UserdefinedurlValue = dummyHTMLFile;
            }
            UrlValue = RegistryUtilities.GetValue(RegistryKeyURL, "Url");

            if (string.IsNullOrEmpty(UrlValue))
            {
                RegistryUtilities.SetValue(RegistryKeyURL, "Url", dummyHTMLFile);
                UrlValue = dummyHTMLFile;
            }

            StampValue = RegistryUtilities.GetValue(RegistryKeyURL, "Stamp");

            if (string.IsNullOrEmpty(StampValue))
            {
                RegistryUtilities.SetValue(RegistryKeyURL, "Stamp", "1");
                StampValue = "1";
            }
        }

        private bool IsAutoLoogedIn()
        {
            return TaleoFormHelper.IsAutoLoogedIn();
        }

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        private static void ShowRequestFeedbackPage()
        {
            RequestFeedBack requestFeedBack = new RequestFeedBack();
            requestFeedBack.ShowDialog();
        }

        private static void ShowFileFeedbackPage(MailItem item, string selectedText)
        {
            TaleoFileFeedBackForm taleoFileFeedBackForm = new TaleoFileFeedBackForm(item, selectedText);
            taleoFileFeedBackForm.ShowDialog();
        }
        private static void ShowAddressBookForm()
        {
            AddressBookForm addressBookForm = new AddressBookForm();
            addressBookForm.ShowDialog();
        }

        private string createTaleoResourceCenterLink(string logInToken, string serviceURL, string companyCode)
        {
            String myViewLink = "";

            myViewLink = serviceURL.Substring(0, serviceURL.IndexOf("ats") + 4) + "outlook/resource/main.jsp?org=" + companyCode + "&pword=" + logInToken;
            //			myViewLink = serviceURL.Substring(0, serviceURL.IndexOf("ats") + 4) + "outlook/myview/MyView.jsp";

            return myViewLink;
        }
        public static void ShowNoAttachmentForm(MailItem mailItem, bool fromAttachedForm, List<string> attachedFiles, List<string> remainingFiles, int selectedIndex, AddToTaleoWithAttachment attachedForm, string refer = "")
        {
            attachedForm.Close();
            attachedForm.Dispose();
            var addToTaleoForm = new AddToTaleo.AddToTaleo(mailItem, true, attachedFiles, remainingFiles, selectedIndex, refer);
            addToTaleoForm.ShowDialog();
        }

        public static void ShowWithAttachmentForm(MailItem mailItem, List<string> attachedFiles, AddToTaleo.AddToTaleo addToTaleo, int selectedIndex = 0, string refer = "", bool fromTaleoForm = false)
        {
            addToTaleo.Close();
            addToTaleo.Dispose();
            var attachedForm = new AddToTaleoWithAttachment(mailItem, attachedFiles, selectedIndex, refer, true);
            attachedForm.ShowDialog();
        }

        /// <summary>
        /// Get access permision response
        /// </summary>
        /// <param name="accessName">permission name to check</param>
        /// <returns>return true or false</returns>
        public bool GetAccessPermisionResponse(string accessName)
        {
            if (TaleoAddIn.sessionData == null)
                TaleoAddIn.LoadTaleoStarterInfo();
            if (!TaleoAddIn.USE_REST)
            {
                EnableServiceResponse enableServiceResponse =
                    TaleoAddIn.addInService.GetEnableServiceResponseSOAP(TaleoAddIn.serviceURL,
                        TaleoAddIn.sessionData.authToken);
                return TaleoAddIn.addInService.CheckAccessPermission(accessName, enableServiceResponse.Response);
            }
            else
            {
                EnableServiceResponse response = TaleoAddIn.OrgSettingsResponse;
                if (response == null || !response.SuccessResult)
                {
                    response = TaleoAddIn.addInService.GetEnableServiceResponse_REST(TaleoFormHelper.GetAuthTokenForRest());
                    TaleoAddIn.OrgSettingsResponse = response;
                }


                if (accessName != Modules.None.ToString() && response.SuccessResult)
                {
                    if (accessName == Modules.FileFeedback.ToString())
                    {
                        return response.ResponseObject.response.orgsetting.performAccess;
                    }

                    if (accessName == Modules.RequestFeedback.ToString())
                    {
                        return response.ResponseObject.response.orgsetting.performAccess;
                    }

                    if (accessName == Modules.AddtoTaleo.ToString())
                    {
                        return response.ResponseObject.response.orgsetting.recruitAccess;
                    }

                }

                return false;
            }
        }

        /// <summary>
        /// Get selected email body for file feedback  
        /// </summary>
        /// <param name="item"> mail item</param>
        /// <returns></returns>
        public string GetFileFeedbackSelectedEmailBody(MailItem item)
        {

            string selectedText = "";
            if (item == null) return selectedText;
            try
            {
                Inspector inspector = item.GetInspector;
                Microsoft.Office.Interop.Word.Document document = inspector.WordEditor;
                selectedText = document.Application.Selection.Text;
            }
            catch (System.Exception)
            {

                selectedText = "";
            }
            return selectedText;
        }

        /// <summary>
        /// Get selection items for file feedback
        /// </summary>
        /// <param name="selections">items</param>
        /// <returns></returns>
        public MailItem GetFileFeedbackSelectedItem(Selection selections)
        {
            MailItem item = null;
            foreach (var selection in selections)
            {
                item = selection as MailItem;
                if (item == null) continue;
            }
            return item;
        }

        #endregion
    }
}
