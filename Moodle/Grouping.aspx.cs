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
    public partial class Grouping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/Grouping.aspx";
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
                grvGroup.DataBind();
        }

        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvGroup.PageSize = Convert.ToInt16(cboPageSize.SelectedValue);
        }

        protected void grvGroup_PreRender(object sender, EventArgs e)
        {
            grvGroup.DataBind();
            PopulateCheckedValues();
        }

        protected void grvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = grvGroup.Rows.Count.ToString();
                e.Row.Cells[1].Text = "Trang " + (grvGroup.PageIndex + 1) + "/" + grvGroup.PageCount;
            }
        }

        protected void LinqDataSourceGroup_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            DCVimaruDataContext dc = new DCVimaruDataContext();
            var rs = from nhom in dc.Nhoms
                     where nhom.MaTKB.ToString() == cboFilterCourse.SelectedValue
                     select new NhomResult
                     {
                         ID_Nhom = nhom.ID_Nhom,
                         ID_To = nhom.ID_To,
                         TenNhom = nhom.TenNhom,
                         TenTo = nhom.To.TenTo,
                         MoTa=nhom.MoTa
                     };
            e.Result = rs;
        }

        protected void LinqDataSourceCourse_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            DCVimaruDataContext dc = new DCVimaruDataContext();
            var rs = from tkb in dc.ThoiKhoaBieus
                     where tkb.MaHP == cboFilterSubject.SelectedValue && tkb.Id != 0
                     select new HocPhanResult
                     {
                         Id = tkb.Id,
                         STT = tkb.STT,
                         MaHP = tkb.MaHP,
                         TenHP = "N" + tkb.MaNH + " (" + tkb.NgayBD.Day
                         + "-" + tkb.NgayBD.Month + "-" + tkb.NgayBD.Year + ")"
                     };
            e.Result = rs.OrderBy(t => t.Id);
        }

        protected void SaveCheckedValues()
        {
            try
            {
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                string MaNhom = "0";
                CheckBox chk;

                foreach (GridViewRow rowItem in grvGroup.Rows)
                {

                    chk = (CheckBox)(rowItem.FindControl("chk"));
                    MaNhom = grvGroup.DataKeys[rowItem.RowIndex]["ID_Nhom"].ToString();
                    if (chk.Checked)
                    {
                        if (!arrIDs.Contains(MaNhom))
                        {
                            arrIDs.Add(MaNhom);
                        }
                    }
                    else
                    {
                        if (arrIDs.Contains(MaNhom))
                        {
                            arrIDs.Remove(MaNhom);
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
                string MaNhom = string.Empty;
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                CheckBox chk;

                foreach (GridViewRow rowItem in grvGroup.Rows)
                {
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
                    MaNhom = grvGroup.DataKeys[rowItem.RowIndex]["ID_Nhom"].ToString();
                    if (arrIDs.Contains(MaNhom))
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

        protected void grvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
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

        protected void cboFilterDepartment_DataBound(object sender, EventArgs e)
        {
            cboFilterSubject.DataBind();
        }

        protected void cboFilterSubject_DataBound(object sender, EventArgs e)
        {
            cboFilterCourse.DataBind();
        }

        protected void cboFilterCourse_DataBound(object sender, EventArgs e)
        {
            cboGrouping.DataBind();
        }

        protected void btnCreateGrouping_Click(object sender, EventArgs e)
        {
            if (cboFilterCourse.Items.Count == 0) return;

            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get courseId
            ThoiKhoaBieu tkb = dc.ThoiKhoaBieus.Single(t => t.STT == Convert.ToInt32(cboFilterCourse.SelectedValue));
            int courseId = Convert.ToInt32(tkb.Id);

            XmlDocument doc = new XmlDocument();
            List<MoodleGroup> list = new List<MoodleGroup>();

            MoodleGroup grouping = new MoodleGroup
            {
                CourseId = courseId,
                Name = txtGroupingName.Text,
                Description = txtDescription.Text,
                DescriptionFormat = 1,
            };

            list.Add(grouping);

            doc.LoadXml(MoodleGroup.CreateGroupings(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\Grouping_Create_" + txtGroupingName.Text + ".xml");

            if (doc.DocumentElement.Name == "RESPONSE")
            {
                long idto = (long)Convert.ToUInt64(doc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText);

                To t = new To();
                t.ID_To = idto;
                t.TenTo = txtGroupingName.Text;
                t.MoTa = txtDescription.Text;
                t.MaTKB = (long)Convert.ToUInt64(cboFilterCourse.SelectedValue);

                dc.Tos.InsertOnSubmit(t);
                dc.SubmitChanges();

                cboGrouping.DataBind();
            }
        }

        protected void btnUpdateGrouping_Click(object sender, EventArgs e)
        {
            if (cboFilterCourse.Items.Count == 0) return;

            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get courseId
            ThoiKhoaBieu tkb = dc.ThoiKhoaBieus.Single(t => t.STT == Convert.ToInt32(cboFilterCourse.SelectedValue));
            int courseId = Convert.ToInt32(tkb.Id);

            XmlDocument doc = new XmlDocument();
            List<MoodleGroup> list = new List<MoodleGroup>();

            int id = Convert.ToInt32(cboGrouping.SelectedValue);
            MoodleGroup grouping = new MoodleGroup
            {
                Id = id,
                Name = txtGroupingName.Text,
                Description = txtDescription.Text,
                DescriptionFormat = 1,
            };

            list.Add(grouping);

            doc.LoadXml(MoodleGroup.UpdateGroupings(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\Grouping_Update_" + txtGroupingName.Text + ".xml");

            if (doc.DocumentElement.Name == "RESPONSE")
            {
                To t = dc.Tos.Single(i => i.ID_To == id);
                t.TenTo = txtGroupingName.Text;
                t.MoTa = txtDescription.Text;

                dc.SubmitChanges();

                cboGrouping.DataBind();
            }
        }

        protected void btnDeleteGrouping_Click(object sender, EventArgs e)
        {
            if (cboGrouping.Items.Count == 0) return;

            DCVimaruDataContext dc = new DCVimaruDataContext();

            XmlDocument doc = new XmlDocument();
            List<int> list = new List<int>();

            int id = Convert.ToInt32(cboGrouping.SelectedValue);
            list.Add(id);

            doc.LoadXml(MoodleGroup.DeleteGroupings(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\Grouping_Delete_" + cboGrouping.SelectedItem.Text + ".xml");

            if (doc.DocumentElement.Name == "RESPONSE")
            {
                To to = dc.Tos.Single(t => t.ID_To == id);
                dc.Tos.DeleteOnSubmit(to);

                dc.ExecuteCommand("UPDATE Nhom SET ID_To = null WHERE ID_To = {0}", id);

                dc.SubmitChanges();

                cboGrouping.DataBind();
            }
        }

        protected void btnAssignGroup_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();
            grvGroup.AllowPaging = false;
            grvGroup.DataBind();

            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get grouping Id
            int groupingId = Convert.ToInt32(cboGrouping.SelectedValue);
            //get group list

            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtListId.Text);
            string idnhom = "0";

            foreach (GridViewRow row in grvGroup.Rows)
            {
                idnhom = grvGroup.DataKeys[row.RowIndex]["ID_Nhom"].ToString();
                if (arrIDs.Contains(idnhom))
                {
                    if (row.Cells[5].Text != "&nbsp;") continue;

                    List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                    list.Add(new KeyValuePair<int, int>(groupingId, Convert.ToInt32(row.Cells[2].Text)));

                    doc.LoadXml(MoodleGroup.AssignGrouping(list, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\Grouping_Assign_Group_" + cboGrouping.Text + ".xml");

                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        Nhom nhom = dc.Nhoms.Single(t => t.ID_Nhom == Convert.ToInt64(row.Cells[2].Text));
                        nhom.ID_To = (long?)groupingId;
                        dc.SubmitChanges();
                    }
                }
            }

            grvGroup.AllowPaging = true;
        }

        protected void btnUnsignGrouping_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();
            grvGroup.AllowPaging = false;
            grvGroup.DataBind();

            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get grouping Id
            int groupingId = Convert.ToInt32(cboGrouping.SelectedValue);
            //get group list

            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtListId.Text);
            string idnhom = "0";

            foreach (GridViewRow row in grvGroup.Rows)
            {
                idnhom = grvGroup.DataKeys[row.RowIndex]["ID_Nhom"].ToString();
                if (arrIDs.Contains(idnhom))
                {
                    if (HttpUtility.HtmlDecode(row.Cells[5].Text) != cboGrouping.SelectedItem.Text) continue;

                    List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                    list.Add(new KeyValuePair<int, int>(groupingId, Convert.ToInt32(row.Cells[2].Text)));

                    doc.LoadXml(MoodleGroup.UnassignGrouping(list, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\Grouping_Unassign_Group_" + cboGrouping.Text + ".xml");

                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        Nhom nhom = dc.Nhoms.Single(t => t.ID_Nhom == Convert.ToInt64(row.Cells[2].Text));
                        nhom.ID_To = null;
                        dc.SubmitChanges();
                    }
                }
            }

            grvGroup.AllowPaging = true;
        }

        protected void grvGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveCheckedValues();
            int rowId = grvGroup.SelectedIndex;
            int groupId = Convert.ToInt32(grvGroup.Rows[rowId].Cells[2].Text.ToString());

            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

            List<int> list = new List<int>();
            list.Add(groupId);

            doc.LoadXml(MoodleGroup.GetGroupMembers(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\Grouping_Get_Members_" + cboGrouping.Text + ".xml");

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