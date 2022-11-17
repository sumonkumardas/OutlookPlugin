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
using Service.Authentication;

namespace TaleoOutlookAddin.Forms.BulkResumeUpload
{
    public partial class BulkResumeUploadForm : Form
    {
        #region Property
        private bool _isStep1 = true;
        private bool _isStep2 = false;
        public string source = null;
        public string status = null;
        public string requisitionID = null;
        public bool isValid = false;
        TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();
        #endregion

        #region Constructor

        public BulkResumeUploadForm(string currentUrl)
        {
            InitializeComponent();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(OnDocumentCompleted);
            LoadInit();
            this.webBrowser.Navigate(currentUrl, "_self", new byte[] { }, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));

        }

        #endregion

        #region All Methodes
        private void LoadInit()
        {
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


            //WireUpButtonEvents();
        }
        protected void OnElementClicked(object sender, EventArgs args)
        {

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
                HtmlDocument doc = webBrowser.Document;
                source = doc.GetElementById("source").GetAttribute("value");
                status = doc.GetElementById("status").GetAttribute("value");
                string rIDInnerHTML = doc.GetElementById("reqCell").InnerHtml;

                if (!string.IsNullOrEmpty(rIDInnerHTML))
                {
                    requisitionID = rIDInnerHTML.Split('(')[1].Trim();
                    requisitionID = requisitionID.Remove(requisitionID.IndexOf(')'));
                    //requisitionID = requisitionID.Replace("]", "");
                }
                else
                    requisitionID = "-1";

                isValid = false;
                if (source == null || source == "-1")
                {
                    TaleoMessageBox.Show("Please select source from dropdown.", "Bulk Resume Upload", Util.Enums.TaleoMsgBoxIcon.Error);
                }

                else if (status == null || status == "-1")
                {
                    TaleoMessageBox.Show("Please select status from dropdown.", "Bulk Resume Upload", Util.Enums.TaleoMsgBoxIcon.Error);
                }

                else if (requisitionID == null || requisitionID == "-1")
                {
                    TaleoMessageBox.Show("Please select requisition.", "Bulk Resume Upload", Util.Enums.TaleoMsgBoxIcon.Error);
                }
                else
                {
                    isValid = true;
                    string currentUrl = "<BASE_URL>/ats/outlook/ats/bulkresumeimport/confirmation.jsp?org=<COMPANY_CODE>&pword=<LOGIN_TOKEN>";
                    currentUrl = currentUrl.Replace("<BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));

                    currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);


                    string loginToken = TaleoAddIn.sessionData.loginToken;
                    AuthenticationService authUtil = Service.Authentication.AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
                    Forms.TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest();
                    TaleoAddIn.sessionData = authUtil.checkAuthentication(TaleoAddIn.sessionData, Util.Enums.AccessDepth.Only_LogInToken, false, true);
                    loginToken = TaleoAddIn.sessionData.loginToken;

                    currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);

                    this.webBrowser.Navigate(currentUrl, "_self", new byte[] { }, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
                    ResumeLayout(true);
                    webBrowser.AllowNavigation = true;
                    _isStep1 = false;
                    _isStep2 = true;
                    btnFileFeedBackNext.Text = @"Finish";


                }


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

            string currentUrl = "<BASE_URL>/ats/outlook/ats/bulkresumeimport/confirmation.jsp.jsp?org=<COMPANY_CODE>&pword=<LOGIN_TOKEN>";
            currentUrl = currentUrl.Replace("<BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));

            currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);


            string loginToken = TaleoAddIn.sessionData.loginToken;
            AuthenticationService authUtil = Service.Authentication.AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);

            TaleoAddIn.sessionData = authUtil.checkAuthentication(TaleoAddIn.sessionData, Util.Enums.AccessDepth.Only_LogInToken, false, true);
            loginToken = TaleoAddIn.sessionData.loginToken;


            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
            _isStep1 = true;
            _isStep2 = false;
        }

        private void btnFIleFeedBackCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string GetFileFeedbackCurrentURL(string emailBody)
        {

            String currentUrl = null;

            return currentUrl;
        }

        protected void WireUpButtonEvents()
        {
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
