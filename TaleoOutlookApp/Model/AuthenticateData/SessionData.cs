using System.Collections.Generic;
using System.Net;
using Util.Enums;

namespace Model.AuthenticateData
{
    public class SessionData
    {
        #region Property
        public LogInData lastLoginData { get; set; }
        public string authToken { get; set; }
        public string loginToken { get; set; }
        public string jSessionID { get; set; }
        public List<Cookie> Cookies { get; set; }

        public bool isLoggedIn { get; set; }
        public bool hasCookies { get; set; }
        public AccessDepth accessDepth { get; set; }
        public string Language { get; set; }
        #endregion

        #region Constructor
        public SessionData(LogInData lastLoginData, string authToken, string loginToken, string jSessionID, List<Cookie> Cookies, bool isLoggedIn, bool hasCookies, AccessDepth accessDepth)
        {
            this.lastLoginData = lastLoginData;
            this.authToken = authToken;
            this.loginToken = loginToken;
            this.jSessionID = jSessionID;
            this.Cookies = Cookies;
            this.isLoggedIn = isLoggedIn;
            this.hasCookies = hasCookies;
            this.accessDepth = accessDepth;
        } 
        #endregion
    }
}
