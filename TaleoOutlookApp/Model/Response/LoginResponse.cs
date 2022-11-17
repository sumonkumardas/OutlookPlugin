using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class LoginResponse : IHTTPResponse
    {
        #region Property

        public Boolean successResult { get; set; }
        public Exception e { get; set; }

        public LoginResponse_Response response { get; set; }
        public LoginResponse_Status status { get; set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// Create Response from JSON string
        /// </summary>
        /// <param name="jsonResult">JSON formatted string</param>
        /// <returns>Response</returns>
        public static IHTTPResponse ConvertLoginResponseFromJson(string jsonResult)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                LoginResponse response = serializer.Deserialize<LoginResponse>(jsonResult);
                response.successResult = true;
                response.e = null;
                return response;
            }
            catch (Exception ex)
            {
                LoginResponse response = new LoginResponse();
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
        public static LoginResponse ConvertLoginResponseFromXMl(string xmlResult)
        {
            try
            {
                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(xmlResult, "loginReturn");
                String authToken = resultList != null && resultList.Count != 0 ? resultList.First() : "";

                LoginResponse response = new LoginResponse();
                response.response = new LoginResponse_Response();
                response.status = new LoginResponse_Status();
                response.status.detail = new LoginResponse_Detail();

                response.response.authToken = authToken;
                response.status.success = true;
                response.successResult = true;
                response.e = null;

                if (authToken == "")
                {
                    response.response.authToken = "";
                    response.status.success = false;
                    response.status.detail.errormessage = xmlResult;
                    response.successResult = false;
                    response.e = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                LoginResponse response = new LoginResponse();
                response.response = new LoginResponse_Response();
                response.status = new LoginResponse_Status();
                response.status.detail = new LoginResponse_Detail();

                response.response.authToken = "";
                response.status.success = false;
                response.successResult = false;
                response.status.detail.errormessage = xmlResult;
                response.e = ex;
                return response;
            }
        }
        #endregion
    }

    #region LoginResponse Object Classes

    public class LoginResponse_Response
    {
        public string authToken { get; set; }
    }

    public class LoginResponse_Status
    {
        public LoginResponse_Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class LoginResponse_Detail
    {
        public string operation { get; set; }
        public string errormessage { get; set; }
        public string error { get; set; }
        public string errorcode { get; set; }
    } 

    #endregion
}
