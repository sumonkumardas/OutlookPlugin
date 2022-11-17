using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Model.Response;
using Service.AddIn;
using TaleoOutlookAddin.Forms.CustomeMessage;
using TaleoOutlookAddin.Forms.TaleoFormHelper;
using Util.ApplicationGlobal;
using Util.Utilities;
using Application = System.Windows.Forms.Application;
using Exception = System.Exception;

namespace TaleoOutlookAddin.Forms.AddToTaleo
{
    /// <summary>
    /// It is the form for email with attachment to add to Taleo
    /// </summary>
    public partial class AddToTaleoWithAttachment : Form
    {
        #region Private Members
        private MailItem _emailItem;
        private List<string> _attachedFiles;
        private ImageList _imageList;
        ParseResumeIntoCandidateResponse _result;
        private int _candidateId;
        private List<string> remainingFiles;
        private AddToTaleoMessageForm _parseWaitingForm;
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
        /// <param name="attachedFiles">Attached Files</param>
        /// <param name="selectedIndex">Selected Index</param>
        /// <param name="refer">Refer name of candidate</param>
        /// <param name="fromTaleoForm">Is called from AddToTaleoAttachment</param>
        public AddToTaleoWithAttachment(MailItem mailItem, List<string> attachedFiles, int selectedIndex = 0, string refer = "", bool fromTaleoForm = false)
        {
            InitializeComponent();
            if (mailItem == null || attachedFiles == null)
            {
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoRemainingAttachmentErrorMsg);
            }
            else
            {
                //Assigning Private Members
                _imageList = new ImageList();
                emailAttachmentsListView.SmallImageList = _imageList;
                _emailItem = mailItem;
                _attachedFiles = attachedFiles;
                candidateTypeComboBox.SelectedIndex = selectedIndex;
                referNameTextBox.Text = refer;
                step3Browser.ScriptErrorsSuppressed = true;
                remainingFiles = new List<string>();

                SetStep1PanelData();
                SetDefaultConfigForForm(fromTaleoForm);
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
            list.Add(firstName + " " + lastName);
            list.Add(address);

            return list;

        }
        #region Step 1 Panel
        private void cencelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (candidateTypeComboBox.SelectedIndex == 0)
            {
                ShowStep2WithFiles(candidateTypeComboBox.SelectedIndex);
            }
            else if (candidateTypeComboBox.SelectedIndex == 1)
            {
                ShowReferralPanel();
            }
            else if (candidateTypeComboBox.SelectedIndex == 2)
            {
                ShowNoAttachmentForm();
            }
            else if (IsNewContact())
            {
                try
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
                catch (Exception ex)
                {
                    Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                    CloseWaitingForm();
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoSelectionErrorMsg);
                    Close();
                    return;
                }
            }
            else if (IsExsistingContact())
            {
                ShowNoAttachmentForm();
            }
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

        #region Step 2 Panel
        private void cancelStep2Button_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void backStep2Button_Click(object sender, EventArgs e)
        {
            if (candidateTypeComboBox.SelectedIndex == 0)
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of5;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step1_5");
                step2.Visible = false;
            }
            else if (candidateTypeComboBox.SelectedIndex == 1)
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer2of6;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step2_6");
                step3.Visible = true;
                step4.Visible = true;
                referral.Visible = true;
            }
            CloseWaitingForm();
        }



        private void nextStep2Button_Click(object sender, EventArgs e)
        {
            if (emailBodyRadio.Checked)
            {
                if (AddAllAttachmentCheckBox.Checked)
                {
                    foreach (ListViewItem listViewItem in emailAttachmentsListView.Items)
                    {
                        if (!listViewItem.Selected)
                        {
                            remainingFiles.Add(listViewItem.Tag.ToString());
                        }
                    }
                }
                ShowNoAttachmentForm();
            }
            else if (emailAttachmentRadio.Checked)
            {
                if (emailAttachmentsListView.SelectedItems.Count == 0)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoFileSelectionErrorMsg);
                    return;
                }
                if (!TaleoFormHelper.TaleoFormHelper.AuthenticateLogin())
                    return;

                //To Do : Check if for this user, his TBE My Settings “Attempt to extract and parse work history and other information” setting is checked
                //To Do : If not checked, parse the contact information of the candidate
                //      else, attempt full data parsing.
                try
                {
                    ParseEmailAttachment();
                }
                catch (Exception ex)
                {
                    Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                    CloseWaitingForm();
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoParseErrorMsg);
                    Close();
                }
                //Showing Step 3
                if (candidateTypeComboBox.SelectedIndex == 0)
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew3of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step3_4");
                else if (candidateTypeComboBox.SelectedIndex == 1)
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer4of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step4_5");
                step3.Visible = true;
            }
            else
            {
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoBodyOrAttachmentErrorMsg);
            }
        }
        #endregion

        #region Step 3 Panel
        private void step3CencelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void step3BackButton_Click(object sender, EventArgs e)
        {
            if (candidateTypeComboBox.SelectedIndex == 0)
            {
                if (emailAttachmentRadio.Checked)
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew2of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step2_4");
                else
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew2of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step2_5");
            }
            else if (candidateTypeComboBox.SelectedIndex == 1)
            {
                if (emailAttachmentRadio.Checked)
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer3of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step3_5");
                else
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer3of6;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step3_6");
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
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoCantDeleteErrMsg);
                Close();
                return;
            }
            CloseWaitingForm();
            step3.Visible = false;
        }
        private void step3AddButton_Click(object sender, EventArgs e)
        {
            //To Do : Make sure that no required fields are left blank. If there are errors, it will be showed on page and candidate shall not be added to the system
            if (candidateTypeComboBox.SelectedIndex == 0)
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew4of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step4_4");
                ShowingFinishInStep3();
                step3Instruction.Text = TaleoMsg.AddToTaleoStep4AddCandidateConfirmation;
            }
            else if (candidateTypeComboBox.SelectedIndex == 1)
            {
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer5of5;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step5_5");
                ShowingFinishInStep3();
                step3Instruction.Text = TaleoMsg.AddToTaleoStep4AddCandidateConfirmation;
            } 
            if (IsNewContact())
            {
                //TODO: Add the new contact using proper rest
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
                // step3Browser.Document.InvokeScript("newContactSumit");

                ShowingFinishInStep3();
                //--- End of submit



                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNewContact3of3;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewContact_Step3");
                step3Instruction.Text = TaleoMsg.AddToTaleoNewContactConfirmInstruc;

                //step3AddButton.Visible = false;
                //step3CencelButton.Enabled = false;
                //step3BackButton.Enabled = false;
                //step3FinishButton.Visible = true;
                //step3FinishButton.Enabled = true;
                // return;
            }
        }
        private void step3FinishButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddInServices addInServices = AddInServices.getInstance();
                if (IsNewContact())
                {
                    ShowWaitingForm(TaleoMsg.WaitingNewLogEntry);
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

                if (AddAllAttachmentCheckBox.Checked)
                {
                    ShowWaitingForm(TaleoMsg.WaitingAddAttachmentToCandidate);
                    foreach (ListViewItem listViewItem in emailAttachmentsListView.Items)
                    {
                        if (!listViewItem.Selected)
                        {
                            remainingFiles.Add(listViewItem.Tag.ToString());
                        }
                    }

                    if (ApplicationGlobal.USE_REST)
                    {
                        foreach (string attachedFile in remainingFiles)
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
                    }
                    else
                    {
                        TaleoMessageBox.Show(TaleoMsg.AddToTaleoRestServiceErrorMsg);
                    }
                    CloseWaitingForm();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                CloseWaitingForm();
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoLogEntryErrMsg);
                Close();
                return;
            }
            Close();
        }
        #endregion

        #region Step 4 Panel
        private void step4FinishButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Referral Panel
        private void referralCencelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void referralBackButton_Click(object sender, EventArgs e)
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of5;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step1_5");
            referral.Visible = false;
            step4.Visible = false;
            step3.Visible = false;
            step2.Visible = false;
        }

        private void referralNextButton_Click(object sender, EventArgs e)
        {
            //To Do : Save referral to the appropiate Web Service
            referral.Visible = false;
            step4.Visible = false;
            step3.Visible = false;
            ShowStep2WithFiles(candidateTypeComboBox.SelectedIndex);
        }

        private void referNameTextBox_TextChanged(object sender, EventArgs e)
        {
            referralNextButton.Enabled = !String.IsNullOrEmpty(referNameTextBox.Text);
        }
        #endregion

        /// <summary>
        /// Methods Associated with AddToTaleoAttacment Form GUI and its private members
        /// </summary>
        #region Helpers Methods
        private string GetUserAgent()
        {
            string userAgent = null;
            userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
                                taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;

            return userAgent;
        }
        private void SetStep1PanelData()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of5;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step1_5");
            senderTextBox.Text = _emailItem.SenderName + " <" + _emailItem.SenderEmailAddress + ">";
            receivedOnTextBox.Text = _emailItem.ReceivedTime.ToString("F");
            subjectTextbox.Text = _emailItem.Subject;
            backButton.Enabled = false;
        }
        private void EnterNewCandidateLog()
        {
            ShowWaitingForm(TaleoMsg.WaitingNewLogEntry);

            AddInServices addInServices = AddInServices.getInstance();

            string email;
            if (ApplicationGlobal.USE_REST)
            {
                email = addInServices.GetCandidateByIdSoap(TaleoAddIn.serviceURL, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(), _candidateId).result;
            }
            else
            {
                email = addInServices.GetCandidateById_REST(_candidateId, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest()).result;
            }

            string utctime = _emailItem.ReceivedTime.ToString("o");
            string encodedString = HttpUtility.UrlEncode(_emailItem.Body);

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
        private void emailAttachmentsListView_Click(object sender, EventArgs e)
        {
            emailAttachmentRadio.Checked = true;
        }
        private void ShowStep2WithFiles(int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                if (emailAttachmentRadio.Checked)
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew2of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step2_4");
                else
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew2of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step2_5");
            }
            else if (selectedIndex == 1)
            {
                if (emailAttachmentRadio.Checked)
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer3of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step3_5");
                else
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer3of6;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step3_6");

            }
            step2Instruction.Text = TaleoMsg.GetAddToTaleoStep2Instruction(_attachedFiles.Count);
            emailAttachmentsListView.Items.Clear();

            //List view for attached file
            foreach (string attachedFile in _attachedFiles)
            {
                ListViewItem item = new ListViewItem(Path.GetFileName(attachedFile));
                item.Tag = attachedFile;

                //Providing Image for file
                string extension = Path.GetExtension(attachedFile);
                if (!_imageList.Images.ContainsKey(extension))
                {
                    Icon iconForFile = Icon.ExtractAssociatedIcon(attachedFile);
                    if (iconForFile != null) _imageList.Images.Add(extension, iconForFile);
                }
                item.ImageKey = extension;
                emailAttachmentsListView.Items.Add(item);
            }

//            if ((_attachedFiles.Count > 1 && emailAttachmentRadio.Checked) || emailBodyRadio.Checked)
            if ((_attachedFiles!= null && _attachedFiles.Count > 1))
                    AddAllAttachmentCheckBox.Visible = true;
            step2.Visible = true;
        }
        private void emailAttachmentRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (candidateTypeComboBox.SelectedIndex == 0)
            {
                if (emailAttachmentRadio.Checked)
                {
                    nextStep2Button.Text = TaleoMsg.ParseStepText;
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew2of4;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step2_4");
                }
                else if (emailBodyRadio.Checked)
                {
                    nextStep2Button.Text = TaleoMsg.NextStepText;
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoNew2of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidate_Step2_5");
                }
            }
            else if (candidateTypeComboBox.SelectedIndex == 1)
            {
                if (emailAttachmentRadio.Checked)
                {
                    nextStep2Button.Text = TaleoMsg.ParseStepText;
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer3of5;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step3_5");
                }
                else if (emailBodyRadio.Checked)
                {
                    nextStep2Button.Text = TaleoMsg.NextStepText;
                    //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer3of6;
                    addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step3_6");
                }
            }
        }
        private void ParseEmailAttachment()
        {
            ShowWaitingForm(TaleoMsg.WaitingResumeParse);

            ((Control)step3Browser).Enabled = false;
            step3AddButton.Enabled = false;

            //Parse Selected Attached File
            ListViewItem selecteditem = emailAttachmentsListView.SelectedItems[0];
            string filePath = selecteditem.Tag.ToString();
            string ext = Path.GetExtension(filePath);

            //Sending Bulk Request For Zip File
            if (AddToTaleoStarter.IsZippedFile(ext))
            {
                CloseWaitingForm();
                Close();
                if (AddAllAttachmentCheckBox.Checked)
                {
                    AddToTaleoStarter.SendBulkRequestForAllAttachedFileFile(_emailItem);
                }
                else
                {
                    AddToTaleoStarter.SendZipToRest(filePath);
                }
                return;
            }

            string base64String = TaleoFormHelper.TaleoFormHelper.EncodeFileTextToBase64(filePath);
            AddInServices addInServices = AddInServices.getInstance();

            //Assign Candidate Id if not Failure
            if (ApplicationGlobal.USE_REST)
                _result = addInServices.ParseResumeIntoCandidate_REST(Convert.FromBase64String(base64String), Path.GetFileName(filePath), ext, TaleoFormHelper.TaleoFormHelper.GetMimeType(ext), TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest());
            else
                _result = addInServices.parseResumeIntoCandidateSOAP(TaleoAddIn.serviceURL, TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(), base64String, candidateTypeComboBox.SelectedIndex == 2 ? referNameTextBox.Text : "", "");

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

            string currentUrl = TaleoFormHelper.TaleoFormHelper.GetUrlToCreateCandidate(_result);
            string cookieString = TaleoFormHelper.TaleoFormHelper.GetCookieString();
            ShowPageInStep3Browser(currentUrl, cookieString);
        }
        private void ShowNoAttachmentForm()
        {
            TaleoButtonAction.ShowNoAttachmentForm(_emailItem, true, _attachedFiles, remainingFiles, candidateTypeComboBox.SelectedIndex, this, referNameTextBox.Text);
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
        private void ShowReferralPanel()
        {
            //headerPanel.BackgroundImage = Properties.Resources.AddToTaleoRefer2of6;
            addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_NewCandidateWithReferral_Step2_6");
            step2.Visible = true;
            step3.Visible = true;
            step4.Visible = true;
            referral.Visible = true;
        }
        private void SetDefaultConfigForForm(bool fromTaleoForm)
        {
            if (fromTaleoForm)
            {
                if (candidateTypeComboBox.SelectedIndex == 0)
                {
                    ShowStep2WithFiles(candidateTypeComboBox.SelectedIndex);
                }
                else if (candidateTypeComboBox.SelectedIndex == 1)
                {
                    ShowReferralPanel();
                }
            }
            step4BackButton.Enabled = false;
            Step4CencelButton.Enabled = false;
            if (referNameTextBox.Text == "") referralNextButton.Enabled = false;
            step3Browser.DocumentCompleted += OnDocumentCompletedStep3Browser;
        }
        private void ShowWaitingForm(string message)
        {
            _parseWaitingForm = new AddToTaleoMessageForm(message);
            _parseWaitingForm.Show();
            Application.DoEvents();
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
        private void OnDocumentCompletedStep3Browser(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            HtmlDocument doc = step3Browser.Document;
            HtmlElement head = doc.GetElementsByTagName("head")[0];
            HtmlElement scriptAttr = doc.CreateElement("script");
            scriptAttr.SetAttribute("text", TaleoInvokedScript.PopUpRemovalScript);
            head.AppendChild(scriptAttr);
            step3Browser.Document.InvokeScript("removePopUpScreen");
            ((Control)step3Browser).Enabled = true;
            scriptAttr.SetAttribute("text", TaleoInvokedScript.AddToTaleoWithAttachmentInvokeHiddenFieldScript);
            head.AppendChild(scriptAttr);
            step3Browser.Document.InvokeScript("addHiddenField");
            step3AddButton.Enabled = true;
            step3AddButton.Enabled = true;
            CloseWaitingForm();
        }
        private void CloseWaitingForm()
        {
            if (_parseWaitingForm != null)
            {
                _parseWaitingForm.Close();
            }
        }
        #endregion

        private void AddToTaleoWithAttachment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_parseWaitingForm != null)
            {
                CloseWaitingForm();
            }
        }

        private void candidateTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsNewContact())
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of3;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_InitialMessage");
            else if (IsExsistingContact())
                //headerPanel.BackgroundImage = Properties.Resources.AddToTaleo1of4;
                addtoTaleoStepsLabel.Text = TaleoFormHelper.TaleoFormHelper.GetResourceValueByName("AddToTaleo_InitialMessage");
        }

        private bool IsExsistingContact()
        {
            return candidateTypeComboBox.SelectedIndex == 4;
        }

        private bool IsNewContact()
        {
            return candidateTypeComboBox.SelectedIndex == 3;
        }

        private void ShowStep1Panel()
        {
            step1.Visible = true;
            step2.Visible = false;
            step3.Visible = false;
            step4.Visible = false;
            referral.Visible = false;
        }
    }
}
