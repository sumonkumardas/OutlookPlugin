using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class UserResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public UserResponse_RootObject User { get; set; }
        public Exception exception { get; set; }
        #endregion

        #region Constructor

        public UserResponse(Boolean successResult, UserResponse_RootObject User, Exception newException)
        {
            isSuccess = successResult;
            this.User = User;
            exception = newException;
        }

        #endregion

        #region Public Methods

        public static UserResponse ConvertUserResponseFromJson(string jsonResult)
        {
            UserResponse response = new UserResponse(false, null, null);
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                UserResponse_RootObject obj = serializer.Deserialize<UserResponse_RootObject>(jsonResult);
                response.isSuccess = true;
                response.exception = null;
                response.User = obj;
                return response;
            }
            catch (Exception ex)
            {
                response = new UserResponse(false, null, ex);
                response.exception = ex;
                response.isSuccess = false;
                return response;
            }
        }

        #endregion
    }


    public class UserResponse_Pagination
    {
        public int total { get; set; }
        public string self { get; set; }
    }

    public class UserResponse_RelationshipUrls
    {
        public string department { get; set; }
        public string division { get; set; }
        public string employee { get; set; }
        public string location { get; set; }
        public string manager { get; set; }
        public string region { get; set; }
        public string status { get; set; }
        public string attachments { get; set; }
        public string historylog { get; set; }
    }

    public class UserResponse_User
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public int userId { get; set; }
        public string lastName { get; set; }
        public string role { get; set; }
        public int status { get; set; }
        public string loginName { get; set; }
    }

    public class UserResponse_SearchResult
    {
        public UserResponse_User user { get; set; }
    }

    public class UserResponse_Response
    {
        public UserResponse_Pagination pagination { get; set; }
        public List<UserResponse_SearchResult> searchResults { get; set; }
    }

    public class UserResponse_Detail
    {
    }

    public class UserResponse_Status
    {
        public Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class UserResponse_RootObject
    {
        public UserResponse_Response response { get; set; }
        public Status UserResponse_Status { get; set; }
    }
}
