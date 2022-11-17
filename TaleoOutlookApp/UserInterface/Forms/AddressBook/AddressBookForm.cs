using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Model.Response;
using TaleoOutlookAddin.Forms.CustomeMessage;
using Util.Utilities;
using Exception = System.Exception;
using Util.ApplicationGlobal;

namespace TaleoOutlookAddin.Forms.AddressBook
{
    public partial class AddressBookForm : Form
    {
        TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();
        public AddressBookForm()
        {
            InitializeComponent();
            wbAddressBook.ScriptErrorsSuppressed = true;
            LoadLanguageResource();
            LoadEmailAddressTextBox();
            GetCandidatesOrContactsList("Candidates");
            wbAddressBook.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(OnDocumentCompleted);
        }
        private void LoadLanguageResource()
        {
            notifierLabel.Text = GetResourceValueByName("Address_NotifierLabelInit");
            displayLabel.Text = GetResourceValueByName("AddressBook_DisplayLabel");
            candidatesRadioButton.Text = GetResourceValueByName("AddressBook_RbCandidates");
            contactsRadioButton.Text = GetResourceValueByName("AddressBook_RbContacts");
            contactsRadioButton.Text = GetResourceValueByName("AddressBook_RbContacts");
            toRadioButton.Text = GetResourceValueByName("AddressBook_RbTo");
            ccRadioButton.Text = GetResourceValueByName("AddressBook_RbCc");
            bccRadioButton.Text = GetResourceValueByName("AddressBook_RbBCc");
            cancelButton.Text = GetResourceValueByName("AddressBook_BtnCancel");
            okButton.Text = GetResourceValueByName("AddressBook_BtnOk");
        }

        private string GetUserAgent()
        {
            string userAgent = null;
            userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
                                taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;

            return userAgent;
        }
        private void GetCandidatesOrContactsList(string addressBookModule)
        {
            try
            {
                this.wbAddressBook.AllowNavigation = true;
                notifierLabel.Text = GetResourceValueByName("Address_NotifierLabel");
                String cookieString = TaleoFormHelper.TaleoFormHelper.GetCookieString();
                String currentUrl = GetUrlCandidatesOrContactsList(addressBookModule);
                this.ResumeLayout(true);
                this.ResumeLayout(false);
                try
                {
                    string customheader = string.IsNullOrEmpty(cookieString) ? "" : string.Format("Cookie: {0}\r\n", cookieString);

                    this.wbAddressBook.Navigate(currentUrl, "_self", new byte[] { }, customheader + string.Format("User-Agent: {0}\r\n", ApplicationGlobal.GetUserAgent()));

                }
                catch (Exception ex)
                {
                    Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "Can't navigate");
                    TaleoMessageBox.Show(ex.Message);
                }
                this.ResumeLayout(true);
                ((Control)wbAddressBook).Enabled = false;
                EnabledDisabledAddressBookControl(true);
            }
            catch (Exception ex)
            {

                Logger.WriteLogInformation("TaleoFormHelper", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "Get Cookie String Failed");
                TaleoMessageBox.Show(ex.Message);
            }
        }

        private string GetUrlCandidatesOrContactsList(string addressBookModule)
        {
            String currentUrl = String.Empty;
            try
            {
                if (!TaleoAddIn.USE_REST)
                {
                    currentUrl = TaleoAddIn.addInService.GetTaleoSettingsValue("ADDRESS_BOOK").Replace("<SOAP_BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));
                    currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);
                }
                else
                {
                    currentUrl = TaleoAddIn.serviceURL.Replace(@"api/v1/", "outlook/ats/addressbook/main.jsp?org=" + TaleoAddIn.sessionData.lastLoginData.companyCode + "&pword=<LOGIN_TOKEN>&szModule=<ADDRESS_BOOK_MODULE>");
                }
                String loginToken = TaleoAddIn.USE_REST ? ((LoginTokenResponse)(TaleoAddIn.addInService.LogInTokenREST(TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest()))).response.loginToken : ((LoginTokenResponse)(TaleoAddIn.addInService.logInTokenSOAP(TaleoAddIn.serviceURL, TaleoAddIn.sessionData.authToken))).response.loginToken;
                currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
                currentUrl = currentUrl.Replace("<ADDRESS_BOOK_MODULE>", addressBookModule);

            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name,
                    ex);
            }
            return currentUrl;
        }

        private void EnabledDisabledAddressBookControl(bool isEnabled)
        {
            toRadioButton.Enabled = isEnabled;
            ccRadioButton.Enabled = isEnabled;
            bccRadioButton.Enabled = isEnabled;
            tbTo.Enabled = isEnabled;
            tbCc.Enabled = isEnabled;
            tbBcc.Enabled = isEnabled;
            okButton.Enabled = isEnabled;
            cancelButton.Enabled = isEnabled;
        }

        private void rbCandidates_Click(object sender, EventArgs e)
        {
            EnabledDisabledAddressBookControl(false);
            GetCandidatesOrContactsList("Candidates");
            ClearSelectedContacts();
        }
        private void rbContacts_Click(object sender, EventArgs e)
        {
            EnabledDisabledAddressBookControl(false);
            GetCandidatesOrContactsList("Contacts");
            ClearSelectedContacts();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        protected void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WireUpButtonEvents();
        }
        protected void WireUpButtonEvents()
        {
            try
            {
                ((Control)wbAddressBook).Enabled = true;
                HtmlDocument doc = wbAddressBook.Document;
                HtmlElement head = doc.GetElementsByTagName("head")[0];
                HtmlElement scriptAttr = doc.CreateElement("script");
                if (candidatesRadioButton.Checked)
                    scriptAttr.SetAttribute("text", TaleoInvokedScript.AddressBookOnclickRemovalScript);
                else
                    scriptAttr.SetAttribute("text", TaleoInvokedScript.AddressBookContactOnclickRemovalScript);
                head.AppendChild(scriptAttr);
                wbAddressBook.Document.InvokeScript("removeOnclickAddressBook");
                HtmlElementCollection elements = wbAddressBook.Document.GetElementsByTagName("a");
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
                wbAddressBook.Document.InvokeScript("removePopUpScreen");
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name,
                    ex);
            }
        }
        protected void OnElementClicked(object sender, EventArgs args)
        {
            HtmlElement el = sender as HtmlElement;
            if (el != null)
            {
                String empString = el.GetAttribute("hello");
                StringBuilder separator = new StringBuilder(empString);
                separator.Replace("selectCand(", string.Empty);
                separator.Replace("'", string.Empty);
                separator.Replace(")", string.Empty);
                separator.Replace(";", string.Empty);
                empString = separator.ToString();
                string[] empArray = empString.Split(',');
                if (candidatesRadioButton.Checked)
                    LoadEmailAddressTextBox(empArray);
                else
                    LoadEmailAddressTextBoxForContacts(empArray);
            }
        }
        private string[] GetCCBCCFromEmail(Microsoft.Office.Interop.Outlook.MailItem email)
        {
            string[] ccBCC = new string[] { "", "" };//cc y bcc
            Microsoft.Office.Interop.Outlook.Recipients recipients = email.Recipients;

            foreach (Microsoft.Office.Interop.Outlook.Recipient item in recipients)
            {
                switch (item.Type)
                {
                    case (int)Microsoft.Office.Interop.Outlook.OlMailRecipientType.olTo:
                        ccBCC[0] += GetEmail(item.AddressEntry) + ";";
                        break;
                    case (int)Microsoft.Office.Interop.Outlook.OlMailRecipientType.olCC:
                        ccBCC[0] += GetEmail(item.AddressEntry) + ";";
                        break;
                    case (int)Microsoft.Office.Interop.Outlook.OlMailRecipientType.olBCC:
                        ccBCC[1] += GetEmail(item.AddressEntry) + ";";
                        break;
                }
            }
            return ccBCC;
        }
        private string GetEmail(Microsoft.Office.Interop.Outlook.AddressEntry address)
        {
            string addressStr = "";

            if (address.AddressEntryUserType ==
                    Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.
                    olExchangeUserAddressEntry
                    || address.AddressEntryUserType ==
                    Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.
                    olExchangeRemoteUserAddressEntry)
            {
                //Use the ExchangeUser object PrimarySMTPAddress
                Microsoft.Office.Interop.Outlook.ExchangeUser exchUser =
                    address.GetExchangeUser();
                if (exchUser != null)
                {
                    addressStr = exchUser.PrimarySmtpAddress;
                }
            }
            //Get the address from externals
            if (address.AddressEntryUserType == Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.
                olSmtpAddressEntry)
            {
                addressStr = address.Address;
            }

            return addressStr;
        }
        private void LoadEmailAddressTextBox()
        {
            MailItem mailItem = Globals.TaleoAddIn.Application.ActiveInspector().CurrentItem as MailItem;
            string[] a = GetCCBCCFromEmail(mailItem);
            var emailItem = mailItem;
            if (emailItem != null)
            {
                tbTo.Text = "";
                tbCc.Text = "";
                tbBcc.Text = "";
                Microsoft.Office.Interop.Outlook.Recipients recips1 = emailItem.Recipients;
                foreach (Microsoft.Office.Interop.Outlook.Recipient item in recips1)
                {
                    switch (item.Type)
                    {
                        case (int)Microsoft.Office.Interop.Outlook.OlMailRecipientType.olTo:
                            if (item.AddressEntry.AddressEntryUserType.ToString() == "olSmtpAddressEntry")
                            {
                                if (item.AddressEntry.Name.Contains("@"))
                                {
                                    tbTo.Text += item.AddressEntry.Name + "; ";
                                }
                                else
                                {
                                    tbTo.Text += item.Name + " <" + item.Address + ">; ";
                                }
                            }
                            else
                                tbTo.Text += item.AddressEntry.Name + "; ";
                            break;
                        case (int)Microsoft.Office.Interop.Outlook.OlMailRecipientType.olCC:
                            if (item.AddressEntry.AddressEntryUserType.ToString() == "olSmtpAddressEntry")
                            {
                                if (item.AddressEntry.Name.Contains("@"))
                                {
                                    tbCc.Text += item.AddressEntry.Name + "; ";
                                }
                                else
                                {
                                    tbCc.Text += item.Name + " <" + item.Address + ">; ";
                                }
                            }
                            else
                                tbCc.Text += item.AddressEntry.Name + "; ";
                            break;
                        case (int)Microsoft.Office.Interop.Outlook.OlMailRecipientType.olBCC:
                            if (item.AddressEntry.AddressEntryUserType.ToString() == "olSmtpAddressEntry")
                            {
                                if (item.AddressEntry.Name.Contains("@"))
                                {
                                    tbBcc.Text += item.AddressEntry.Name + "; ";
                                }
                                else
                                {
                                    tbBcc.Text += item.Name + " <" + item.Address + ">; ";
                                }
                            }
                            else
                                tbBcc.Text += item.AddressEntry.Name + "; ";
                            break;
                    }
                }
            }

        }
        private void LoadEmailAddressTextBox(string[] empArray)
        {
            string toCcBccEmail = "";
            if (empArray != null && empArray.Length > 0)
            {
                toCcBccEmail = empArray[1].Trim() + " <" + empArray[2] + ">; ";
            }
            if (toRadioButton.Checked)
            {
                tbTo.AppendText(toCcBccEmail);
            }
            else if (ccRadioButton.Checked)
            {
                tbCc.AppendText(toCcBccEmail);
            }
            else if (bccRadioButton.Checked)
            {
                tbBcc.AppendText(toCcBccEmail);
            }
        }
        private void LoadEmailAddressTextBoxForContacts(string[] empArray)
        {
            string toCcBccEmail = "";
            if (empArray != null && empArray.Length > 0)
            {
                toCcBccEmail = empArray[2].Trim() + " <" + empArray[1] + ">; ";
            }
            if (toRadioButton.Checked)
            {
                tbTo.AppendText(toCcBccEmail);
            }
            else if (ccRadioButton.Checked)
            {
                tbCc.AppendText(toCcBccEmail);
            }
            else if (bccRadioButton.Checked)
            {
                tbBcc.AppendText(toCcBccEmail);
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                MailItem mailItem = Globals.TaleoAddIn.Application.ActiveInspector().CurrentItem as MailItem;
                if (mailItem != null)
                {
                    mailItem.To = tbTo.Text;
                    mailItem.CC = tbCc.Text;
                    mailItem.BCC = tbBcc.Text;
                }
                Close();
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name,
                    ex);
            }
        }

        private static string GetResourceValueByName(string resourceName)
        {
            return TaleoFormHelper.TaleoFormHelper.GetResourceValueByName(resourceName);
        }

        private void ClearSelectedContacts()
        {
            tbTo.Clear();
            tbCc.Clear();
            tbBcc.Clear();
        }
    }
}
