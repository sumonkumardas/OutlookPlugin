using System;

namespace Model.Response
{
    public class ParseResumeIntoCandidateResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public int candidateId { get; set; }
        public int dup { get; set; }
        public Exception exception { get; set; }
        #endregion

        #region Constructor

        public ParseResumeIntoCandidateResponse(Boolean successResult, int candidateId, int dup, Exception newException)
        {
            isSuccess = successResult;
            this.candidateId = candidateId;
            this.dup = dup;
            exception = newException;
        }
        #endregion
    }

}
