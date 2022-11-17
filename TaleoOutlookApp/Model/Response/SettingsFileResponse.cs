using System;

namespace Model.Response
{
    public class SettingsFileResponse : IHTTPResponse
    {
        #region Property
        public Boolean isSuccess { get; set; }
        public String settingsContent { get; set; }
        public Exception exception { get; set; }
        #endregion

        #region Constructor

        public SettingsFileResponse(Boolean successResult, String newServiceURL, Exception newException)
        {
            isSuccess = successResult;
            settingsContent = newServiceURL;
            exception = newException;
        }
        #endregion
    }
}
