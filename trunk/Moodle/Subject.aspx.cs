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
    public partial class Subject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/Subject.aspx";
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvSubject.PageSize = Convert.ToInt16(cboPageSize.SelectedValue);
        }

        protected void grvSubject_PreRender(object sender, EventArgs e)
        {
            grvSubject.DataBind();
            PopulateCheckedValues();
        }

        protected void grvSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = grvSubject.Rows.Count.ToString();
                e.Row.Cells[1].Text = "Trang " + (grvSubject.PageIndex + 1) + "/" + grvSubject.PageCount;
            }
        }

        protected void SaveCheckedValues()
        {
            try
            {
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                string MaHP = "0";
                CheckBox chk;

                foreach (GridViewRow rowItem in grvSubject.Rows)
                {

                    chk = (CheckBox)(rowItem.FindControl("chk"));
                    MaHP = grvSubject.DataKeys[rowItem.RowIndex]["MaHP"].ToString();
                    if (chk.Checked)
                    {
                        if (!arrIDs.Contains(MaHP))
                        {
                            arrIDs.Add(MaHP);
                        }
                    }
                    else
                    {
                        if (arrIDs.Contains(MaHP))
                        {
                            arrIDs.Remove(MaHP);
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
                string MaHP = string.Empty;
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                CheckBox chk;

                foreach (GridViewRow rowItem in grvSubject.Rows)
                {
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
                    MaHP = grvSubject.DataKeys[rowItem.RowIndex]["MaHP"].ToString();
                    if (arrIDs.Contains(MaHP))
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

        protected void grvSubject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
        }

        protected void grvSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowId = grvSubject.SelectedIndex;
            txtId.Text = grvSubject.Rows[rowId].Cells[2].Text.ToString();
            txtIdnumber.Text = grvSubject.Rows[rowId].Cells[3].Text.ToString();
            txtName.Text = HttpUtility.HtmlDecode(grvSubject.Rows[rowId].Cells[4].Text.ToString());
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();

            grvSubject.AllowPaging = false;
            grvSubject.DataBind();

            MoodleCategory category;
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtListId.Text);
            string idnum = "0";

            DCVimaruDataContext dc = new DCVimaruDataContext();
            BoMon bomon = dc.BoMons.Single(t => t.MaBoMon == Convert.ToInt32(cboFilterDepartment.SelectedValue));
            int parent = Convert.ToInt32(bomon.Id);

            foreach (GridViewRow row in grvSubject.Rows)
            {
                if (row.Cells[2].Text != "0") continue;
                idnum = grvSubject.DataKeys[row.RowIndex]["MaHP"].ToString();
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
                    doc.Save("E:\\Z-TMP\\subject_" + category.IdNumber + ".xml");
                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        long id = (long)Convert.ToUInt32(doc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText);
                        HocPhan hocphan = dc.HocPhans.Single(t => t.MaHP == category.IdNumber);
                        hocphan.Id = id;
                        dc.SubmitChanges();
                    }
                }
            }

            grvSubject.AllowPaging = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtId.Text == "0")
            {
                lblUpdateMessage.Text = "Vui lòng nhập một ID môn học > 0";
                txtId.Focus();
                return;
            }

            DCVimaruDataContext dc = new DCVimaruDataContext();
            BoMon bomon = dc.BoMons.Single(t => t.MaBoMon == Convert.ToInt32(cboFilterDepartment.SelectedValue));
            int parent = Convert.ToInt32(bomon.Id);

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
            doc.Save("E:\\Z-TMP\\subject_" + txtId.Text + ".xml");
        }

        protected void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtId.Text == "0")
            {
                lblUpdateMessage.Text = "Vui lòng nhập một ID môn học > 0";
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
            doc.Save("E:\\Z-TMP\\subject_" + txtId.Text + ".xml");
            XmlNode xmlnode = doc.ChildNodes[1];
            treeDetail.Nodes.Clear();
            treeDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeDetail.Nodes[0];
            MoodleUtilites.AddNode(xmlnode, tNode);
            treeDetail.ExpandAll();
            treeDetail.Focus();
        }

        protected void LinqDataSourceSubject_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            cboPageSize_SelectedIndexChanged(sender, e);
        }
    }
}