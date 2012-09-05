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
                grvTaiKhoan.DataBind();
        }

        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvTaiKhoan.PageSize = Convert.ToInt16(cboPageSize.SelectedValue);
        }

        protected void LinqDataSourceTaiKhoan_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            DCVimaruDataContext dc = new DCVimaruDataContext();
            var rs = from dk in dc.DangKies
                     where dk.MaHP + dk.MaNH == cboAcountFilter.SelectedValue
                     select new UserResult
                         {
                             Id = dk.SinhVien.Id,
                             MaSV = dk.MaSV,
                             Ho = dk.SinhVien.Ho,
                             Ten = dk.SinhVien.Ten,
                             Email = dk.SinhVien.MaSV
                         };
            e.Result = rs.OrderBy(u => u.Ten);
            cboPageSize_SelectedIndexChanged(sender, e);
        }

        protected void grvTaiKhoan_PreRender(object sender, EventArgs e)
        {
            grvTaiKhoan.DataBind();
            PopulateCheckedValues();
        }

        protected void grvTaiKhoan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = grvTaiKhoan.Rows.Count.ToString();
                e.Row.Cells[1].Text = "Trang " + (grvTaiKhoan.PageIndex + 1) + "/" + grvTaiKhoan.PageCount;
            }
        }

        protected void LinqDataSourceHocPhan_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            DCVimaruDataContext dc = new DCVimaruDataContext();
            var rs = from tkb in dc.ThoiKhoaBieus
                     select new HocPhanResult
                     {
                         MaHP = tkb.MaHP + tkb.MaNH,
                         TenHP = tkb.HocPhan.TenHP + " (N" + tkb.MaNH + ")",
                     };
            e.Result = rs.OrderBy(t=>t.TenHP);
        }

        protected void SaveCheckedValues()
        {
            try
            {
                ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);

                string MaSV = "0";
                CheckBox chk;

                foreach (GridViewRow rowItem in grvTaiKhoan.Rows)
                {

                    chk = (CheckBox)(rowItem.FindControl("chk"));
                    MaSV = grvTaiKhoan.DataKeys[rowItem.RowIndex]["MaSV"].ToString();
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

                foreach (GridViewRow rowItem in grvTaiKhoan.Rows)
                {
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chk"));
                    MaSV = grvTaiKhoan.DataKeys[rowItem.RowIndex]["MaSV"].ToString();
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

        protected void grvTaiKhoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        protected void grvTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblErrorMessage.Text = txtUsername.Text;
            int rowId = grvTaiKhoan.SelectedIndex;
            txtId.Text = grvTaiKhoan.Rows[rowId].Cells[2].Text.ToString();
            txtNewUsername.Text = grvTaiKhoan.Rows[rowId].Cells[3].Text.ToString();
            txtNewPassword.Text = grvTaiKhoan.Rows[rowId].Cells[3].Text.ToString();
            txtFirstName.Text = HttpUtility.HtmlDecode(grvTaiKhoan.Rows[rowId].Cells[4].Text.ToString());
            txtLastName.Text = HttpUtility.HtmlDecode(grvTaiKhoan.Rows[rowId].Cells[5].Text.ToString());
            txtEmail.Text = grvTaiKhoan.Rows[rowId].Cells[6].Text.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();

            grvTaiKhoan.AllowPaging = false;
            grvTaiKhoan.DataBind();

            MoodleUser user;
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);
            string MaSV="0";

            foreach (GridViewRow row in grvTaiKhoan.Rows)
            {
                MaSV = grvTaiKhoan.DataKeys[row.RowIndex]["MaSV"].ToString();
                if (arrIDs.Contains(MaSV))
                {
                    user = new MoodleUser
                        {
                            Username = row.Cells[3].Text,
                            Password = row.Cells[3].Text,
                            Firstname = HttpUtility.HtmlDecode(row.Cells[4].Text),
                            Lastname =  HttpUtility.HtmlDecode(row.Cells[5].Text),
                            Email = row.Cells[6].Text,
                            Timezone =  "7.0",
                            City = "Hai Phong",
                            Country = "VN"
                        };

                    List<MoodleUser> lstUser = new List<MoodleUser>();
                    lstUser.Add(user);
                    doc.LoadXml(MoodleUser.CreateUsers(lstUser, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\" + user.Username + ".xml");
                    if (doc.DocumentElement.Name == "RESPONSE")
                    {
                        long? userId = (long?)Convert.ToUInt64(doc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText);
                        DCVimaruDataContext dc = new DCVimaruDataContext();
                        SinhVien sv = dc.SinhViens.Single(t => t.MaSV == user.Username);
                        sv.Id = userId;
                        dc.SubmitChanges();
                    }
                }
            }
            
            grvTaiKhoan.AllowPaging = true;
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
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
            doc.Save("E:\\Z-TMP\\" + txtId.Text + ".xml");
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            SaveCheckedValues();
            grvTaiKhoan.AllowPaging = false;
            grvTaiKhoan.DataBind();

            MoodleUser user = new MoodleUser();
            XmlDocument doc = new XmlDocument();
            ArrayList arrIDs = ConvertToArrayList(txtMaSV.Text);
            string MaSV = "0";

            foreach (GridViewRow row in grvTaiKhoan.Rows)
            {
                MaSV = grvTaiKhoan.DataKeys[row.RowIndex]["MaSV"].ToString();
                if (arrIDs.Contains(MaSV))
                {
                    double userId = Convert.ToDouble(row.Cells[2].Text);
                    if(userId != 0)
                        user = new MoodleUser
                        {
                            Id = userId
                        };
                    else
                        continue;

                    List<MoodleUser> lstUser = new List<MoodleUser>();
                    lstUser.Add(user);
                    doc.LoadXml(MoodleUser.DeleteUsers(lstUser, (string)Session["token"]));
                    doc.Save("E:\\Z-TMP\\" + user.Id + ".xml");
                    if (userId > 0)
                    {
                        DCVimaruDataContext dc = new DCVimaruDataContext();
                        SinhVien sv = dc.SinhViens.Single(t => t.MaSV == MaSV);
                        sv.Id = 0;
                        dc.SubmitChanges();
                    }
                }
            }

            grvTaiKhoan.AllowPaging = true;
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

        protected void btnGetUser_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtId.Text == "0")
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
            doc.LoadXml(MoodleUser.GetUsersById(lstUser, (string)Session["token"]));
            doc.Save("E:\\Z-TMP\\" + user.Id + ".xml");
            XmlNode xmlnode = doc.ChildNodes[1];
            treeUserDetail.Nodes.Clear();
            treeUserDetail.Nodes.Add(new TreeNode(doc.DocumentElement.Name));
            TreeNode tNode;
            tNode = treeUserDetail.Nodes[0];
            AddNode(xmlnode, tNode);
            treeUserDetail.ExpandAll();
        }

        
    }
}