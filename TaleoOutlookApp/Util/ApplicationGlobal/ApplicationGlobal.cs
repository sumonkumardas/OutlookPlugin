using System;
using System.IO;
using Util.Utilities;

namespace Util.ApplicationGlobal
{
    public static class ApplicationGlobal
    {
        //public static bool USE_REST = true;
        public static bool QA_Environment { get; set; }
        public static bool USE_REST { get; set; }
        public static string QA_REST_DISPATCHER { get; set; }
        public static string PRODUCTION_REST_DISPATCHER { get; set; }
		public static string UserSettingsPath { get; set; }
        public static string SettingsContent { get; set; }
        private static TaleoVersionPath taleoVersionInfo = new TaleoVersionPath();

        public static string FinalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Oracle\\HCM\\SMBOutlookPlugin\\";

		public static string CustomOutlookTodayFileName = "colt.htm";
		public static string OriginalOutlookTodayFileName = "olt.htm";
        public static string OracleTermsAndConditionUrl = "http://www.oracle.com/us/legal/privacy/index.html";
        public static string EncryptionKey = "Together Initiative Pvt. Ltd. Oracle Corporation.";
        public static bool isOutlookVersion2007 { get; set; }

        private static string[] CustomOutlookTodayContent =	{	!isOutlookVersion2007?"<!DOCTYPE html>":"",
																"<html itemscope=\"\" itemtype=\"http://schema.org/WebPage\" lang=\"en\">",
																"	<head>",
																"		<meta http-equiv=\"Refresh\" content=\"1; url=CUSTOM_URL\"/>",
                                                                "       <script>",
                                                                "           function initF()",
                                                                "           {",
//                                                              "               alert('int nua');",
                                                                "               window.location='CUSTOM_URL';",
//                                                              "               var nua = [];",
//                                                              "               alert('before nua');",
//                                                              "               for(var property in navigator)",
//                                                              "                   if (property != 'User-Agent')",
//                                                              "                       nua[property] = navigator[property];",
//                                                              "                   else",
//                                                              "                       nua[property] = '" +  GetUserAgent() +"';",
//                                                              "               alert('after loop');",
//                                                              "               navigator = nua;",
//                                                              "               alert('after nua');",
                                                                "           }",
                                                                "       </script>",
																"	</head>",
																"	<body onload=\"javascript:initF();\">",
																"	</body>",
																"</html>"
															};
		private static string[] prepareCustomHTM(string newURL)
		{
			string[] newCustomOutlookTodayContent = new string[CustomOutlookTodayContent.Length];
			for (int i = 0; i < CustomOutlookTodayContent.Length; i++)
                newCustomOutlookTodayContent[i] = CustomOutlookTodayContent[i].Replace("CUSTOM_URL", newURL );
			return newCustomOutlookTodayContent;
		}

		public static void writeCustomHTM(string newURL)
		{
			File.WriteAllLines(FinalPath + CustomOutlookTodayFileName, prepareCustomHTM(newURL));
		}

        public static string GetUserAgent()
        {
            string userAgent = null;
            userAgent = "Taleo Outlook Plugin/" + taleoVersionInfo.taleoVersion + "(" + taleoVersionInfo.osVersion + "; Microsoft Outlook " + taleoVersionInfo.outlookVersion + ";" + taleoVersionInfo.locale + ")";
            //userAgent = taleoVersionInfo.taleoVersion + ";" + taleoVersionInfo.outlookVersion + ";" +
            //                    taleoVersionInfo.osVersion + ";" + taleoVersionInfo.locale;

            return userAgent;
        }

        public static long CurrentTimeStampInMiliseconds()
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }
    }
}
