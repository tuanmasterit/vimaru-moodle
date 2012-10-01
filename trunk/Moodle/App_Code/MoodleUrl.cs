using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
namespace Moodle
{
    public class MoodleUrl
    {
        public static string RestUrl = WebConfigurationManager.AppSettings["ServerUrl"] + 
            "webservice/rest/server.php";
        public static string LoginUrl = WebConfigurationManager.AppSettings["ServerUrl"] +
            "login/token.php";
        public static string SoapUrl = WebConfigurationManager.AppSettings["ServerUrl"] + 
            "webservice/soap/server.php";
    }
}