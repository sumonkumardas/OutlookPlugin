using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class EnableServiceResponse
    {
        #region Property
        public Boolean SuccessResult { get; set; }
        public Exception E { get; set; }
        public EnableServiceResponse_Object ResponseObject { get; set; }
        public string Response { get; set; }
        #endregion

        #region Constructor
        public EnableServiceResponse(Boolean successResult, Exception e, string response,EnableServiceResponse_Object responseObject)
        {
            this.SuccessResult = successResult;
            this.Response = response;
            this.E = e;
            this.ResponseObject = responseObject;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create Response from XML string
        /// </summary>
        /// <param name="xmlResult">XML formatted string</param>
        /// <returns>Response</returns>
        public static EnableServiceResponse ConvertEnableServiceResponseFromXMl(string xmlResult)
        {
            try
            {
                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(xmlResult, "getEnabledServicesReturn");
                String result = resultList != null && resultList.Count != 0 ? resultList.First() : "";

                EnableServiceResponse response = new EnableServiceResponse(false, null, null,null);
                response.Response = result;
                response.SuccessResult = true;
                response.E = null;

                if (result == "")
                {
                    response.Response = null;
                    response.SuccessResult = false;
                    response.E = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                EnableServiceResponse response = new EnableServiceResponse(false, null, null,null);
                response.Response = null;
                response.SuccessResult = false;
                response.E = ex;
                return response;
            }
        }

        public static EnableServiceResponse ConvertEnableServiceResponseFromJson(string jsonResult)
        {
            try
            {
                EnableServiceResponse response = new EnableServiceResponse(false,null,null,null);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                EnableServiceResponse_Object obj = serializer.Deserialize<EnableServiceResponse_Object>(jsonResult);
                response.SuccessResult = true;
                response.E = null;
                response.ResponseObject = obj;
                return response;
            }
            catch (Exception ex)
            {
                EnableServiceResponse response = new EnableServiceResponse(false, null, null, null)
                {
                    E = ex,
                    SuccessResult = false
                };
                return response;
            }
        }
        #endregion

    }

    public class Orgsetting
    {
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public bool mobileAppEnabled { get; set; }
        public bool taleo2GoPinRequired { get; set; }
        public bool genericAccess { get; set; }
        public bool recruitAccess { get; set; }
        public bool performAccess { get; set; }
        public bool tscAccess { get; set; }
        public bool service5Access { get; set; }
        public bool backupAccess { get; set; }
        public bool offcpAccess { get; set; }
        public bool staffingAccess { get; set; }
        public bool onBoardingAccess { get; set; }
        public bool compAccess { get; set; }
        public bool advancedReportingAccess { get; set; }
        public bool talentExchangeAccess { get; set; }
        public bool advancedOfccpAccess { get; set; }
        public bool positionControlAccess { get; set; }
        public bool singleSignOnAccess { get; set; }
        public bool socialSourcingAccess { get; set; }
        public bool service8Access { get; set; }
        public bool service9Access { get; set; }
    }

    public class EnableServiceResponse_Response
    {
        public Orgsetting orgsetting { get; set; }
    }

    public class EnableServiceResponse_Detail
    {
    }

    public class EnableServiceResponse_Status
    {
        public EnableServiceResponse_Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class EnableServiceResponse_Object
    {
        public EnableServiceResponse_Response response { get; set; }
        public EnableServiceResponse_Detail status { get; set; }
    }

}
