using System;

namespace Model.Response
{
    public  class EmailSentLogResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public string  result { get; set; }
        public Exception exception { get; set; } 
        #endregion

        #region Constructor

        public EmailSentLogResponse(Boolean successResult, string result, Exception newException)
        {
            isSuccess = successResult;
            this.result = result;
            exception = newException;
        }

        
        #endregion
    }

}
