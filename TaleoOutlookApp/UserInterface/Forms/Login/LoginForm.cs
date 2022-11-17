using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows.Forms;
using Model.AuthenticateData;
using Service.AddIn;
using Service.Authentication;
using TaleoOutlookAddin.Forms.CustomeMessage;
using Util.ApplicationGlobal;
using Util.Enums;
using Util.Utilities;

namespace TaleoOutlookAddin.Forms.Login
{
    public partial class LoginForm : Form
    {
        private bool isCookieNeeded=true;
        /// <summary>
        /// Initialize Login Form
        /// </summary>
        public LoginForm()
        {
            InitLoginForm();
        }
        public LoginForm(bool isCookieNeeded)
        {
            this.isCookieNeeded = isCookieNeeded;
            InitLoginForm();
        }
        private void InitLoginForm()
        {
            InitializeComponent();
            if (TaleoAddIn.lastLogInData != null)
            {
                tbTaleoLoginUserName.Text = TaleoAddIn.lastLogInData.userName;
                tbTaleoLoginPassword.Text = TaleoAddIn.lastLogInData.password;
                tbTaleoLoginCompanyCode.Text = TaleoAddIn.lastLogInData.companyCode;
                cbRememberPassword.Checked = (TaleoAddIn.lastLogInData.remember != null &&
                                              TaleoAddIn.lastLogInData.remember.ToLower() == "true") ? true : false;
            }
        }

        private void btnTaleoLogin_Click(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                AuthenticationMessageForm authenticationMessageForm=new AuthenticationMessageForm();
                authenticationMessageForm.Show();
                Application.DoEvents();
                if (TaleoAddIn.sessionData == null)
                    TaleoAddIn.LoadTaleoStarterInfo();
                LogInData logInDataObj = new LogInData();
                logInDataObj.userName = tbTaleoLoginUserName.Text;
                logInDataObj.password = tbTaleoLoginPassword.Text;
                logInDataObj.companyCode = tbTaleoLoginCompanyCode.Text;
                logInDataObj.remember = cbRememberPassword.Checked + "";
                //needtoremove
                if (TaleoAddIn.sessionData == null) return;
                TaleoAddIn.sessionData.lastLoginData = logInDataObj;
                AddInServices addInService = AddInServices.getInstance();

                //TaleoAddIn.getInstance().showLink(PagePaneIndex.MY_VIEW);
                if (logInDataObj != null && logInDataObj.userName.Length > 0 && logInDataObj.password.Length > 0 && logInDataObj.companyCode.Length > 0)
                {
                    try
                    {
                        string serviceURL = addInService.getServiceURL(logInDataObj.companyCode);
                        TaleoAddIn.serviceURL = serviceURL;
                        string previousAuthToken = TaleoAddIn.sessionData.authToken;

                        AuthenticationService authUtil = AuthenticationService.getInstance(serviceURL, ApplicationGlobal.USE_REST);
                        SessionData sessionData = new SessionData(logInDataObj, string.Empty, string.Empty, string.Empty, new List<System.Net.Cookie>(), false, false, AccessDepth.None);
                        sessionData = authUtil.checkAuthentication(sessionData,isCookieNeeded ? AccessDepth.Upto_Cookies : AccessDepth.Only_LogInToken, false, true);
                        //if (TaleoAddIn.checkLogIn(logInDataObj, serviceURL))
                        if (sessionData.isLoggedIn)
                        {
                            TaleoAddIn.sessionData = sessionData;
//                            TaleoAddIn.authToken = sessionData.authToken;
                            //TaleoAddIn.getInstance().showLink(PagePaneIndex.MY_VIEW);
                            this.DialogResult = DialogResult.OK;
                            saveLoginData(logInDataObj, addInService);
                            authenticationMessageForm.Close();
                            this.Close();
                        }
                        else
                        {
                            authenticationMessageForm.Close();
                            LogInFailedMsgForm logInFailedMsgForm = new LogInFailedMsgForm();
                            logInFailedMsgForm.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        authenticationMessageForm.Close();
                        Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, "MainThread", ex);
                    }
                }
                else
                {
                    authenticationMessageForm.Close();
                    TaleoMessageBox.Show(TaleoMsg.TaleoLogInFailureMsg);
                }
            }
            else
            {
                TaleoMessageBox.Show(TaleoMsg.TaleoOffLineMsg);
            }
        }
        /// <summary>
        /// Save login data to settings file and set necessary data to addinservice and taleoadon 
        /// </summary>
        /// <param name="logInDataObj"></param>
        /// <param name="addInService"></param>
        public void saveLoginData(LogInData logInDataObj, AddInServices addInService)
        {
            if (logInDataObj.remember!="True")
            {
                addInService.SetUserSettingsKeyAndValue("username", "");
                addInService.SetUserSettingsKeyAndValue("password", "");
                addInService.SetUserSettingsKeyAndValue("companyCode", "");
                addInService.SetUserSettingsKeyAndValue("remember", "false");

                TaleoAddIn.lastLogInData.companyCode = "";
                TaleoAddIn.lastLogInData.userName = "";
                TaleoAddIn.lastLogInData.password = "";
                TaleoAddIn.lastLogInData.remember = "";
            }
            else
            {
                long currentMiliSec = ApplicationGlobal.CurrentTimeStampInMiliseconds();
                EncryptDecrypt encryptDecrypt = new EncryptDecrypt(ApplicationGlobal.EncryptionKey + currentMiliSec.ToString());
                
                addInService.SetUserSettingsKeyAndValue("username", encryptDecrypt.Encrypt(logInDataObj.userName));
                addInService.SetUserSettingsKeyAndValue("password", encryptDecrypt.Encrypt( logInDataObj.password));
                addInService.SetUserSettingsKeyAndValue("companyCode", encryptDecrypt.Encrypt(logInDataObj.companyCode.ToUpper()));
                addInService.SetUserSettingsKeyAndValue("remember", logInDataObj.remember);
                addInService.SetUserSettingsKeyAndValue("TimeStamp", currentMiliSec.ToString());

                TaleoAddIn.lastLogInData.companyCode = logInDataObj.companyCode.ToUpper();
                TaleoAddIn.lastLogInData.userName = logInDataObj.userName;
                TaleoAddIn.lastLogInData.password = logInDataObj.password;
                TaleoAddIn.lastLogInData.remember = logInDataObj.remember;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkTaleoTermsAndCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(ApplicationGlobal.OracleTermsAndConditionUrl);
        }
    }
}
