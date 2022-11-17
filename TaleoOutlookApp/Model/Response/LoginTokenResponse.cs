using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class LoginTokenResponse : IHTTPResponse
    {
        #region Property

        public Boolean successResult { get; set; }
        public Exception e { get; set; }

        public LoginTokenResponse_Response response { get; set; }
        public LoginTokenResponse_Status status { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create Response from JSON string
        /// </summary>
        /// <param name="jsonResult">JSON formatted string</param>
        /// <returns>Response</returns>
        public static IHTTPResponse ConvertLoginTokenResponseFromJson(string jsonResult)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                LoginTokenResponse response = serializer.Deserialize<LoginTokenResponse>(jsonResult);
                response.successResult = true;
                response.e = null;
                return response;
            }
            catch (Exception ex)
            {
                LoginTokenResponse response = new LoginTokenResponse();
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
        public static LoginTokenResponse ConvertLoginTokenResponseFromXMl(string xmlResult)
        {
            try
            {
                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(xmlResult, "getLoginTokenReturn");
                String loginToken = resultList != null && resultList.Count != 0 ? resultList.First() : "";

                LoginTokenResponse response = new LoginTokenResponse();
                response.response = new LoginTokenResponse_Response();
                response.status = new LoginTokenResponse_Status();
                response.status.detail = new LoginTokenResponse_Detail();

                response.response.loginToken = loginToken;
                response.status.success = true;
                response.successResult = true;
                response.e = null;

                if (loginToken == "")
                {
                    response.response.loginToken = "";
                    response.status.success = false;
                    response.status.detail.errormessage = xmlResult;
                    response.successResult = false;
                    response.e = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                LoginTokenResponse response = new LoginTokenResponse();
                response.response = new LoginTokenResponse_Response();
                response.status = new LoginTokenResponse_Status();
                response.status.detail = new LoginTokenResponse_Detail();

                response.response.loginToken = "";
                response.status.success = false;
                response.successResult = false;
                response.status.detail.errormessage = xmlResult;
                response.e = ex;
                return response;
            }
        }
        #endregion
    }

    #region LoginTokenResponse Object Classes

    public class LoginTokenResponse_Response
    {
        public string loginToken { get; set; }
    }

    public class LoginTokenResponse_Status
    {
        public LoginTokenResponse_Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class LoginTokenResponse_Detail
    {
        public string operation { get; set; }
        public string errormessage { get; set; }
        public string error { get; set; }
        public string errorcode { get; set; }
    }

    #endregion
}
