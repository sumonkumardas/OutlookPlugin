using System;
using System.Collections.Generic;
using System.Net;

namespace Model.Response  
{
    public class JSessionIDResponse : IHTTPResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public String jSessionID { get; set; }
        public Exception exception { get; set; }
        public List<Cookie> cookies { get; set; }
        #endregion

        #region Constructor

		public JSessionIDResponse(Boolean successResult, String newJSessionID, Exception newException , List<Cookie> newCookies)
        {
            isSuccess = successResult;
			jSessionID = newJSessionID;
            exception = newException;
            cookies = newCookies;
        } 
        #endregion
    }
}
