using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Moodle
{
    public partial class GetTokenAndServiceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetToken_Click(object sender, EventArgs e)
        {
            MoodleUser u = new MoodleUser(txtUsername.Text, txtPassword.Text);
            txtToken.Text = u.GetToken(txtServiceShortName.Text);
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