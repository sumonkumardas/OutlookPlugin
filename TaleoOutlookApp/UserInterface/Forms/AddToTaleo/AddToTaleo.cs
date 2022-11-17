using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Word;
using Model.AuthenticateData;
using Model.Response;
using Service.AddIn;
using Service.Authentication;
using TaleoOutlookAddin.Forms.CustomeMessage;
using TaleoOutlookAddin.Forms.TaleoFormHelper;
using Util.ApplicationGlobal;
using Util.Enums;
using Util.Utilities;
using Application = System.Windows.Forms.Application;
using Exception = System.Exception;

namespace TaleoOutlookAddin.Forms.AddToTaleo
{
    /// <summary>
    /// It is the form for email without attachment to add to Taleo
    /// </summary>
    public partial class AddToTaleo : Form
    {
        #region Private Members
        private MailItem _emailItem;
        private ParseResumeIntoCandidateResponse _result;
        private bool _fromAttachedForm;
        private List<string> _attachedFiles;
        private List<string> _remainingFiles;
        private int _attachmentSelectedIndex;
        private string _attachmentRefer;
        private string _exsistingCandidateEmail;
        private int _candidateId;
        private AddToTaleoMessageForm _parseWaitingForm;
        private bool _hasDefaultSearch = true;
        private int _contactId = -1;
        TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();
        #endregion

        #region comment on candidateTypeComboBox.SelectedIndex
        //0 -> new candidate
        //1 -> new candidate with Referral 
        //2 -> existing candidate
        #endregion

        /// <summary>
        /// This constructor is called for email without attachment
        /// </summary>
        /// <param name="mailItem">Selected Email</param>
        public AddToTaleo(MailItem mailItem)
        {
            InitializeComponent();
            if (mailItem == null)
            {
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoLoadEmailErrorMsg);
            }
            else
            {
                _emailItem = mailItem;
                ShowStep1PanelInitial();
                AttachEventsWithBrowser();
            }
        }

        /// <summary>
        /// This Constructor is only called from AddToTaleoWithAttachment
        /// </summary>
        /// <param name="mailItem">Selected Email</param>
        /// <param name="fromAttachedForm">Is called from AddToTaleoWithAttachment</param>
        /// <param name="attachedFiles">Attachment Files Path</param>
        /// <param name="selectedIndex">Selected Index in AddToTaleoWithAttachment Form</param>
        /// <param name="refer">Refer name that selected in AddToTaleoWithAttachment form</param>
        public AddToTaleo(MailItem mailItem, bool fromAttachedForm, List<string> attachedFiles, List<string> remainingFiles, int selectedIndex, string refer = "")
        {
            InitializeComponent();
            if (mailItem == null || attachedFiles == null)
            {
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoLoadEmailErrorMsg);
            }
            else
            {
                _remainingFiles = new List<string>();
                _remainingFiles = remainingFiles;
                _emailItem = mailItem;
                SaveDataFromAttachmentFrom(attachedFiles, selectedIndex, refer);
                ShowStep2PanelFromAttachment();
                AttachEventsWithBrowser();
            }
        }

        #region Step1 Panel
        private void cencelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (candidateTypeComboBox.SelectedIndex == 0)
            {
                ShowStep2Panel();
            }
            else if (candidateTypeComboBox.SelectedIndex == 1)
            {
                ShowReferralPanel();
            }
            else if (candidateTypeComboBox.SelectedIndex == 2)
            {
                ShowStep2PanelForExistingCandidate();
            }
            else if (IsNewContact())
            {
                //--------------------Comment out: Add New Contact To Rest ----------------------------//
                //ShowWaitingForm(TaleoMsg.WaitingNewContactAdd);
                //string[] name = _emailItem.SenderName.Split(' ');
                //string lastName = "";
                //string firstName = "";
                //if (name.Count() == 1)
                //{
                //    lastName = name[0];
                //}
                //else
                //{
                //    firstName = name[0];
                //    lastName = name[1];
                //}

                //AddInServices addInServices = AddInServices.getInstance();

                //UserResponse userresponse = addInServices.GetUserByEmailREST("loginName", TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(),
                //    TaleoAddIn.sessionData.lastLoginData.userName);
                //if (userresponse == null)
                //{
                //    TaleoMessageBox.Show(TaleoMsg.AddToTaleoCanNotAddContact);
                //    Close();
                //    return;
                //}
                //if (userresponse.isSuccess == false)
                //{
                //    TaleoMessageBox.Show(TaleoMsg.AddToTaleoCanNotAddContact);
                //    Close();
                //    return;
                //}

                //var contactResult = addInServices.AddNewContactREST(userresponse, firstName, lastName, _emailItem.SenderEmailAddress, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
                //if (contactResult == null)
                //{
                //    TaleoMessageBox.Show(TaleoMsg.AddToTaleoCanNotAddContact);
                //    Close();
                //    return;
                //}
                //if (contactResult.Success == false)
                //{
                //    TaleoMessageBox.Show(TaleoMsg.AddToTaleoCanNotAddContact);
                //    Close();
                //    return;
                //}
                //int contactId = contactResult.Response.response.contactId;
                //CloseWaitingForm();


                //------------- Dummy Candidate Id for Url-------------------//
                int contactId = -1;

                List<string> list = GetEmailAddressWithFirstnameLastname();

                //TODO: ASNazrul  new add contact
                ShowWaitingForm(TaleoMsg.WaitingPageLoad);
                string currentUrl = TaleoFormHelper.TaleoFormHelper.GetUrlToAddContact(contactId, list);
                step3Browser.Navigate(currentUrl, "_self", null, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
                ShowStep3ForNewContact();
            }
            else if (IsExsistingContact())
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExistingContact2of4 AddToTaleo_NewLog_Step2;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLog_Step2");
                step2Instruction.Text = TaleoMsg.AddToTaleoExistingContactLongContent;
                parseButton.Text = TaleoMsg.NextStepText;
                emailBodyTextbox.Text = _emailItem.Body;
                step2.Visible = true;
            }
        }

        private List<string> GetEmailAddressWithFirstnameLastname()
        {
            MailItem mailItem = _emailItem;
            string[] name = _emailItem.SenderName.Split(' ');
                string lastName = "";
                string firstName = "";
                if (name.Count() == 1)
                {
                    lastName = name[0];
                }
                else
                {
                    firstName = name[0];
                    lastName = name[1];
                }

                string address = _emailItem.Sender.Address;
                List<string> list = new List<string>();
                list.Add(firstName+" "+lastName );
                list.Add(address);

                return list;

        }

        private void ShowStep3ForNewContact()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNewContact2of3;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewContact_Step2");
            step3Instruction.Text = TaleoMsg.AddToTaleoStep3NewContactInstruc;

            //TODO: Show Add Contact Page in step3 Web browser

            step1.Visible = true;
            step2.Visible = true;
            step3.Visible = true;
            step4.Visible = false;
            referral.Visible = false;
        }
        #endregion

        #region Step2 Panel
        private void step2CencelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void step2BackButton_Click(object sender, EventArgs e)
        {
            if (_fromAttachedForm)
            {
                //Back to AddToTaleoWithAttachment Form and closing this form
                TaleoButtonAction.ShowWithAttachmentForm(_emailItem, _attachedFiles, this, _attachmentSelectedIndex, _attachmentRefer, true);
            }
            if (IsExsistingContact())
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of4;
                step2.Visible = false;
                return;
            }
            if (candidateTypeComboBox.SelectedIndex == 1)
            {
                GoBackToReferralPanel();
            }
            else
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_InitialMessage");
                ShowStep1Panel();
            }
            CloseWaitingForm();
        }
        private void parseButton_Click(object sender, EventArgs e)
        {
            if (!TaleoFormHelper.TaleoFormHelper.AuthenticateLogin()) return;

            if (IsExsistingContact())
            {
                //TODO: ASNazrul  select contact
                ShowWaitingForm(TaleoMsg.WaitingPageLoad);
                string currentUrl = TaleoFormHelper.TaleoFormHelper.GetContactListURL();
                step3Browser.Navigate(currentUrl, "_self", null, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
               // headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExistingContact3of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLog_Step3");
                step3Instruction.Text = TaleoMsg.AddToTaleoExistingContactSelect;
                step3.Visible = true;
                step3AddButton.Enabled = false;
                step3Browser.DocumentCompleted += OnSelectOfContact;
                return;
            }

            if (_attachmentSelectedIndex == 2 || candidateTypeComboBox.SelectedIndex == 2)
            {
                try
                {
                    ShowCandidateSelectionPage();
                }
                catch (Exception ex)
                {
                    Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                    CloseWaitingForm();
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoSelectionErrorMsg);
                    Close();
                    return;
                }
                ShowStep3ForExistingCandidate();
                return;
            }

            try
            {
                ParseCandidateBody();
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                CloseWaitingForm();
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoParseErrorMsg);
                Close();
                return;
            }

            if (_fromAttachedForm)
            {
                if (_attachmentSelectedIndex == 0)
                {
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew4of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step4_5");
                }
                else if (_attachmentSelectedIndex == 1)
                {
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer5of6;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step5_6");
                }
            }
            else
            {
                if (candidateTypeComboBox.SelectedIndex == 0)
                {
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew3of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step3_4");

                }
                else if (candidateTypeComboBox.SelectedIndex == 1)
                {
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer4of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step4_5");
                }
            }

            //To Do : Check if for this user, his TBE My Settings “Attempt to extract and parse work history and other information” setting is checked
            //To Do : If not checked, parse the contact information of the candidate
            //        else, attempt full data parsing.
            
            ShowStep3Panel();
        }
        #endregion

        #region Step3 Panel
        private void step3CencelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void step3BackButton_Click(object sender, EventArgs e)
        {
            if (IsExsistingContact())
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExistingContact2of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLog_Step2");
                step3.Visible = false;
                return;
            }

            if (_fromAttachedForm)
            {
                if (_attachmentSelectedIndex == 0) 
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew3of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step3_5");
                else if (_attachmentSelectedIndex == 1)
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer4of6;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step4_6");
                else if (_attachmentSelectedIndex == 2)
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExisting2of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLogExistingCandidate_Step2");
            }
            else if (candidateTypeComboBox.SelectedIndex == 1)
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer3of5;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step3_5");
            }
            else if (candidateTypeComboBox.SelectedIndex == 0)
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew2of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step2_4");
            }
            else if (candidateTypeComboBox.SelectedIndex == 2)
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExisting2of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLog_Step2");
            }
            else if (IsNewContact())
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of3;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewContact_Step1");
                ShowStep1Panel();
                return;
            }
            try
            {
                if (_result != null && _result.dup == 0)
                {
                    if (ApplicationGlobal.USE_REST)
                    {
                        var result = TaleoAddIn.addInService.DeleteCandidate_REST(_result.candidateId, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
                        if (result == null || !result.isSuccess) TaleoMessageBox.Show(TaleoMsg.AddToTaleoCantDeleteErrMsg);
                    }
                    else
                    {
                        var result = TaleoAddIn.addInService.DeleteCandidateSOAP(TaleoAddIn.serviceURL, TaleoAddIn.sessionData.authToken, _result.candidateId);
                        if (result == null || !result.isSuccess) TaleoMessageBox.Show(TaleoMsg.AddToTaleoCantDeleteErrMsg);
                    }
                }
            }
            catch (System.Exception ex)
            {
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoCantDeleteErrMsg);
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
            }
            _hasDefaultSearch = true;
            CloseWaitingForm();
            step3.Visible = false;
        }

        private void ShowStep1Panel()
        {
            step1.Visible = true;
            step2.Visible = false;
            step3.Visible = false;
            step4.Visible = false;
            referral.Visible = false;
        }
        private void step3AddButton_Click(object sender, EventArgs e)
        {
            if (_attachmentSelectedIndex == 2 || candidateTypeComboBox.SelectedIndex == 2)
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExisting4of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLogExistingCandidate_Step4");
                EnterExistingCandidateLog();
                ShowStep4ExistingCandidate();
                return;
            }

            if (IsExsistingContact())
            {
                ShowWaitingForm(TaleoMsg.WaitingNewLogEntry);
                AddInServices addInServices = AddInServices.getInstance();
                string utctime = _emailItem.ReceivedTime.ToString("o");
                string encodedString = HttpUtility.UrlEncode(emailBodyTextbox.Text);

                if (ApplicationGlobal.USE_REST)
                {
                    var result = addInServices.CreateEmailSentLogREST(_contactId, "CTCT", _emailItem.ReceivedTime,
                        encodedString, HttpUtility.UrlEncode(_emailItem.Subject), 3, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
                    //Check Failure
                    if (result == null || result.SuccessResult == false)
                    {
                        CloseWaitingForm();
                        TaleoMessageBox.Show(TaleoMsg.AddToTaleoLogEntryNotAddedErrMsg);
                        Close();
                        return;
                    }
                }

                string loginToken = "";
                loginToken = TaleoAddIn.addInService.LogInTokenREST(TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest()).response.loginToken;

                string currentUrl = TaleoFormHelper.TaleoFormHelper.GetUrlForLogConfirmation(loginToken);
                ShowPageInStep4Browser(currentUrl, "");
                CloseWaitingForm();

                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExistingContact4of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLog_Step4");
                step4Instruction.Text = TaleoMsg.AddToTaleoExistingContactConfirm;
                step4.Visible = true;
                return;
            }

            if (IsNewContact())
            {
                //--- Adding New Contact
                ShowWaitingForm(TaleoMsg.WaitingNewContactAdd);
                string[] name = _emailItem.SenderName.Split(' ');
                string lastName = "";
                string firstName = "";
                if (name.Count() == 1)
                {
                    lastName = name[0];
                }
                else
                {
                    firstName = name[0];
                    lastName = name[1];
                }

                AddInServices addInServices = AddInServices.getInstance();

                UserResponse userresponse = addInServices.GetUserByEmailREST("loginName", TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(),
                    TaleoAddIn.sessionData.lastLoginData.userName);
                if (userresponse == null)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoCanNotAddContact);
                    Close();
                    return;
                }
                if (userresponse.isSuccess == false)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoCanNotAddContact);
                    Close();
                    return;
                }

                var contactResult = addInServices.AddNewContactREST(userresponse, firstName, lastName, _emailItem.SenderEmailAddress, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
                if (contactResult == null)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoCanNotAddContact);
                    Close();
                    return;
                }
                if (contactResult.Success == false)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoCanNotAddContact);
                    Close();
                    return;
                }
                _contactId = contactResult.Response.response.contactId;
                CloseWaitingForm();
                //--- End of addin New Contact

                //--- Browser form submit
                //step3Browser.Document.InvokeScript("newContactSumit");
                //--- End of submit


                ShowingFinishInStep3();
                //TODO: Add the contact using proper rest

               // headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNewContact3of3;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewContact_Step3");
                step3Instruction.Text = TaleoMsg.AddToTaleoNewContactConfirmInstruc;

                //step3AddButton.Visible = false;
                //step3CencelButton.Enabled = false;
                //step3BackButton.Enabled = false;
                //step3FinishButton.Visible = true;
                //step3FinishButton.Enabled = true;

                return;
            }

            if (_fromAttachedForm)
            {
                if (_attachmentSelectedIndex == 0)
                {
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew5of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step5_5");
                    ShowingFinishInStep3();
                    step3Instruction.Text = TaleoMsg.AddToTaleoStep4AddCandidateConfirmation;
                }
                else if (_attachmentSelectedIndex == 1)
                {
                   // headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer6of6;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step6_6");
                    ShowingFinishInStep3();
                    step3Instruction.Text = TaleoMsg.AddToTaleoStep4AddCandidateConfirmation;
                }
            }
            else
            {
                if (candidateTypeComboBox.SelectedIndex == 0)
                {
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew4of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step4_4");
                    ShowingFinishInStep3();
                    step3Instruction.Text = TaleoMsg.AddToTaleoStep4AddCandidateConfirmation;
                }
                else if (candidateTypeComboBox.SelectedIndex == 1)
                {
                   // headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer5of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step5_5");
                    ShowingFinishInStep3();
                    step3Instruction.Text = TaleoMsg.AddToTaleoStep4AddCandidateConfirmation;
                }
            }
        }


        private void step3FinishButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (IsNewContact())
                {
                    ShowWaitingForm(TaleoMsg.WaitingNewLogEntry);
                    AddInServices addInServices = AddInServices.getInstance();
                    string encodedString = HttpUtility.UrlEncode(_emailItem.Body);

                    if (ApplicationGlobal.USE_REST)
                    {
                        var result = addInServices.CreateEmailSentLogREST(_contactId, "CTCT", _emailItem.ReceivedTime, encodedString,
                            HttpUtility.UrlEncode(_emailItem.Subject), 3, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
                        if (result == null || result.SuccessResult == false) MessageBox.Show(TaleoMsg.AddToTaleoLogEntryNotAddedErrMsg);
                    }
                    CloseWaitingForm();
                    Close();
                    return;
                }

                EnterNewCandidateLog();

                if (_fromAttachedForm && (_attachmentSelectedIndex == 0 || _attachmentSelectedIndex == 1) && _remainingFiles.Count > 0)
                {
                    ShowWaitingForm(TaleoMsg.WaitingAddAttachmentToCandidate);

                    AddInServices addInServices = AddInServices.getInstance();
                    foreach (string attachedFile in _remainingFiles)
                    {
                        string ext = Path.GetExtension(attachedFile);
                        string base64String = TaleoFormHelper.TaleoFormHelper.EncodeFileTextToBase64(attachedFile);

                        var result =
                            addInServices.AddAttachmentIntoCandidate_REST(Convert.FromBase64String(base64String),
                                _candidateId, Path.GetFileName(attachedFile), ext,
                                TaleoFormHelper.TaleoFormHelper.GetMimeType(ext), TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());

                        if (result == null || !result.isSuccess)
                        {
                            TaleoMessageBox.Show(TaleoMsg.AddToTaleoRemainingAttachmentErrorMsg);
                            Close();
                            return;
                        }
                    }
                    CloseWaitingForm();
                }
            }
            catch (Exception ex)
            {
                CloseWaitingForm();
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoLogEntryErrMsg);
                Close();
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                return;
            }
            Close();
        }
        #endregion

        #region Step4 Panel
        private void step4FinishButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Referral Panel
        private void referralNextButton_Click(object sender, EventArgs e)
        {
            //To Do : Save referral to the appropiate Web Service
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer3of5;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step3_5");
            emailBodyTextbox.Text = _emailItem.Body;
            referral.Visible = false;
            step4.Visible = false;
            step3.Visible = false;
        }

        private void referralBackButton_Click(object sender, EventArgs e)
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of4;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step1_5");
            referral.Visible = false;
            step4.Visible = false;
            step3.Visible = false;
            step2.Visible = false;
        }

        private void referralCencelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void referNameTextBox_TextChanged(object sender, EventArgs e)
        {
            referralNextButton.Enabled = !String.IsNullOrEmpty(referNameTextBox.Text);
        }
        #endregion

        /// <summary>
        /// Methods Associated with AddToTaleo Form GUI and its private members
        /// </summary>
        #region Helper Methods

        private string GetUserAgent()
        {
            string userAgent = null;
            userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
                                taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;

            return userAgent;
        }
        private void ShowStep1PanelInitial()
        {
            try
            {
                //Populating Step 1 data
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of4;
                candidateTypeComboBox.SelectedIndex = 0;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_InitialMessage");
                senderTextBox.Text = _emailItem.SenderName + " <" + _emailItem.SenderEmailAddress + ">";
                receivedOnTextBox.Text = _emailItem.ReceivedTime.ToString("F");
                subjectTextbox.Text = _emailItem.Subject;
                backButton.Enabled = false;

                //Other Config
                step4BackButton.Enabled = false;
                Step4CencelButton.Enabled = false;
                referralNextButton.Enabled = false;
                step3Browser.ScriptErrorsSuppressed = true;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoLoadEmailErrorMsg);
                Close();
            }
        }
        private void AttachEventsWithBrowser()
        {
            try
            {
                step3Browser.DocumentCompleted += OnDocumentCompletedStep3Browser;
                step4WebBrowser.DocumentCompleted += OnDocumentCompletedStep4Browser;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoAttachEventErrorMsg);
                Close();
            }
        }
        private void SaveDataFromAttachmentFrom(List<string> attachedFiles, int selectedIndex, string refer)
        {
            try
            {
                _fromAttachedForm = true;
                _attachedFiles = attachedFiles;
                _attachmentSelectedIndex = selectedIndex;
                _attachmentRefer = refer;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoSaveDataFromAttachmentErrorMsg);
                Close();
            }
        }
        private void ShowStep2PanelFromAttachment()
        {
            try
            {
                //Populating Step 2 data
                step2Instruction.Text = TaleoMsg.AddToTaleoStep2Instruction;
                if (_attachmentSelectedIndex == 0)
                {
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew3of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step3_5");
                }
                else if (_attachmentSelectedIndex == 1)
                {
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer4of6;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step4_6");
                }
                else if (_attachmentSelectedIndex == 2)
                {
                    AddAllAttachmentCheckBox.Visible = true;
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExisting2of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLogExistingCandidate_Step2");
                    parseButton.Text = TaleoMsg.NextStepText;
                    step2Instruction.Text = TaleoMsg.AddToTaleoStep2InstructionForExistingCandidate;
                }
                else if (_attachmentSelectedIndex == 4)
                {
                    candidateTypeComboBox.SelectedIndex = 4;
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExistingContact2of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLog_Step2");
                    step2Instruction.Text = TaleoMsg.AddToTaleoExistingContactLongContent;
                    parseButton.Text = TaleoMsg.NextStepText;
                }
                emailBodyTextbox.Text = _emailItem.Body;
                step2.Visible = true;

                //Other config
                step4BackButton.Enabled = false;
                Step4CencelButton.Enabled = false;
                step3Browser.ScriptErrorsSuppressed = true;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoSaveDataFromAttachmentErrorMsg);
                Close();
            }
        }
        private void ShowStep2Panel()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew2of4;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step2_4");
            emailBodyTextbox.Text = _emailItem.Body;
            step2.Visible = true;
        }
        private void ShowReferralPanel()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer2of5;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step2_5");
            step2.Visible = true;
            step3.Visible = true;
            step4.Visible = true;
            referral.Visible = true;
        }
        private void ShowStep2PanelForExistingCandidate()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExisting2of4;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLogExistingCandidate_Step2");
            emailBodyTextbox.Text = _emailItem.Body;
            parseButton.Text = TaleoMsg.NextStepText;
            step2Instruction.Text = TaleoMsg.AddToTaleoStep2InstructionForExistingCandidate;
            step2.Visible = true;
        }
        private void GoBackToStep1Panel()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of4;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step1_4");
            step2.Visible = false;
        }
        private void GoBackToReferralPanel()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer2of5;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step2_5");
            step3.Visible = true;
            step4.Visible = true;
            referral.Visible = true;
        }
        private void ShowCandidateSelectionPage()
        {
            ShowWaitingForm(TaleoMsg.WaitingCandidateSelectionPage);
            DisableControlOnStep3();

            string loginToken;
            if (ApplicationGlobal.USE_REST)
                loginToken = TaleoAddIn.addInService.LogInTokenREST(TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest()).response.loginToken;
            else
                loginToken = TaleoAddIn.addInService.logInTokenSOAP(TaleoAddIn.serviceURL, TaleoAddIn.sessionData.authToken).response.loginToken;

            string currentUrl;
            if (ApplicationGlobal.USE_REST)
                currentUrl = TaleoFormHelper.TaleoFormHelper.GetUrlToSelectCandidateRest(loginToken);
            else
                currentUrl = TaleoFormHelper.TaleoFormHelper.GetUrlToSelectCandidateSoap(loginToken);

            string cookieString = TaleoFormHelper.TaleoFormHelper.GetCookieString();
            ShowPageInStep3Browser(currentUrl, cookieString);

            //Adding Event on click of a candidate
            step3Browser.DocumentCompleted += OnSelectOfCandidate;
        }
        private void ShowPageInStep3Browser(string currentUrl, string cookieString)
        {
            ResumeLayout(true);
            ResumeLayout(false);
            string customheader = string.IsNullOrEmpty(cookieString) ? "" : string.Format("Cookie: {0}\r\n", cookieString);

            step3Browser.Navigate(currentUrl, "_self", null, customheader + string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
            step3Browser.ResumeLayout(false);
            ResumeLayout(true);
            step3Browser.ResumeLayout(true);
        }
        private void OnSelectOfCandidate(object senderElement, WebBrowserDocumentCompletedEventArgs e)
        {
            if (step3Browser.Document != null)
            {
                HtmlElementCollection elements = step3Browser.Document.GetElementsByTagName("a");
                foreach (HtmlElement element in elements)
                {
                    element.AttachEventHandler("onclick", (sender, args) => OnElementClicked(element, EventArgs.Empty));
                }
            }
        }
        private void OnSelectOfContact(object senderElement, WebBrowserDocumentCompletedEventArgs e)
        {
            if (step3Browser.Document != null)
            {
                HtmlElementCollection elements = step3Browser.Document.GetElementsByTagName("a");
                foreach (HtmlElement element in elements)
                {
                    element.AttachEventHandler("onclick", (sender, args) => OnElementClicked(element, EventArgs.Empty));
                }
            }
        }
        private void OnElementClicked(HtmlElement element, EventArgs eventArgs)
        {
            var elementHtml = element.OuterHtml;
            int begin = elementHtml.IndexOf('(');
            int end = elementHtml.IndexOf(')');
            var subString = elementHtml.Substring(begin + 1, end - begin);
            var stringArr = subString.Split(',');

            if (IsExsistingContact())
            {
                _contactId = Convert.ToInt32(stringArr[0]);
                _exsistingCandidateEmail = stringArr[2].Replace("'", "");
                step3AddButton.Enabled = true;
                return;
            }
            _candidateId = Convert.ToInt32(stringArr[0]);
            _exsistingCandidateEmail = stringArr[2].Replace("'", "");
            step3AddButton.Enabled = true;
        }
        private void ShowStep3ForExistingCandidate()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoExisting3of4;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLogExistingCandidate_Step3");
            step3Instruction.Text = TaleoMsg.AddToTaleoStep3CandidateSelectionInstruction;
            step3AddButton.Enabled = false;
            step3AddButton.Text = TaleoMsg.NextStepText;
            step3.Visible = true;
        }
        private void ParseCandidateBody()
        {
            ShowWaitingForm(TaleoMsg.WaitingResumeParse);
            DisableControlOnStep3();

            //Parse email body
            if (ApplicationGlobal.USE_REST)
            {
                string filePath = ApplicationGlobal.FinalPath + "EmailBody.txt";
                string ext = Path.GetExtension(filePath);
                File.Create(filePath).Dispose();
                File.WriteAllText(filePath, emailBodyTextbox.Text);

                string base64String = TaleoFormHelper.TaleoFormHelper.EncodeFileTextToBase64(filePath);
                AddInServices addInServices = AddInServices.getInstance();

                //Assign Candidate Id if not Failure
                AuthenticationService authenticationService = AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);
                TaleoAddIn.sessionData = authenticationService.checkAuthentication(TaleoAddIn.sessionData, AccessDepth.Only_AuthToken, false, false);
                _result = addInServices.ParseResumeIntoCandidate_REST(Convert.FromBase64String(base64String), Path.GetFileName(filePath), ext, TaleoFormHelper.TaleoFormHelper.GetMimeType(ext), TaleoAddIn.sessionData.authToken);

                if (_result == null || _result.isSuccess == false)
                {
                    CloseWaitingForm();
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoParseErrorMsg);
                    Close();
                    return;
                }

                //if (_result.dup == 1)
                //{
                //    CloseWaitingForm();
                //    TaleoMessageBox.Show(TaleoMsg.AddToTaleoDuplicateCandidateErrorMsg);
                //    Close();
                //    return;
                //}

                //TODO: New candidate exsisting Resume -- as nazrul
                _candidateId = _result.candidateId;

            }
            else
            {
                string base64String = TaleoFormHelper.TaleoFormHelper.Base64Encode(emailBodyTextbox.Text);
                AddInServices addInServices = AddInServices.getInstance();
                _result = addInServices.parseResumeIntoCandidateSOAP(TaleoAddIn.serviceURL, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(), base64String, candidateTypeComboBox.SelectedIndex == 2 ? referNameTextBox.Text : "", "");

                //Assign Candidate Id if not Failure
                if (_result == null || _result.isSuccess == false)
                {
                    CloseWaitingForm();
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoParseErrorMsg);
                    Close();
                    return;
                }
                _candidateId = _result.candidateId;
            }

            //Show Result In Browser

            string currentUrl = TaleoFormHelper.TaleoFormHelper.GetUrlToCreateCandidate(_result);
            string cookieString = TaleoFormHelper.TaleoFormHelper.GetCookieString();
            ShowPageInStep3Browser(currentUrl, cookieString);
        }
        private void DisableControlOnStep3()
        {
            ((Control)step3Browser).Enabled = false;
            step3AddButton.Enabled = false;
        }
        private void ShowWaitingForm(string message)
        {
            _parseWaitingForm = new AddToTaleoMessageForm(message);
            _parseWaitingForm.Show(this);
            Application.DoEvents();
        }
        private void EnterExistingCandidateLog()
        {
            ShowWaitingForm(TaleoMsg.WaitingNewLogEntry);

            //Calling Soap for Log Entry
            AddInServices addInServices = AddInServices.getInstance();
            string utctime = _emailItem.ReceivedTime.ToString("o");
            string encodedString = HttpUtility.UrlEncode(emailBodyTextbox.Text);

            if (ApplicationGlobal.USE_REST)
            {
                var result = addInServices.CreateEmailSentLogREST(_candidateId, "CAND", _emailItem.ReceivedTime,
                    encodedString, HttpUtility.UrlEncode(_emailItem.Subject), 3, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
                //Check Failure
                if (result == null || result.SuccessResult == false)
                {
                    CloseWaitingForm();
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoLogEntryNotAddedErrMsg);
                    Close();
                    return;
                }
            }
            else
            {
                var result = addInServices.createEmailSentLogSOAP(TaleoAddIn.serviceURL, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(),
                    _exsistingCandidateEmail, encodedString, _emailItem.Subject, utctime);
                //Check Failure
                if (result == null || result.isSuccess == false)
                {
                    CloseWaitingForm();
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoLogEntryNotAddedErrMsg);
                    Close();
                    return;
                }

            }

            if (AddAllAttachmentCheckBox.Checked)
            {
                if (ApplicationGlobal.USE_REST)
                {
                    foreach (string attachedFile in _attachedFiles)
                    {
                        string ext = Path.GetExtension(attachedFile);
                        string base64String = TaleoFormHelper.TaleoFormHelper.EncodeFileTextToBase64(attachedFile);

                        var result = addInServices.AddAttachmentIntoCandidate_REST(Convert.FromBase64String(base64String), _candidateId, Path.GetFileName(attachedFile), ext, TaleoFormHelper.TaleoFormHelper.GetMimeType(ext), TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
                        if (result == null || !result.isSuccess)
                        {
                            TaleoMessageBox.Show(TaleoMsg.AddToTaleoRemainingAttachmentErrorMsg);
                            Close();
                            return;
                        }
                    }
                }
                else
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoRestServiceErrorMsg);
                }
            }

            string cookieString = TaleoFormHelper.TaleoFormHelper.GetCookieString();

            string loginToken;
            if (ApplicationGlobal.USE_REST)
                loginToken = TaleoAddIn.addInService.LogInTokenREST(TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest()).response.loginToken;
            else
                loginToken = TaleoAddIn.addInService.logInTokenSOAP(TaleoAddIn.serviceURL, TaleoAddIn.sessionData.authToken).response.loginToken;

            string currentUrl = TaleoFormHelper.TaleoFormHelper.GetUrlForLogConfirmation(loginToken);
            ShowPageInStep4Browser(currentUrl, cookieString);
            CloseWaitingForm();
        }
        private void ShowPageInStep4Browser(string currentUrl, string cookieString)
        {
            ResumeLayout(true);
            ResumeLayout(false);
            TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();
            string userAgent = null;
            userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
                                taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;
            try
            {
                step4WebBrowser.Navigate(currentUrl, "_self", new byte[] { }, string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));
            }
            catch (System.Exception ex)
            {
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoNotShowConfirmPaeErrMsg);
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
            }
            ResumeLayout(true);
            step4WebBrowser.ResumeLayout(true);
            step4WebBrowser.AllowNavigation = true;

        }
        private void ShowStep3Panel()
        {
            step3Instruction.Text = TaleoMsg.AddToTaleoStep3ReviewInstruction;
            step3.Visible = true;
        }
        private void EnterNewCandidateLog()
        {
            ShowWaitingForm(TaleoMsg.WaitingNewLogEntry);

            AddInServices addInServices = AddInServices.getInstance();

            string email;
            if (ApplicationGlobal.USE_REST)
            {
                email = addInServices.GetCandidateById_REST(_candidateId, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest()).ResponseObject.response.candidate.email;
            }
            else
            {
                email = addInServices.GetCandidateByIdSoap(TaleoAddIn.serviceURL, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(), _candidateId).result;
            }

            string utctime = _emailItem.ReceivedTime.ToString("o");
            string encodedString = HttpUtility.UrlEncode(emailBodyTextbox.Text);

            if (ApplicationGlobal.USE_REST)
            {
                var result = addInServices.CreateEmailSentLogREST(_candidateId, "CAND", _emailItem.ReceivedTime, encodedString,
                    HttpUtility.UrlEncode(_emailItem.Subject), 3, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
                if (result == null || result.SuccessResult == false) MessageBox.Show(TaleoMsg.AddToTaleoLogEntryNotAddedErrMsg);

            }
            else
            {
                var result = addInServices.createEmailSentLogSOAP(TaleoAddIn.serviceURL, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(), email, encodedString, _emailItem.Subject, utctime);
                if (result == null || result.isSuccess == false) MessageBox.Show(TaleoMsg.AddToTaleoLogEntryNotAddedErrMsg);

            }
            CloseWaitingForm();
        }

        private void ShowingFinishInStep3()
        {
            step3AddButton.Visible = false;
            step3FinishButton.Visible = true;
            step3BackButton.Enabled = false;
            step3CencelButton.Enabled = false;

            HtmlElement element = step3Browser.Document.GetElementById("theNameSubmit");
            if (element != null)
            {
                    element.InvokeMember("click");
            }
            else
            {
                element = step3Browser.Document.GetElementById("update");
                if (element != null)
                    element.InvokeMember("click");
            }
        }
        private void OnDocumentCompletedStep3Browser(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = step3Browser.Document;
            HtmlElement head = doc.GetElementsByTagName("head")[0];
            HtmlElement scriptAttr = doc.CreateElement("script");
            scriptAttr.SetAttribute("text", TaleoInvokedScript.PopUpRemovalScript);
            head.AppendChild(scriptAttr);
            step3Browser.Document.InvokeScript("removePopUpScreen");
            ((Control)step3Browser).Enabled = true;

            if (IsNewContact() || (_attachmentSelectedIndex != 2 && candidateTypeComboBox.SelectedIndex != 2))
            {
                scriptAttr.SetAttribute("text", TaleoInvokedScript.AddToTaleoWithAttachmentInvokeHiddenFieldScript);
                head.AppendChild(scriptAttr);
                step3Browser.Document.InvokeScript("addHiddenField");
                step3AddButton.Enabled = true;
            }
            if (IsExsistingContact())
            {
                step3AddButton.Enabled = false;
            }
            else if (_attachmentSelectedIndex == 2 || candidateTypeComboBox.SelectedIndex == 2)
            {
                if (_hasDefaultSearch)
                {
                    _hasDefaultSearch = false;
                    Object[] objectsArray = new object[1];
                    objectsArray[0] = _emailItem.SenderEmailAddress;
                    scriptAttr.SetAttribute("text", TaleoInvokedScript.AddToTaleoInvokeSearchCandidateInvokedScript);
                    head.AppendChild(scriptAttr);
                    step3Browser.Document.InvokeScript("searchCandidateWithEmail", objectsArray);
                }
                else
                {
                    scriptAttr.SetAttribute("text", TaleoInvokedScript.SetWindowLoadNull);
                    head.AppendChild(scriptAttr);
                    step3Browser.Document.InvokeScript("setWindowLoadNull");
                }
            }

            CloseWaitingForm();
        }
        private void OnDocumentCompletedStep4Browser(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = step4WebBrowser.Document;
            HtmlElement head = doc.GetElementsByTagName("head")[0];
            HtmlElement scriptAttr = doc.CreateElement("script");
            scriptAttr.SetAttribute("text", TaleoInvokedScript.PopUpRemovalScript);
            head.AppendChild(scriptAttr);
            step4WebBrowser.Document.InvokeScript("removePopUpScreen");
        }
        private void ShowStep4ExistingCandidate()
        {
            step4Instruction.Text = TaleoMsg.AddToTaleoStep4LogEntryConfirmation;
            step4.Visible = true;
        }
        private void CloseWaitingForm()
        {
            if (_parseWaitingForm != null)
            {
                _parseWaitingForm.Close();
            }
        }
        #endregion

        private void AddToTaleo_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseWaitingForm();
        }

        private void candidateTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (candidateTypeComboBox.SelectedIndex == 0)
            {
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_InitialMessage");
            }
            else if (candidateTypeComboBox.SelectedIndex == 1)
            {
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step1_5");
            }
            else if (IsNewContact())
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of3;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewContact_Step1");
            else if (IsExsistingContact())
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewLog_Step1");
        }

        private bool IsExsistingContact()
        {
            return candidateTypeComboBox.SelectedIndex == 4;
        }

        private bool IsNewContact()
        {
            return candidateTypeComboBox.SelectedIndex == 3;
        }
    }
}
