using System;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Model.Response;
using TaleoOutlookAddin.Forms.CustomeMessage;
using Exception = System.Exception;
using Util.Utilities;
using System.Reflection;
using System.Threading;
using Application = Microsoft.Office.Interop.Outlook.Application;
using System.IO;
using Util.ApplicationGlobal;

namespace TaleoOutlookAddin.Forms.FileFeedBack
{
    public partial class TaleoFileFeedBackForm : Form
    {
        #region Property
        private bool _isStep1 = true;
        private bool _isStep2 = false;
        private string commonFeedBackMsgHeader = "";


        private string commonFeedbackMsgBody = "";
        private string commonFeedbackMsgFooter = "";
        TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();
        #endregion

        #region Constructor
        public TaleoFileFeedBackForm()
        {
            InitializeComponent();
            LoadInit();
            webBrowser.ScriptErrorsSuppressed = true;
        }

        public TaleoFileFeedBackForm(MailItem item, string selectedText)
        {
            InitializeComponent();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(OnDocumentCompleted);
            commonFeedbackMsgBody = !String.IsNullOrEmpty(selectedText) && selectedText.Length>5 ? selectedText : item.Body;
            fileFeedBackStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("FileFeedback_Step1Title");
            //SetHeaderAndFooter(item);
            LoadInit();
        }

        #endregion

        #region All Methodes
        private void LoadInit()
        {
            btnFileFeedBackBack.Enabled = false;
            fileFeedbackEmailBodyRTB.Text = commonFeedBackMsgHeader + commonFeedbackMsgBody + commonFeedbackMsgFooter;
            if (this.webBrowser == null)
            {
                InitializeWebBrowser();
            }
        }
        private void InitializeWebBrowser()
        {
            webBrowser = new WebBrowser();
            this.SuspendLayout();
            this.webBrowser.Dock = DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(32, 32);
            this.webBrowser.Name = "webBrowserFileFeedbackEmployeeList";
            this.webBrowser.Size = new System.Drawing.Size(512, 300);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.AllowNavigation = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.webBrowser);
            this.Name = "FileFeedbackEmployeeList";
            this.Size = new System.Drawing.Size(412, 283);
            this.webBrowser.Url = new Uri("about: nothing", System.UriKind.Absolute);
        }
        protected void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WireUpButtonEvents();
        }
        protected void OnElementClicked(object sender, EventArgs args)
        {
            HtmlElement htmlElement = sender as HtmlElement;
            try
            {
                string employeeId = GetEmployeeIdFromHtmlCustomAttribute(htmlElement.GetAttribute("customAttribute"));
                string currentUrl = TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats"));
                String cookieString = TaleoFormHelper.TaleoFormHelper.GetCookieString();
                currentUrl += "/ats/outlook/pm/empcomments/doempcomments.jsp?szEmployeeID=" + employeeId;
                this.ResumeLayout(true);
                this.ResumeLayout(false);
                try
                {
                    string customheader = string.IsNullOrEmpty(cookieString) ? "" : string.Format("Cookie: {0}\r\n", cookieString);
            
                    this.webBrowser.Navigate(currentUrl, "_self", new byte[] { }, customheader
                        + string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));

                }
                catch (Exception ex)
                {
                    TaleoMessageBox.Show(TaleoMsg.TaleoCantNavigateCommon + currentUrl);
                    Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                }
                this.ResumeLayout(true);
                this.webBrowser.AllowNavigation = true;
                btnFileFeedBackNext.Text = @"Finish";
                fileFeedBackStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("FileFeedback_Step3Title");
                lblFileFeedBackNotifier.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("FileFeedback_Step3NotifireMessage"); ;
               // pbFileFeedBackBanner.Image = Properties.Resources.file_feedback_step3;
                btnFileFeedBackBack.Enabled = false;
                btnFIleFeedBackCancel.Enabled = false;
                Thread.Sleep(500);
                btnFileFeedBackNext.Enabled = true;
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
            }
        }

        private string GetEmployeeIdFromHtmlCustomAttribute(string customAttrString)
        {
            StringBuilder separator = new StringBuilder(customAttrString);
            separator.Replace("  ", string.Empty);
            separator.Replace("return selectEmployee(", string.Empty);
            separator.Replace("'", string.Empty);
            separator.Replace(")", string.Empty);
            separator.Replace(";", string.Empty);
            customAttrString = separator.ToString();
            string[] empArray = customAttrString.Split(',');
            string empId = empArray[0];
            return empId;
        }

        private void btnFileFeedBackNext_Click(object sender, EventArgs e)
        {
            if (_isStep1)
            {
                btnFileFeedBackNext.Enabled = false;
                ((Control)webBrowser).Enabled = false;
                lblFileFeedBackNotifier.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("FileFeedback_Step2NotifireMessage");
                btnFileFeedBackBack.Enabled = true;
                fileFeedbackEmailBodyRTB.Visible = false;
                //pbFileFeedBackBanner.Image = Properties.Resources.file_feedback_step2;
                fileFeedBackStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("FileFeedback_Step2Title");
                webBrowser.Visible = true;
                String cookieString = TaleoFormHelper.TaleoFormHelper.GetCookieString();
                String currentUrl = GetFileFeedbackCurrentURL(fileFeedbackEmailBodyRTB.Text);
                ResumeLayout(true);
                ResumeLayout(false);
                try
                {
                    string customheader = string.IsNullOrEmpty(cookieString) ? "" : string.Format("Cookie: {0}\r\n", cookieString);
                    webBrowser.Navigate(currentUrl, "_self", new byte[] { }, customheader + string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
                }
                catch (Exception ex)
                {
                    TaleoMessageBox.Show(ex.Message);
                    Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                }
                ResumeLayout(true);
                webBrowser.AllowNavigation = true;
                _isStep1 = false;
                _isStep2 = true;

            }
            else if (_isStep2)
            {
                _isStep1 = false;
                _isStep2 = false;
                Close();
            }
        }

        private void btnFileFeedBackBack_Click(object sender, EventArgs e)
        {
            if (!_isStep2) return;
            lblFileFeedBackNotifier.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("FileFeedback_Step1NotifireMessage");
            fileFeedBackStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("FileFeedback_Step1Title");
            btnFileFeedBackBack.Enabled = false;
            webBrowser.Visible = false;
            fileFeedbackEmailBodyRTB.Visible = true;
            //pbFileFeedBackBanner.Image = Properties.Resources.file_feedback_step1;
            _isStep1 = true;
            _isStep2 = false;
            btnFileFeedBackNext.Enabled = true;
        }

        private void btnFIleFeedBackCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string GetFileFeedbackCurrentURL(string emailBody)
        {

            String currentUrl = null;
            try
            {

                if (!TaleoAddIn.USE_REST)
                {
                    currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("FILE_FEEDBACK").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
                    currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
                }
                else
                {
                    currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/pm/empcomments/main.jsp?org=" + TaleoAddIn.sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>&txtContents=<FEEDBACK_MESSAGE>");
                }

                String loginToken = "";
                loginToken = TaleoAddIn.USE_REST ? ((LoginTokenResponse)(TaleoAddIn.addInService.LogInTokenREST(TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest()))).response.loginToken 
                    : ((LoginTokenResponse)(TaleoAddIn.addInService.logInTokenSOAP(TaleoAddIn.serviceURL, TaleoAddIn.sessionData.authToken))).response.loginToken;
                currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
                currentUrl = currentUrl.Replace("<FEEDBACK_MESSAGE>", HttpUtility.UrlEncode(emailBody));
                return currentUrl;
            }
            catch (Exception ex)
            {

                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation("GetFileFeedbackCurrentURL", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
            }
            return currentUrl;
        }

        protected void WireUpButtonEvents()
        {
            try
            {
                HtmlDocument doc = webBrowser.Document;
                HtmlElement head = doc.GetElementsByTagName("head")[0];
                HtmlElement scriptAttr = doc.CreateElement("script");
                scriptAttr.SetAttribute("text", TaleoInvokedScript.FileFeedBackOnclickRemovalScript);
                head.AppendChild(scriptAttr);
                webBrowser.Document.InvokeScript("removeFileFeedBackOnclick");
                HtmlElementCollection elements = webBrowser.Document.GetElementsByTagName("a");
                for (int i = 0; i < elements.Count; i++)
                {
                    HtmlElement element = elements[i];

                    if (element.Parent != null && element.Parent.TagName.Equals("TD"))
                    {
                        element.AttachEventHandler("onclick", (sender, args) => OnElementClicked(element, EventArgs.Empty));
                    }
                }
                scriptAttr.SetAttribute("text", TaleoInvokedScript.PopUpRemovalScript);
                head.AppendChild(scriptAttr);
                webBrowser.Document.InvokeScript("removePopUpScreen");
                ((Control)webBrowser).Enabled = true;
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
            }
        }

        private string GetUserAgent()
        {
            string userAgent = null;
            userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
                                taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;

            return userAgent;
        }
        #endregion
    }
}
