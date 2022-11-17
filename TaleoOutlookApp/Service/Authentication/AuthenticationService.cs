using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Model.AuthenticateData;
using Model.Response;
using Service.AddIn;
using Util.Enums;
using Util.Utilities;
using System.Threading;

namespace Service.Authentication
{
    public  class AuthenticationService
    {
        #region Property
        private AddInServices addInService = null;
        private string serviceURL = null;
        public bool USE_REST { get; set; }
        public string authToken { get; set; }
        public string loginToken { get; set; }
        public string jSessionId { get; set; }
        public LogInData _lastLogInData { get; set; }
        public List<Cookie> cookies { get; set; }
        private static AuthenticationService authenticationUtil = null; 
        #endregion

        #region Constructor
        public AuthenticationService()
        {
            authenticationUtil = this;
        }

        public static AuthenticationService getInstance(string serviceURL, bool USE_REST)
        {
            if (authenticationUtil == null || string.IsNullOrEmpty(authenticationUtil.serviceURL))
            {
                
                authenticationUtil = new AuthenticationService(serviceURL, USE_REST);
            }
            return authenticationUtil;
        }

        public AuthenticationService(string serviceURL, bool USE_REST)
        {
            addInService = AddInServices.getInstance();
            this.serviceURL = serviceURL;
            this.USE_REST = USE_REST;
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// Check Logindata and set access level
        /// </summary>
        /// <param name="currentSessionData">Current login data</param>
        /// <param name="accessDepth">access depth</param>
        /// <param name="autoLogout">autologout</param>
        /// <param name="checkSessionValidityBeforeNewAuthentication">check session validity</param>
        /// <returns>Session Data</returns>
        public SessionData checkAuthentication(SessionData currentSessionData, AccessDepth accessDepth, bool autoLogout, bool checkSessionValidityBeforeNewAuthentication)
        {
            Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "Inside checkAuthentication");
            bool validLogIn = false;
            JSessionIDResponse jSessionResponse = null;
            if (currentSessionData != null)
            {
                string previousAuthToken = currentSessionData.authToken;

                if (checkSessionValidityBeforeNewAuthentication)
                {
                    try
                    {
                        //validLogIn = addInService.getServiceList(previousAuthToken).successResult;
                        validLogIn = currentSessionData.isLoggedIn;
                    }
                    catch (Exception)
                    {
                        validLogIn = false;
                    }
                }
                if ((checkSessionValidityBeforeNewAuthentication && !validLogIn) || !checkSessionValidityBeforeNewAuthentication)
                {
                    //login --> get & save currentSessionData
                    if (!checkLogIn(currentSessionData.lastLoginData))
                    {
                        currentSessionData = new SessionData(new LogInData(), string.Empty, string.Empty, string.Empty, new List<Cookie>(), false, false, AccessDepth.None);
                        return currentSessionData;
                    }
                    else
                    {
                        currentSessionData.authToken = authToken;
                    }

                    Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "After checkLogIn-" + "previousAuthToken-" + previousAuthToken + "-currentSessionData.authToken-" + currentSessionData.authToken);
                }
                else if (checkSessionValidityBeforeNewAuthentication && validLogIn)
                {
                    // no need to login again
                }
                // i have valid login

                currentSessionData.accessDepth = accessDepth;
                switch (accessDepth)
                {
                    case AccessDepth.None:
                        currentSessionData.isLoggedIn = false;
                        currentSessionData.hasCookies = false;
                        if (!USE_REST)
                            addInService.logOutSOAP(serviceURL, currentSessionData.authToken);
                        else
                            addInService.logout(currentSessionData.authToken);
                        break;

                    case AccessDepth.Only_AuthToken:
                        currentSessionData.isLoggedIn = true;
                        currentSessionData.hasCookies = false;

                        break;

                    case AccessDepth.Only_Cookies:
                        //get cookies & loginToken & jSessionID --> currentSessionData
                        jSessionResponse = addInService.getJSessionResponse(serviceURL, loginToken, currentSessionData.lastLoginData.companyCode);
                        currentSessionData.jSessionID = jSessionResponse.jSessionID;
                        currentSessionData.Cookies = jSessionResponse.cookies;
                        currentSessionData.loginToken = loginToken;
                        currentSessionData.isLoggedIn = false;
                        currentSessionData.hasCookies = true;
                        if (!USE_REST)
                            addInService.logOutSOAP(serviceURL, currentSessionData.authToken);
                        else
                            addInService.logout(currentSessionData.authToken);
                        //logout(currentSessionData.authToken)
                        break;

                    case AccessDepth.Upto_Cookies:
                        //get cookies & loginToken & jSessionID --> currentSessionData
                        LoginTokenResponse loginTokenResponse = null;
                        if(!USE_REST)
                            loginTokenResponse = addInService.logInTokenSOAP(serviceURL, currentSessionData.authToken);
                        else
                            loginTokenResponse = addInService.LogInTokenREST(currentSessionData.authToken);
                        if (loginTokenResponse.successResult)
                        {
                            loginToken = loginTokenResponse.response.loginToken;
                        }
                        jSessionResponse = addInService.getJSessionResponse(serviceURL, loginToken, currentSessionData.lastLoginData.companyCode);
                        currentSessionData.jSessionID = jSessionResponse.jSessionID;
                        currentSessionData.Cookies = jSessionResponse.cookies;
                        currentSessionData.loginToken = loginToken;

                        currentSessionData.isLoggedIn = true;
                        currentSessionData.hasCookies = true;
                        break;

                    case AccessDepth.Only_LogInToken:
                        //get cookies & loginToken & jSessionID --> currentSessionData
                        LoginTokenResponse loginTokenResponse1 = null;
                        if (!USE_REST)
                            loginTokenResponse1 = addInService.logInTokenSOAP(serviceURL, currentSessionData.authToken);
                        else
                            loginTokenResponse1 = addInService.LogInTokenREST(currentSessionData.authToken);
                        if (loginTokenResponse1.successResult)
                        {
                            loginToken = loginTokenResponse1.response.loginToken;
                        }
                        currentSessionData.loginToken = loginToken;

                        currentSessionData.isLoggedIn = true;
                        currentSessionData.hasCookies = false;
                        break;
                }

                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "Inside checkAuthentication-" + "previousAuthToken-" + previousAuthToken + "-currentSessionData.authToken-" + currentSessionData.authToken);

                if (currentSessionData.authToken != previousAuthToken && !(String.IsNullOrEmpty(previousAuthToken)))
                {
                    //logout(previousAuthToken)
                    if (!USE_REST)
                        addInService.logOutSOAP(serviceURL, previousAuthToken);
                    else
                        addInService.logout(previousAuthToken);
                }
            }

            if (currentSessionData != null && (autoLogout && currentSessionData.isLoggedIn == true && !(String.IsNullOrEmpty(currentSessionData.authToken))))
            {
                Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "Inside checkAuthentication-" + "logout current-currentSessionData.authToken-" + currentSessionData.authToken);
                if (!USE_REST)
                    addInService.logOutSOAP(serviceURL, currentSessionData.authToken);
                else
                    addInService.logout(currentSessionData.authToken);
            }

            return currentSessionData;
        }

        #endregion

        #region Miscellaneous
        /// <summary>
        /// Try to determine that login with last login data is successful or not
        /// </summary>
        /// <param name="lastLogInData">Last login data object includes username,password,comany code and remember ne</param>
        /// <returns></returns>
        private bool checkLogIn(LogInData lastLogInData)
        {
            addInService.setBaseURL(serviceURL);
            LoginResponse loginResponse = null;
            
            loginResponse = logInServiceCaller(lastLogInData, loginResponse);
            //Logger.WriteLogInformation(GetType().Name, MethodBase.GetCurrentMethod().Name, Thread.CurrentThread.Name, "After checkLogIn-" + "-currentSessionData.authToken-" + loginResponse.response.authToken);
            

            return (!String.IsNullOrEmpty(authToken));
        }

        /// <summary>
        /// Get login response with last login data
        /// </summary>
        /// <param name="lastLogInData">Last login data object includes username,password,comany code and remember ne</param>
        /// <param name="loginResponse">login response of login request</param>
        /// <returns></returns>
        private LoginResponse logInServiceCaller(LogInData lastLogInData, LoginResponse loginResponse)
        {
            if (USE_REST)
            {
                //serviceURL = serviceURL.Replace("servlet/rpcrouter", "api/v1");
                //serviceURL = serviceURL.Replace("services/rpcrouter", "api/v1");
                addInService.setBaseURL(serviceURL);
                loginResponse = addInService.LogInREST(lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
                if (loginResponse.successResult)
                    authToken = loginResponse.response.authToken;
                else
                    authToken = "";
            }

            if (string.IsNullOrEmpty(authToken) && (serviceURL.Contains("servlet/rpcrouter") || serviceURL.Contains("services/rpcrouter")) && !USE_REST)
            {
                addInService.setBaseURL(serviceURL);
                loginResponse = addInService.logInSOAP(serviceURL, lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
                if (loginResponse.successResult)
                    authToken = loginResponse.response.authToken;
                else
                    authToken = "";

            }
            if (string.IsNullOrEmpty(authToken) && serviceURL.Contains("api/v1") && !USE_REST)
            {
                serviceURL = serviceURL.Replace("api/v1", "services/rpcrouter");

                if (serviceURL.EndsWith("/"))
                    serviceURL = serviceURL.Substring(0, serviceURL.Length - 1);

                addInService.setBaseURL(serviceURL);
                loginResponse = addInService.logInSOAP(serviceURL, lastLogInData.userName, lastLogInData.password, lastLogInData.companyCode);
                if (loginResponse.successResult)
                    authToken = loginResponse.response.authToken;
                else
                    authToken = "";

            }
            Logger.WriteLogInformation(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "", new string[] { authToken });
            return loginResponse;
        }

        //unused
        private void postLogInActivity(bool autoLogOut)
        {
            LoginTokenResponse loginTokenResponse = addInService.logInTokenSOAP(serviceURL, authToken);

            if (loginTokenResponse.successResult)
            {
                loginToken = loginTokenResponse.response.loginToken;
            }
            JSessionIDResponse response = addInService.getJSessionResponse(serviceURL, loginToken, _lastLogInData.companyCode);
            jSessionId = response.jSessionID;
            cookies = response.cookies;
            if (autoLogOut)
            {
                LogoutResponse los = addInService.logOutSOAP(serviceURL, authToken);
                if (los.successResult)
                {
                }

            }
        } 
        #endregion
    }


}
