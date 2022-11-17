using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Model.Response;
using Service.HTTP;
using Util.ApplicationGlobal;
using Util.Utilities;
using System.IO;
using Util.Enums;

namespace Service.AddIn
{
    public class AddInServices
    {
        #region Property
        private static HTTPService serviceHTTP = new HTTPService(HttpBaseUrl.IsUseREST(), taleoSetting);

        private static Settings userSetting = null;
        private static Settings taleoSetting = null;
        private static Settings applicationSetting = null;

        private static FileUtil fileUtil = new FileUtil();
        private static EncryptDecrypt encryptDecrypt = new EncryptDecrypt();
        private static Resources resources = new Resources();

        private static AddInServices addInServices = null;
        #endregion

        #region Constructor
        public AddInServices()
        {
            addInServices = this;
        }

        public static AddInServices getInstance()
        {
            if (addInServices == null)
                addInServices = new AddInServices();
            return addInServices;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Set Current usersettings from file
        /// </summary>
        /// <param name="settingsPath">file location of usersettings</param>
        public void SetCurrentUserSettings(string settingsPath)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                userSetting = new Settings(settingsPath);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }
        }

        /// <summary>
        /// Set current application settings
        /// </summary>
        /// <param name="settingsPath">file location of application settings</param>
        public void SetCurrentApplicationSettings(string settingsPath)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                applicationSetting = new Settings(settingsPath);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }
        }

        /// <summary>
        /// Set current taleo settings
        /// </summary>
        /// <param name="settingsPath">file location of taleosettings</param>
        public void SetCurrentTaleoSettings(string settingsPath)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                taleoSetting = new Settings(settingsPath);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }
        }

        /// <summary>
        /// Get service URL for specified company
        /// </summary>
        /// <param name="companyCode">Company code</param>
        /// <returns>Service url of specified company</returns>
        public String getServiceURL(String companyCode)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                ServiceURLResponse response = (ServiceURLResponse)serviceHTTP.makeHTTPRequest_GetBaseURL(companyCode);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel2))
                        Logger.WriteLogInformation(className, methodName, threadId, "Url: "+response.serviceURL);
                    return response.serviceURL;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return "";
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }

        }

        /// <summary>
        /// get list of Taleo settings
        /// </summary>
        /// <param name="path">path of taleo settings</param>
        /// <returns>list of taleo strings</returns>
        public List<String[]> getSetTaleoSettings(String path)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                if (String.IsNullOrEmpty(path))
                {
                    if (!ApplicationGlobal.QA_Environment)
                    {
                        if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel2))
                            Logger.WriteLogInformation(className, methodName, threadId, "Url: " + path);
                        path = "http://tbe.taleo.net/outlook/Toolbar.settings";
                    }
                    else
                    {
                        if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel2))
                            Logger.WriteLogInformation(className, methodName, threadId, "Url: " + path);
                        path = "https://qa.tbe.taleocloud.net/outlook/Toolbar.settings";
                    }
                }

                string oldFileText = File.ReadAllText(ApplicationGlobal.FinalPath + "TaleoSettings.ini");
                taleoSetting = new Settings(ApplicationGlobal.FinalPath + "TaleoSettings.ini", oldFileText);
                string responseText = null;
                string latestpart = null;

                string oldFileDispatcher = null;
                List<string> oldFileComments = new List<string>();
                StringReader reader = null;

                string oldFileVersionText;

                if (!string.IsNullOrEmpty(ApplicationGlobal.SettingsContent))
                    responseText = ApplicationGlobal.SettingsContent;
                else
                {
                    SettingsFileResponse response = (SettingsFileResponse)serviceHTTP.makeHTTPRequest_GetSettingsFile(path);
                    responseText = response.settingsContent;
                }


                string major = this.GetTaleoSettingsValue("MAJOR");
                if (major == "") major = "0";
                string minor = addInServices.GetTaleoSettingsValue("MINOR");
                if (minor == "") minor = "0";
                string revision = addInServices.GetTaleoSettingsValue("REVISION");
                if (revision == "") revision = "0";
                string newVersion = major + "." + minor + "." + revision;

                major = addInServices.GetApplicationSettingsValue("MAJOR");
                if (major == "") major = "0";
                minor = addInServices.GetApplicationSettingsValue("MINOR");
                if (minor == "") minor = "0";
                revision = addInServices.GetApplicationSettingsValue("REVISION");
                if (revision == "") revision = "0";

                //Current Version : Read OLD Settings
                if ((major == "0" && minor == "0" && revision == "0"))
                {
                    latestpart = responseText.Substring(responseText.IndexOf("[LATEST]"), responseText.Length - responseText.IndexOf("[LATEST]"));
                    using (reader = new StringReader(latestpart))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("MAJOR"))
                            {
                                major = line.Trim().Split('=')[1];
                            }

                            if (line.Contains("MINOR"))
                            {
                                minor = line.Trim().Split('=')[1];
                            }

                            if (line.Contains("REVISION"))
                            {
                                revision = line.Trim().Split('=')[1];
                                break;
                            }

                        }
                    }
                }
                string currentVersion = major + "." + minor + "." + revision;

                //Checking and Showing
                bool hasUpdatedVersion = newVersion == currentVersion;



                if (hasUpdatedVersion)
                {
                    try
                    {
                        oldFileVersionText = oldFileText.Substring(oldFileText.IndexOf("[" + newVersion + "]"), oldFileText.Length - oldFileText.IndexOf("[" + newVersion + "]"));


                        if (!string.IsNullOrEmpty(oldFileVersionText))
                        {
                            reader = new StringReader(oldFileVersionText);
                            string line;

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains("DISPATCHER"))
                                {
                                    oldFileDispatcher = line;
                                }
                                if (line.Contains(";"))
                                {
                                    if (!line.StartsWith(";-") && !line.StartsWith(";//")&&line.StartsWith(";"))
                                    {
                                        oldFileComments.Add(line);
                                    }
                                }
                            }


                            if (!string.IsNullOrEmpty(responseText))
                            {
                                string upperLastPart = responseText.Substring(responseText.IndexOf("[" + newVersion + "]"), responseText.Length - responseText.IndexOf("[" + newVersion + "]"));
                                upperLastPart = upperLastPart.Remove(upperLastPart.IndexOf("[LATEST]"));
                                foreach (string text in oldFileComments)
                                {
                                    upperLastPart += text+"\n\n";
                                }

                                latestpart = responseText.Substring(responseText.IndexOf("[LATEST]"), responseText.Length - responseText.IndexOf("[LATEST]"));
                                upperLastPart += latestpart;

                                reader = new StringReader(upperLastPart);

                                line = null;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (line.Contains("DISPATCHER"))
                                    {
                                        upperLastPart.Replace(line, oldFileDispatcher);
                                        break;
                                    }
                                }

                                oldFileText = oldFileText.Remove(oldFileText.IndexOf("[" + newVersion + "]"));
                                oldFileText = oldFileText + upperLastPart;


                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                        Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                    }

                }

                if (!hasUpdatedVersion)
                {

                    string upperLastPart = responseText.Substring(responseText.IndexOf("[" + major + "." + minor + "." + revision + "]"), responseText.Length - responseText.IndexOf("[" + major + "." + minor + "." + revision + "]"));

                    oldFileText = oldFileText.Remove(oldFileText.IndexOf("[LATEST]"));
                    oldFileText = oldFileText + upperLastPart;
                    //File.WriteAllText(ApplicationGlobal.FinalPath + "TaleoSettings.ini",oldFileText);

                }
                if (!string.IsNullOrEmpty(oldFileText) && oldFileText.Contains("[LATEST]"))
                    taleoSetting = new Settings(ApplicationGlobal.FinalPath + "TaleoSettings.ini", oldFileText);
                else
                    taleoSetting = new Settings(ApplicationGlobal.FinalPath + "TaleoSettings.ini", responseText);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return taleoSetting.allSettings;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }

        }

        /// <summary>
        /// Set service URL
        /// </summary>
        /// <param name="serviceURL">service URL</param>
        public void setBaseURL(String serviceURL)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                serviceHTTP.setBaseURL(serviceURL);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }
        }

        #region REST
        /// <summary>
        /// login into Taleo
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>
        /// <param name="companyCode">company code</param>
        /// <returns>LoginResponse based on specified parameter</returns>
        public LoginResponse LogInREST(String userName, String password, String companyCode)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                long currentTimeMillis = (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;

                string digest = "currentTimeMillis:" + currentTimeMillis + ";orgCode:" + companyCode + ";partnerCode:OUTLOOK_PLUGIN"+";userName:"+userName+";";

                EncryptDecrypt encryptDecrypt = new EncryptDecrypt();
                digest = encryptDecrypt.SHA256Encrypt(digest);
                String[][] parameters = new String[6][] { new String[2] { "orgCode", companyCode }, new String[2] { "partnerCode", "OUTLOOK_PLUGIN" }, new String[2] { "currentTimeMillis", currentTimeMillis.ToString() }, new String[2] { "userName", userName }, new String[2] { "password", password }, new String[2] { "digest", digest } };
                //String[][] parameters = new String[3][] { new String[2] { "orgCode", companyCode }, new String[2] { "userName", userName }, new String[2] { "password", password } };
                

                HTTPResponse logInResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("login", parameters, "POST", "application/json", "", null);

                LoginResponse logInResponse;
                if (logInResponseString.isSuccess)
                {
                    logInResponse = (LoginResponse)LoginResponse.ConvertLoginResponseFromJson(logInResponseString.responseString);
                    logInResponse.successResult = true;
                }
                else
                {
                    logInResponse = new LoginResponse { successResult = false, e = logInResponseString.exception };
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return logInResponse;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return new LoginResponse();
            }

        }

        /// <summary>
        /// Add commnet to specific candidate
        /// </summary>
        /// <param name="text">commnet text</param>
        /// <param name="entityType">candidate type</param>
        /// <param name="entityId">candidate ID</param>
        /// <returns>CommentResponse based on specified parameter</returns>
        public CommentResponse AddCommentREST(String text, String entityType, int entityId, string authToken)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                String[][] parameters = new String[3][] { new String[2] { "text", text }, new String[2] { "entityType", entityType }, new String[2] { "entityId", entityId.ToString() } };
                String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };
                string body = @"{""comment"":{""text"":""" + text + @""",""entityType"":""" + entityType + @""",""entityId"":" + entityId + @"}}";
                HTTPResponse commentResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("/object/comment/", null, "POST", "application/json", body, cookies, "application/json");

                CommentResponse commentResponse;
                if (commentResponseString.isSuccess)
                {
                    commentResponse = (CommentResponse)CommentResponse.ConvertCommentResponseFromJson(commentResponseString.responseString);
                    commentResponse.successResult = true;
                }
                else
                {
                    commentResponse = new CommentResponse { successResult = false, e = commentResponseString.exception };
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return commentResponse;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return new CommentResponse();
            }

        }


        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public LoginTokenResponse LogInTokenREST(string authtoken)
        {
            LoginTokenResponse loginTokenResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                //Todo Get Response
                String[][] parameters = null;
                String[][] cookies = new String[1][] { new String[2] { "authToken", authtoken } };

                HTTPResponse logInTokenResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("resources/uiLoginToken", parameters, "GET", "application/json", "", cookies);

                loginTokenResponse = new LoginTokenResponse();
                if (logInTokenResponseString.isSuccess)
                {
                    loginTokenResponse = (LoginTokenResponse)LoginTokenResponse.ConvertLoginTokenResponseFromJson(logInTokenResponseString.responseString);
                    loginTokenResponse.successResult = true;
                }
                else
                {
                    loginTokenResponse = new LoginTokenResponse { successResult = false, e = logInTokenResponseString.exception };
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return loginTokenResponse;
        }

        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        //        public CandidateAttachmentUploadResponse SaveCandidateAttachmentREST(string authtoken, int candidateID, Dictionary<string, object> postParameters)
        //        {
        //            CandidateAttachmentUploadResponse attachmentResponse = null;
        //            string className = this.GetType().Name;
        //            string methodName = MethodBase.GetCurrentMethod().Name;
        //            string threadId = Thread.CurrentThread.Name;

        //            try
        //            {
        //                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
        //                //Todo Get Response
        ////                String[][] parameters = null;
        //                String[][] cookies = new String[1][] { new String[2] { "authToken", authtoken } };

        //                HTTPResponse logInTokenResponseString = (HTTPResponse)serviceHTTP.makeMultipartHTTPRequest(@"https://stgweb1.tbetaleo.com/QANA3/ats/api/v1/object/candidate", candidateID, postParameters, cookies);

        //                attachmentResponse = new CandidateAttachmentUploadResponse();
        //                if (logInTokenResponseString.isSuccess)
        //                {
        //                    attachmentResponse.response = logInTokenResponseString.responseString;
        //                    attachmentResponse.successResult = true;
        //                }
        //                else
        //                {
        //                    attachmentResponse = new CandidateAttachmentUploadResponse { successResult = false, e = logInTokenResponseString.exception };
        //                }
        //                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.WriteLogInformation(className, methodName, threadId, ex);
        //            }

        //            return attachmentResponse;
        //        }

        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public ParseResumeIntoCandidateResponse ParseResumeIntoCandidate_REST(byte[] resume, string filename, string ext, string mimeType, string authtoken)
        {
            ParseResumeIntoCandidateResponse parseResumeResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                Dictionary<string, object> postParameters = new Dictionary<string, object>
                {
                    {"filename", filename},
                    {"fileformat", ext},
                    {"file", new FormUpload.FileParameter(resume, filename , mimeType)}
                };

                String[][] cookies = new String[1][] { new String[2] { "authToken", authtoken } };
                parseResumeResponse = new ParseResumeIntoCandidateResponse(false, 0, 0, null);
                string resultString = serviceHTTP.makeMultipartHTTPRequest(@"object/candidate/resumetocandidate", postParameters, cookies);

                ParseResumeCandidateUrlResponse candidateUrlResponse = ParseResumeCandidateUrlResponse.ConvertResumeCandidateUrlFromJson(resultString);

                int candId = 0;
                if (candidateUrlResponse.response.status.success)
                {
                    candId = GetCandidateById_REST(0, authtoken, candidateUrlResponse.response.response.url, true).ResponseObject.response.candidate.candId;
                    parseResumeResponse.candidateId = candId;
                    parseResumeResponse.dup = resultString == "-1" ? 1 : 0;
                    parseResumeResponse.exception = null;
                    parseResumeResponse.isSuccess = true;
                }
                else
                {
                    parseResumeResponse.isSuccess = false;
                    parseResumeResponse.dup = 0;
                    if (candidateUrlResponse.response.status.detail.errormessage.Contains("Candidate ID") && candidateUrlResponse.response.status.detail.errorcode == "400")
                    {
                        string errorMsg = candidateUrlResponse.response.status.detail.errormessage;
                        candId = Convert.ToInt32(errorMsg.Substring(errorMsg.IndexOf("Candidate ID:") + 14, errorMsg.Length - errorMsg.IndexOf("Candidate ID:") - 14));
                        parseResumeResponse.isSuccess = true;
                        parseResumeResponse.dup = 1;
                    }
                    parseResumeResponse.candidateId = candId;

                    parseResumeResponse.exception = null;

                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception ex)
            {
                parseResumeResponse = new ParseResumeIntoCandidateResponse(false, 0, 0, null);
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return parseResumeResponse;
        }

        /// <summary>
        /// Upload bulk resume
        /// </summary>
        /// <param name="resume">byte array of resume file</param>
        /// <param name="filename">file name of resume</param>
        /// <param name="ext">resume file type</param>
        /// <param name="mimeType">accepted content type of web request</param>
        /// <param name="authtoken">current auth token</param>
        /// <returns></returns>
        public BulkResumeUploadResponse BulkResumeUpload_REST(byte[] resume, string filename, string ext, string mimeType, string authtoken, string source, string status, string requisitionID)
        {
            BulkResumeUploadResponse bulkResumeUploadResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                Dictionary<string, object> postParameters = new Dictionary<string, object>
                {
                    {"filename", filename},
                    {"fileformat", ext},
                    {"file", new FormUpload.FileParameter(resume, filename , mimeType)}
                };

                String[][] cookies = new String[1][] { new String[2] { "authToken", authtoken } };

                

                bulkResumeUploadResponse = new BulkResumeUploadResponse(false, null, null);
                string resultString = serviceHTTP.makeMultipartHTTPRequest(@"object/candidate/bulkresumeimport?requisitionId="+requisitionID+"&statusId="+status+"&sourceId="+source, postParameters, cookies);

                bulkResumeUploadResponse.responseString = resultString;
                bulkResumeUploadResponse.exception = null;
                bulkResumeUploadResponse.isSuccess = resultString.Contains("\"success\":false") ? false : true;
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception ex)
            {
                bulkResumeUploadResponse = new BulkResumeUploadResponse(false, null, null);
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return bulkResumeUploadResponse;
        }

        /// <summary>
        /// Upload single resume
        /// </summary>
        /// <param name="resume">byte array of resume file</param>
        /// <param name="candID">candidate ID</param>
        /// <param name="filename">file name of resume</param>
        /// <param name="ext">resume file type</param>
        /// <param name="mimeType">accepted content type of web request</param>
        /// <param name="authtoken">current auth token</param>
        /// <returns></returns>
        public AttachmentIntoCandidateResponse AddAttachmentIntoCandidate_REST(byte[] resume, int candID, string filename, string ext, string mimeType, string authtoken)
        {
            AttachmentIntoCandidateResponse attachmentIntoCandidateResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                Dictionary<string, object> postParameters = new Dictionary<string, object>
                {
                    {"filename", filename},
                    {"fileformat", ext},
                    {"file", new FormUpload.FileParameter(resume, filename , mimeType)}
                };

                String[][] cookies = new String[1][] { new String[2] { "authToken", authtoken } };
                string response = serviceHTTP.makeMultipartHTTPRequest(@"object/candidate/" + candID + @"/attachment", postParameters, cookies);

                attachmentIntoCandidateResponse = new AttachmentIntoCandidateResponse(true, response, null);
                attachmentIntoCandidateResponse.responseString = response;
                attachmentIntoCandidateResponse.isSuccess = true;
                attachmentIntoCandidateResponse.exception = null;
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return attachmentIntoCandidateResponse;
        }
        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public DeleteCandidateResponse DeleteCandidate_REST(int candidateID, string authToken)
        {
            DeleteCandidateResponse deleteCandidateByIdResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                String[][] parameters = null;
                String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };
                HTTPResponse deleteCandidateByIDResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("object/candidate/" + candidateID, parameters, "DELETE", "application/json", "", cookies);
                deleteCandidateByIdResponse = new DeleteCandidateResponse(false, null, null);
                if (deleteCandidateByIDResponseString.isSuccess)
                {
                    deleteCandidateByIdResponse = (DeleteCandidateResponse)DeleteCandidateResponse.ConvertLoginTokenResponseFromJson(deleteCandidateByIDResponseString.responseString);
                    deleteCandidateByIdResponse.isSuccess = true;
                }
                else
                {
                    deleteCandidateByIdResponse = new DeleteCandidateResponse(false, deleteCandidateByIDResponseString.responseString, deleteCandidateByIDResponseString.exception);
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return deleteCandidateByIdResponse;
        }

        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public CandidateResponse CandidateResponse_REST(string authToken, string email)
        {
            CandidateResponse candidateResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };

                HTTPResponse serviceListResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("object/candidate/search?email=" + email, null, "GET", "application/json", "", cookies);

                if (serviceListResponseString.isSuccess)
                {
                    candidateResponse = (CandidateResponse)CandidateResponse.ConvertLoginTokenResponseFromJson(serviceListResponseString.responseString);
                }
                else
                {
                    candidateResponse = new CandidateResponse(false, 0, serviceListResponseString.exception);
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return candidateResponse;
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return candidateResponse;
        }

        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public GetCandidateByIdResponse GetCandidateById_REST(int candidateID, string authToken, string url = null, bool useFullUrl = false)
        {
            GetCandidateByIdResponse getCandidateByIdResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                String[][] parameters = null;
                String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };
                string functionSpecifiedUrl = null;

                if (string.IsNullOrEmpty(url))
                    functionSpecifiedUrl = "object/candidate/" + candidateID;
                else
                {
                    functionSpecifiedUrl = url;
                }

                HTTPResponse getCandidateByIDResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest(functionSpecifiedUrl, parameters, "GET", "application/json", "", cookies, null, useFullUrl);
                getCandidateByIdResponse = new GetCandidateByIdResponse(false, null, null);
                if (getCandidateByIDResponseString.isSuccess)
                {
                    getCandidateByIdResponse = (GetCandidateByIdResponse)GetCandidateByIdResponse.ConvertLoginTokenResponseFromJson(getCandidateByIDResponseString.responseString);
                    getCandidateByIdResponse.isSuccess = true;
                }
                else
                {
                    getCandidateByIdResponse = new GetCandidateByIdResponse(false, getCandidateByIDResponseString.responseString, getCandidateByIDResponseString.exception);
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return getCandidateByIdResponse;
        }

        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public EmailSentLogResponse CreateEmailSentLog_REST()
        {
            EmailSentLogResponse emailSentLogResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                //Todo Get Response
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return emailSentLogResponse;
        }

        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public CreateEmailLogResponse CreateEmailLog_REST()
        {
            CreateEmailLogResponse createEmailLogResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                //Todo Get Response
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return createEmailLogResponse;
        }

        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public EnableServiceResponse GetEnableServiceResponse_REST(string authToken)
        {
            EnableServiceResponse enableServiceResponse = null;
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };

                HTTPResponse serviceListResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("object/orgsetting", null, "GET", "application/json", "", cookies);

                EnableServiceResponse serviceListResponse = null;
                if (serviceListResponseString.isSuccess)
                {
                    serviceListResponse = (EnableServiceResponse)EnableServiceResponse.ConvertEnableServiceResponseFromJson(serviceListResponseString.responseString);
                }
                else
                {
                    serviceListResponse = new EnableServiceResponse(false, serviceListResponseString.exception, null, null);
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return serviceListResponse;
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, ex);
            }

            return enableServiceResponse;
        }

        #endregion

        /// <summary>
        /// Logout from Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>Logout Response</returns>
        public LogoutResponse logout(String authToken)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };

                HTTPResponse logOutResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("logout", null, "POST", "application/json", "", cookies);

                LogoutResponse logOutResponse = null;
                if (logOutResponseString.isSuccess)
                {
                    logOutResponse = (LogoutResponse)LogoutResponse.ConvertLogoutResponseFromJson(logOutResponseString.responseString);
                    logOutResponse.successResult = true;
                }
                else
                {
                    logOutResponse = new LogoutResponse();
                    logOutResponse.successResult = false;
                    logOutResponse.e = logOutResponseString.exception;
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return logOutResponse;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return new LogoutResponse();
            }

        }

        /// <summary>
        /// Get service List of Taleo
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>ServiceList Response</returns>
        public ServiceListResponse getServiceList(String authToken)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };

                HTTPResponse serviceListResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("object/info", null, "GET", "application/json", "", cookies);

                ServiceListResponse serviceListResponse = null;
                if (serviceListResponseString.isSuccess)
                {
                    serviceListResponse = (ServiceListResponse)ServiceListResponse.ConvertServiceListResponseFromJson(serviceListResponseString.responseString);
                    serviceListResponse.successResult = true;
                }
                else
                {
                    serviceListResponse = new ServiceListResponse();
                    serviceListResponse.successResult = false;
                    serviceListResponse.e = serviceListResponseString.exception;
                }
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return serviceListResponse;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return new ServiceListResponse();
            }
        }

        /// <summary>
        /// Set user settings value from a specified key
        /// </summary>
        /// <param name="key">keyname</param>
        /// <param name="value">value of specified key</param>
        /// <returns>true if value is successfully set</returns>
        public bool SetUserSettingsKeyAndValue(string key, string value)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var flag = userSetting.Set(key, value);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return flag;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return false;
            }
        }

        /// <summary>
        /// Get list of user settings value 
        /// </summary>
        /// <returns>user settings value list</returns>
        public List<string[]> GetUserSettingsKeyAndValue()
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var list = userSetting.Get();
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return list;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }
        }

        /// <summary>
        /// Set taleo settings value 
        /// </summary>
        /// <param name="key">keyname</param>
        /// <param name="value">value of specified key</param>
        /// <returns>true if value is successfully set</returns>
        public bool SetTaleoSettingsKeyAndValue(string key, string value)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var flag = taleoSetting.Set(key, value);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return flag;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return false;
            }
        }

        /// <summary>
        /// Get list of taleo settings value 
        /// </summary>
        /// <returns>user settings value list</returns>
        public List<string[]> GetTaleoSettingsKeyAndValue()
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var list = taleoSetting.Get();
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return list;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }
        }

        /// <summary>
        /// Set application settings value from a specified key
        /// </summary>
        /// <param name="key">keyname</param>
        /// <param name="value">value of specified key</param>
        /// <returns>true if value is successfully set</returns>
        public bool SetApplicationSettingsKeyAndValue(string key, string value)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var flag = applicationSetting.Set(key, value);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return flag;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return false;
            }
        }

        /// <summary>
        /// Get list of Application settings value 
        /// </summary>
        /// <returns>Application settings value list</returns>
        public List<string[]> GetApplicationSettingsKeyAndValue()
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var list = applicationSetting.Get();
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return list;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }
        }

        /// <summary>
        /// Change file encoding system
        /// </summary>
        /// <param name="filePath">existing file path</param>
        /// <param name="newFilePath">new file path</param>
        /// <param name="encoding">encoding system</param>
        public void ChangeFileEncoding(string filePath, string newFilePath, Encoding encoding)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                fileUtil.ChangeFileEncoding(filePath, newFilePath, encoding);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }
        }

        /// <summary>
        /// Encrypt a given string
        /// </summary>
        /// <param name="input">string value which will be encoded</param>
        /// <returns>Returns Encrypted string</returns>
        public string EncryptString(string input)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                string output = encryptDecrypt.Encrypt(input);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return output;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="input">string which needs to decrypt</param>
        /// <returns>Decrypted string</returns>
        public string DecryptString(string input)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                string output = encryptDecrypt.Decrypt(input);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return output;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }
        }

        /// <summary>
        /// write logger information of each class and function
        /// </summary>
        /// <param name="className">class name</param>
        /// <param name="functionName">function name</param>
        /// <param name="threadName">thread name</param>
        /// <param name="logInformations">List of login informations</param>
        /// <returns>true if successfully written</returns>
        public bool WriteLoggerInformation(string className, string functionName, string threadName, string[] logInformations)
        {

            try
            {
                return Logger.WriteLogInformation(className, functionName, threadName, logInformations);
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// write logger information of each class and function
        /// </summary>
        /// <param name="className">class name</param>
        /// <param name="functionName">function name</param>
        /// <param name="threadName">thread name</param>
        /// <param name="logInformation">value of login informations</param>
        /// <returns>true if successfully written</returns>
        public bool WriteLoggerInformation(string className, string functionName, string threadName, string logInformation)
        {
            try
            {
                return Logger.WriteLogInformation(className, functionName, threadName, logInformation);
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// write logger information with exception of each class and function
        /// </summary>
        /// <param name="className">class name</param>
        /// <param name="functionName">function name</param>
        /// <param name="threadName">thread name</param>
        /// <param name="exception">exception</param>
        /// <returns>true if successfully written</returns>
        public bool WriteLoggerInformation(string className, string functionName, string threadName, Exception exception)
        {
            try
            {
                return Logger.WriteLogInformation(className, functionName, threadName, exception);
            }
            catch (Exception ex)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Check whether the resource is exists or not
        /// </summary>
        /// <param name="fileName">name of the resource file</param>
        /// <returns>true if resource found</returns>
        public bool IsResourceExist(string fileName)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var flag = resources.IsResourceExist(fileName);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return flag;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return false;
            }
        }

        /// <summary>
        /// Get the latest verson of resource
        /// </summary>
        /// <param name="fileName">name of the resource file</param>
        /// <returns>string value of latest version</returns>
        public string ResourceLatestVersion(string fileName)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var output = resources.LatestVersion(fileName);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return output;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }
        }

        /// <summary>
        /// Get the all resource info as a nested list
        /// </summary>
        /// <param name="source">2D array of source path</param>
        /// <param name="checkUpdate">needed to check update</param>
        /// <param name="checkNew">check for new</param>
        /// <returns>nested list of resource info</returns>
        public List<List<string>> ResourceInfo(string[,] source, bool checkUpdate, bool checkNew)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var list = resources.ResourceInfo(source, checkUpdate, checkNew);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return list;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }
        }

        /// <summary>
        /// Add a resource
        /// </summary>
        /// <param name="fileName">file name of resource</param>
        /// <returns>true if successfully added</returns>
        public bool AddResource(string fileName)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                var flag = resources.AddResource(fileName);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return flag;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return false;
            }
        }

        /// <summary>
        /// set looger file path
        /// </summary>
        /// <param name="logFilePath">path of the logger file</param>
        public void SetLoggerPath(String logFilePath)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                Logger.FilePath = logFilePath;
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }
        }

        /// <summary>
        /// Set resource path
        /// </summary>
        /// <param name="resourceFilePath">path of resource</param>
        public void SetResourcePath(String resourceFilePath)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                resources.ResourcesPath = resourceFilePath;
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }
        }

        /// <summary>
        /// Set source file path
        /// </summary>
        /// <param name="sourceFilePath">source file path name</param>
        public void SetsourcePath(String sourceFilePath)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                resources.SourcePath = sourceFilePath;
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }
        }

        /// <summary>
        /// Get taleo settings value of a key
        /// </summary>
        /// <param name="key">key name of taleo settings</param>
        /// <returns>value of the specified key</returns>
        public String GetTaleoSettingsValue(string key)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                String value = taleoSetting.getValue(key);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return value;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }

        }

        /// <summary>
        /// Get service URL via rest API call
        /// </summary>
        /// <param name="dispatcher">base URL of Taleo</param>
        /// <param name="companyCode">company code</param>
        /// <returns>service URL of specified company</returns>
        public string getServiceURL(string dispatcher, string companyCode)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                ServiceURLResponse response = (ServiceURLResponse)serviceHTTP.makeHTTPRequest_GetBaseURL(dispatcher, companyCode);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return response.serviceURL;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return "";
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }

        }

        /// <summary>
        /// Get service url via SOAP api call
        /// </summary>
        /// <param name="dispatcher">base URL</param>
        /// <param name="companyCode">company code</param>
        /// <returns>service URL of spcified company</returns>
        public string getServiceURLSOAP(string dispatcher, string companyCode)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                ServiceURLResponse response = (ServiceURLResponse)serviceHTTP.makeHTTPRequest_GetBaseURLSOAP(dispatcher, companyCode);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return response.serviceURL;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return "";
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }
        }

        /// <summary>
        /// Get service list via soap call
        /// </summary>
        /// <param name="dispatcher">base url</param>
        /// <param name="authToken">auth token during login</param>
        /// <returns>service list of Taleo</returns>
        public string getServiceListSOAP(string dispatcher, string authToken)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                NewServiceListResponse response = (NewServiceListResponse)serviceHTTP.makeHTTPRequest_GetServiceURLSOAP(dispatcher, authToken);

                if (response.successResult)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return response.response;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return "";
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }
        }

        /// <summary>
        /// Login to Taleo via soap call
        /// </summary>
        /// <param name="dispatcher">base URL</param>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>
        /// <param name="companyCode">company code</param>
        /// <returns>LoginResponse of Login request</returns>
        public LoginResponse logInSOAP(string dispatcher, String userName, String password, String companyCode)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            LoginResponse logInResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse logInResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest_LoginSOAP(dispatcher, companyCode, userName, password);

                if (logInResponseString.isSuccess)
                {
                    logInResponse = LoginResponse.ConvertLoginResponseFromXMl(logInResponseString.responseString);
                    logInResponse.successResult = true;
                }
                else
                {
                    logInResponse = new LoginResponse
                    {
                        response = new LoginResponse_Response(),
                        status = new LoginResponse_Status { detail = new LoginResponse_Detail() }
                    };

                    logInResponse.response.authToken = "";
                    logInResponse.status.success = false;
                    logInResponse.successResult = false;
                    logInResponse.status.detail.errormessage = logInResponseString.responseString;
                    logInResponse.e = logInResponseString.exception;
                }
            }
            catch (Exception e)
            {
                logInResponse = new LoginResponse();
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                logInResponse.response = new LoginResponse_Response();
                logInResponse.status = new LoginResponse_Status { detail = new LoginResponse_Detail() };

                logInResponse.response.authToken = "";
                logInResponse.status.success = false;
                logInResponse.successResult = false;
                logInResponse.status.detail.errormessage = e.Message;
                logInResponse.e = e;
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }

            return logInResponse;
        }

        /// <summary>
        /// Logout to Taleo via soap call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>LogoutResponse of Logout request</returns>
        public LogoutResponse logOutSOAP(string dispatcher, String authToken)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            LogoutResponse logoutResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse logInResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest_LogoutSOAP(dispatcher, authToken);

                if (logInResponseString.isSuccess)
                {
                    logoutResponse = LogoutResponse.ConvertLoginResponseFromXMl(logInResponseString.responseString);
                    logoutResponse.successResult = true;
                }
                else
                {
                    logoutResponse = new LogoutResponse
                    {
                        response = new ParseEmailResponse_Response(),
                        status = new LogoutResponse_Status { detail = new LogoutResponse_Detail() }
                    };

                    logoutResponse.response.responseString = "";
                    logoutResponse.status.success = false;
                    logoutResponse.successResult = false;
                    logoutResponse.status.detail.errormessage = logInResponseString.responseString;
                    logoutResponse.e = logInResponseString.exception;
                }
            }
            catch (Exception e)
            {
                logoutResponse = new LogoutResponse();
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                logoutResponse.response = new ParseEmailResponse_Response();
                logoutResponse.status = new LogoutResponse_Status { detail = new LogoutResponse_Detail() };

                logoutResponse.response.responseString = "";
                logoutResponse.status.success = false;
                logoutResponse.successResult = false;
                logoutResponse.status.detail.errormessage = e.Message;
                logoutResponse.e = e;
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }

            return logoutResponse;
        }

        /// <summary>
        /// Parse Resume to Mail attachment via soap call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachments">byte array of attachment </param>
        /// <returns>ParseResumeResponse of ParseResume request</returns>
        public ParseResumeResponse parseResumeRequestSOAP(string dispatcher, String authToken, byte[] attachments)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            ParseResumeResponse parseResumeResponseResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse parseResumeResponseeString = (HTTPResponse)serviceHTTP.makeHTTPRequest_ParseResumeRequestSOAP(dispatcher, authToken, attachments);

                if (parseResumeResponseeString.isSuccess)
                {
                    parseResumeResponseResponse.result = parseResumeResponseeString.responseString;
                    parseResumeResponseResponse.isSuccess = true;
                    parseResumeResponseResponse.exception = null;
                }
                else
                {
                    parseResumeResponseResponse = new ParseResumeResponse(false, null, null);

                }
            }
            catch (Exception e)
            {
                parseResumeResponseResponse = new ParseResumeResponse(false, null, e);
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }

            return parseResumeResponseResponse;
        }

        /// <summary>
        /// Parse Resume to Mail attachment via soap call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachments">base64 encoded string of attachment </param>
        /// <returns>ParseResumeResponse of ParseResume request</returns>
        public ParseResumeResponse parseResumeRequestSOAP(string dispatcher, String authToken, string attachments)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            ParseResumeResponse parseResumeResponseResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse parseResumeResponseeString = (HTTPResponse)serviceHTTP.makeHTTPRequest_ParseResumeRequestSOAP(dispatcher, authToken, attachments);

                if (parseResumeResponseeString.isSuccess)
                {
                    parseResumeResponseResponse = new ParseResumeResponse(false, null, null)
                    {
                        result = parseResumeResponseeString.responseString,
                        isSuccess = true,
                        exception = null
                    };
                }
                else
                {
                    parseResumeResponseResponse = new ParseResumeResponse(false, null, null);

                }
            }
            catch (Exception e)
            {
                parseResumeResponseResponse = new ParseResumeResponse(false, null, e);
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }

            return parseResumeResponseResponse;
        }

        /// <summary>
        /// Parse Candidate via SOAP request
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateBean">string with xml format encoded value</param>
        /// <returns></returns>
        public CreateCandidateResponse parseCreateCandidateRequestSOAP(string dispatcher, String authToken, string candidateBean)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            CreateCandidateResponse createCandidateResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse parseResumeResponseeString = (HTTPResponse)serviceHTTP.makeHTTPRequest_ParseCreateCandidateRequestSOAP(dispatcher, authToken, candidateBean);

                if (parseResumeResponseeString.isSuccess)
                {
                    createCandidateResponse = CreateCandidateResponse.ConvertCreateCandidateResponseFromXMl(parseResumeResponseeString.responseString);
                    createCandidateResponse.isSuccess = true;
                }
                else
                {
                    createCandidateResponse = new CreateCandidateResponse(false, -1, null);

                }
            }
            catch (Exception e)
            {
                createCandidateResponse = new CreateCandidateResponse(false, -1, e);
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }

            return createCandidateResponse;
        }

        /// <summary>
        /// Emainsent Log via SOAP request
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="email">email address</param>
        /// /// <param name="body">body of that email</param>
        /// /// <param name="subject">subject of that email</param>
        /// /// <param name="utcTimeString">recivied time as a string</param>
        /// <returns>EmailSentLog response of emailsent request</returns>
        public EmailSentLogResponse createEmailSentLogSOAP(string dispatcher, String authToken, string email, string body, string subject, string utcTimeString)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            EmailSentLogResponse emailSentLogResponseResponse = null;
            try
            {
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse emailSentLogResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest_CreateEmailSentLogSOAP(dispatcher, authToken, email, body, subject, utcTimeString);

                emailSentLogResponseResponse = new EmailSentLogResponse(true, emailSentLogResponseString.responseString, emailSentLogResponseString.exception);
                return emailSentLogResponseResponse;

            }
            catch (Exception e)
            {
                emailSentLogResponseResponse = new EmailSentLogResponse(false, null, e);
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }

            return emailSentLogResponseResponse;
        }

        /// <summary>
        /// Parse Resume into candidate via soap call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachments">byte array of attachment </param>
        /// <returns>parseResumeIntoCandidateResponse of ParseResumeIntoCandidate request</returns>
        public ParseResumeIntoCandidateResponse parseResumeIntoCandidateSOAP(string dispatcher, String authToken, byte[] attachments)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            ParseResumeIntoCandidateResponse parseResumeIntoCandidateResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                ParseResumeIntoCandidateResponse response = serviceHTTP.makeHTTPRequest_ParseResumeIntoCandidateSOAP(dispatcher, authToken, attachments);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return parseResumeIntoCandidateResponse;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return null;
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }


        }

        /// <summary>
        /// Parse Resume into candidate via soap call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachments">base64 encoded string of attachment </param>
        /// <returns>parseResumeIntoCandidateResponse of ParseResumeIntoCandidate request</returns>
        public ParseResumeIntoCandidateResponse parseResumeIntoCandidateSOAP(string dispatcher, String authToken, string attachments, string reference, string filename)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            ParseResumeIntoCandidateResponse parseResumeIntoCandidateResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                parseResumeIntoCandidateResponse = serviceHTTP.makeHTTPRequest_ParseResumeIntoCandidateSOAP(dispatcher, authToken, attachments, reference, filename);

                if (parseResumeIntoCandidateResponse.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return parseResumeIntoCandidateResponse;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return null;
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }


        }

        /// <summary>
        /// Get binaryResumeResquest Soap call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>Binary Request Resume response of request</returns>
        public BinaryResumeRequestResponse getBinaryResumeRequestSOAP(string dispatcher, String authToken, int candidateID)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            BinaryResumeRequestResponse binaryResumeRequestResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                BinaryResumeRequestResponse response = serviceHTTP.makeHTTPRequest_GetBinaryResumeSOAP(dispatcher, authToken, candidateID);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return binaryResumeRequestResponse;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return null;
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }


        }

        /// <summary>
        /// Get candidateid via soap call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>get candidateidresonse via request</returns>
        public GetCandidateByIdResponse GetCandidateByIdSoap(string dispatcher, String authToken, int candidateID)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                GetCandidateByIdResponse response = serviceHTTP.makeHTTPRequest_GetCandidatebyIDRSOAP(dispatcher, authToken, candidateID);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    var candidateByIdRequestResponse = new GetCandidateByIdResponse(true, null, null) { isSuccess = true };
                    candidateByIdRequestResponse = GetCandidateByIdResponse.ConvertCandidateEmailFromXMl(response.result);
                    return candidateByIdRequestResponse;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return null;
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }


        }

        /// <summary>
        /// Delete Candidate via SOAP call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>DeleteCandidate response of request</returns>
        public DeleteCandidateResponse DeleteCandidateSOAP(string dispatcher, String authToken, int candidateID)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            DeleteCandidateResponse binaryResumeRequestResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                DeleteCandidateResponse response = serviceHTTP.makeHTTPRequest_DeleteCandidateSOAP(dispatcher, authToken, candidateID);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return binaryResumeRequestResponse;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return null;
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }


        }

        /// <summary>
        /// Get Logintoken via SOAP call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>LoginToken response of request</returns>
        public LoginTokenResponse logInTokenSOAP(string dispatcher, String authToken)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            LoginTokenResponse logInTokenResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse logInResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest_LoginTokenSOAP(dispatcher, authToken);

                if (logInResponseString.isSuccess)
                {
                    logInTokenResponse = LoginTokenResponse.ConvertLoginTokenResponseFromXMl(logInResponseString.responseString);
                    logInTokenResponse.successResult = true;
                }
                else
                {
                    logInTokenResponse = new LoginTokenResponse();
                    logInTokenResponse.response = new LoginTokenResponse_Response();
                    logInTokenResponse.status = new LoginTokenResponse_Status();
                    logInTokenResponse.status.detail = new LoginTokenResponse_Detail();

                    logInTokenResponse.response.loginToken = "";
                    logInTokenResponse.status.success = false;
                    logInTokenResponse.successResult = false;
                    logInTokenResponse.status.detail.errormessage = logInResponseString.responseString;
                    logInTokenResponse.e = logInResponseString.exception;
                }
            }
            catch (Exception e)
            {
                logInTokenResponse = new LoginTokenResponse();
                logInTokenResponse.response = new LoginTokenResponse_Response();
                logInTokenResponse.status = new LoginTokenResponse_Status();
                logInTokenResponse.status.detail = new LoginTokenResponse_Detail();

                logInTokenResponse.response.loginToken = "";
                logInTokenResponse.status.success = false;
                logInTokenResponse.successResult = false;
                logInTokenResponse.status.detail.errormessage = e.Message;
                logInTokenResponse.e = e;
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }

            return logInTokenResponse;
        }

        /// <summary>
        /// Get JsessionID via SOAP call
        /// </summary>
        /// <param name="serviceURL">service url of Taleo</param>
        /// <param name="loginToken">Generated Logintoken during login</param>
        /// <param name="companyCode">company code</param>
        /// <returns>JSessionID response of request</returns>
        public JSessionIDResponse getJSessionResponse(string serviceURL, string loginToken, string companyCode)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                JSessionIDResponse response = serviceHTTP.makeHTTPRequest_GetJSessionID(serviceURL, loginToken, companyCode);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return response;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return null;
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }
        }

        /// <summary>
        /// Get JsessionID via REST call
        /// </summary>
        /// <param name="serviceURL">service url of Taleo</param>
        /// <param name="loginToken">Generated Logintoken during login</param>
        /// <param name="companyCode">company code</param>
        /// <returns>JSessionID response of request</returns>
        public JSessionIDResponse getJSessionResponse_REST(string serviceURL, string loginToken, string companyCode)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                JSessionIDResponse response = serviceHTTP.makeHTTPRequest_GetJSessionID(serviceURL, loginToken, companyCode);

                if (response.isSuccess)
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return response;
                }
                else
                {
                    if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                    Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                    return null;
                }
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return null;
            }
        }

        /// <summary>
        /// Get application settings value of request
        /// </summary>
        /// <param name="key">key of application settings</param>
        /// <returns>value of specified key</returns>
        public string GetApplicationSettingsValue(string key)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");
                String value = applicationSetting.getValue(key);
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Exit");
                return value;
            }
            catch (Exception e)
            {
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
                return "";
            }
        }

        /// <summary>
        /// By this method get all service response to check access permission
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">user authenticatin token</param>
        /// <returns>return EnableServiceResponse object that contains IsSuccess, Response result and exception information</returns>
        public EnableServiceResponse GetEnableServiceResponseSOAP(string dispatcher, String authToken)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            EnableServiceResponse enableServiceResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse enableServiceResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest_EnableServiceSOAP(dispatcher, authToken);

                if (enableServiceResponseString.isSuccess)
                {
                    enableServiceResponse = EnableServiceResponse.ConvertEnableServiceResponseFromXMl(enableServiceResponseString.responseString);
                    enableServiceResponse.SuccessResult = true;
                }
                else
                {
                    enableServiceResponse = new EnableServiceResponse(false, null, null, null)
                    {
                        Response = null,
                        SuccessResult = false
                    };
                    enableServiceResponse.E = enableServiceResponse.E;
                }
            }
            catch (Exception e)
            {
                enableServiceResponse = new EnableServiceResponse(false, null, null, null)
                {
                    Response = null,
                    SuccessResult = false,
                    E = e
                };
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(className, methodName, threadId, e);
            }

            return enableServiceResponse;
        }

        /// <summary>
        /// get CreateEmailLog response via soap Call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="email">selected email address </param>
        /// <param name="subject">subject of selected email</param>
        /// <param name="body">body of selected mail</param>
        /// <param name="receviedTime">recevied time of selected mail</param>
        /// <returns>CreateEmailLog response of request</returns>
        public CreateEmailLogResponse getCreateEmailLogResponseSOAP(string dispatcher, String authToken, string email, string subject, string body, DateTime receviedTime)
        {
            string className = this.GetType().Name;
            string methodName = MethodBase.GetCurrentMethod().Name;
            string threadId = Thread.CurrentThread.Name;

            CreateEmailLogResponse createEmailLogResponse = null;
            try
            {
                if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel5))
                Logger.WriteLogInformation(className, methodName, threadId, "Entering");

                HTTPResponse createEmailLogResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest_CreateEmailLogSOAP(dispatcher, authToken, email, subject, body, receviedTime);

                if (createEmailLogResponseString.isSuccess)
                {
                    createEmailLogResponse = CreateEmailLogResponse.ConvertCreateEmailLogResponseFromXMl(createEmailLogResponseString.responseString);
                    createEmailLogResponse.SuccessResult = true;
                }
                else
                {
                    createEmailLogResponse = new CreateEmailLogResponse(false, null, 0)
                    {
                        LogID = 0,
                        SuccessResult = false
                    };
                    createEmailLogResponse.E = createEmailLogResponse.E;
                }
            }
            catch (Exception ex)
            {
                createEmailLogResponse = new CreateEmailLogResponse(false, null, 0)
                {
                    LogID = 0,
                    SuccessResult = false,
                    E = ex
                };
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
            }

            return createEmailLogResponse;
        }

        /// <summary>
        /// This method check user has access permission or not
        /// </summary>
        /// <param name="accessName">accessname to match</param>
        /// <param name="accessNamesList">All access permission list</param>
        /// <returns>If user has permission return true otherwise false</returns>
        public bool CheckAccessPermission(string accessName, string accessNamesList)
        {
            if (accessNamesList == null)
            {
                return false;
            }
            String[] splitAccessNamesList = accessNamesList.ToLower().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var match = splitAccessNamesList.FirstOrDefault(stringToCheck => stringToCheck.Contains(accessName.ToLower()));
            return match != null;
        }
        /// <summary>
        /// get CreateEmailLog response via REST Call
        /// </summary>
        /// <param name="entityId">it should candidate ID</param>
        /// <param name="entityType">type should "CAND"</param>
        /// <param name="date">current date</param>
        /// <param name="emailBody"> email body essage</param>
        /// <param name="subject"> email subject</param>
        /// <param name="typeNo">type 3</param>
        /// <param name="authToken">user authentication token</param>
        /// <returns>CreateEmailLog response of request</returns>
        public CreateEmailLogResponse CreateEmailSentLogREST(int entityId, string entityType, DateTime date, string emailBody, string subject, int typeNo, string authToken)
        {
            String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };
            string requestBody = @"{""contactlog"":{""entityId"":""" + entityId + @""",""date"":""" + date + @""",""entityType"":""" + entityType + @""",""comment"":""" + emailBody + @""",""subject"":""" + subject + @""",""typeNo"":" + typeNo + @"}}";
            HTTPResponse logEmailResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("object/contactlog", null, "POST", "application/json", requestBody, cookies, "application/json");

            CreateEmailLogResponse createEmailLogResponse = null;
            if (logEmailResponseString.isSuccess)
            {
                createEmailLogResponse = (CreateEmailLogResponse)CreateEmailLogResponse.ConvertCreateLogEmailFromJson(logEmailResponseString.responseString);
            }
            else
            {
                createEmailLogResponse = new CreateEmailLogResponse(false, logEmailResponseString.exception, 0);
            }
            return createEmailLogResponse;
        }

        public CreateContactResponse AddNewContactREST(UserResponse response, string firstName, string lastName, string email, string authToken)
        {
            String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };
            //string requestBody = @"{""user"":{""loginName"":""" + loginName + @""",""lastName"":""" + lastName + @""",""role"":""" + role + @""",""email"":""" + email + @""",""status"":" + status + @"}}";
            //string requestBody=@"{ ""user"":{ ""loginName"":"""+loginName+"", ""lastName"":"""+lastName+""", ""role"":"""+role+""", ""status"":"+status+ """,email"":"""+email+""" } ,""contact"":{""lastName"":"""+lastName+""", ""firstName"":"""+firstName""",""email"":"""+email+"""}}";
            string requestBody = "{ \"user\":{ \"loginName\":\"" + response.User.response.searchResults[0].user.loginName + "\", \"lastName\":\"" + response.User.response.searchResults[0].user.lastName + "\", \"role\":\"" + response.User.response.searchResults[0].user.role + "\", \"status\":\"" + response.User.response.searchResults[0].user.status + "\", \"email\":\"" + response.User.response.searchResults[0].user.email + "\" } ,\"contact\":{\"lastName\":\"" + lastName + "\", \"firstName\":\"" + firstName + "\",\"email\":\"" + email + "\"}}";
            HTTPResponse addContactResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("object/contact", null, "POST", "application/json", requestBody, cookies, "application/json");

            CreateContactResponse addContactResponse = null;
            if (addContactResponseString.isSuccess)
            {
                addContactResponse = CreateContactResponse.ConvertCreateContactFromJson(addContactResponseString.responseString);
            }
            else
            {
                addContactResponse = new CreateContactResponse(false, null, addContactResponseString.exception);
            }
            return addContactResponse;
        }

        public CreateEmailLogResponse AddExistingContactREST(int entityId, string entityType, DateTime date, string emailBody, string subject, int typeNo, string authToken)
        {
            String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };
            string requestBody = @"{""contactlog"":{""entityId"":""" + entityId + @""",""date"":""" + date + @""",""entityType"":""" + entityType + @""",""comment"":""" + emailBody + @""",""subject"":""" + subject + @""",""typeNo"":" + typeNo + @"}}";
            HTTPResponse logEmailResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("object/contactlog", null, "POST", "application/json", requestBody, cookies, "application/json");

            CreateEmailLogResponse createEmailLogResponse = null;
            if (logEmailResponseString.isSuccess)
            {
                createEmailLogResponse = (CreateEmailLogResponse)CreateEmailLogResponse.ConvertCreateLogEmailFromJson(logEmailResponseString.responseString);
            }
            else
            {
                createEmailLogResponse = new CreateEmailLogResponse(false, logEmailResponseString.exception, 0);
            }
            return createEmailLogResponse;
        }

        public UserResponse GetUserByEmailREST(string type, string authToken, string typeValue)
        {
            String[][] cookies = new String[1][] { new String[2] { "authToken", authToken } };
            string requestBody = null;
            HTTPResponse userResponseString = (HTTPResponse)serviceHTTP.makeHTTPRequest("object/user/search?" + type + "=" + typeValue, null, "GET", "application/json", requestBody, cookies, "application/json");

            UserResponse userResponse = null;
            if (userResponseString.isSuccess)
            {
                userResponse = (UserResponse)UserResponse.ConvertUserResponseFromJson(userResponseString.responseString);
            }
            else
            {
                userResponse = new UserResponse(false, null, userResponseString.exception);
            }
            return userResponse;
        }

        #endregion

    }
}
