﻿using System;

namespace Model.Response
{
    public class BulkResumeUploadResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public string responseString { get; set; }
        public Exception exception { get; set; }
        #endregion

        #region Constructor

        public BulkResumeUploadResponse(Boolean successResult, string responseString, Exception newException)
        {
            isSuccess = successResult;
            this.responseString = responseString;
            exception = newException;
        }
        #endregion
    }

}
