using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace Moodle
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/User.aspx";
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
            var rs = from dk in dc.DangKies
                     where dk.MaTKB.ToString() == cboFilterCourse.SelectedValue
                     select new UserResult
                         {
                             STT = dk.STT,
                             Id = dk.SinhVien.Id,
                             GhiDanh = dk.GhiDanh,
                             MaSV = dk.MaSV,
                             Ho = dk.SinhVien.Ho,
                             Ten = dk.SinhVien.Ten,
                             Email = dk.SinhVien.MaSV,
                             TenLop = dk.SinhVien.Lop.TenLop,
                             IdNhom = dk.ID_Nhom,
                             TenNhom = dk.Nhom.TenNhom
                         };
            
            switch(cboFilter.SelectedIndex)
            {
                case 1: 
                    rs = rs.Where(c => c.Id != 0);
                    break;
                case 2:
                    rs = rs.Where(c => c.Id == 0);
                    break;
                case 3:
                    rs = rs.Where(c => c.GhiDanh);
                    break;
                case 4:
                    rs = rs.Where(c => !c.GhiDanh);
                    break;
                case 5:
                    rs = rs.Where(c => c.IdNhom != null);
                    break;
                case 6:
                    rs = rs.Where(c =>c.IdNhom == null);
                    break;
                default:
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
                    rs = rs.Where(c => c.TenLop.Contains(txtKeyword.Text));
                    break;
                case 3:
                    if (txtKeyword.Text != "")
                        rs = rs.Where(c => c.TenNhom.Contains(txtKeyword.Text));
                    break;
            }

            e.Result = rs.OrderBy(u => u.Ten);
            cboPageSize_SelectedIndexChanged(sender, e);
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
                         + "-" + tkb.NgayBD.Month + "-" + tkb.NgayBD.Year + ")",
                         MaNH = tkb.MaNH,
                         NgayBD = tkb.NgayBD
                     };
            e.Result = rs.OrderBy(t => t.Id);
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

        protected void grvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        protected void grvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblErrorMessage.Text = txtUsername.Text;
            int rowId = grvUser.SelectedIndex;
            txtId.Text = grvUser.Rows[rowId].Cells[3].Text.ToString();
            txtNewUsername.Text = grvUser.Rows[rowId].Cells[5].Text.ToString();
            txtNewPassword.Text = grvUser.Rows[rowId].Cells[5].Text.ToString();
            txtFirstName.Text = HttpUtility.HtmlDecode(grvUser.Rows[rowId].Cells[6].Text.ToString());
            txtLastName.Text = HttpUtility.HtmlDecode(grvUser.Rows[rowId].Cells[7].Text.ToString());
            txtEmail.Text = grvUser.Rows[rowId].Cells[8].Text.ToString();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();

            grvUser.AllowPaging = false;
            grvUser.DataBind();

            MoodleUser user;
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);
            string MaSV="0";

            foreach (GridViewRow row in grvUser.Rows)
            {
                MaSV = grvUser.DataKeys[row.RowIndex]["MaSV"].ToString();
                if (arrIDs.Contains(MaSV))
                {
                    if (row.Cells[3].Text != "0") continue;
                    user = new MoodleUser
                        {
                            Username = row.Cells[5].Text,
                            Password = row.Cells[5].Text,
                            Firstname = HttpUtility.HtmlDecode(row.Cells[6].Text),
                            Lastname =  HttpUtility.HtmlDecode(row.Cells[7].Text),
                            Email = row.Cells[8].Text,
                            Timezone =  "7.0",
                            City = "Hai Phong",
                            Country = "VN"
                        };

                    List<MoodleUser> lstUser = new List<MoodleUser>();
                    lstUser.Add(user);
                    doc.LoadXml(MoodleUser.CreateUsers(lstUser, (string)Session["token"]));
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
            if(txtId.Text == "" || txtId.Text=="0")
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

            List<MoodleUser> lstUser = new List<MoodleUser>();
            lstUser.Add(user);
            doc.LoadXml(MoodleUser.UpdateUsers(lstUser, (string)Session["token"]));
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

                    if(doc.DocumentElement.Name == "RESPONSE")
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

        protected void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtId.Text == "0")
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
            treeUserDetail.Nodes.Clear();
            treeUserDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeUserDetail.Nodes[0];
            AddNode(xmlnode, tNode);
            treeUserDetail.ExpandAll();
        }

        protected void cboFilterDepartment_DataBound(object sender, EventArgs e)
        {
            cboFilterSubject.DataBind();
        }

        protected void cboFilterSubject_DataBound(object sender, EventArgs e)
        {
            cboFilterCourse.DataBind();
        }

        protected void btnGetEnrolledUsers_Click(object sender, EventArgs e)
        {
            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get courseId
            ThoiKhoaBieu tkb = dc.ThoiKhoaBieus.Single(t => t.STT == Convert.ToInt32(cboFilterCourse.SelectedValue));

            int courseId = Convert.ToInt32(tkb.Id);

            //create options to get 4 fields of user is id, username, fullname, email
            List<KeyValuePair<string, string> > list = new List<KeyValuePair<string, string>>();

            list.Add(new KeyValuePair<string, string>("userfields", "id"));
            list.Add(new KeyValuePair<string, string>("userfields", "username"));
            list.Add(new KeyValuePair<string, string>("userfields", "fullname"));
            list.Add(new KeyValuePair<string, string>("userfields", "email"));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(MoodleEnrol.GetEnrolledUsers(courseId, list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\enrolled_users_" + courseId + ".xml");

            XmlNode xmlnode = doc.ChildNodes[1];
            treeUserDetail.Nodes.Clear();
            treeUserDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeUserDetail.Nodes[0];
            AddNode(xmlnode, tNode);
            treeUserDetail.ExpandAll();
        }

        private void EnrolUsers(int suspend)
        {
            SaveCheckedValues();
            grvUser.AllowPaging = false;
            grvUser.DataBind();

            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get courseId
            ThoiKhoaBieu tkb = dc.ThoiKhoaBieus.Single(t => t.STT == Convert.ToInt32(cboFilterCourse.SelectedValue));
            int courseId = Convert.ToInt32(tkb.Id);
            //role: Học viên
            int roleId = 5;

            MoodleEnrol enrol;
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);
            string MaSV = "0";

            foreach (GridViewRow row in grvUser.Rows)
            {
                MaSV = grvUser.DataKeys[row.RowIndex]["MaSV"].ToString();
                if (arrIDs.Contains(MaSV))
                {
                    CheckBox chk = row.Cells[4].Controls[0] as CheckBox;
                    if (row.Cells[3].Text == "0" || (suspend == 0 && chk.Checked) || (suspend == 1 && !chk.Checked)) continue;
                    enrol = new MoodleEnrol
                    {
                        RoleId = roleId,
                        UserId = Convert.ToInt32(row.Cells[3].Text),
                        CourseId = courseId,
                        TimeStart = 0,
                        TimeEnd = 0,
                        Suspend = suspend
                    };

                    List<MoodleEnrol> list = new List<MoodleEnrol>();
                    list.Add(enrol);
                    doc.LoadXml(MoodleEnrol.EnrolUsers(list, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\enrol_" + enrol.UserId + ".xml");
                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        DangKy dk = dc.DangKies.Single(t => t.STT == Convert.ToInt64(row.Cells[2].Text));
                        dk.GhiDanh = !chk.Checked;
                        dc.SubmitChanges();
                    }
                }
            }

            grvUser.AllowPaging = true;
        }
        protected void btnEnrolUsers_Click(object sender, EventArgs e)
        {
            EnrolUsers(0);
        }


        protected void btnSuspendUsers_Click(object sender, EventArgs e)
        {
            EnrolUsers(1);
        }

        protected void btnGetCourses_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtId.Text == "0")
            {
                lblUpdateUserMessage.Text = "Vui lòng nhập một ID người dùng > 0";
                txtId.Focus();
                return;
            }

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(MoodleEnrol.GetUsersCourses(Convert.ToInt32(txtId.Text), (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\user_courses_" + txtId.Text + ".xml");

            XmlNode xmlnode = doc.ChildNodes[1];
            treeUserDetail.Nodes.Clear();
            treeUserDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeUserDetail.Nodes[0];
            AddNode(xmlnode, tNode);
            treeUserDetail.ExpandAll();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grvUser.DataBind();
        }

        protected void btnCreateGroup_Click(object sender, EventArgs e)
        {
            if (cboFilterCourse.Items.Count == 0) return;

            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get courseId
            ThoiKhoaBieu tkb = dc.ThoiKhoaBieus.Single(t => t.STT == Convert.ToInt32(cboFilterCourse.SelectedValue));
            int courseId = Convert.ToInt32(tkb.Id);

            XmlDocument doc = new XmlDocument();
            List<MoodleGroup> list = new List<MoodleGroup>();

            MoodleGroup group = new MoodleGroup
                {
                    CourseId = courseId,
                    Name = txtGroupName.Text,
                    Description = txtDescription.Text,
                    DescriptionFormat = 1,
                    EnrolmentKey = null
                };

            list.Add(group);

            doc.LoadXml(MoodleGroup.CreateGroups(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\Group_Create_" + txtGroupName.Text + ".xml");

            if (doc.DocumentElement.Name == "RESPONSE")
            {
                 long idnhom = (long)Convert.ToUInt64(doc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText);

                Nhom nhom = new Nhom();
                nhom.ID_Nhom = idnhom;
                nhom.TenNhom = txtGroupName.Text;
                nhom.MoTa = txtDescription.Text;
                nhom.MaTKB = (long)Convert.ToUInt64(cboFilterCourse.SelectedValue);

                dc.Nhoms.InsertOnSubmit(nhom);
                dc.SubmitChanges();

                cboGroup.DataBind();
            }
        }

        protected void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (cboGroup.Items.Count == 0) return;

            DCVimaruDataContext dc = new DCVimaruDataContext();

            XmlDocument doc = new XmlDocument();
            List<int> list = new List<int>();

            int id = Convert.ToInt32(cboGroup.SelectedValue);
            list.Add(id);

            doc.LoadXml(MoodleGroup.DeleteGroups(list, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\Group_Delete_" + cboGroup.SelectedItem.Text + ".xml");

            if (doc.DocumentElement.Name == "RESPONSE")
            {
                Nhom nhom = dc.Nhoms.Single(t=> t.ID_Nhom == id);
                dc.Nhoms.DeleteOnSubmit(nhom);
                dc.ExecuteCommand("UPDATE DangKy SET ID_Nhom = null WHERE ID_Nhom = {0}", id);
                dc.SubmitChanges();

                cboGroup.DataBind();
            }
        }

        protected void btnGetDetailGroup_Click(object sender, EventArgs e)
        {

        }

        protected void cboFilterCourse_DataBound(object sender, EventArgs e)
        {
            cboGroup.DataBind();
        }

        protected void btnAddGroupMember_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();
            grvUser.AllowPaging = false;
            grvUser.DataBind();

            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get group Id
            int groupId = Convert.ToInt32(cboGroup.SelectedValue);
            //get user list

            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);
            string MaSV = "0";

            foreach (GridViewRow row in grvUser.Rows)
            {
                MaSV = grvUser.DataKeys[row.RowIndex]["MaSV"].ToString();
                if (arrIDs.Contains(MaSV))
                {
                    CheckBox chk = row.Cells[4].Controls[0] as CheckBox;

                    if (row.Cells[3].Text == "0" || !chk.Checked || row.Cells[10].Text != "&nbsp;")continue;

                    List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                    list.Add(new KeyValuePair<int, int>(groupId, Convert.ToInt32(row.Cells[3].Text)));

                    doc.LoadXml(MoodleGroup.AddGroupMembers(list, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\Group_Add_Member_" + cboGroup.Text + ".xml");

                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        DangKy dk = dc.DangKies.Single(t => t.STT == Convert.ToInt64(row.Cells[2].Text));
                        dk.ID_Nhom = (long?)groupId;
                        dc.SubmitChanges();
                    }
                }
            }

            grvUser.AllowPaging = true;
        }

        protected void btnDeleteGroupMember_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();
            grvUser.AllowPaging = false;
            grvUser.DataBind();

            DCVimaruDataContext dc = new DCVimaruDataContext();
            //get group Id
            int groupId = Convert.ToInt32(cboGroup.SelectedValue);
            //get user list

            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);
            string MaSV = "0";

            foreach (GridViewRow row in grvUser.Rows)
            {
                MaSV = grvUser.DataKeys[row.RowIndex]["MaSV"].ToString();
                if (arrIDs.Contains(MaSV))
                {
                    CheckBox chk = row.Cells[4].Controls[0] as CheckBox;
                    if (HttpUtility.HtmlDecode(row.Cells[10].Text) != cboGroup.SelectedItem.Text) continue;
                    List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();

                    list.Add(new KeyValuePair<int, int>(groupId, Convert.ToInt32(row.Cells[3].Text)));
                    doc.LoadXml(MoodleGroup.DeleteGroupMembers(list, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\Group_Delete_Member_" + cboGroup.Text + ".xml");

                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        DangKy dk = dc.DangKies.Single(t => t.STT == Convert.ToInt64(row.Cells[2].Text));
                        dk.ID_Nhom = null;
                        dc.SubmitChanges();
                    }
                }
            }

            grvUser.AllowPaging = true;
        }
    }
}