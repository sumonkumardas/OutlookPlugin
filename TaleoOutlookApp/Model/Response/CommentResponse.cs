using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class CommentResponse : IHTTPResponse
    {
        #region Property

        public Boolean successResult { get; set; }
        public Exception e { get; set; }

        public string response { get; set; }

        public RootObject ResponseObject { get; set; }  

        #endregion

        #region Public Methods

        /// <summary>
        /// Create Response from JSON string
        /// </summary>
        /// <param name="jsonResult">JSON formatted string</param>
        /// <returns>Response</returns>
        public static CommentResponse ConvertCommentResponseFromJson(string jsonResult)
        {
            try
            {
                CommentResponse response = new CommentResponse();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                RootObject obj = serializer.Deserialize<RootObject>(jsonResult);
                response.successResult = true;
                response.e = null;
                response.ResponseObject = obj;
                return response;
            }
            catch (Exception ex)
            {
                CommentResponse response = new CommentResponse();
                response.e = ex;
                response.successResult = false;
                return response;
            }
        }

        ///// <summary>
        ///// Create Response from XML string
        ///// </summary>
        ///// <param name="xmlResult">XML formatted string</param>
        ///// <returns>Response</returns>
        //public static LoginResponse ConvertLoginResponseFromXMl(string xmlResult)
        //{
        //    try
        //    {
        //        XmlParseValue xmlParser = new XmlParseValue();
        //        List<String> resultList = xmlParser.GetNodeValue(xmlResult, "loginReturn");
        //        String authToken = resultList != null && resultList.Count != 0 ? resultList.First() : "";

        //        ContactResponse response = new ContactResponse();
        //        response.response = new LoginResponse_Response();
        //        response.status = new LoginResponse_Status();
        //        response.status.detail = new LoginResponse_Detail();

        //        response.response.authToken = authToken;
        //        response.status.success = true;
        //        response.successResult = true;
        //        response.e = null;

        //        if (authToken == "")
        //        {
        //            response.response.authToken = "";
        //            response.status.success = false;
        //            response.status.detail.errormessage = xmlResult;
        //            response.successResult = false;
        //            response.e = null;
        //        }

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        LoginResponse response = new LoginResponse();
        //        response.response = new LoginResponse_Response();
        //        response.status = new LoginResponse_Status();
        //        response.status.detail = new LoginResponse_Detail();

        //        response.response.authToken = "";
        //        response.status.success = false;
        //        response.successResult = false;
        //        response.status.detail.errormessage = xmlResult;
        //        response.e = ex;
        //        return response;
        //    }
        //}


        #endregion
    }

    public class Response
    {
        public int commentId { get; set; }
        public string @object { get; set; }
    }

    public class Detail
    {
    }

    public class Status
    {
        public Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class RootObject
    {
        public Response response { get; set; }
        public Status status { get; set; }
    }
}
