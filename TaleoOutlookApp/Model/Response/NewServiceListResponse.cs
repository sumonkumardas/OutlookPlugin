using System;

namespace Model.Response
{
    public class NewServiceListResponse : IHTTPResponse
    {
        #region Property
        public Boolean successResult { get; set; }
        public Exception e { get; set; }

        public string response { get; set; }
        #endregion

        #region Constructor

        public NewServiceListResponse(Boolean successResult, string response, Exception e)
        {
            this.successResult = successResult;
            this.e = e;
            this.response = response;
        }

        //public static IHTTPResponse ConvertServiceListResponseFromJson(string responseText)
        //{
        //    try
        //    {
        //        responseText = responseText.Replace("\"object\":", "\"_object\":");
        //        responseText = responseText.Replace("\"standard-description\":", "\"standard_description\":");
        //        responseText = responseText.Replace("\"custom-description\":", "\"custom_description\":");

        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        ServiceListResponse response = serializer.Deserialize<ServiceListResponse>(responseText);
        //        response.successResult = true;
        //        response.e = null;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceListResponse response = new ServiceListResponse();
        //        response.e = ex;
        //        response.successResult = false;
        //        return response;
        //    }
        //}

        #endregion

        #region Public Methods



        #endregion
    }


}
