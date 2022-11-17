using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class CandidateAttachmentUploadResponse : IHTTPResponse
    {
        #region Property

        public Boolean successResult { get; set; }
        public Exception e { get; set; }

        public string response { get; set; }

        #endregion

        //#region Public Methods
        ///// <summary>
        ///// Create Response from JSON string
        ///// </summary>
        ///// <param name="jsonResult">JSON formatted string</param>
        ///// <returns>Response</returns>
        //public static IHTTPResponse ConvertLoginResponseFromJson(string jsonResult)
        //{
        //    try
        //    {
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        CommentResponse response = serializer.Deserialize<CommentResponse>(jsonResult);
        //        response.successResult = true;
        //        response.e = null;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommentResponse response = new CommentResponse();
        //        response.e = ex;
        //        response.successResult = false;
        //        return response;
        //    }
        //}

        ///// <summary>
        ///// Create Response from XML string
        ///// </summary>
        ///// <param name="xmlResult">XML formatted string</param>
        ///// <returns>Response</returns>
        ////public static LoginResponse ConvertLoginResponseFromXMl(string xmlResult)
        ////{
        ////    try
        ////    {
        ////        XmlParseValue xmlParser = new XmlParseValue();
        ////        List<String> resultList = xmlParser.GetNodeValue(xmlResult, "loginReturn");
        ////        String authToken = resultList != null && resultList.Count != 0 ? resultList.First() : "";

        ////        ContactResponse response = new ContactResponse();
        ////        response.response = new LoginResponse_Response();
        ////        response.status = new LoginResponse_Status();
        ////        response.status.detail = new LoginResponse_Detail();

        ////        response.response.authToken = authToken;
        ////        response.status.success = true;
        ////        response.successResult = true;
        ////        response.e = null;

        ////        if (authToken == "")
        ////        {
        ////            response.response.authToken = "";
        ////            response.status.success = false;
        ////            response.status.detail.errormessage = xmlResult;
        ////            response.successResult = false;
        ////            response.e = null;
        ////        }

        ////        return response;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        LoginResponse response = new LoginResponse();
        ////        response.response = new LoginResponse_Response();
        ////        response.status = new LoginResponse_Status();
        ////        response.status.detail = new LoginResponse_Detail();

        ////        response.response.authToken = "";
        ////        response.status.success = false;
        ////        response.successResult = false;
        ////        response.status.detail.errormessage = xmlResult;
        ////        response.e = ex;
        ////        return response;
        ////    }
        ////}
        //#endregion
    }

    
}
