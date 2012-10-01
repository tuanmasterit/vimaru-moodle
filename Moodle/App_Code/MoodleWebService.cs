using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;


namespace Moodle
{
    public class MoodleWebService
    {
        public static string GetServiceXml(string token)
        {
            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.SoapUrl + "?wsdl=1&wstoken=" + token);
            return web.GetResponse();
        }
        public static ListItemCollection GetServiceList(string token)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(GetServiceXml(token));
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("x", "http://schemas.xmlsoap.org/wsdl/");
            XmlNodeList node = doc.SelectNodes("//x:portType/x:operation",nsmgr);
            ListItemCollection ls = new ListItemCollection();

            foreach (XmlNode xn in node)
            {
                ls.Add(new ListItem(xn.Attributes["name"].Value.ToString()));
            }

            //doc.Save("E://test.xml");
            return ls;
        }
    }
}