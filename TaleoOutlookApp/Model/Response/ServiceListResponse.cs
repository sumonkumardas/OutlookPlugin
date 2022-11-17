using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Model.Response
{
    public class ServiceListResponse : IHTTPResponse
    {
        #region Property
        public Boolean successResult { get; set; }
        public Exception e { get; set; }

        public ServiceListResponse_Response response { get; set; }
        public ServiceListResponse_Response_Status status { get; set; }
        #endregion

        #region Constructor
        

        #endregion

        #region Public Methods

        /// <summary>
        /// Create Response from JSON string
        /// </summary>
        /// <param name="jsonResult">JSON formatted string</param>
        /// <returns>Response</returns>
        public static IHTTPResponse ConvertServiceListResponseFromJson(string responseText)
        {
            try
            {
                responseText = responseText.Replace("\"object\":", "\"_object\":");
                responseText = responseText.Replace("\"standard-description\":", "\"standard_description\":");
                responseText = responseText.Replace("\"custom-description\":", "\"custom_description\":");

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                ServiceListResponse response = serializer.Deserialize<ServiceListResponse>(responseText);
                response.successResult = true;
                response.e = null;
                return response;
            }
            catch (Exception ex)
            {
                ServiceListResponse response = new ServiceListResponse();
                response.e = ex;
                response.successResult = false;
                return response;
            }
        }

        #endregion
    }

    #region Response Result Object Classes

    public class ServiceListResponse_Response_Object_Urls
    {
        public string _object { get; set; }
        public string standard_description { get; set; }
        public string custom_description { get; set; }
        public string instance { get; set; }
        public string collection { get; set; }
    }

    public class ServiceListResponse_Response_Object
    {
        public string name { get; set; }
        public string label { get; set; }
        public string searchable { get; set; }
        public string creatable { get; set; }
        public string deletable { get; set; }
        public ServiceListResponse_Response_Object_Urls urls { get; set; }
    }

    public class ServiceListResponse_Response
    {
        public List<ServiceListResponse_Response_Object> objects { get; set; }
    }

    public class ServiceListResponse_Response_Status_Detail
    {
        public string operation { get; set; }
        public string errormessage { get; set; }
        public string error { get; set; }
        public string errorcode { get; set; }
    }

    public class ServiceListResponse_Response_Status
    {
        public ServiceListResponse_Response_Status_Detail detail { get; set; }
        public bool success { get; set; }
    }
    #endregion
}
