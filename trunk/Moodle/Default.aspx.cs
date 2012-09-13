using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Moodle
{
    public partial class Token : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnRedirectLogin.Visible = (Session["token"] == null || (string)Session["token"] == "");
            if (!IsPostBack)
            {
                cboService.DataSource = MoodleUtilites.GetServiceTable(Server.MapPath("~") + "./App_Data/ServiceList.txt");
                cboService.DataBind();
            }
        }

        protected void btnGetToken_Click(object sender, EventArgs e)
        {
            MoodleUser u = new MoodleUser(txtUsername.Text, txtPassword.Text);
            txtToken.Text = u.GetToken(cboService.SelectedItem.Value);
            txtFunctions.Text = cboService.SelectedItem.Value;
        }

        protected void btnGetFunctionList_Click(object sender, EventArgs e)
        {
            ListItemCollection ls = MoodleWebService.GetServiceList(txtToken.Text);
            txtFunctions.Text = "";
            foreach (ListItem item in ls)
            {
                txtFunctions.Text += item.Text + "\n";
            }
        }
    }
}