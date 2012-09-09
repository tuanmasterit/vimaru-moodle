using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Xml;

namespace Moodle
{
    public partial class Faculty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/Faculty.aspx";
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
                grvFaculty.DataBind();
        }

        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvFaculty.PageSize = Convert.ToInt16(cboPageSize.SelectedValue);
        }

        protected void grvFaculty_PreRender(object sender, EventArgs e)
        {
            grvFaculty.DataBind();
            PopulateCheckedValues();
        }

        protected void grvFaculty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = grvFaculty.Rows.Count.ToString();
                e.Row.Cells[1].Text = "Trang " + (grvFaculty.PageIndex + 1) + "/" + grvFaculty.PageCount;
            }
        }

        protected void SaveCheckedValues()
        {
            try
            {
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                string MaKhoa = "0";
                CheckBox chk;

                foreach (GridViewRow rowItem in grvFaculty.Rows)
                {

                    chk = (CheckBox)(rowItem.FindControl("chk"));
                    MaKhoa = grvFaculty.DataKeys[rowItem.RowIndex]["MaKhoa"].ToString();
                    if (chk.Checked)
                    {
                        if (!arrIDs.Contains(MaKhoa))
                        {
                            arrIDs.Add(MaKhoa);
                        }
                    }
                    else
                    {
                        if (arrIDs.Contains(MaKhoa))
                        {
                            arrIDs.Remove(MaKhoa);
                        }
                    }
                }
                string[] hiddenIDs = (string[])arrIDs.ToArray(typeof(String));
                txtListId.Text = string.Join("|", hiddenIDs);
            }
            catch// (System.Exception ex)
            {
                //lblErrorMessage.Text = ex.ToString();
            }

        }

        protected void PopulateCheckedValues()
        {
            try
            {
                string MaKhoa = string.Empty;
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                CheckBox chk;

                foreach (GridViewRow rowItem in grvFaculty.Rows)
                {
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
                    MaKhoa = grvFaculty.DataKeys[rowItem.RowIndex]["MaKhoa"].ToString();
                    if (arrIDs.Contains(MaKhoa))
                    {
                        chk.Checked = true;
                        rowItem.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#fdffb8");
                    }
                    else
                    {
                        chk.Checked = false;
                        rowItem.Style.Remove(HtmlTextWriterStyle.BackgroundColor);
                    }
                }
            }
            catch //(System.Exception ex)
            {
                //lblErrorMessage.Text = ex.ToString();
            }
        }

        protected ArrayList ConvertToArrayList(String s)
        {
            string[] hiddenIDs = new string[] { };
            if (s != string.Empty)
            {
                hiddenIDs = s.Split(new char[] { '|' });
            }

            ArrayList arrIDs = new ArrayList();

            if (hiddenIDs.Length != 0)
            {
                arrIDs.AddRange(hiddenIDs);
            }

            return arrIDs;
        }

        protected void grvFalcuty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
        }

        protected void grvFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowId = grvFaculty.SelectedIndex;
            txtId.Text = grvFaculty.Rows[rowId].Cells[2].Text.ToString();
            txtIdnumber.Text = grvFaculty.Rows[rowId].Cells[3].Text.ToString();
            txtName.Text = HttpUtility.HtmlDecode(grvFaculty.Rows[rowId].Cells[4].Text.ToString());
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();

            grvFaculty.AllowPaging = false;
            grvFaculty.DataBind();

            MoodleCategory category;
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtListId.Text);
            string idnum = "0";

            foreach (GridViewRow row in grvFaculty.Rows)
            {
                if (row.Cells[2].Text != "0") continue;
                idnum = grvFaculty.DataKeys[row.RowIndex]["MaKhoa"].ToString();
                if (arrIDs.Contains(idnum))
                {
                    category = new MoodleCategory
                    {
                        Name = HttpUtility.HtmlDecode(row.Cells[4].Text),
                        Parent = 0,
                        IdNumber = HttpUtility.HtmlDecode(row.Cells[3].Text),
                        Description = HttpUtility.HtmlDecode(row.Cells[4].Text),
                        DescriptionFormat = 1,
                        Theme = null
                    };

                    List<MoodleCategory> lst = new List<MoodleCategory>();
                    lst.Add(category);
                    doc.LoadXml(MoodleCategory.CreateCategories(lst, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\faculty_" + category.IdNumber + ".xml");
                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        long id = (long)Convert.ToUInt32(doc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText);
                        DCVimaruDataContext dc = new DCVimaruDataContext();
                        Khoa khoa = dc.Khoas.Single(t => t.MaKhoa == category.IdNumber);
                        khoa.Id = id;
                        dc.SubmitChanges();
                    }
                }
            }

            grvFaculty.AllowPaging = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtId.Text == "0")
            {
                lblUpdateMessage.Text = "Vui lòng nhập một ID khoa > 0";
                txtId.Focus();
                return;
            }

            MoodleCategory category;
            XmlDocument doc = new XmlDocument();

            category = new MoodleCategory
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = HttpUtility.HtmlDecode(txtName.Text),
                Parent = 0,
                IdNumber = HttpUtility.HtmlDecode(txtIdnumber.Text),
                Description = HttpUtility.HtmlDecode(txtName.Text),
                DescriptionFormat = 1,
                Theme = null
            };

            List<MoodleCategory> lst = new List<MoodleCategory>();
            lst.Add(category);
            doc.LoadXml(MoodleCategory.UpdateCategories(lst, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\faculty_" + txtId.Text + ".xml");
        }

        protected void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtId.Text == "0")
            {
                lblUpdateMessage.Text = "Vui lòng nhập một ID khoa> 0";
                txtId.Focus();
                return;
            }

            List<MoodleGetCategory> lst = new List<MoodleGetCategory>();
            int id = ddlCriteria.SelectedIndex;

            if (id == 0)
            {
                lst.Add(new MoodleGetCategory("id", txtId.Text));
            }
            else if (id == 1)
            {
                lst.Add(new MoodleGetCategory("idnumber", txtIdnumber.Text));
            }
            else
            {
                lst.Add(new MoodleGetCategory("name", HttpUtility.HtmlDecode(txtName.Text)));
            }

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(MoodleGetCategory.GetCategories(lst, chkSubCategory.Checked, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\faculty_" + txtId.Text + ".xml");
            XmlNode xmlnode = doc.ChildNodes[1];
            treeDetail.Nodes.Clear();
            treeDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeDetail.Nodes[0];
            MoodleUtilites.AddNode(xmlnode, tNode);
            treeDetail.ExpandAll();
            treeDetail.Focus();
        }
    }
}