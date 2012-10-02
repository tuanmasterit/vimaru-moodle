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
    public partial class Department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/Department.aspx";
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (!IsPostBack)
                grvDepartment.DataBind();
        }

        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvDepartment.PageSize = Convert.ToInt16(cboPageSize.SelectedValue);
        }

        protected void grvDepartment_PreRender(object sender, EventArgs e)
        {
            grvDepartment.DataBind();
            PopulateCheckedValues();
        }

        protected void grvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = grvDepartment.Rows.Count.ToString();
                e.Row.Cells[1].Text = "Trang " + (grvDepartment.PageIndex + 1) + "/" + grvDepartment.PageCount;
            }
        }

        protected void SaveCheckedValues()
        {
            try
            {
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                string MaBoMon = "0";
                CheckBox chk;

                foreach (GridViewRow rowItem in grvDepartment.Rows)
                {

                    chk = (CheckBox)(rowItem.FindControl("chk"));
                    MaBoMon = grvDepartment.DataKeys[rowItem.RowIndex]["MaBoMon"].ToString();
                    if (chk.Checked)
                    {
                        if (!arrIDs.Contains(MaBoMon))
                        {
                            arrIDs.Add(MaBoMon);
                        }
                    }
                    else
                    {
                        if (arrIDs.Contains(MaBoMon))
                        {
                            arrIDs.Remove(MaBoMon);
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
                string MaBoMon = string.Empty;
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                CheckBox chk;

                foreach (GridViewRow rowItem in grvDepartment.Rows)
                {
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
                    MaBoMon = grvDepartment.DataKeys[rowItem.RowIndex]["MaBoMon"].ToString();
                    if (arrIDs.Contains(MaBoMon))
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

        protected void grvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
        }

        protected void grvDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveCheckedValues();
            int rowId = grvDepartment.SelectedIndex;
            txtId.Text = grvDepartment.Rows[rowId].Cells[2].Text.ToString();
            txtIdnumber.Text = grvDepartment.Rows[rowId].Cells[3].Text.ToString();
            txtName.Text = HttpUtility.HtmlDecode(grvDepartment.Rows[rowId].Cells[4].Text.ToString());
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();

            grvDepartment.AllowPaging = false;
            grvDepartment.DataBind();

            MoodleCategory category;
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtListId.Text);
            string idnum = "0";

            DCVimaruDataContext dc = new DCVimaruDataContext();
            Khoa khoa = dc.Khoas.Single(t => t.MaKhoa == cboFilter.SelectedValue);
            int parent = Convert.ToInt32(khoa.Id);

            foreach (GridViewRow row in grvDepartment.Rows)
            {
                if (row.Cells[2].Text != "0") continue;
                idnum = grvDepartment.DataKeys[row.RowIndex]["MaBoMon"].ToString();
                if (arrIDs.Contains(idnum))
                {
                    category = new MoodleCategory
                    {
                        Name = HttpUtility.HtmlDecode(row.Cells[4].Text),
                        Parent = parent,
                        IdNumber = HttpUtility.HtmlDecode(row.Cells[3].Text),
                        Description = HttpUtility.HtmlDecode(row.Cells[4].Text),
                        DescriptionFormat = 1,
                        Theme = null
                    };

                    List<MoodleCategory> lst = new List<MoodleCategory>();
                    lst.Add(category);
                    doc.LoadXml(MoodleCategory.CreateCategories(lst, (string)Session["token"]));
                    doc.Save("D:\\department_" + category.IdNumber + ".xml");
                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        long id = (long)Convert.ToUInt32(doc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText);
                        BoMon bomon = dc.BoMons.Single(t => t.MaBoMon == Convert.ToInt32(category.IdNumber));
                        bomon.Id = id;
                        dc.SubmitChanges();
                    }
                }
            }

            grvDepartment.AllowPaging = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || Convert.ToInt32(txtId.Text) < 1)
            {
                lblUpdateMessage.Text = "Vui lòng nhập một ID bộ môn > 0";
                txtId.Focus();
                return;
            }

            DCVimaruDataContext dc = new DCVimaruDataContext();
            Khoa khoa = dc.Khoas.Single(t => t.MaKhoa == cboFilter.SelectedValue);
            int parent = Convert.ToInt32(khoa.Id);

            MoodleCategory category;
            XmlDocument doc = new XmlDocument();

            category = new MoodleCategory
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = HttpUtility.HtmlDecode(txtName.Text),
                Parent = parent,
                IdNumber = HttpUtility.HtmlDecode(txtIdnumber.Text),
                Description = HttpUtility.HtmlDecode(txtName.Text),
                DescriptionFormat = 1,
                Theme = null
            };

            List<MoodleCategory> lst = new List<MoodleCategory>();
            lst.Add(category);
            doc.LoadXml(MoodleCategory.UpdateCategories(lst, (string)Session["token"]));
            doc.Save("D:\\department_" + txtId.Text + ".xml");
        }

        protected void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || Convert.ToInt32(txtId.Text) < 1)
            {
                lblUpdateMessage.Text = "Vui lòng nhập một ID bộ môn > 0";
                txtId.Focus();
                return;
            }

            List<KeyValuePair<string, string> > list = new List<KeyValuePair<string, string> >();
            int id = ddlCriteria.SelectedIndex;

            if (id == 0)
            {
                list.Add(new KeyValuePair<string, string> ("id", txtId.Text));
            }
            else if (id == 1)
            {
                list.Add(new KeyValuePair<string, string> ("idnumber", txtIdnumber.Text));
            }
            else
            {
                list.Add(new KeyValuePair<string, string> ("name", HttpUtility.HtmlDecode(txtName.Text)));
            }

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(MoodleCategory.GetCategories(list, chkSubCategory.Checked, (string)Session["token"]));
            doc.Save("D:\\department_" + txtId.Text + ".xml");
            XmlNode xmlnode = doc.ChildNodes[1];
            treeDetail.Nodes.Clear();
            treeDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeDetail.Nodes[0];
            MoodleUtilites.AddNode(xmlnode, tNode);
            treeDetail.ExpandAll();
            treeDetail.Focus();
        }

        protected void LinqDataSourceDepartment_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            cboPageSize_SelectedIndexChanged(sender, e);
        }
    }
}