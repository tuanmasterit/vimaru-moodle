using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace Moodle
{
    public partial class _Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null || (string)Session["token"] == "")
            {
                Session["refUrl"] = "~/Default.aspx";
                Response.Redirect("~/Login.aspx");
                return;
            }

            System.Collections.IList visibleTables = Global.DefaultModel.VisibleTables;
            if (visibleTables.Count == 0)
            {
                throw new InvalidOperationException("There are no accessible tables. Make sure that at least one data model is registered in Global.asax and scaffolding is enabled or implement custom pages.");
            }
            Menu1.DataSource = visibleTables;
            Menu1.DataBind();
        }

    }
}
