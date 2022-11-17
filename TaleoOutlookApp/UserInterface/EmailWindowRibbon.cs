using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using Model.Response;
using TaleoOutlookAddin.Forms.AddressBook;
using TaleoOutlookAddin.Forms.CustomeMessage;
using TaleoOutlookAddin.Forms.Login;
using TaleoOutlookAddin.Forms.TaleoFormHelper;
using Util.Enums;
using Util.Utilities;
using Application = Microsoft.Office.Interop.Outlook.Application;
using Exception = System.Exception;

namespace TaleoOutlookAddin
{
    public partial class EmailWindowRibbon
    {
        public Microsoft.Office.Core.IRibbonUI ribbon;
        private TaleoAddIn taleoAddIn;
        private void EmailWindowRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            taleoAddIn = TaleoAddIn.getInstance();
            taleoAddIn.LogEmailClicked = false;
        }

        private void addressBookButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (!NetworkInterface.GetIsNetworkAvailable() || !TaleoAddIn.CheckForInternetConnection())
            {
                TaleoMessageBox.Show(TaleoMsg.TaleoOffLineMsg);
                return;
            }

            if (!IsAutoLoogedIn())
            {
                LoginForm loginFormObj = new LoginForm();
                loginFormObj.ShowDialog();
                DialogResult result = loginFormObj.DialogResult;
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            bool accessResponse = false;

            if (!TaleoAddIn.USE_REST)
                accessResponse = GetAccessPermisionResponse("Recruit");
            else
            {
                accessResponse = GetAccessPermisionResponse(Modules.AddressBook.ToString());
            }

            if (!accessResponse) return;

            var outlookObj = new Application();
            if (outlookObj.ActiveInspector() == null)
            {
                TaleoMessageBox.Show("Please open new email window.");
                return;
            }
            ShowAddressBookForm();
        }

        private void logEmailToggleButton_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var ol = new Application();
                var activeWindowType = ol.ActiveWindow();


                if (activeWindowType is Inspector)
                {
                    taleoAddIn.LogEmailClicked = !taleoAddIn.LogEmailClicked;
                    taleoAddIn.AddinRibbon = ribbon;
                }
                /* If need to logemail for inbox or sent items in explorer window then uncomment this code*/
                //else if (activeWindowType is Explorer)
                //{
                //    Selection selections = ol.ActiveExplorer().Selection;
                //    string path = ol.ActiveExplorer().CurrentFolder.FolderPath;
                //    var folderName = path.Substring(path.LastIndexOf(("\\")) + 1);
                //    if (folderName == "Inbox")
                //    {
                //        taleoAddIn.InboxLogEmailAction(selections);
                //    }
                //    else if (folderName == "Sent Items")
                //    {
                //        taleoAddIn.SentItemLogEmailActio(selections);
                //    }
                //}
            }
            catch (Exception)
            {
                TaleoMessageBox.Show(TaleoMsg.TaleoSystemErrorMsg);
            }
        }

        #region Helper Function
        private bool IsAutoLoogedIn()
        {
            return TaleoFormHelper.IsAutoLoogedIn();
        }

        public bool GetAccessPermisionResponse(string accessName)
        {
            if (TaleoAddIn.sessionData == null)
                TaleoAddIn.LoadTaleoStarterInfo();
            if (!TaleoAddIn.USE_REST)
            {
                EnableServiceResponse enableServiceResponse =
                    TaleoAddIn.addInService.GetEnableServiceResponseSOAP(TaleoAddIn.serviceURL,
                        TaleoFormHelper.GetAuthTokenForRest());
                return TaleoAddIn.addInService.CheckAccessPermission(accessName, enableServiceResponse.Response);
            }
            else
            {
                EnableServiceResponse response = TaleoAddIn.OrgSettingsResponse;
                if (response == null || !response.SuccessResult)
                {
                    response = TaleoAddIn.addInService.GetEnableServiceResponse_REST(TaleoFormHelper.GetAuthTokenForRest());
                    TaleoAddIn.OrgSettingsResponse = response;
                }
                if (accessName != Modules.None.ToString() && response.SuccessResult)
                {
                    if (accessName == Modules.FileFeedback.ToString())
                    {
                        return response.ResponseObject.response.orgsetting.performAccess;
                    }

                    if (accessName == Modules.RequestFeedback.ToString())
                    {
                        return response.ResponseObject.response.orgsetting.performAccess;
                    }

                    if (accessName == Modules.AddtoTaleo.ToString())
                    {
                        return response.ResponseObject.response.orgsetting.recruitAccess;
                    }

                    if (accessName == Modules.AddressBook.ToString())
                    {
                        return response.ResponseObject.response.orgsetting.recruitAccess;
                    }

                    if (accessName == Modules.LogEmail.ToString())
                    {
                        return response.ResponseObject.response.orgsetting.recruitAccess;
                    }

                }

                return false;
            
            }
        }

        private static void ShowAddressBookForm()
        {
            AddressBookForm addressBookForm = new AddressBookForm();
            addressBookForm.ShowDialog();
        }
        #endregion

        
    }
}
