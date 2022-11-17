using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Model.Response;
using Service.AddIn;
using TaleoOutlookAddin.Forms.CustomeMessage;
using Util.Utilities;
using Util.ApplicationGlobal;

namespace TaleoOutlookAddin.Forms.AddComments
{
    public partial class AddCommentsForm : Form
    {

        //private string _exsistingCandidateID;
        private AuthenticationMessageForm _authenticationMessageForm;
        AddCommentLogMessageForm addCommentLogMessageForm = new AddCommentLogMessageForm();
        private string _comment;
        private bool _step1,_step2;
        TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();
        /// <summary>
        /// constructor
        /// </summary>
        public AddCommentsForm()
        {
            InitializeComponent();
            InitializeInformaion();
            addCommentWebBrowser.ScriptErrorsSuppressed = true;
            if (!TaleoAddIn.USE_REST)
            {
                addCommentWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(OnDocumentCompleted);
            }
        }

        private void InitializeInformaion()
        {
            subtitleLabel.Parent = headerPictureBox;
            subtitleLabel.BackColor = Color.Transparent;
            backButton.Enabled = false;
            addCommentTextBox.Visible = true;
            addCommentWebBrowser.Visible = false;
            _step1 = true;

            subtitleLabel.Text = GetResourceValueByName("AddComments_Subtitle1of3");
            instructionalLabel.Text = GetResourceValueByName("AddComments_Instruction1of3");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string GetResourceValueByName(string resourceName)
        {
            return TaleoFormHelper.TaleoFormHelper.GetResourceValueByName(resourceName);
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (_step1)
            {
                if (!addCommentTextBox.Visible) return;
                if (addCommentTextBox.Text == "")
                {
                    MessageBox.Show(GetResourceValueByName("AddComments_EmptyComment"), GetResourceValueByName("RequestFeedback_MessageTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!TaleoAddIn.USE_REST)
                {
                    TaleoMessageBox.Show(TaleoMsg.AccessOnlyByRest);
                    return;
                }
                addCommentTextBox.Visible = false;
                addCommentWebBrowser.Visible = true;
                backButton.Enabled = true;
                nextButton.Enabled = false;
                _authenticationMessageForm = new AuthenticationMessageForm(GetResourceValueByName("AddComments_LoadCandidateMsg"));
                _authenticationMessageForm.Show();
                Application.DoEvents();

                subtitleLabel.Text = GetResourceValueByName("AddComments_Subtitle2of3");
                instructionalLabel.Text = GetResourceValueByName("AddComments_Instruction2of3");
                _comment = addCommentTextBox.Text;
                _authenticationMessageForm.Close();
                ShowCandidateSelectionPage();
                Thread.Sleep(1000);
                _authenticationMessageForm.Close();
            }
            else if (_step2)
            {
                Close();
            }
            
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (!addCommentWebBrowser.Visible) return;
            addCommentTextBox.Visible = true;
            addCommentWebBrowser.Visible = false;
            backButton.Enabled = false;
            nextButton.Enabled = true;

            subtitleLabel.Text = GetResourceValueByName("AddComments_Subtitle1of3");
            instructionalLabel.Text = GetResourceValueByName("AddComments_Instruction1of3");
        }

        private void ShowCandidateSelectionPage()
        {
            _authenticationMessageForm = new AuthenticationMessageForm(GetResourceValueByName("AddComments_LoadCandidateMsg"));
            _authenticationMessageForm.Show();
            Application.DoEvents();

            try
            {
                string cookieString = TaleoFormHelper.TaleoFormHelper.GetCookieString();
                string currentUrl = TaleoFormHelper.TaleoFormHelper.GetCandidateListURL();
                ShowCandidateInBrowser(currentUrl, cookieString);

                //Adding Event on click of a candidate
                if (TaleoAddIn.USE_REST)
                {
                    addCommentWebBrowser.DocumentCompleted += OnSelectOfCandidate;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name,ex);
            }

        }

        private void ShowCandidateInBrowser(string currentUrl, string cookieString)
        {
            ResumeLayout(true);
            ResumeLayout(false);
            string customheader = string.IsNullOrEmpty(cookieString) ? "" : string.Format("Cookie: {0}\r\n", cookieString);
            addCommentWebBrowser.Navigate(currentUrl, "_self", null, customheader + string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
            addCommentWebBrowser.ResumeLayout(false);
            ResumeLayout(true);
            addCommentWebBrowser.ResumeLayout(true);
        }

        private void OnSelectOfCandidate(object senderElement, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (addCommentWebBrowser.Document == null) return;
            //HtmlElementCollection elements = addCommentWebBrowser.Document.GetElementsByTagName("a");
            //foreach (HtmlElement element in elements)
            //{
            //    element.AttachEventHandler("onclick", (sender, args) => OnElementClicked(element, EventArgs.Empty));
            //}
            WireUpButtonEvents();
            
        }

        private void OnElementClicked(HtmlElement element, EventArgs eventArgs)
        {
            //AddCommentLogMessageForm addCommentLogMessageForm =new AddCommentLogMessageForm();
            

            var elementHtml = element.OuterHtml;
            int begin = elementHtml.IndexOf('(');
            int end = elementHtml.IndexOf(')');
            var subString = elementHtml.Substring(begin, end - begin);
            var stringArr = subString.Split(',');
            int candidateId = Convert.ToInt16(stringArr[0].Replace("(", ""));

            AddCommentUsingREST(candidateId);
        }

        private void AddCommentUsingREST(int candidateId)
        {
            addCommentLogMessageForm = new AddCommentLogMessageForm();
            addCommentLogMessageForm.Show();
            Application.DoEvents();

            AddInServices addInServices = new AddInServices();
            CommentResponse aCommentResponse = addInServices.AddCommentREST(_comment, "CAND", candidateId, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
            if (!aCommentResponse.successResult)
            {
                addCommentLogMessageForm.Close();
                TaleoMessageBox.Show(TaleoMsg.CommentAddFailed);
                return;
            }
            try
            {
                subtitleLabel.Text = GetResourceValueByName("AddComments_Subtitle3of3");
                instructionalLabel.Text = GetResourceValueByName("AddComments_Instruction3of3");
                string path = AppDomain.CurrentDomain.BaseDirectory;
                addCommentWebBrowser.Url = new Uri(Path.Combine(path, TaleoMsg.CustomSuccesUrl));
            }
            catch (Exception)
            {
                
                
            }
            addCommentLogMessageForm.Close();
            backButton.Enabled = false;
            cancelButton.Enabled = false;
            _step2 = true;
            _step1 = false;
            nextButton.Text = @"Finish";
            nextButton.Enabled = true;
        }
        protected void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WireUpButtonEvents();
        }
        protected void WireUpButtonEvents()
        {
            ((Control)addCommentWebBrowser).Enabled = true;
            HtmlDocument doc = addCommentWebBrowser.Document;
            HtmlElement head = doc.GetElementsByTagName("head")[0];
            HtmlElement scriptAttr = doc.CreateElement("script");
            scriptAttr.SetAttribute("text", TaleoInvokedScript.AddressBookOnclickRemovalScript);
            head.AppendChild(scriptAttr);
            addCommentWebBrowser.Document.InvokeScript("removeOnclickAddressBook");
            HtmlElementCollection elements = addCommentWebBrowser.Document.GetElementsByTagName("a");
            for (int i = 0; i < elements.Count; i++)
            {
                HtmlElement el = elements[i];

                if (el.Parent != null && el.Parent.TagName.Equals("TD"))
                {
                    el.AttachEventHandler("onclick", (sender, args) => OnElementClicked(el, EventArgs.Empty));
                }
            }
            scriptAttr.SetAttribute("text", TaleoInvokedScript.PopUpRemovalScript);
            head.AppendChild(scriptAttr);
            addCommentWebBrowser.Document.InvokeScript("removePopUpScreen");
            addCommentWebBrowser.DocumentCompleted += OnSelectOfCandidate;
        }

        private string GetUserAgent()
        {
            string userAgent = null;
            userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
                                taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;

            return userAgent;
        }
    }
}
