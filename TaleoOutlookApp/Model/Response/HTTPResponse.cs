using System;

namespace Model.Response
{
    public class HTTPResponse : IHTTPResponse
    {
        #region Property

        public Boolean isSuccess { get; set; }
        public String responseString { get; set; }
        public Exception exception { get; set; } 

        #endregion

        #region Constructor

        public HTTPResponse(Boolean successResult, String newResponseString, Exception newException)
        {
            isSuccess = successResult;
            responseString = newResponseString;
            exception = newException;
        } 

        #endregion
    }
}
