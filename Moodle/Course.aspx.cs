﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Xml;

namespace Moodle
{
    public partial class Course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/Course.aspx";
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
                grvCourse.DataBind();
        }

        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvCourse.PageSize = Convert.ToInt16(cboPageSize.SelectedValue);
        }

        protected void LinqDataSourceCourse_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            DCVimaruDataContext dc = new DCVimaruDataContext();
            var rs = from tkb in dc.ThoiKhoaBieus
                     where tkb.MaHP == cboFilterSubject.SelectedValue
                     select new HocPhanResult
                     {
                         Id = tkb.Id,
                         STT = tkb.STT,
                         MaHP = tkb.MaHP,
                         TenHP = tkb.HocPhan.TenHP,
                         MaNH = tkb.MaNH,
                         NgayBD = tkb.NgayBD
                     };
            e.Result = rs.OrderByDescending(t=>t.Id);
            cboPageSize_SelectedIndexChanged(sender, e);
        }

        protected void grvCourse_PreRender(object sender, EventArgs e)
        {
            grvCourse.DataBind();
            PopulateCheckedValues();
        }

        protected void grvCourse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = grvCourse.Rows.Count.ToString();
                e.Row.Cells[1].Text = "Trang " + (grvCourse.PageIndex + 1) + "/" + grvCourse.PageCount;
            }
        }

        protected void SaveCheckedValues()
        {
            try
            {
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                string STT = "0";
                CheckBox chk;

                foreach (GridViewRow rowItem in grvCourse.Rows)
                {

                    chk = (CheckBox)(rowItem.FindControl("chk"));
                    STT = grvCourse.DataKeys[rowItem.RowIndex]["STT"].ToString();
                    if (chk.Checked)
                    {
                        if (!arrIDs.Contains(STT))
                        {
                            arrIDs.Add(STT);
                        }
                    }
                    else
                    {
                        if (arrIDs.Contains(STT))
                        {
                            arrIDs.Remove(STT);
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
                string STT = string.Empty;
                ArrayList arrIDs = ConvertToArrayList(txtListId.Text);

                CheckBox chk;

                foreach (GridViewRow rowItem in grvCourse.Rows)
                {
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
                    STT = grvCourse.DataKeys[rowItem.RowIndex]["STT"].ToString();
                    if (arrIDs.Contains(STT))
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

        protected void grvCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
        }

        protected void cboFilterDepartment_DataBound(object sender, EventArgs e)
        {
            cboFilterSubject.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();
            grvCourse.AllowPaging = false;
            grvCourse.DataBind();

            MoodleCourse course;
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtListId.Text);
            string idnum = "0";

            DCVimaruDataContext dc = new DCVimaruDataContext();
            HocPhan hocphan = dc.HocPhans.Single(t => t.MaHP == cboFilterSubject.SelectedValue);
            int parent = Convert.ToInt32(hocphan.Id);

            foreach (GridViewRow row in grvCourse.Rows)
            {
                idnum = grvCourse.DataKeys[row.RowIndex]["STT"].ToString();

                if (arrIDs.Contains(idnum))
                {
                    string fullname = HttpUtility.HtmlDecode(row.Cells[4].Text + " N" + row.Cells[5].Text + "-" + row.Cells[6].Text);

                    course = new MoodleCourse(
                        fullname,
                        fullname,
                        parent,
                        MoodleUtilites.GetShortName(fullname),
                        fullname,
                        MoodleUtilites.ConvertToTimestamp(row.Cells[6].Text)
                        );

                    List<MoodleCourse> lst = new List<MoodleCourse>();
                    lst.Add(course);
                    doc.LoadXml(MoodleCourse.CreateCourses(lst, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\course_" + row.Cells[3].Text + ".xml");

                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        long id = (long)Convert.ToUInt32(doc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText);
                        ThoiKhoaBieu tkb = dc.ThoiKhoaBieus.Single(t => t.STT == Convert.ToInt32(row.Cells[3].Text));
                        tkb.Id = id;
                        dc.SubmitChanges();
                    }
                }
            }

            grvCourse.AllowPaging = true;
        }
    }
}