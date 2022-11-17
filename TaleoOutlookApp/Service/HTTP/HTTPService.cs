using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml;
using Model.Response;
using Util.ApplicationGlobal;
using Util.Utilities;
using DataAccess.SOAP;
using System.Reflection;
using System.Threading;

namespace Service.HTTP
{
    public class HTTPService
    {
        #region Property
        /// <summary>
        /// 
        /// </summary>
        private String baseURL = "";
        public bool _USE_REST { get; set; }
        public Settings TaleoSetting = null;

        private static TaleoVersionPath taleoVersionInfo { get; set; }

        #endregion

        #region Construction

        public HTTPService(String newBaseURL)
        {
            baseURL = newBaseURL;
            taleoVersionInfo = new TaleoVersionPath();
        }

        public HTTPService(bool USE_REST, Settings taleoSetting)
        {
            taleoVersionInfo = new TaleoVersionPath();
            _USE_REST = USE_REST;
            TaleoSetting = taleoSetting;
            string path = null;
            //todo :: need to remove redundent code
            if (TaleoSetting == null)
            {
                if (!ApplicationGlobal.QA_Environment)
                    path = "http://tbe.taleo.net/outlook/Toolbar.settings";
                else
                {
                    path = "https://qa.tbe.taleocloud.net/outlook/Toolbar.settings";
                }

                try
                {
                    SettingsFileResponse response = null;
                    try
                    {
                        response = (SettingsFileResponse)makeHTTPRequest_GetSettingsFile(path);
                        ApplicationGlobal.SettingsContent = response.settingsContent;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            path = "https://qa.tbe.taleocloud.net/outlook/Toolbar.settings";
                            response = (SettingsFileResponse)makeHTTPRequest_GetSettingsFile(path);
                            ApplicationGlobal.SettingsContent = response.settingsContent;
                        }
                        catch (Exception exp) { ApplicationGlobal.SettingsContent = null; }

                    }


                    if (File.Exists(ApplicationGlobal.FinalPath + "TaleoSettings.ini") && !string.IsNullOrEmpty(File.ReadAllText(ApplicationGlobal.FinalPath + "TaleoSettings.ini")))
                    {
                        TaleoSetting = new Settings(ApplicationGlobal.FinalPath + "TaleoSettings.ini",
                        File.ReadAllText(ApplicationGlobal.FinalPath + "TaleoSettings.ini"));
                    }
                    if (!File.Exists(ApplicationGlobal.FinalPath + "TaleoSettings.ini") && !string.IsNullOrEmpty(ApplicationGlobal.SettingsContent))
                        TaleoSetting = new Settings(ApplicationGlobal.FinalPath + "TaleoSettings.ini",
                        ApplicationGlobal.SettingsContent);
                }
                catch (Exception ex)
                {
                    Logger.WriteLogInformation("HTTPService", "HTTPService", Thread.CurrentThread.Name, ex);
                }
            }
        }


        #endregion

        #region Public Methods
        /// <summary>
        /// get application base URL
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Get baseurl 
        /// </summary>
        /// <returns></returns>
        public String getBaseURL()
        {
            return baseURL;
        }
        /// <summary>
        /// set base URL for http service that get by argument
        /// </summary>
        /// <param name="newBaseURL">get base URL</param>
        /// <summary>
        /// Set base URL
        /// </summary>
        /// <param name="newBaseURL">url</param>
        public void setBaseURL(String newBaseURL)
        {
            baseURL = newBaseURL;
        }

        /// <summary>
        /// make httprequest 
        /// </summary>
        /// <param name="functionSpecificURL">function specified url which will be added with base url</param>
        /// <param name="parameterList">2D array of parameter list</param>
        /// <param name="requestMethod">request http verb (GET/POST/DELETE etc)</param>
        /// <param name="requestAccept">accpted HHTP header</param>
        /// <param name="body">request body</param>
        /// <param name="cookies">cookie 2D string with cookie name and value</param>
        /// <param name="contentType">accepted content tyle</param>
        /// <param name="usefunctionSpecificUrlAsFullUrl">use functionSpecificURL as request URL</param>
        /// <returns>HTTP response</returns>
        public IHTTPResponse makeHTTPRequest(String functionSpecificURL, String[][] parameterList, String requestMethod, String requestAccept, String body, String[][] cookies, string contentType = null, bool usefunctionSpecificUrlAsFullUrl = false)
        {
            String resultString = "";
            String fullURL = null;
            if (usefunctionSpecificUrlAsFullUrl)
                fullURL = functionSpecificURL;
            else
                fullURL = baseURL + functionSpecificURL;

            Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "fullURL (beginning)-" + fullURL);
            HTTPResponse response = null;

            if (parameterList != null && parameterList.Length > 0)
                fullURL = fullURL + "?";

            for (int i = 0; parameterList != null && i < parameterList.Length; i++)
            {
                fullURL = fullURL + parameterList[i][0] + "=" + parameterList[i][1];

                if ((i + 1) < parameterList.Length)
                    fullURL = fullURL + "&";
            }
            Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "fullURL (with paramters)-" + fullURL);
            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(fullURL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (!String.IsNullOrEmpty(requestMethod))
                request.Method = requestMethod;

            if (!String.IsNullOrEmpty(requestAccept))
                request.Accept = requestAccept;

            //request.UserAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
            //                    taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;
            request.UserAgent = ApplicationGlobal.GetUserAgent();

            if (!string.IsNullOrEmpty(contentType))
                request.ContentType = contentType;

            if (cookies != null && cookies.Length > 0)
            {
                request.CookieContainer = new CookieContainer();

                try
                {
                    for (int i = 0; i < cookies.Length; i++)
                        request.CookieContainer.Add(new Uri(fullURL), new Cookie(cookies[i][0], cookies[i][1]));

                }
                catch (Exception ex)
                {
                    Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
                }
            }

            try
            {
                if (requestMethod == "POST")
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(body);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                WebResponse webResponse = request.GetResponse();
                resultString = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                response = new HTTPResponse(true, resultString, null);
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
            }

            return response;
        }

        /// <summary>
        /// make multipart httprequest 
        /// </summary>
        /// <param name="functionSpecificURL">function specified url which will be added with base url</param>
        /// <param name="postParameters">request parameter dictonary</param>
        /// <param name="cookies">cookie 2D string with cookie name and value</param>
        /// <returns>HTTP response</returns>
        public string makeMultipartHTTPRequest(String functionSpecificURL, Dictionary<string, object> postParameters, String[][] cookies)
        {
            String resultString = "";
            String fullURL = baseURL + functionSpecificURL;
            HTTPResponse response = null;


            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(fullURL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            request.UserAgent = ApplicationGlobal.GetUserAgent(); ;

            try
            {
                WebResponse webResponse = FormUpload.MultipartFormDataPost(fullURL, request.UserAgent, postParameters, cookies);

                resultString = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                response = new HTTPResponse(true, resultString, null);
            }
            catch (WebException e)
            {
                resultString = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                response = new HTTPResponse(false, resultString, e);

            }

            return resultString;
        }

        /// <summary>
        /// Get base URL of specified company code
        /// </summary>
        /// <param name="companyCode">company code</param>
        /// <returns></returns>
        public IHTTPResponse makeHTTPRequest_GetBaseURL(String companyCode)
        {
            String instanceURL = null;

            if (ApplicationGlobal.USE_REST)
            {
                string dispatcherFromFile = TaleoSetting.getValue("DISPATCHER");
                string dispatcher = "https://tbe.taleo.net/MANAGER/dispatcher/api/v1/serviceUrl/";

                if (ApplicationGlobal.QA_Environment)
                {
                    if (ApplicationGlobal.QA_REST_DISPATCHER.Contains("servlet/rpcrouter"))
                    {
                        ApplicationGlobal.QA_REST_DISPATCHER = ApplicationGlobal.QA_REST_DISPATCHER.Replace("servlet/rpcrouter", "api/v1/serviceUrl");
                    }
                    dispatcher = ApplicationGlobal.QA_REST_DISPATCHER;
                    instanceURL = ApplicationGlobal.QA_REST_DISPATCHER + companyCode + ".xml";
                }
                else
                {
                    if (ApplicationGlobal.PRODUCTION_REST_DISPATCHER.Contains("servlet/rpcrouter"))
                    {
                        ApplicationGlobal.PRODUCTION_REST_DISPATCHER = ApplicationGlobal.PRODUCTION_REST_DISPATCHER.Replace("servlet/rpcrouter", "api/v1/serviceUrl");
                    }
                    dispatcher = ApplicationGlobal.PRODUCTION_REST_DISPATCHER;
                    instanceURL = ApplicationGlobal.PRODUCTION_REST_DISPATCHER + companyCode + ".xml";
                }
            }
            else
            {
                string dispatcherFromFile = TaleoSetting.getValue("DISPATCHER");
                string dispatcher = "https://tbe.taleo.net/MANAGER/dispatcher/servlet/rpcrouter/";
                if (!string.IsNullOrEmpty(dispatcherFromFile))
                {
                    dispatcherFromFile = dispatcherFromFile + "/";
                    if (dispatcherFromFile.Contains("servlet/rpcrouter"))
                    {
                        dispatcherFromFile = dispatcherFromFile.Replace("servlet/rpcrouter", "api/v1/serviceUrl");
                    }
                    dispatcher = dispatcherFromFile;
                }
                if (ApplicationGlobal.QA_Environment)
                {
                    instanceURL = dispatcher + companyCode + ".xml";
                }
                else
                {
                    instanceURL = dispatcher + companyCode + ".xml";
                }
            }

            String result = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(instanceURL);
            request.UserAgent = ApplicationGlobal.GetUserAgent(); ;
            ServiceURLResponse response = null;

            try
            {

                //todo: use without .XML, get JSON result, parse JSON
                WebResponse webResponse = request.GetResponse();
                result = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

                var xmlParser = new XmlParseValue();
                var resultList = xmlParser.GetNodeValue(result, "URL");
                result = resultList.Count != 0 ? resultList.First() : "";

                response = new ServiceURLResponse(true, result, null);
            }
            catch (Exception e)
            {
                response = new ServiceURLResponse(false, null, e);
            }
            return response;
        }

        /// <summary>
        /// get settings file of specified path
        /// </summary>
        /// <param name="path">settings file path</param>
        /// <returns></returns>
        public IHTTPResponse makeHTTPRequest_GetSettingsFile(String path)
        {
            String instanceURL = path;
            String result = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(instanceURL);
            request.UserAgent = ApplicationGlobal.GetUserAgent(); ;
            SettingsFileResponse response = null;

            try
            {
                WebResponse webResponse = request.GetResponse();
                result = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

                response = new SettingsFileResponse(true, result, null);
            }
            catch (Exception e)
            {
                response = new SettingsFileResponse(false, null, e);
            }
            return response;
        }

        /// <summary>
        /// Get base URL of specified company code 
        /// </summary>
        /// <param name="url">dispatcher url</param>
        /// <param name="companyCode">company code</param>
        /// <returns></returns>
        public IHTTPResponse makeHTTPRequest_GetBaseURL(String url, String companyCode)
        {
            if (!url.EndsWith("/"))
                url = url + "/";
            String instanceURL = url + companyCode + ".xml";
            String result = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(instanceURL);
            request.UserAgent = ApplicationGlobal.GetUserAgent(); ;
            ServiceURLResponse response = null;

            try
            {
                WebResponse webResponse = request.GetResponse();
                result = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

                var xmlParser = new XmlParseValue();
                var resultList = xmlParser.GetNodeValue(result, "URL");
                result = resultList.Count != 0 ? resultList.First() : "";

                response = new ServiceURLResponse(true, result, null);
            }
            catch (Exception e)
            {
                response = new ServiceURLResponse(false, null, e);
            }
            return response;
        }
        #endregion

        #region SOAP
        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.UserAgent = ApplicationGlobal.GetUserAgent();
            return webRequest;
        }
        private static XmlDocument CreateSoapEnvelope(string authToken)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml("<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/1999/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/1999/XMLSchema\"><SOAP-ENV:Body><m:getLoginToken xmlns:m=\"urn:TBEWebAPI\"> <name>" + authToken + "</name></m:getLoginToken></SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelop;
        }
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        /// <summary>
        /// Get MyView link from SOAP
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="_url">myview base URL</param>
        public static void GetSOAPMyView(string authToken, string _url)
        {
            var _action = "http://xxxxxxxx/Service1.asmx?op=HelloWorld";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(authToken);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);
            }
        }

        /// <summary>
        ///  Get dispatcher url for specified company
        /// </summary>
        /// <param name="companyCode">Company code</param>
        /// <returns>SOAP request result response</returns>
        public IHTTPResponse makeHTTPRequest_GetBaseURLSOAP(string companyCode)
        {
            String instanceURL = "https://tbe.taleo.net/MANAGER/dispatcher/servlet/rpcrouter";
            ServiceURLResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(instanceURL);
                String result = serviceURLSOAPHandler.getCompanyURL(companyCode);

                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(result, "return");
                result = resultList != null && resultList.Count != 0 ? resultList.First() : "";

                response = new ServiceURLResponse(true, result, null);
                return response;
            }
            catch (Exception e)
            {
                response = new ServiceURLResponse(false, "", e);
                return response;
            }
        }

        /// <summary>
        ///  Get dispatcher url for specified company
        /// </summary>
        /// <param name="url">dispatcher url</param>
        /// <param name="companyCode">Company code</param>
        /// <returns>SOAP request result string</returns>
        public IHTTPResponse makeHTTPRequest_GetBaseURLSOAP(string url, string companyCode)
        {
            ServiceURLResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String result = serviceURLSOAPHandler.getCompanyURL(companyCode);

                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(result, "return");
                result = resultList != null && resultList.Count != 0 ? resultList.First() : "";

                response = new ServiceURLResponse(true, result, null);
                return response;
            }
            catch (Exception e)
            {
                response = new ServiceURLResponse(false, "", e);
                return response;
            }
        }

        /// <summary>
        /// get service url
        /// </summary>
        /// <param name="url">dispatcher url</param>
        /// <param name="authtoken">generated auth token from during login</param>
        /// <returns></returns>
        public IHTTPResponse makeHTTPRequest_GetServiceURLSOAP(string url, string authtoken)
        {
            NewServiceListResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String result = serviceURLSOAPHandler.serviceListSOAP(authtoken);


                response = new NewServiceListResponse(true, result, null);
                return response;
            }
            catch (Exception e)
            {
                response = new NewServiceListResponse(false, "", e);
                return response;
            }
        }

        /// <summary>
        /// Login to Taleo via soap call
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>
        /// <param name="companyCode">company code</param>
        /// <returns>SOAP request result response</returns>
        public IHTTPResponse makeHTTPRequest_LoginSOAP(string url, string companyCode, string userName, string password)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.logInSOAP(companyCode, userName, password);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// Logout to Taleo via soap call
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>SOAP request result response</returns>
        public IHTTPResponse makeHTTPRequest_LogoutSOAP(string url, string authToken)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.logOutSOAP(authToken);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        ///  Parse resume
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachmentArray">byte array</param>
        /// <returns>SOAP request result response</returns>
        public IHTTPResponse makeHTTPRequest_ParseResumeRequestSOAP(string url, string authToken, byte[] attachments)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.ParseResumeSOAP(authToken, attachments);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        ///  Parse resume
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachmentArray">base64 encoded strig</param>
        /// <returns>SOAP request result response</returns>
        public IHTTPResponse makeHTTPRequest_ParseResumeRequestSOAP(string url, string authToken, string attachments)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.ParseResumeSOAP(authToken, attachments);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// Parse Candidate via SOAP request
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateBean">string with xml format encoded value</param>
        /// <returns>SOAP request result response</returns>
        public IHTTPResponse makeHTTPRequest_ParseCreateCandidateRequestSOAP(string url, string authToken, string candidateBean)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.ParseCreateCandidateSOAP(authToken, candidateBean);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// Emainsent Log via SOAP request
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="email">email address</param>
        /// /// <param name="body">body of that email</param>
        /// /// <param name="subject">subject of that email</param>
        /// /// <param name="utcTimeString">recivied time as a string</param>
        /// <returns>SOAP request result string</returns>
        public IHTTPResponse makeHTTPRequest_CreateEmailSentLogSOAP(string url, string authToken, string email, string body, string subject, string utcTimeString)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.createEmailSentLogSOAP(authToken, email, body, subject, utcTimeString);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// Parse Resume into candidate via soap call
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachments">byte array of attachment </param>
        /// <returns>SOAP request result response</returns>
        public ParseResumeIntoCandidateResponse makeHTTPRequest_ParseResumeIntoCandidateSOAP(string url, string authToken, byte[] attachments)
        {
            ParseResumeIntoCandidateResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String result = serviceURLSOAPHandler.ParseResumeIntoCandidateSOAP(authToken, attachments);

                XmlParseValue xmlParser = new XmlParseValue();
                List<String> dup = xmlParser.GetNodeValue(result, "dup");
                List<String> candidateId = xmlParser.GetNodeValue(result, "candidateId");

                if (dup.Count >= 1 && candidateId.Count >= 1)
                {
                    response = new ParseResumeIntoCandidateResponse(true, Convert.ToInt32(candidateId[0]), Convert.ToInt32(dup[0]), null);
                }



                return response;
            }
            catch (Exception e)
            {
                response = new ParseResumeIntoCandidateResponse(false, 0, 0, e);
                return response;
            }
        }

        /// <summary>
        /// Parse Resume into candidate via soap call
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachments">base64 array string of attachment </param>
        /// <returns>SOAP request result response</returns>
        public ParseResumeIntoCandidateResponse makeHTTPRequest_ParseResumeIntoCandidateSOAP(string url, string authToken, string attachments, string reference, string fileName)
        {
            ParseResumeIntoCandidateResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String result = serviceURLSOAPHandler.ParseResumeIntoCandidateSOAP(authToken, attachments, reference, fileName);

                XmlParseValue xmlParser = new XmlParseValue();
                List<String> dup = xmlParser.GetNodeValue(result, "dup");
                List<String> candidateId = xmlParser.GetNodeValue(result, "candidateId");

                if (dup.Count >= 1 && candidateId.Count >= 1)
                {
                    response = new ParseResumeIntoCandidateResponse(true, Convert.ToInt32(candidateId[0]), Convert.ToInt32(dup[0]), null);
                }



                return response;
            }
            catch (Exception e)
            {
                response = new ParseResumeIntoCandidateResponse(false, 0, 0, e);
                return response;
            }
        }

        /// <summary>
        /// Get binaryResumeResquest Soap call
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>SOAP request result response</returns>
        public BinaryResumeRequestResponse makeHTTPRequest_GetBinaryResumeSOAP(string url, string authToken, int candidateID)
        {
            BinaryResumeRequestResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String result = serviceURLSOAPHandler.GetBinaryResumeSOAP(authToken, candidateID);

                XmlParseValue xmlParser = new XmlParseValue();
                List<String> attachments = xmlParser.GetNodeValue(result, "array");

                if (attachments.Count >= 1)
                {
                    response = new BinaryResumeRequestResponse(true, attachments[0], null);
                }



                return response;
            }
            catch (Exception e)
            {
                response = new BinaryResumeRequestResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// Get candidateid via soap call
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>SOAP request result response</returns>
        public GetCandidateByIdResponse makeHTTPRequest_GetCandidatebyIDRSOAP(string url, string authToken, int candidateID)
        {
            GetCandidateByIdResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String result = serviceURLSOAPHandler.GetCandidatebyIDSOAP(authToken, candidateID);


                response = new GetCandidateByIdResponse(true, result, null);



                return response;
            }
            catch (Exception e)
            {
                response = new GetCandidateByIdResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// Get Logintoken via SOAP call
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>SOAP request result string</returns>
        public IHTTPResponse makeHTTPRequest_LoginTokenSOAP(string url, string authToken)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.logInTokenSoap(authToken);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// get jsessionID of after login
        /// </summary>
        /// <param name="serviceURL">service url</param>
        /// <param name="loginToken">Login token</param>
        /// <param name="companyCode">specified company code</param>
        /// <returns>JSessionIDResponse</returns>
        public JSessionIDResponse makeHTTPRequest_GetJSessionID(string serviceURL, string loginToken, string companyCode)
        {
            JSessionIDResponse jSessionIDResponse = new JSessionIDResponse(false, "", null, null);

            if (ApplicationGlobal.USE_REST)
                serviceURL = serviceURL + "outlook/myview/main.jsp";
            else
                serviceURL = serviceURL.Replace("services/rpcrouter", "outlook/myview/main.jsp");

            serviceURL += "?org=" + companyCode + "&pword=";

            try
            {
                CookieContainer cookies = new CookieContainer();
                HttpClientHandler handler = new HttpClientHandler();
                List<Cookie> cookieList = new List<Cookie>();
                handler.CookieContainer = cookies;

                HttpClient client = new HttpClient(handler);
                HttpResponseMessage response = client.GetAsync(serviceURL + loginToken).Result;

                Uri uri = new Uri(serviceURL + loginToken);
                CookieCollection responseCookies = cookies.GetCookies(uri);
                String jsi = "";
                foreach (Cookie cookie in responseCookies)
                {
                    cookieList.Add(cookie);
                    if (cookie.Name == "JSESSIONID")
                    {
                        jsi = cookie.Value;
                        //Console.WriteLine(cookie.Name + ": " + cookie.Value);
                    }
                }
                jSessionIDResponse = new JSessionIDResponse(true, jsi, null, cookieList);
            }
            catch (Exception ex)
            {
                jSessionIDResponse = new JSessionIDResponse(false, null, ex, null);
            }
            return jSessionIDResponse;
        }

        /// <summary>
        /// Delete Candidate via SOAP call
        /// </summary>
        /// <param name="url">service URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>SOAP request result response</returns>
        public DeleteCandidateResponse makeHTTPRequest_DeleteCandidateSOAP(string url, string authToken, int candidateID)
        {
            DeleteCandidateResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String result = serviceURLSOAPHandler.DeleteCandidateSOAP(authToken, candidateID);


                response = new DeleteCandidateResponse(true, result, null);



                return response;
            }
            catch (Exception e)
            {
                response = new DeleteCandidateResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// This method make a HTTP request to get Enabled services list 
        /// </summary>
        /// <param name="url"> enable service URL </param>
        /// <param name="authToken">User authentication token</param>
        /// <returns>HTTP response</returns>
        public IHTTPResponse makeHTTPRequest_EnableServiceSOAP(string url, string authToken)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.EnableServiceResponseSOAP(authToken);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        /// <summary>
        /// This method make a HTTP request to create Email log using SOAP result
        /// </summary>
        /// <param name="url">Email log URL</param>
        /// <param name="authToken">User authentication token</param>
        /// <param name="email">Receiver email</param>
        /// <param name="subject">Emal subject</param>
        /// <param name="body">Email body</param>
        /// <param name="receviedTime">Log time</param>
        /// <returns>Request response</returns>
        public IHTTPResponse makeHTTPRequest_CreateEmailLogSOAP(string url, string authToken, string email, string subject, string body, DateTime receviedTime)
        {
            HTTPResponse response = null;

            try
            {
                SOAPHandler serviceURLSOAPHandler = new SOAPHandler(url);
                String resultString = serviceURLSOAPHandler.createEmailLogSOAP(authToken, email, subject, body, receviedTime);

                response = new HTTPResponse(true, resultString, null);

                return response;
            }
            catch (Exception e)
            {
                response = new HTTPResponse(false, null, e);
                return response;
            }
        }

        #endregion

    }

}
