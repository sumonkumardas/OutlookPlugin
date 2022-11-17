using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Script.Serialization;
using Util.Utilities;

namespace Model.Response
{
    public class CreateContactResponse
    {
        #region Property
        public Boolean Success { get; set; }
        public CreateContactResponse_RootObject Response { get; set; }
        public Exception Exception { get; set; }
        #endregion

        #region Constructor

        public CreateContactResponse(Boolean successResult, CreateContactResponse_RootObject responseString, Exception newException)
        {
            Success = successResult;
            Response = responseString;
            Exception = newException;
        }

        #endregion

        #region Public Methods

        public static CreateContactResponse ConvertCreateContactFromJson(string jsonResult)
        {
            CreateContactResponse response = new CreateContactResponse(false, null, null);
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                CreateContactResponse_RootObject obj = serializer.Deserialize<CreateContactResponse_RootObject>(jsonResult);
                response.Success = true;
                response.Exception = null;
                response.Response = obj;
                return response;
            }
            catch (Exception ex)
            {
                Logger.WriteLogInformation("CreateContactResponse", "CreateContactResponse", Thread.CurrentThread.Name, ex);
                response = new CreateContactResponse(false, null, null);
                return response;
            }
        }

        #endregion
    }

    public class CreateContactResponse_Response
    {
        public int contactId { get; set; }
        public string @object { get; set; }
    }

    public class CreateContactResponse_Detail
    {
    }

    public class CreateContactResponse_Status
    {
        public CreateContactResponse_Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class CreateContactResponse_RootObject
    {
        public CreateContactResponse_Response response { get; set; }
        public CreateContactResponse_Status status { get; set; }
    }

}
