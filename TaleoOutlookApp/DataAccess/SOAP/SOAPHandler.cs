using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using Util.ApplicationGlobal;
using Util.Enums;
using Util.Utilities;

namespace DataAccess.SOAP
{
    public class SOAPHandler
    {
        #region Property
        private String dispatcherURL = "https://tbe.taleo.net/MANAGER/dispatcher/servlet/rpcrouter";
        private TaleoVersionPath taleoVersionInfo { get; set; }
        #endregion

        #region Constructor
        public SOAPHandler()
        {
            taleoVersionInfo = new TaleoVersionPath();
        }

        public SOAPHandler(String newDispatcherURl)
        {
            taleoVersionInfo = new TaleoVersionPath();
            //if(ApplicationGlobal.QA_Environment)
                dispatcherURL = newDispatcherURl;

            if (dispatcherURL.Contains("api/v1"))
            {
                dispatcherURL = dispatcherURL.Replace("api/v1/", "services/rpcrouter");
            }


        }
        #endregion

        #region Miscelenious
        private HttpWebRequest CreateWebRequest()
        {
            if (Logger.LogLabel == Convert.ToInt16(Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel2)))
            Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "Start request to" + dispatcherURL);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(dispatcherURL);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.UserAgent = ApplicationGlobal.GetUserAgent(); ;
            if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel0) || Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel1))
            Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "return " + webRequest);
            return webRequest;
        }

        private HttpWebRequest CreateWebRequest(String methodName)
        {
            if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel2))
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "Start request to" + dispatcherURL);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(dispatcherURL);
            webRequest.Headers.Add("SOAPAction: http://tempuri.org/" + methodName);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.UserAgent = ApplicationGlobal.GetUserAgent();
            if (Logger.LogLabel == Convert.ToInt16(LogLabel.LogLabel2))
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "return " + webRequest);
            return webRequest;
        }

        private string StreamWriteRead(HttpWebRequest request, String soapBody)
        {
            string result = "";
            using (Stream stream = request.GetRequestStream())
            {
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(soapBody);
                sw.Flush();
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        result = rd.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                WebResponse errRsp = ex.Response;
                using (StreamReader rdr = new StreamReader(errRsp.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
                if (Logger.LogLabel >= Convert.ToInt16(LogLabel.LogLabel1))
                Logger.WriteLogInformation(this.GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex);
            }
            return result;
        }

        #endregion

        #region Public Methods
        /// <summary>
        ///  Get base url for specified company
        /// </summary>
        /// <param name="companyCode">Company code</param>
        /// <returns>SOAP request result string</returns>
        public string getCompanyURL(String companyCode)
        {
            HttpWebRequest request = CreateWebRequest();

            String soapBody = @"
                         <SOAP-ENV:Envelope
                            xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                            xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                            xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                            <SOAP-ENV:Body>
                                 <m:getURL
                                    xmlns:m=""urn:TBEDispatcherAPI""
                                    SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                                    <orgCode xsi:type=""xsd:string"">"+companyCode+@"</orgCode>
                                    </m:getURL>
                            </SOAP-ENV:Body>
                            </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// Login to Taleo via soap call
        /// </summary>
        /// <param name="dispatcher">base URL</param>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>
        /// <param name="companyCode">company code</param>
        /// <returns>SOAP request result string</returns>
        public string logInSOAP(String companyCode, String userName, String password)
        {
            HttpWebRequest request = CreateWebRequest("login");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:login xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + companyCode + @"</in0>
                                            <in1>" + userName + @"</in1>
                                            <in2>" + password + @"</in2>
                                        </m:login>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// get service list via soap call
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>SOAP request result string</returns>
        public string serviceListSOAP(String authtoken)
        {
            HttpWebRequest request = CreateWebRequest("login");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:login xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authtoken + @"</in0>
                                        </m:login>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// Logout to Taleo via soap call
        /// </summary>
        /// <param name="dispatcher">dispatcher URL</param>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>SOAP request result string</returns>
        public string logOutSOAP(String authToken)
        {
            HttpWebRequest request = CreateWebRequest("logout");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:logout xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                        </m:logout>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
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
        /// <returns>SOAP request result string</returns>
        public string createEmailSentLogSOAP(String authToken, string email, string body, string subject, string utcTimeString)
        {
            HttpWebRequest request = CreateWebRequest("createEmailSentLog");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:createEmailSentLog xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>" + email + @"</in1>
                                            <in2>" + subject + @"</in2>
                                            <in3>" + body + @"</in3>
                                            <in4>" + utcTimeString + @"</in4>
                                        </m:createEmailSentLog>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// This method contains SOAP envelope to create log for outgoing mail
        /// </summary>
        /// <param name="authToken">User authentication token</param>
        /// <param name="email">Receiver email</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <param name="receiviedTime">Log time</param>
        /// <returns>SOAP request result string</returns>
        public string createEmailLogSOAP(String authToken, string email, string subject, string body, DateTime receiviedTime)
        {
            HttpWebRequest request = CreateWebRequest("createEmailLog");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:createEmailLog xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>" + email + @"</in1>
                                            <in2>" + subject + @"</in2>
                                            <in3>" + body + @"</in3>
                                            <in4>" + receiviedTime.ToString("o") + @"</in4>
                                        </m:createEmailLog>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// Parse Resume into candidate via soap call
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachments">byte array of attachment </param>
        /// <returns>SOAP request result string</returns>
        public string ParseResumeIntoCandidateSOAP(String authToken, byte[] array)
        {
            string attachmentArray = null;
            for (int i = 0; i < array.Length; i++)
                attachmentArray += array[i];
            HttpWebRequest request = CreateWebRequest("parseResumeIntoCandidate");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:parseResumeIntoCandidate xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>
                                                
                                                <array>
                                
				                                    " + attachmentArray + @"
			                                    </array>
                                            </in1>
                                            <referredBy></referredBy>
                                            <filename></filename>
                                        </m:parseResumeIntoCandidate>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }


//        public string ParseResumeIntoCandidateSOAP(String authToken, string attachmentArray)
//        {
//            HttpWebRequest request = CreateWebRequest("parseResumeIntoCandidate");
//            String soapBody = @"<SOAP-ENV:Envelope
//                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
//                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
//                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
//                                    <SOAP-ENV:Body>
//                                         <m:parseResumeIntoCandidate xmlns:m=""urn:TBEWebAPI"">
//                                            <in0>" + authToken + @"</in0>
//                                            <in1>
//                                                
//                                                <array>
//                                
//				                                    " + attachmentArray + @"
//			                                    </array>
//                                            </in1>
//                                            <referredBy></referredBy>
//                                            <filename></filename>
//                                        </m:parseResumeIntoCandidate>
//                                    </SOAP-ENV:Body>
//                                </SOAP-ENV:Envelope>";
//            string result = StreamWriteRead(request, soapBody);

//            return result;
//        }

        /// <summary>
        /// Parse Resume into candidate via soap call
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachments">byte array of attachment </param>
        /// <returns>SOAP request result string</returns>
        public string ParseResumeIntoCandidateSOAP(String authToken, string attachmentArray, string reference, string fileName)
        {
            HttpWebRequest request = CreateWebRequest("parseResumeIntoCandidate");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:parseResumeIntoCandidate xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>
                                                
                                                <array>
                                
				                                    " + attachmentArray + @"
			                                    </array>
                                            </in1>
                                            <referredBy>" + reference + @"</referredBy>
                                            <filename>" + fileName + @"</filename>
                                        </m:parseResumeIntoCandidate>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        ///  Parse resume
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachmentArray">byte array</param>
        /// <returns>SOAP request result string</returns>
        public string ParseResumeSOAP(String authToken, byte[] array)
        {
            string attachmentArray = null;
            for (int i = 0; i < array.Length; i++)
                attachmentArray += array[i];
            HttpWebRequest request = CreateWebRequest("parseResume");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:parseResume xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>
                                                
                                                <array>
                                
				                                    " + attachmentArray + @"
			                                    </array>
                                            </in1>
                                            <referredBy></referredBy>
                                            <filename></filename>
                                        </m:parseResume>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        ///  Parse resume
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="attachmentArray">base64 encoded strig</param>
        /// <returns>SOAP request result string</returns>
        public string ParseResumeSOAP(String authToken, string attachmentArray)
        {

            HttpWebRequest request = CreateWebRequest("parseResume");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:parseResume xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>
                                                
                                                <array>
                                
				                                    " + attachmentArray + @"
			                                    </array>
                                            </in1>
                                        </m:parseResume>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// Parse Candidate via SOAP request
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateBean">string with xml format encoded value</param>
        /// <returns>SOAP request result string</returns>
        public string ParseCreateCandidateSOAP(String authToken, string candidateBean)
        {
            HttpWebRequest request = CreateWebRequest("parseResumeIntoCandidate");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:createCandidate xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>
                                                <flexValues>
                                                    <item>
				                                    
                                                    </item>
			                                    </flexValues>
                                                " + candidateBean + @"
                                            </in1>
                                        </m:createCandidate>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// Get binaryResumeResquest Soap call
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>SOAP request result string</returns>
        public string GetBinaryResumeSOAP(String authToken, int candidateID)
        {
            HttpWebRequest request = CreateWebRequest("parseResumeIntoCandidate");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:getBinaryResume xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>" + candidateID + @"</in1>
                                        </m:getBinaryResume>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// Get candidateid via soap call
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>SOAP request result string</returns>
        public string GetCandidatebyIDSOAP(String authToken, int candidateID)
        {
            HttpWebRequest request = CreateWebRequest("parseResumeIntoCandidate");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:getCandidateById xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>" + candidateID + @"</in1>
                                        </m:getCandidateById>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// Get Logintoken via SOAP call
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <returns>SOAP request result string</returns>
        public string logInTokenSoap(String authToken)
        {
            HttpWebRequest request = CreateWebRequest("getLoginToken");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
										<m:getLoginToken xmlns:m=""urn:TBEWebAPI"">
											<name>" + authToken + @"</name>
										</m:getLoginToken>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// Delete Candidate via SOAP call
        /// </summary>
        /// <param name="authToken">generated authtoken during login</param>
        /// <param name="candidateID">candidateID</param>
        /// <returns>SOAP request result string</returns>
        public string DeleteCandidateSOAP(String authToken, int candidateID)
        {
            HttpWebRequest request = CreateWebRequest("deleteCandidate");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                    <SOAP-ENV:Body>
                                         <m:deleteCandidate xmlns:m=""urn:TBEWebAPI"">
                                            <in0>" + authToken + @"</in0>
                                            <in1>" + candidateID + @"</in1>
                                        </m:deleteCandidate>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        }

        /// <summary>
        /// This method contains enable service SOAP Envelope
        /// </summary>
        /// <param name="authToken">User authentication token</param>
        /// <returns>SOAP request result string</returns>
        public string EnableServiceResponseSOAP(String authToken)
        {
            HttpWebRequest request = CreateWebRequest("getLoginToken");
            String soapBody = @"<SOAP-ENV:Envelope
                                    xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                    xmlns:urn=""urn:TBEWebAPI"">
                                    <SOAP-ENV:Body>
										<urn:getEnabledServices>
                                            <in0>" + authToken + @"</in0>
                                        </urn:getEnabledServices>
                                    </SOAP-ENV:Body>
                                </SOAP-ENV:Envelope>";
            string result = StreamWriteRead(request, soapBody);

            return result;
        } 
        #endregion

    }
}
