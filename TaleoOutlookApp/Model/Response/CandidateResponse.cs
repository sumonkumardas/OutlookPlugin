using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class CandidateResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public int CandidateID { get; set; }
        public Exception exception { get; set; }
        #endregion

        #region Constructor

        public CandidateResponse(Boolean successResult, int candidateID, Exception newException)
        {
            isSuccess = successResult;
            this.CandidateID = candidateID;
            exception = newException;
        }

        #endregion

        #region Public Methods

        public static CandidateResponse ConvertLoginTokenResponseFromJson(string jsonResult)
        {
            CandidateResponse response = new CandidateResponse(false,0,null);
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                CandidateResponse_Object obj = serializer.Deserialize<CandidateResponse_Object>(jsonResult);
                response.isSuccess = true;
                response.exception = null;
                response.CandidateID = obj.response.searchResults[0].candidate.candId;
                return response;
            }
            catch (Exception ex)
            {
                response = new CandidateResponse(false, 0, ex);
                response.exception = ex;
                response.isSuccess = false;
                return response;
            }
        }

        #endregion
    }


    public class Pagination
    {
        public int total { get; set; }
        public string self { get; set; }
    }

    public class CandidateResponse_RelationshipUrls
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

    public class CandidateResponse_Candidate
    {
        public string candAcceptedOfferPDF { get; set; }
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
        public string email { get; set; }
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
        public int candId { get; set; }
        public string inStatusDate { get; set; }
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
        public string preferredLocale { get; set; }
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
    }

    public class SearchResult
    {
        public CandidateResponse_Candidate candidate { get; set; }
    }

    public class CandidateResponse_Response
    {
        public Pagination pagination { get; set; }
        public List<SearchResult> searchResults { get; set; }
    }

    public class CandidateResponse_Detail
    {
    }

    public class CandidateResponse_Status
    {
        public Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class CandidateResponse_Object
    {
        public CandidateResponse_Response response { get; set; }
        public CandidateResponse_Status status { get; set; }
    }
}
