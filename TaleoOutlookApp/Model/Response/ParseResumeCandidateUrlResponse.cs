using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class ParseResumeCandidateUrlResponse : IHTTPResponse
    {
        #region Property

        public Boolean successResult { get; set; }
        public Exception e { get; set; }

        public ParseResumeCandidateUrlResponse_Object response { get; set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// Create Response from JSON string
        /// </summary>
        /// <param name="jsonResult">JSON formatted string</param>
        /// <returns>Response</returns>
        public static ParseResumeCandidateUrlResponse ConvertResumeCandidateUrlFromJson(string jsonResult)
        {
            ParseResumeCandidateUrlResponse response = new ParseResumeCandidateUrlResponse();
            try
            {
                

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                ParseResumeCandidateUrlResponse_Object obj = serializer.Deserialize<ParseResumeCandidateUrlResponse_Object>(jsonResult);
                response.successResult = true;
                response.e = null;
                response.response = obj;
                return response;
            }
            catch (Exception ex)
            {
                response = new ParseResumeCandidateUrlResponse();
                response.e = ex;
                response.successResult = false;
                response.response = null;
                return response;
            }
        }
        #endregion
    }

    public class ParseResumeCandidateUrlResponse_Response
    {
        public string url { get; set; }
    }

    public class ParseResumeCandidateUrlResponse_Detail
    {
        public string operation { get; set; }
        public string errormessage { get; set; }
        public string error { get; set; }
        public string errorcode { get; set; }
    }

    public class ParseResumeCandidateUrlResponse_Status
    {
        public ParseResumeCandidateUrlResponse_Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class ParseResumeCandidateUrlResponse_Object
    {
        public ParseResumeCandidateUrlResponse_Response response { get; set; }
        public ParseResumeCandidateUrlResponse_Status status { get; set; }
    }
}
