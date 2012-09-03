using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class MoodleUrl
    {
        public static string RestUrl = "http://localhost:8080/moodle/webservice/rest/server.php";
        public static string LoginUrl = "http://localhost:8080/moodle/login/token.php";
        public static string SoapUrl = "http://localhost:8080/moodle/webservice/soap/server.php";
    }
}