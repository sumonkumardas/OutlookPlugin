using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class CreateEmailLogResponse
    {
        #region Property
        public Boolean SuccessResult { get; set; }
        public Exception E { get; set; }


        public int LogID { get; set; } 
        #endregion

        #region Constructor
        public CreateEmailLogResponse(Boolean successResult, Exception e, int logID)
        {
            this.SuccessResult = successResult;
            this.LogID = logID;
            this.E = e;
        } 
        #endregion

        #region Public Method
        /// <summary>
        /// Create Response from XML string
        /// </summary>
        /// <param name="xmlResult">XML formatted string</param>
        /// <returns>Response</returns>
        public static CreateEmailLogResponse ConvertCreateEmailLogResponseFromXMl(string xmlResult)
        {
            try
            {
                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(xmlResult, "createEmailLogReturn");
                String result = resultList != null && resultList.Count != 0 ? resultList.First() : "";

                CreateEmailLogResponse response = new CreateEmailLogResponse(false, null, 0);
                response.LogID = Convert.ToInt32(result);
                response.SuccessResult = true;
                response.E = null;

                if (result == "")
                {
                    response.LogID = 0;
                    response.SuccessResult = false;
                    response.E = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                CreateEmailLogResponse response = new CreateEmailLogResponse(false, null, 0);
                response.LogID = 0;
                response.SuccessResult = false;
                response.E = ex;
                return response;
            }
        }

        public static CreateEmailLogResponse ConvertCreateLogEmailFromJson(string jsonResult)
        {
            CreateEmailLogResponse response = new CreateEmailLogResponse(false,null,0);
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                CreateEmailLogResponse_RootObject obj = serializer.Deserialize<CreateEmailLogResponse_RootObject>(jsonResult);
                response.SuccessResult = true;
                response.E = null;
                response.LogID = obj.response.contactLogId;
                return response;
            }
            catch (Exception ex)
            {
                response = new CreateEmailLogResponse(false,ex, 0);
                return response;
            }
        }
        #endregion
    }

    public class CreateEmailLogResponse_Response
    {
        public int contactLogId { get; set; }
        public string @object { get; set; }
    }

    public class CreateEmailLogResponse_Detail
    {
    }

    public class CreateEmailLogResponse_Status
    {
        public CreateEmailLogResponse_Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class CreateEmailLogResponse_RootObject
    {
        public CreateEmailLogResponse_Response response { get; set; }
        public CreateEmailLogResponse_Status status { get; set; }
    }
}
