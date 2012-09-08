using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Moodle
{
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/Category.aspx";
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnCreateCategory_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                lblCategoryMessage.Text = "Vui lòng nhập tên mục!";
                txtName.Focus();
                return;
            }

            MoodleCategory category = new MoodleCategory();
            category.Name = txtName.Text;

            if (txtParent.Text == "")
            {
                category.Parent = 0;
            }
            else
                category.Parent = Convert.ToInt32(txtParent.Text);

            if (txtIdnumber.Text == "")
            {
                category.IdNumber = null;
            }
            else
                category.IdNumber = txtIdnumber.Text;

            if (txtDecscription.Text == "")
            {
                category.Description = null;
            }
            else
                category.Description = txtDecscription.Text;

            if (txtDescriptionFormat.Text == "")
            {
                category.DescriptionFormat = 1;
            }
            else
                category.DescriptionFormat = Convert.ToInt32(txtDescriptionFormat.Text);

            if (txtTheme.Text == "")
            {
                category.Theme = null;
            }
            else
                category.Theme = txtTheme.Text;

            XmlDocument doc = new XmlDocument();

            List<MoodleCategory> list = new List<MoodleCategory>();
            list.Add(category);
            doc.LoadXml(MoodleCategory.CreateCategories(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\Category_" + txtIdnumber.Text + ".xml");
            XmlNode xmlnode = doc.ChildNodes[1];
            treeCategoryDetail.Nodes.Clear();
            treeCategoryDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeCategoryDetail.Nodes[0];
            AddNode(xmlnode, tNode);
            treeCategoryDetail.ExpandAll();
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                lblCategoryMessage.Text = "Vui lòng nhập id mục cần cập nhật!";
                txtId.Focus();
                return;
            }

            MoodleCategory category = new MoodleCategory();

            category.Id = Convert.ToInt32(txtId.Text);

            if (txtName.Text != "")
                category.Name = txtName.Text;

            if (txtParent.Text == "")
            {
                category.Parent = 0;
            }
            else
                category.Parent = Convert.ToInt32(txtParent.Text);

            if (txtIdnumber.Text == "")
            {
                category.IdNumber = null;
            }
            else
                category.IdNumber = txtIdnumber.Text;

            if (txtDecscription.Text == "")
            {
                category.Description = null;
            }
            else
                category.Description = txtDecscription.Text;

            if (txtDescriptionFormat.Text == "")
            {
                category.DescriptionFormat = 1;
            }
            else
                category.DescriptionFormat = Convert.ToInt32(txtDescriptionFormat.Text);

            if (txtTheme.Text == "")
            {
                category.Theme = null;
            }
            else
                category.Theme = txtTheme.Text;

            XmlDocument doc = new XmlDocument();

            List<MoodleCategory> lst = new List<MoodleCategory>();
            lst.Add(category);
            doc.LoadXml(MoodleCategory.UpdateCategories(lst, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\" + txtId.Text + ".xml");
            XmlNode xmlnode = doc.ChildNodes[1];
            treeCategoryDetail.Nodes.Clear();
            treeCategoryDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeCategoryDetail.Nodes[0];
            AddNode(xmlnode, tNode);
            treeCategoryDetail.ExpandAll();
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {
            List<MoodleGetCategory> lst = new List<MoodleGetCategory>();
            int id = ddlCriteria.SelectedIndex;
            if (id == 0)
            {
                lst.Add(new MoodleGetCategory("id",txtId.Text));
                lst.Add(new MoodleGetCategory("name", HttpUtility.HtmlDecode(txtName.Text)));
                lst.Add(new MoodleGetCategory("parent", txtParent.Text));
                lst.Add(new MoodleGetCategory("idnumber", txtIdnumber.Text));
            }
            else if (id == 1)
            {
                lst.Add(new MoodleGetCategory("id", txtId.Text));
            }
            else if (id == 2)
            {
                lst.Add(new MoodleGetCategory("name", HttpUtility.HtmlDecode(txtName.Text)));
            }
            else if(id==3)
            {
                lst.Add(new MoodleGetCategory("parent", txtParent.Text));
            }
            else
            {
                lst.Add(new MoodleGetCategory("idnumber", txtIdnumber.Text));
            }

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(MoodleGetCategory.GetCategories(lst, chkSubCategory.Checked, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\" + txtId.Text + ".xml");
            XmlNode xmlnode = doc.ChildNodes[1];
            treeCategoryDetail.Nodes.Clear();
            treeCategoryDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeCategoryDetail.Nodes[0];
            AddNode(xmlnode, tNode);
            treeCategoryDetail.ExpandAll();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            MoodleDeleteCategory category = new MoodleDeleteCategory
            {
                Id = Convert.ToInt32(txtDeleteId.Text),
                NewParent = Convert.ToInt32(txtNewParent.Text),
                Recursive = rdbDeleteAllChildren.Checked == true?1:0
            };

            List<MoodleDeleteCategory> lst = new List<MoodleDeleteCategory>();
            lst.Add(category);

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(MoodleDeleteCategory.DeleteCategories(lst, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\" + category.Id + ".xml");
            XmlNode xmlnode = doc.ChildNodes[1];
            treeCategoryDetail.Nodes.Clear();
            treeCategoryDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeCategoryDetail.Nodes[0];
            AddNode(xmlnode, tNode);
            treeCategoryDetail.ExpandAll();
        }
    }
}