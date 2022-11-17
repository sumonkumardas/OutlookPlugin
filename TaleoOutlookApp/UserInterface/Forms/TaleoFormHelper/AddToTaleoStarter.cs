using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Word;
using Service.AddIn;
using TaleoOutlookAddin.Forms.AddToTaleo;
using TaleoOutlookAddin.Forms.CustomeMessage;
using Util.ApplicationGlobal;
using System.IO.Compression;
using Util.Utilities;
using Selection = Microsoft.Office.Interop.Outlook.Selection;
using Service.Authentication;
using TaleoOutlookAddin.Forms.BulkResumeUpload;

namespace TaleoOutlookAddin.Forms.TaleoFormHelper
{
    /// <summary>
    /// This Class contains methods for starting AddToTaleo feature
    /// </summary>
    public static class AddToTaleoStarter
    {
        /// <summary>
        /// This method sends Bulk REST for multiple selected emails or zip file.
        /// And it opens AddToTaleo form for single selected email
        /// </summary>
        /// <param name="selections">Selected Emails By Users</param>
        public static void ShowFormCheckingSelection(Selection selections)
        {
            //Create Saving Folder Directory
            string savingFolderPath = ApplicationGlobal.FinalPath + "Email_Information_Folder\\";
            CreateOrReplaceDirectory(savingFolderPath);

            int n = selections.Count;

            //For Single email
            if (n == 1)
            {
                MailItem emailItem = null;
                foreach (var obj in selections)
                {
                    emailItem = obj as MailItem;
                }
                if (emailItem == null) return;

                List<string> validAttachedFilePaths = new List<string>();
                if (emailItem.Attachments.Count != 0)
                {
                    foreach (var obj in emailItem.Attachments)
                    {
                        var attachment = obj as Attachment;
                        if (attachment == null) continue;

                        var ext = Path.GetExtension(attachment.FileName);
                        if (ext == null) continue;
                        var extension = ext.ToLower();

                        if (IsDocumentFile(extension) || IsZippedFile(extension))
                        {
                            string filePath = savingFolderPath + attachment.FileName;
                            attachment.SaveAsFile(filePath);
                            validAttachedFilePaths.Add(filePath);
                        }
                    }
                }

                //Showing AddToTaleoForm for valid file
                if (validAttachedFilePaths.Count != 0)
                {
                    var taleoFormWithAttachment = new AddToTaleoWithAttachment(emailItem, validAttachedFilePaths);
                    taleoFormWithAttachment.ShowDialog();
                }
                else
                {
                    var taleoForm = new AddToTaleo.AddToTaleo(emailItem);
                    taleoForm.ShowDialog();
                }
            }
            //For Multiple Selected Emails
            else
            {
                bool isSuccess = true;

                //Create folder to save the content of all selected email
                string zipMultipleEmailFolderPath = savingFolderPath + "\\ZipMultipleEmail";
                CreateOrReplaceDirectory(zipMultipleEmailFolderPath);

                //Save all contents in the folder
                int i = 0;
                foreach (var obj in selections)
                {
                    var item = obj as MailItem;
                    if (item == null) continue;

                    var email = item;

                    string zipSingleEmailFolderPath = zipMultipleEmailFolderPath + "\\" + (++i) + "\\";
                    CreateOrReplaceDirectory(zipSingleEmailFolderPath);

                    string emailBodyPath = zipSingleEmailFolderPath + "EmailBody.txt";
                    File.Create(emailBodyPath).Dispose();
                    File.WriteAllText(emailBodyPath, email.Body);

                    if (email.Attachments.Count == 0) continue;
                    foreach (var obj2 in email.Attachments)
                    {
                        var attachment = obj2 as Attachment;
                        if (attachment == null) continue;

                        var ext = Path.GetExtension(attachment.FileName);
                        if (ext == null) continue;

                        var extension = ext.ToLower();
                        if (IsZippedFile(extension))
                        {
                            string filePath = savingFolderPath + attachment.FileName;
                            attachment.SaveAsFile(filePath);

                            if (!SendZipToRest(filePath))
                                isSuccess = false;
                        }
                        else if (IsDocumentFile(extension))
                        {
                            string filePath = zipSingleEmailFolderPath + attachment.FileName;
                            attachment.SaveAsFile(filePath);
                        }
                    }
                }

                //Zip the folder where all contents has been saved
                string resultZipFile = savingFolderPath + "Result_Zip_File.zip";
                ZipFile.CreateFromDirectory(zipMultipleEmailFolderPath, resultZipFile);

                if (!SendZipToRest(resultZipFile))
                    isSuccess = false;

                if (isSuccess) TaleoMessageBox.Show(TaleoMsg.AddToTaleoZipRestOkMessage);
            }
        }

        public static bool SendZipToRest(string resultZipFile)
        {
            string currentUrl = "<BASE_URL>/ats/outlook/ats/bulkresumeimport/main.jsp?org=<COMPANY_CODE>&pword=<LOGIN_TOKEN>";
                currentUrl = currentUrl.Replace("<BASE_URL>", TaleoAddIn.serviceURL.Substring(0, TaleoAddIn.serviceURL.IndexOf("/ats")));

                currentUrl = currentUrl.Replace("<COMPANY_CODE>", TaleoAddIn.sessionData.lastLoginData.companyCode);


                String loginToken = TaleoAddIn.sessionData.loginToken;
                AuthenticationService authUtil = Service.Authentication.AuthenticationService.getInstance(TaleoAddIn.serviceURL, ApplicationGlobal.USE_REST);

                TaleoAddIn.sessionData = authUtil.checkAuthentication(TaleoAddIn.sessionData, Util.Enums.AccessDepth.Only_LogInToken, false, true);
            loginToken = TaleoAddIn.sessionData.loginToken;

            //loginToken = USE_REST ? ((LoginTokenResponse)(addInService.LogInTokenREST(sessionData.authToken))).response.loginToken : ((LoginTokenResponse)(addInService.logInTokenSOAP(serviceURL, sessionData.authToken))).response.loginToken;
            //bulk step 3
            currentUrl = currentUrl.Replace("<LOGIN_TOKEN>", loginToken);
            //bulk step 2
            //AddToTaleoMessageForm addToTaleoMessageForm = new AddToTaleoMessageForm(TaleoMsg.WaitingResumeParse);
            //addToTaleoMessageForm.Show();
            //System.Windows.Forms.Application.DoEvents();
            BulkResumeUploadForm taleoFileFeedBackForm = new BulkResumeUploadForm(currentUrl);
            taleoFileFeedBackForm.ShowDialog();

            if (taleoFileFeedBackForm.isValid)
            {
                AddInServices addInServices = AddInServices.getInstance();
                byte[] byteArray = File.ReadAllBytes(resultZipFile);

                string ext = Path.GetExtension(resultZipFile);
                if (ext == null) return false;


                var result = addInServices.BulkResumeUpload_REST(byteArray, Path.GetFileName(resultZipFile), ext,
                    TaleoFormHelper.GetMimeType(ext), Forms.TaleoFormHelper.TaleoFormHelper.GetAuthTokenForRest(), taleoFileFeedBackForm.source, taleoFileFeedBackForm.status, taleoFileFeedBackForm.requisitionID);

                //addToTaleoMessageForm.Close();
                if (result == null || result.isSuccess == false)
                {
                    TaleoMessageBox.Show(TaleoMsg.AddToTaleoParseErrorMsg);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                TaleoMessageBox.Show(TaleoMsg.AddToTaleoParseErrorMsg);
                return false;
            }
        }

        private static void CreateOrReplaceDirectory(string savingFolderPath)
        {
            if (Directory.Exists(savingFolderPath)) Directory.Delete(savingFolderPath, true);
            Directory.CreateDirectory(savingFolderPath);
        }

        public static bool IsZippedFile(string extension)
        {
            return extension == ".zip" || extension == ".zipx" || extension == ".rar" || extension == ".tar";
        }

        public static bool IsDocumentFile(string extension)
        {
            return extension == ".docx" || extension == ".txt" || extension == ".doc" ||
                   extension == ".pdf" || extension == ".rtf" || extension == ".html";
        }

        public static void SendBulkRequestForAllAttachedFileFile(MailItem email)
        {
            bool isSuccess = true;

            string savingFolderPath = ApplicationGlobal.FinalPath + "Email_Information_Folder\\";
            CreateOrReplaceDirectory(savingFolderPath);

            string zipSingleEmailFolderPath = savingFolderPath + "\\ZipSingleEmail\\";
            CreateOrReplaceDirectory(zipSingleEmailFolderPath);

            foreach (var obj2 in email.Attachments)
            {
                var attachment = obj2 as Attachment;
                if (attachment == null) continue;

                var ext = Path.GetExtension(attachment.FileName);
                if (ext == null) continue;

                var extension = ext.ToLower();
                if (IsZippedFile(extension))
                {
                    string filePath = savingFolderPath + attachment.FileName;
                    attachment.SaveAsFile(filePath);

                    if (!SendZipToRest(filePath)) isSuccess = false;
                }
                else if (IsDocumentFile(extension))
                {
                    string filePath = zipSingleEmailFolderPath + attachment.FileName;
                    attachment.SaveAsFile(filePath);
                }
            }
            //Zip the folder where all contents has been saved
            string resultZipFile = savingFolderPath + "Result_Zip_File.zip";
            ZipFile.CreateFromDirectory(zipSingleEmailFolderPath, resultZipFile);

            if (!SendZipToRest(resultZipFile)) isSuccess = false;

            if (isSuccess) TaleoMessageBox.Show(TaleoMsg.AddToTaleoZipRestOkMessage);
        }
    }
}