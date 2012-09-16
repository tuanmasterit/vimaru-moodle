using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.UI.WebControls;

namespace Moodle
{
    public partial class Site : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["token"] != null && Session["token"].ToString() != "")
            {
                palLogin.Visible = false;
                palUser.Visible = true;
                MainMenu.Visible = true;
            }
            else
            {
                palLogin.Visible = true;
                palUser.Visible = false;
                MainMenu.Visible = false;
                if (!IsPostBack)
                {
                    cboService.DataSource = MoodleUtilites.GetServiceTable();
                    cboService.DataBind();
                }
            }
        }

        protected void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "") return;
            MoodleUser u = new MoodleUser(txtUsername.Text, txtPassword.Text);
            string s = u.GetToken(cboService.SelectedValue);
            Session["token"] = s;
            if (s != "")
            {
                if (Session["refUrl"] != null)
                    Response.Redirect((string)Session["refUrl"]);
                else
                    Response.Redirect("~/");
            }
        }

        protected void btnLogout_Click(object sender, System.EventArgs e)
        {
            Session["token"] = null;
            Session["refUrl"] = Request.Url.ToString();
            Response.Redirect("~/");
        }
    }
}
