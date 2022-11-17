using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class GetCandidateByIdResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public string result { get; set; }
        public Exception exception { get; set; }

        public GetCandidateByIdResponse_Object ResponseObject { get; set; }

        #endregion

        #region Constructor
        public GetCandidateByIdResponse(Boolean successResult, string result, Exception newException)
        {
            isSuccess = successResult;
            this.result = result;
            exception = newException;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create Response from XML string
        /// </summary>
        /// <param name="xmlResult">XML formatted string</param>
        /// <returns>Response</returns>
        /// 
        public static GetCandidateByIdResponse ConvertLoginTokenResponseFromJson(string jsonResult)
        {
            GetCandidateByIdResponse response = new GetCandidateByIdResponse(false,null,null);
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                GetCandidateByIdResponse_Object obj = serializer.Deserialize<GetCandidateByIdResponse_Object>(jsonResult);
                response.ResponseObject = obj;
                response.isSuccess = true;
                response.exception = null;
                return response;
            }
            catch (Exception ex)
            {
                response = new GetCandidateByIdResponse(false, null, ex);
                response.exception = ex;
                response.isSuccess = false;
                return response;
            }
        }
        public static GetCandidateByIdResponse ConvertCandidateEmailFromXMl(string xmlResult)
        {
            try
            {
                XmlParseValue xmlParser = new XmlParseValue();
                List<String> resultList = xmlParser.GetNodeValue(xmlResult, "email");
                String email = resultList != null && resultList.Count != 0 ? resultList.First() : "";

                GetCandidateByIdResponse response = new GetCandidateByIdResponse(true, email, null);

                if (email == "")
                {
                    response = new GetCandidateByIdResponse(false, email, null);
                }

                return response;
            }
            catch (Exception ex)
            {
                return new GetCandidateByIdResponse(false, null, ex);
            }
        }
        #endregion

    }

    public class RelationshipUrls
    {
        public string referredById { get; set; }
        public string employee { get; set; }
        public string status { get; set; }
        public string requisition { get; set; }
        public string attachments { get; set; }
        public string resume { get; set; }
        public string interview { get; set; }
        public string workhistory { get; set; }
        public string reference { get; set; }
        public string education { get; set; }
        public string residence { get; set; }
        public string certificate { get; set; }
        public string backgroundcheck { get; set; }
        public string offer { get; set; }
        public string expense { get; set; }
        public string comment { get; set; }
        public string historylog { get; set; }
        public string contactlog { get; set; }
    }

    public class Candidate
    {
/*        public string candAcceptedOfferPDF { get; set; }
        public object candAcceptedOfferId { get; set; }
        public string creationDate { get; set; }
        public int referredById { get; set; }
        public int candStartStatus { get; set; }
        public bool candReqAce { get; set; }
        public string cc305PDFCandidate { get; set; }
        public string cws { get; set; }
        public string IndividualWithDisability { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string resumeText { get; set; }
        public string birthdate { get; set; }
*/        public string email { get; set; }
        /*
        public int employee { get; set; }
        public object EncryptedDate { get; set; }
        public object EncryptedDateTest { get; set; }
        public string firstName { get; set; }
        public bool flagged { get; set; }
        public string gender { get; set; }
        public string googleSearch { get; set; }
        public object hiredDate { get; set; }
        public object hiredForReqDepartment { get; set; }
        public object hiredForReqId { get; set; }
        public string hiredForReqJobCode { get; set; }
        public object hiredForReqLocation { get; set; }
        public string hiredForReqTitle { get; set; }
*/        public int candId { get; set; }
/*        public string inStatusDate { get; set; }
        public string KathyCustomCurrency { get; set; }
        public string KathyNewEncrypDate { get; set; }
        public string lastName { get; set; }
        public string lastUpdated { get; set; }
        public string legalStatus { get; set; }
        public string licenseNumber { get; set; }
        public string linkedInSearch { get; set; }
        public string county { get; set; }
        public string ReasonRej { get; set; }
        public int status { get; set; }
        public string maritalStatus { get; set; }
        public int rank { get; set; }
        public string middleInitial { get; set; }
        public string cellPhone { get; set; }
        public string passportNumber { get; set; }
        public string cwsPassword { get; set; }
        public string phone { get; set; }
        public List<object> PickListMultipe { get; set; }
        public object preferredLocale { get; set; }
        public string race { get; set; }
        public string referredBy { get; set; }
        public string religion { get; set; }
        public string salutation { get; set; }
        public string SinglePicklist { get; set; }
        public string ssn { get; set; }
        public string source { get; set; }
        public object startDate { get; set; }
        public string state { get; set; }
        public string address2 { get; set; }
        public string address { get; set; }
        public string nameSuffix { get; set; }
        public string CAND__001 { get; set; }
        public List<object> veteran { get; set; }
        public string zipCode { get; set; }
        public object resumeFileName { get; set; }
        public object resumeContentType { get; set; }
        public RelationshipUrls relationshipUrls { get; set; }
*/    }

    public class GetCandidateByIdResponse_Response
    {
        public Candidate candidate { get; set; }
    }

    public class GetCandidateByIdResponse_Detail
    {
    }

    public class GetCandidateByIdResponse_Status
    {
        public Detail GetCandidateByIdResponse_Detail { get; set; }
        public bool success { get; set; }
    }

    public class GetCandidateByIdResponse_Object
    {
        public GetCandidateByIdResponse_Response response { get; set; }
        public GetCandidateByIdResponse_Status status { get; set; }
    }
}

