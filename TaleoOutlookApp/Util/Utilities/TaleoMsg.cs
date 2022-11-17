namespace Util.Utilities
{
    public static class TaleoMsg
    {
        public const string ResourceLanguagePath = "TaleoOutlookAddin.Resources.Language.Taleo";
        public const string TaleoLogInFailureMsg = "Please enter your user name and password. For testing purpose use 'user'/'pass'";
        public const string TaleoLoginCommonFailureMsg = @"""Login Failed""! Please try again.";
        public const string TaleoOffLineMsg = "Please connect to the internet for further process.";
        public const string TaleoSystemErrorMsg = "Try again, and if the problem persists contact your system administrator.";
        public const string NextStepText = "Next >";
        public const string ParseStepText = "Parse >";

        public const string TaleoCantNavigateCommon = "Can't Navigate to ";
    

        /**************Waiting Msg************/
        public const string WaitingCandidateSelectionPage = "Please wait while the candidate selection page is loaded. It may take a few minutes . . .";
        public const string WaitingNewLogEntry = "Please wait while a new log is being added. It may take a few minutes . . .";
        public const string WaitingResumeParse = "Please wait while the resume is being parsed. It may take a few minutes . . .";
        public const string WaitingZipFileForParse = "Please wait while zip file is being parsed. It may take a few minutes . . .";
        public const string WaitingAddAttachmentToCandidate = "Please wait while attachment is being added to candidate record. It may take a few minutes . . .";
        public const string WaitingNewContactAdd = "Please wait while new contact is added.";
        public const string WaitingPageLoad = "Please wait while the page is loading";

        /************Add to Taleo Msg*****/
        public const string TaleoCannotOpenAddToTaleo = "Error: Can not open Add To Taleo form";
        public const string AddToTaleoLoadEmailErrorMsg = "Error: Can not Load email informations.";
        public const string AddToTaleoCantDeleteErrMsg = "Error: Can not delete created candidate.";
        public const string AddToTaleoLogEntryErrMsg = "Error: Taleo Outlook Toolbar can not enter new candidate log.";
        public const string AddToTaleoLogEntryNotAddedErrMsg = "Error: The new log has not been added";
        public const string AddToTaleoNotShowConfirmPaeErrMsg = "Error: Can not show the log comfirmation page.";
        public const string AddToTaleoSelectEmailMsg = "Please select Email to add to Taleo.";
        public const string AddToTaleoSelectEmailAnEmailMsg = "Please select an Email to file request.";
        public const string AddToTaleoSelectEmailOnlyOneEmailMsg = "Please select only one Email to file request.";
        public const string AddToTaleoSelectionErrorMsg = "Taleo Outlook Toolbar can not show candidate selection page. If the problem persists, contact Taleo customer support.";
        public const string AddToTaleoParseErrorMsg = "Taleo Outlook Toolbar cannot parse the selected resume. If the problem persists, contact Taleo customer support.";
        public const string AddToTaleoFileSelectionErrorMsg = "Please select an attachment file to parse";
        public const string AddToTaleoBodyOrAttachmentErrorMsg = "Please select either \'Email body\' or \'Email attachment\' to parse";
        public const string AddToTaleoAttachEventErrorMsg = "Error: Can not Attach Events with Browser.";
        public const string AddToTaleoSaveDataFromAttachmentErrorMsg = "Error: Can not load information from Attachment Form.";
        public const string AddToTaleoStep2Instruction = "Outlook Toolbar assumes that the resume is located in the message body. Make the necessary changes to it if needed, then click “Parse” to continue.";
        public const string AddToTaleoStep2InstructionForExistingCandidate = "To add a new log to the existing candidate in Taleo, Outlook Toolbar will use the email message body as the log content. Make the necessary changes to it if needed, then click \"Next\" to continue.";
        public const string AddToTaleoStep3CandidateSelectionInstruction = "Please select a candidate from the list to whom the new log applies:";
        public const string AddToTaleoStep3ReviewInstruction = "Please review the new candidate information.  Once confirmed, click \"Add\" to add this candidate to Taleo.";
        public const string AddToTaleoStep4LogEntryConfirmation = "A new log was successfully added to the existing candidate in Taleo. Click \"Finish\" to close the dialog.";
        public const string AddToTaleoStep4AddCandidateConfirmation = "A new candidate was successfully added to Taleo. Click \"Finish\" to close the dialog.";
        public const string AddToTaleoRemainingAttachmentErrorMsg = "Error: Can not add remaining attachment to candidate record.";
        public const string AddToTaleoRestServiceErrorMsg = "Error: SOAP Service is unavailable for adding remaining attachment to candidate record.";
        public const string AddToTaleoDuplicateCandidateErrorMsg = "Error: The candidate already exsists.";
        public const string AddToTaleoZipRestOkMessage = "Zip file has succussfully sent to parse";
        public const string TaleoUnreachableMessage = "Taleo is unreachable.";
        public const string PR_SMTP_ADDRESS = "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";


        public const string AddToTaleoStep3NewContactInstruc = "Please review the new contact information. Once confirmed, click “Add” to add this contact to Taleo.";
        public const string AddToTaleoNewContactConfirmInstruc = "A new contact was successfully added to Taleo. Click \"Finish\" to close the dialog.";
        public const string AddToTaleoExistingContactLongContent = "To add a new log to the existing contact in Taleo, Outlook Toolbar will use the email message body as the log content. Make the necessary changes to it if needed, then click \"Next\" to continue.";
        public const string AddToTaleoExistingContactSelect = "Please select a contact from the list to whom the new log applies:";
        public const string AddToTaleoExistingContactConfirm = "A new log was successfully added to the existing contact in Taleo. Click \"Finish\" to close the dialog.";
        public const string AddToTaleoCanNotAddContact = "Error: Cant not add new contact to Taleo.";


        /***************Request FeedBack***************/
        public const string EmployeeReviewFeedback = "Hello [Enter Name Here],\r\n\r\nI am in the process of performing a review for [Enter Employee Name Here] and would like to have your thoughts and comments before I complete the process.  Your feedback is valued and will be kept confidential.\r\n\r\nI appreciate your participation in this review cycle.\r\n\r\n";
        public const string PeerReviewFeedback = "Hello [Enter Name Here],\r\n\r\nAs part of our review process, we like to extend an opportunity for peers to provide input on their working relationships with their colleagues. I would appreciate your thoughts and comments regarding [Enter Employee Name Here]. Your feedback is valued and will be kept confidential.\r\n\r\nI appreciate your participation in this review cycle.\r\n\r\n";
        public const string ExceptionalJobPerformanceFeedback = "Hello [Enter Name Here],\r\n\r\nAs part of our review process, we like to include feedback within our employee review documents. If you have an example of where [Enter Employee Name] has contributed above and beyond their job responsibilities could you please respond with the details and examples. Your feedback is valued and will be kept confidential.\r\n\r\nYour input and participation in the review cycle is appreciated.\r\n\r\n";
        /*********"Accessable only for REST"***********/
        public const string AccessOnlyByRest = "Accessable only for REST";

        /**********Taleo Rest/SOAP/Other Url******/
        public const string CustomSuccesUrl = @"AddCommentSuccessHTML.html";
        public const string CommentAddFailed = @"Comment added failed!!";

        /*****file feedbac*****/
        public const string EmailLengthError = "Email body is too big!!!";
        public static string GetAddToTaleoStep2Instruction(int numberOfAttachment)
        {
            return "To add a new candidate, the Outlook Toolbar needs to parse the resume. Since the selected email contains " + numberOfAttachment + " Taleo compatible attachment(s), please indicate if the resume is contained in the body of the email or if it is an email attachment, then click \"Parse\".";
        }
        /*******Logged Email**********/
        public const string LoggedEmailUserNotExist = @"Failed ""Log Email""! This user may not exist";
        public const string LoggedEmailError = "Error occured during log email";
        public const string LoggedEmailException = @"An exception occur during ""Log Email"" please try again.";
    }
}
