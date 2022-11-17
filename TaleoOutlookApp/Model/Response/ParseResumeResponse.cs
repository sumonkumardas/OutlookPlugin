using System;

namespace Model.Response
{
    public class ParseResumeResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public string result { get; set; }
        public Exception exception { get; set; } 
        #endregion

        #region Constructor

        public ParseResumeResponse(Boolean successResult, string result, Exception newException)
        {
            isSuccess = successResult;
            this.result = result;
            exception = newException;
        }

        #region For future Todo
        //public static ParseResumeResponse ConvertParseResumeResponseFromXMl(string xmlResult)
        //{

        //    ParseResumeResponse parseResumeResponse = new ParseResumeResponse(false, null, null);

        //    String isSuccess = false + "";

        //    try
        //    {
        //        XmlParseValue xmlParser = new XmlParseValue();
        //        List<String> resultList = xmlParser.GetNodeValue(xmlResult, "parseResumeReturn");
        //        isSuccess = resultList != null && resultList.Count != 0 ? resultList.First() : "";
        //        parseResumeResponse.isSuccess = isSuccess.ToLower() == "true" ? true : false;
        //        parseResumeResponse.result = resultList;
        //        parseResumeResponse.exception = null;

        //    }
        //    catch (Exception e)
        //    {
        //        parseResumeResponse.isSuccess = false;
        //        parseResumeResponse.result = null;
        //        parseResumeResponse.exception = e;

        //    }

        //    return parseResumeResponse;

        //} 
        #endregion
        #endregion
    }

}
