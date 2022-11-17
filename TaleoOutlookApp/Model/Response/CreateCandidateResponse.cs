using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Util.Utilities;

namespace Model.Response
{
    public class CreateCandidateResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public int CandidateID { get; set; }
        public Exception exception { get; set; }
        #endregion

        #region Constructor

        public CreateCandidateResponse(Boolean successResult, int candidateID, Exception newException)
        {
            isSuccess = successResult;
            this.CandidateID = candidateID;
            exception = newException;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Create Response from XML string
        /// </summary>
        /// <param name="xmlResult">XML formatted string</param>
        /// <returns>Response</returns>
        public static CreateCandidateResponse ConvertCreateCandidateResponseFromXMl(string xmlResult)
        {

            CreateCandidateResponse createCandidateResponse = new CreateCandidateResponse(false, -1, null);

            String isSuccess = false + "";

            try
            {
                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(xmlResult, "createCandidateReturn");
                isSuccess = resultList != null && resultList.Count != 0 ? resultList.First() : "";
                createCandidateResponse.isSuccess = isSuccess.ToLower() == "true" ? true : false;
                createCandidateResponse.CandidateID = Convert.ToInt32(resultList[0]);
                createCandidateResponse.exception = null;

            }
            catch (Exception ex)
            {
                createCandidateResponse.isSuccess = false;
                createCandidateResponse.CandidateID = -1;
                createCandidateResponse.exception = ex;
                Logger.WriteLogInformation("ConvertCreateCandidateResponseFromXMl", MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, ex.Message);
            }

            return createCandidateResponse;

        }
        #endregion
    }

}
