using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using Model.RequestFeedback;
using Service.AddIn;
using Util.Utilities;
using Outlook = Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace Service.RequestFeedback
{
    public class RequestFeedbackService
    {
        readonly RequestFeedbackModel _requestFeedbackModel = new RequestFeedbackModel();
        /// <summary>
        /// Send mail to recipents
        /// </summary>
        /// <param name="recipients">recipients email address</param>
        /// <param name="subject">email subject</param>
        /// <param name="messageBody"> email body</param>
        /// <returns>true of false accoring to success</returns>
        public bool SendMail(IEnumerable<string> recipients, string subject, string messageBody)
        {
            try
            {
                foreach (var recipient in recipients)
                {
                    /* send using outlook */
                    var app = new Outlook.Application();
                    Microsoft.Office.Interop.Outlook._MailItem mailItem = app.CreateItem(Outlook.OlItemType.olMailItem);
                    mailItem.To = recipient;
                    mailItem.Subject = subject;
                    mailItem.Body = messageBody;
                    //mailItem.Importance = Outlook.OlImportance.olImportanceHigh;
                    mailItem.Send();//Warning	2	Ambiguity between method 'Microsoft.Office.Interop.Outlook._MailItem.Send()' and non-method 'Microsoft.Office.Interop.Outlook.ItemEvents_10_Event.Send'. Using method group.	D:\workspaces\workspaceTaleo\project\Taleo\TaleoOutlookAddin\TaleoOutlookApp\Service\RequestFeedback\RequestFeedbackService.cs	31	30	Service

                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                return false;
            }
        }
        /// <summary>
        /// Show Contacts Folder as Initial Address List
        /// </summary>
        /// <returns>return selcted folder path</returns>
        public string ShowContactsFolderAsInitialAddressList()
        {
            StringBuilder sb = new StringBuilder();
            var outlookObj = new Outlook.Application();
            try
            {
                var snd = outlookObj.Session.GetSelectNamesDialog();
                var contactsFolder = outlookObj.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts) as Outlook.Folder;
                Outlook.AddressLists addrLists = outlookObj.Session.AddressLists;
                foreach (Outlook.AddressList addrList in addrLists)
                {
                    var testFolder = addrList.GetContactsFolder() as Outlook.Folder;
                    if (testFolder == null) continue;
                    if (contactsFolder == null ||
                        !outlookObj.Session.CompareEntryIDs(contactsFolder.EntryID, testFolder.EntryID)) continue;
                    snd.InitialAddressList = addrList;
                    snd.Display();
                }
                //When ok button is clicked then following method is working.
                Outlook.Recipients recips = snd.Recipients;
                if (recips.Count > 0)
                {
                    foreach (Outlook.Recipient recip in recips)
                    {
                        sb.AppendLine(recip.Address);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                return "";
            }
            return "";
        }
        /// <summary>
        /// split all email address from string
        /// </summary>
        /// <param name="emails"></param>
        /// <returns>return splited emails</returns>
        public IEnumerable<string> SplitEmails(string emails)
        {
            try
            {
                String[] splitEmails = emails.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                return splitEmails;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                return null;
            }
        }
        /// <summary>
        /// Get signature from resource file for request feedback email signature
        /// </summary>
        /// <returns>return signature</returns>
        public string GetSignature()
        {
            try
            {
                AddInServices addInServices = AddInServices.getInstance();
                List<string[]> userSettingsList = addInServices.GetUserSettingsKeyAndValue();
                string signature = "";
                foreach (var userSettingInfo in userSettingsList)
                {
                    if (userSettingInfo[0] == "signature")
                    {
                        signature = userSettingInfo[1];
                    }
                }
                return signature;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
            return "";
        }
        /// <summary>
        /// get selected email body and subject
        /// </summary>
        /// <param name="index"> selected index</param>
        /// <param name="subject"> email subject</param>
        /// <param name="signature"> email signature</param>
        /// <returns>return request feedback model</returns>
        public RequestFeedbackModel GetEmailBodyAndSubject(int index, string subject, string signature)
        {
            switch (index)
            {
                case 0:
                    _requestFeedbackModel.EmailBody = TaleoMsg.EmployeeReviewFeedback + signature.Replace("@", Environment.NewLine);
                    _requestFeedbackModel.EmailSubject = subject;
                    break;
                case 1:
                    _requestFeedbackModel.EmailBody = TaleoMsg.PeerReviewFeedback + signature.Replace("@", Environment.NewLine);
                    _requestFeedbackModel.EmailSubject = subject + " on Employee";
                    break;
                case 2:
                    _requestFeedbackModel.EmailBody = TaleoMsg.ExceptionalJobPerformanceFeedback + signature.Replace("@", Environment.NewLine);
                    _requestFeedbackModel.EmailSubject = "Exceptional Employee Performance" + " (Private)";
                    break;
                default:
                    _requestFeedbackModel.EmailBody = TaleoMsg.ExceptionalJobPerformanceFeedback + signature.Replace("@", Environment.NewLine);
                    _requestFeedbackModel.EmailSubject = subject;
                    break;
            }
            return _requestFeedbackModel;
        }
    }
}
