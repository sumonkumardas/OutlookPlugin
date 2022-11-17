using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.ApplicationGlobal;

namespace Util.Utilities
{
    public static class SetCommonSettings
        
    {
        private static Settings applicationSetting = new Settings(ApplicationGlobal.ApplicationGlobal.FinalPath + "ApplicationSettings.ini");
        private static bool USE_REST = false;
        private static bool QA_ENVIRONMENT = false;
        private static string QA_REST_DISPATCHER = "https://qa.tbe.taleocloud.net/qa2/dispatcher/api/v1/serviceUrl/";
        private static string PRODUCTION_REST_DISPATCHER = "https://tbe.taleo.net/MANAGER/dispatcher/api/v1/serviceUrl/";

        /// <summary>
        ///  Check is use REST or not
        /// </summary>
        /// <returns></returns>
        public static bool IsUseREST()
        {
            string restValue = applicationSetting.getValue("USE_REST").ToLower();
            switch (restValue)
            {
                case "true":
                    USE_REST = true;
                    break;
                case "false":
                    USE_REST = false;
                    break;
                default: USE_REST = true;
                    break;
            }
            return USE_REST;
        }
        /// <summary>
        /// Check is use QA environment or production environment
        /// </summary>
        /// <returns>if use QA environment then return true otherwise return false</returns>
        /// <summary>
        /// Check is production/QA environment or not
        /// </summary>
        /// <returns></returns>
        public static bool IsUseQAEnvironment()
        {
            string qaValue = applicationSetting.getValue("QA_ENVIRONMENT").ToLower();
            switch (qaValue)
            {
                case "true":
                    QA_ENVIRONMENT = true;
                    break;
                case "false":
                    QA_ENVIRONMENT = false;
                    break;
                default:
                    QA_ENVIRONMENT = false;
                    break;
            }
            return QA_ENVIRONMENT;
        }
        /// <summary>
        /// Globally set dispatcher URL for REST
        /// </summary>
        /// <summary>
        /// Set dispatcher url for REST
        /// </summary>
        public static void SetDispatcherForREST()
        {
            string value = applicationSetting.getValue("QA_REST_DISPATCHER");

            if (!string.IsNullOrEmpty(value))
            {
                ApplicationGlobal.ApplicationGlobal.QA_REST_DISPATCHER = value;
            }
            else
            {
                ApplicationGlobal.ApplicationGlobal.QA_REST_DISPATCHER = QA_REST_DISPATCHER;
            }

            value = applicationSetting.getValue("PRODUCTION_REST_DISPATCHER");

            if (!string.IsNullOrEmpty(value))
            {
                ApplicationGlobal.ApplicationGlobal.PRODUCTION_REST_DISPATCHER = value;
            }
            else
            {
                ApplicationGlobal.ApplicationGlobal.PRODUCTION_REST_DISPATCHER = PRODUCTION_REST_DISPATCHER;
            }

        }
    }
}
