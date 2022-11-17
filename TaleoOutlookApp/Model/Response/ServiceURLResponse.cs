using System;

namespace Model.Response  
{
    public class ServiceURLResponse : IHTTPResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public String serviceURL { get; set; }
        public Exception exception { get; set; } 
        #endregion

        #region Constructor
        public ServiceURLResponse(Boolean successResult, String newServiceURL, Exception newException)
        {
            isSuccess = successResult;
            serviceURL = newServiceURL;
            exception = newException;
        } 
        #endregion
    }
}
