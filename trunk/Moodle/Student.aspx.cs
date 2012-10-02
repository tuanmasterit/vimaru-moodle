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
    public partial class Student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/Student.aspx";
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
                grvUser.DataBind();
        }

        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvUser.PageSize = Convert.ToInt16(cboPageSize.SelectedValue);
        }

        protected void LinqDataSourceUser_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            DCVimaruDataContext dc = new DCVimaruDataContext();
            var rs = from user in dc.SinhViens
                     where user.MaLop.ToString() == cboClass.SelectedValue
                     select new UserResult
                     {
                         Id = user.Id,
                         MaSV = user.MaSV,
                         Ho = user.Ho,
                         Ten = user.Ten,
                         Email = user.MaSV,
                         MaLop = user.MaLop
                     };

            switch (cboFilter.SelectedIndex)
            {
                case 1:
                    rs = rs.Where(c => c.Id != 0);
                    break;
                case 2:
                    rs = rs.Where(c => c.Id == 0);
                    break;
            }

            switch (cboField.SelectedIndex)
            {
                case 0:
                    rs = rs.Where(c => c.Ten.Contains(txtKeyword.Text));
                    break;
                case 1:
                    rs = rs.Where(c => c.Ho.Contains(txtKeyword.Text));
                    break;
                case 2:
                    rs = rs.Where(c => c.MaSV.Contains(txtKeyword.Text));
                    break;
            }

            e.Result = rs.OrderBy(u => u.Ten);
            cboPageSize_SelectedIndexChanged(sender, e);
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

        protected void SaveCheckedValues()
        {
            try
            {
                ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);

                string MaSV = "0";
                CheckBox chk;

                foreach (GridViewRow rowItem in grvUser.Rows)
                {

                    chk = (CheckBox)(rowItem.FindControl("chk"));
                    MaSV = grvUser.DataKeys[rowItem.RowIndex]["MaSV"].ToString();
                    if (chk.Checked)
                    {
                        if (!arrIDs.Contains(MaSV))
                        {
                            arrIDs.Add(MaSV);
                        }
                    }
                    else
                    {
                        if (arrIDs.Contains(MaSV))
                        {
                            arrIDs.Remove(MaSV);
                        }
                    }
                }
                string[] hiddenIDs = (string[])arrIDs.ToArray(typeof(String));
                txtMaSV.Text = string.Join("|", hiddenIDs);
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
                string MaSV = string.Empty;
                ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);

                CheckBox chk;

                foreach (GridViewRow rowItem in grvUser.Rows)
                {
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
                    MaSV = grvUser.DataKeys[rowItem.RowIndex]["MaSV"].ToString();
                    if (arrIDs.Contains(MaSV))
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

        protected void grvUser_PreRender(object sender, EventArgs e)
        {
            grvUser.DataBind();
            PopulateCheckedValues();
        }

        protected void grvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = grvUser.Rows.Count.ToString();
                e.Row.Cells[1].Text = "Trang " + (grvUser.PageIndex + 1) + "/" + grvUser.PageCount;
            }
        }

        protected void grvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
        }

        protected void grvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveCheckedValues();
            int rowId = grvUser.SelectedIndex;
            txtId.Text = grvUser.Rows[rowId].Cells[2].Text.ToString();
            txtNewUsername.Text = grvUser.Rows[rowId].Cells[3].Text.ToString();
            txtFirstName.Text = HttpUtility.HtmlDecode(grvUser.Rows[rowId].Cells[4].Text.ToString());
            txtLastName.Text = HttpUtility.HtmlDecode(grvUser.Rows[rowId].Cells[5].Text.ToString());
            txtEmail.Text = grvUser.Rows[rowId].Cells[6].Text.ToString();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();

            grvUser.AllowPaging = false;
            grvUser.DataBind();

            MoodleUser user;
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);
            string MaSV = "0";

            foreach (GridViewRow row in grvUser.Rows)
            {
                MaSV = grvUser.DataKeys[row.RowIndex]["MaSV"].ToString();

                if (arrIDs.Contains(MaSV))
                {
                    if (row.Cells[2].Text != "0") continue;
                    user = new MoodleUser
                    {
                        Username = row.Cells[3].Text,
                        Password = row.Cells[3].Text,
                        Firstname = HttpUtility.HtmlDecode(row.Cells[5].Text),
                        Lastname = HttpUtility.HtmlDecode(row.Cells[4].Text),
                        Email = row.Cells[6].Text,
                        Timezone = "7.0",
                        City = "Hai Phong",
                        Country = "VN"
                    };

                    List<MoodleUser> list = new List<MoodleUser>();
                    list.Add(user);
                    doc.LoadXml(MoodleUser.CreateUsers(list, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\user_create_" + user.Username + ".xml");

                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        long userId = (long)Convert.ToUInt64(doc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText);
                        DCVimaruDataContext dc = new DCVimaruDataContext();
                        SinhVien sv = dc.SinhViens.Single(t => t.MaSV == user.Username);
                        sv.Id = userId;
                        dc.SubmitChanges();
                    }
                }
            }

            grvUser.AllowPaging = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || Convert.ToInt32(txtId.Text) < 1)
            {
                lblUpdateUserMessage.Text = "Vui lòng nhập một ID người dùng > 0";
                txtId.Focus();
                return;
            }

            MoodleUser user;
            XmlDocument doc = new XmlDocument();

            user = new MoodleUser
            {
                Id = Convert.ToDouble(txtId.Text),
                Username = txtNewUsername.Text,
                Password = txtNewPassword.Text,
                Firstname = HttpUtility.HtmlDecode(txtFirstName.Text),
                Lastname = HttpUtility.HtmlDecode(txtLastName.Text),
                Email = txtEmail.Text
            };

            List<MoodleUser> list = new List<MoodleUser>();
            list.Add(user);
            doc.LoadXml(MoodleUser.UpdateUsers(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\user_update_" + txtId.Text + ".xml");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();

            grvUser.AllowPaging = false;
            grvUser.DataBind();

            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);
            string MaSV = "0";

            foreach (GridViewRow row in grvUser.Rows)
            {
                MaSV = grvUser.DataKeys[row.RowIndex]["MaSV"].ToString();
                if (arrIDs.Contains(MaSV))
                {
                    int userId = Convert.ToInt32(row.Cells[3].Text);

                    if (userId == 0)
                        continue;

                    List<int> list = new List<int>();
                    list.Add(userId);

                    doc.LoadXml(MoodleUser.DeleteUsers(list, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\user_delete_" + row.Cells[5].Text + ".xml");

                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        DCVimaruDataContext dc = new DCVimaruDataContext();
                        SinhVien sv = dc.SinhViens.Single(t => t.MaSV == MaSV);
                        sv.Id = 0;

                        dc.ExecuteCommand("UPDATE DangKy SET GhiDanh = 0, ID_Nhom = null WHERE MaSV = {0}", MaSV);
                        dc.SubmitChanges();
                    }
                }
            }

            grvUser.AllowPaging = true;
        }

        protected void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || Convert.ToInt32(txtId.Text) < 1)
            {
                lblUpdateUserMessage.Text = "Vui lòng nhập một ID người dùng > 0";
                txtId.Focus();
                return;
            }

            XmlDocument doc = new XmlDocument();

            List<int> list = new List<int>();
            list.Add(Convert.ToInt32(txtId.Text));
            doc.LoadXml(MoodleUser.GetUsersById(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\user_profile_" + txtId.Text + ".xml");

            XmlNode xmlnode = doc.ChildNodes[1];
            treeDetail.Nodes.Clear();
            treeDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeDetail.Nodes[0];
            MoodleUtilites.AddNode(xmlnode, tNode);
            treeDetail.ExpandAll();
            treeDetail.Focus();
        }

        protected void btnGetCourses_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || Convert.ToInt32(txtId.Text) < 1)
            {
                lblUpdateUserMessage.Text = "Vui lòng nhập một ID người dùng > 0";
                txtId.Focus();
                return;
            }

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(MoodleEnrol.GetUsersCourses(Convert.ToInt32(txtId.Text), (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\user_courses_" + txtId.Text + ".xml");

            XmlNode xmlnode = doc.ChildNodes[1];
            treeDetail.Nodes.Clear();
            treeDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeDetail.Nodes[0];
            MoodleUtilites.AddNode(xmlnode, tNode);
            treeDetail.ExpandAll();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grvUser.DataBind();
        }


    }
}