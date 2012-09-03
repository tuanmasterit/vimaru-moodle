using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.Xml;
namespace Moodle
{
    public partial class MoodleWebserviceTest: System.Web.UI.Page
    {
        //XmlDocument doc;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGetToken_Click(object sender, EventArgs e)
        {
            MoodleUser u = new MoodleUser(txtUsername.Text, txtPassword.Text);
            txtToken.Text = u.GetToken("all_service");
            ListItemCollection ls = MoodleWebService.GetServiceList(txtToken.Text);
            txtList.Text = MoodleWebService.GetServiceXml(txtToken.Text);
            ddlWsfuntion.DataSource = ls;
            ddlWsfuntion.DataBind();
        }

        protected void btnGetTreeView_Click(object sender, EventArgs e)
        {
            string postData = "wsfunction=" + ddlWsfuntion.SelectedItem.Text + "&wstoken=" + txtToken.Text + "&" + txtPostData.Text;
            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + "?" + postData);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(web.GetResponse());
            XmlNode xmlnode = doc.ChildNodes[1];
            resultTree.Nodes.Clear();
            resultTree.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = resultTree.Nodes[0];
            AddNode(xmlnode, tNode);
            resultTree.ExpandAll();
        }

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i = 0;
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    try
                    {
                        inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["name"].Value.ToString()));
                    }
                    catch //(System.Exception ex)
                    {
                        inTreeNode.ChildNodes.Add(new TreeNode(xNode.Name));
                    }

                    tNode = inTreeNode.ChildNodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = inXmlNode.InnerText.ToString();
            }
        }
    }
}