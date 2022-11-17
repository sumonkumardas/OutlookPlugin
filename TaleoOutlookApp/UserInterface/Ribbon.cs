using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Win32;
using TaleoOutlookAddin.Forms.TaleoFormHelper;
using Service.Authentication;
using System;
using System.Windows.Forms;
using Util.ApplicationGlobal;
using Util.Enums;
using Util.Utilities;
using System.Threading;

namespace TaleoOutlookAddin
{
    [ComVisible(true)]
    public partial class Ribbon
    {
        /// <summary>
        /// To initialize taleo ribbon
        /// </summary>
        public Microsoft.Office.Core.IRibbonUI ribbon;
        private TaleoButtonAction taleoButtonAction = new TaleoButtonAction();
        private void TaleoRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            TaleoAddIn.ARibbon = this;
            if (!String.IsNullOrEmpty(TaleoAddIn.languageFileName))
            {
                taleoButton.Label = TaleoFormHelper.GetResourceValueByName2("TaleoRibbonButton");
                dashboardButton.Label = TaleoFormHelper.GetResourceValueByName2("DashboardRibbonButton");
                taleoLoginButton.Label = TaleoFormHelper.GetResourceValueByName2("LoginRibbonButton");
                requestFeedbackButton.Label = TaleoFormHelper.GetResourceValueByName2("RequestFeedbackRibbonButton");
                fileFeedbackButton.Label = TaleoFormHelper.GetResourceValueByName2("FileFeedbackRibbonButton");
                addToTaleoButton.Label = TaleoFormHelper.GetResourceValueByName2("AddtoTaleoRibbonButton");
                addCommentsButton.Label = TaleoFormHelper.GetResourceValueByName2("CommentsRibbonButton");
                preferencesButton.Label = TaleoFormHelper.GetResourceValueByName2("PreferenceRibbonButton");
                checkForUpdatesButton.Label = TaleoFormHelper.GetResourceValueByName2("UpdateRibbonButton");
                aboutButton.Label = TaleoFormHelper.GetResourceValueByName2("AboutRibbonButton");
            }
        }

        private void taleoButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetTaleoButtoonAction();
            fileFeedbackButton.Enabled = false;
            addToTaleoButton.Enabled = false;
            SetLoginInfoToBrowser();
        }

        private void SetLoginInfoToBrowser()
        {
            try
            {
                AuthenticationService authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
                authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_LogInToken, false, false);

                string currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/resource/main.jsp?org=" + TaleoAddIn.sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>");
                authUtil = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
                authUtil.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Upto_Cookies, false, true);
                String loginToken = TaleoAddIn.sessionData.loginToken;

                currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
                SHDocVw.InternetExplorer browser = new SHDocVw.InternetExplorer();
                browser.Visible = false;
                browser.Navigate(currentUrl);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Unable to set session on IE");
                Logger.WriteLogInformation("Ribbon", "SetLoginInfoToBrowser", Thread.CurrentThread.Name, Ex);
            }

        }

        private void dashboardButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetDashBoardButtoonAction();
            fileFeedbackButton.Enabled = false;
            addToTaleoButton.Enabled = false;
        }

        private void taleoLoginButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetTaleoLoginButtoonAction();
        }
        private void requestFeedbackButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetRequestFeedBackButtoonAction();

        }

        private void fileFeedbackButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetFileFeedbackButtoonAction();

        }

        private void addToTaleoButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetAddToTaleoButtoonAction();
        }

        private void addCommentsButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetAddCommentsButtonAction();
        }

        private void preferencesButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetPreferencesButtonAction();
        }

        private void checkForUpdatesButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetCheckForUpdatesButtonAction();

        }
        private void aboutButton_Click(object sender, RibbonControlEventArgs e)
        {
            taleoButtonAction.GetAboutButtonAction();

        }
    }
}
