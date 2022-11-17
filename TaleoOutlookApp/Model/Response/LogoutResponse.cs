using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class LogoutResponse : IHTTPResponse
    {
        #region Property

        public Boolean successResult { get; set; }
        public Exception e { get; set; }

        public ParseEmailResponse_Response response { get; set; }
        public LogoutResponse_Status status { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create Response from JSON string
        /// </summary>
        /// <param name="jsonResult">JSON formatted string</param>
        /// <returns>Response</returns>
        public static IHTTPResponse ConvertLogoutResponseFromJson(string jsonResult)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                LogoutResponse response = serializer.Deserialize<LogoutResponse>(jsonResult);
                response.successResult = true;
                response.e = null;
                return response;
            }
            catch (Exception ex)
            {
                LogoutResponse response = new LogoutResponse();
                response.e = ex;
                response.successResult = false;
                return response;
            }
        }

        /// <summary>
        /// Create Response from XML string
        /// </summary>
        /// <param name="xmlResult">XML formatted string</param>
        /// <returns>Response</returns>
        public static LogoutResponse ConvertLoginResponseFromXMl(string xmlResult)
        {

            LogoutResponse logoutResponse = new LogoutResponse();
            logoutResponse.response = new ParseEmailResponse_Response();
            logoutResponse.status = new LogoutResponse_Status();
            logoutResponse.status.detail = new LogoutResponse_Detail();

            logoutResponse.status.detail.error = "";
            logoutResponse.status.detail.errormessage = "";
            logoutResponse.response.responseString = xmlResult;

            String isSuccess = false + "";

            try
            {
                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(xmlResult, "success");
                isSuccess = resultList != null && resultList.Count != 0 ? resultList.First() : "";

            }
            catch (Exception e)
            {
                logoutResponse.status.detail.error = e.StackTrace;
                logoutResponse.status.detail.errormessage = e.Message;
            }
            logoutResponse.successResult = isSuccess.ToLower() == "true" ? true : false;
            logoutResponse.status.success = logoutResponse.successResult;

            return logoutResponse;
        }

        #endregion
    }

    #region Logout Response Objects

    public class ParseEmailResponse_Response
    {
        public string responseString { get; set; }
    }

    public class LogoutResponse_Status
    {
        public LogoutResponse_Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class LogoutResponse_Detail
    {
        public string operation { get; set; }
        public string errormessage { get; set; }
        public string error { get; set; }
        public string errorcode { get; set; }
    }
    #endregion
}
