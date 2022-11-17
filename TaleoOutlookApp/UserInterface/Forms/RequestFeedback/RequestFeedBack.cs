using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Model.RequestFeedback;
using Service.RequestFeedback;
using TaleoOutlookAddin.Forms.CustomeMessage;
using Util.Enums;
using Util.Utilities;
using Exception = System.Exception;

namespace TaleoOutlookAddin.Forms.RequestFeedback
{
    public partial class RequestFeedBack : Form
    {
        #region Property

        public int _changedFlag;
        private int _lastSelectedIndex;
        private bool _isMailSent;
        public bool Inhibit { get; set; }
        RequestFeedbackModel _requestFeedbackModel = new RequestFeedbackModel();
        readonly RequestFeedbackService _requestFeedbackService = new RequestFeedbackService();
        
        #endregion

        #region  Constructor
        public RequestFeedBack()
        {
            InitializeComponent();
        }
        #endregion

        #region Functions

        private void RequestFeedBack_Load(object sender, EventArgs e)
        {
            ddlTaleoMessage.SelectedIndex = 0;
            _lastSelectedIndex = 0;
        }

        private void ddlTaleoMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.None;
            try
            {
                if (Inhibit) return;

                string signature = TaleoFormHelper.TaleoFormHelper.GetSignature();
                signature = !String.IsNullOrEmpty(signature)?signature:GetResourceValueByName("Signature");
                RequestFeedbackModel _requestFeedbackModelObj = new RequestFeedbackModel();
                _requestFeedbackModelObj = _requestFeedbackService.GetEmailBodyAndSubject(_lastSelectedIndex, "", signature);

                if (!String.IsNullOrEmpty(reqFeedbackTemplateTextBox.Text) && _requestFeedbackModelObj.EmailBody != reqFeedbackTemplateTextBox.Text)
                {
                    dr = MessageBox.Show(GetResourceValueByName("RequestFeedback_MessageChange"), GetResourceValueByName("RequestFeedback_MessageTitle"), MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    _changedFlag = 1;
                    switch (dr)
                    {
                        case DialogResult.None:
                        case DialogResult.Yes:
                            _requestFeedbackModel = _requestFeedbackService.GetEmailBodyAndSubject(ddlTaleoMessage.SelectedIndex, ddlTaleoMessage.Text, signature);
                            reqFeedbackTemplateTextBox.Text = _requestFeedbackModel.EmailBody;
                            subjectTextBox.Text = _requestFeedbackModel.EmailSubject;
                            _lastSelectedIndex = ddlTaleoMessage.SelectedIndex;
                            break;
                        case DialogResult.No:
                            Inhibit = true;
                            ddlTaleoMessage.SelectedIndex = _lastSelectedIndex;
                            Inhibit = false;
                            break;
                    }
                }
                else
                {
                    _requestFeedbackModel = _requestFeedbackService.GetEmailBodyAndSubject(ddlTaleoMessage.SelectedIndex, ddlTaleoMessage.Text, signature);
                    reqFeedbackTemplateTextBox.Text = _requestFeedbackModel.EmailBody;
                    subjectTextBox.Text = _requestFeedbackModel.EmailSubject;
                    _lastSelectedIndex = ddlTaleoMessage.SelectedIndex;
                }
               
            }
            catch (Exception ex)
            {
                TaleoMessageBox.Show(ex.Message);
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (subjectTextBox.Text == "")
                {
                    MessageBox.Show(GetResourceValueByName("RequestFeedback_SubjectEmpty"), GetResourceValueByName("RequestFeedback_MessageTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   // TaleoMessageBox.Show(GetResourceValueByName("RequestFeedback_SubjectEmpty"), GetResourceValueByName("RequestFeedback_MessageTitle"), TaleoMsgBoxIcon.Warning, false);
                    return;
                }
                if (toTextBox.Text == "")
                {
                    MessageBox.Show(GetResourceValueByName("RequestFeedback_ToEmpty"), GetResourceValueByName("RequestFeedback_MessageTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //TaleoMessageBox.Show(GetResourceValueByName("RequestFeedback_ToEmpty"));
                    return;
                }
                IEnumerable<string> emails = _requestFeedbackService.SplitEmails(toTextBox.Text);
                bool success = _requestFeedbackService.SendMail(emails, subjectTextBox.Text, reqFeedbackTemplateTextBox.Text);

                if (!success)
                {
                    MessageBox.Show(GetResourceValueByName("RequestFeedback_SendNotSuccess"), GetResourceValueByName("RequestFeedback_MessageTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //TaleoMessageBox.Show(GetResourceValueByName("RequestFeedback_SendNotSuccess"));
                    return;
                }
                MessageBox.Show(GetResourceValueByName("RequestFeedback_SendSuccess"), GetResourceValueByName("RequestFeedback_MessageTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //TaleoMessageBox.Show(GetResourceValueByName("RequestFeedback_SendSuccess"));
                _isMailSent = true;
                Close();
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }

        }

        private void RequestFeedBack_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string signature = TaleoFormHelper.TaleoFormHelper.GetSignature();
                signature = !String.IsNullOrEmpty(signature) ? signature : GetResourceValueByName("Signature");
                RequestFeedbackModel _requestFeedbackModelObj = new RequestFeedbackModel();
                _requestFeedbackModelObj = _requestFeedbackService.GetEmailBodyAndSubject(_lastSelectedIndex, "", signature);

                if (_isMailSent || !String.IsNullOrEmpty(reqFeedbackTemplateTextBox.Text) && _requestFeedbackModelObj.EmailBody == reqFeedbackTemplateTextBox.Text)
                    return;
                DialogResult dr = MessageBox.Show(GetResourceValueByName("RequestFeedback_FormClossing"), GetResourceValueByName("RequestFeedback_MessageTitle"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                e.Cancel = true;
            }

        }

        private void toButton_Click(object sender, EventArgs e)
        {
            try
            {
                toTextBox.Text = _requestFeedbackService.ShowContactsFolderAsInitialAddressList();
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
        }

        private static string GetResourceValueByName(string resourceName)
        {
            return TaleoFormHelper.TaleoFormHelper.GetResourceValueByName(resourceName);
        }

        #endregion
    }
}
