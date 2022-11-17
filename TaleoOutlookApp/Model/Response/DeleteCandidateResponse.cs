using System;
using System.Web.Script.Serialization;

namespace Model.Response
{
	public class DeleteCandidateResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public string result { get; set; }
        public Exception exception { get; set; } 
        #endregion

        #region Constructor
        public DeleteCandidateResponse(Boolean successResult, string result, Exception newException)
        {
            isSuccess = successResult;
            this.result = result;
            exception = newException;
        } 
        #endregion

        public static DeleteCandidateResponse ConvertLoginTokenResponseFromJson(string jsonResult)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                DeleteCandidateResponse response = serializer.Deserialize<DeleteCandidateResponse>(jsonResult);
                response.isSuccess = true;
                response.exception = null;
                return response;
            }
            catch (Exception ex)
            {
                DeleteCandidateResponse response = new DeleteCandidateResponse(false, null, ex);
                response.exception = ex;
                response.isSuccess = false;
                return response;
            }
        }
    }
}

