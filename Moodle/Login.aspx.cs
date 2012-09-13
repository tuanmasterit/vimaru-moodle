﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Moodle
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] != null && (string)Session["token"] != "")
            {
                if (Session["refUrl"] != null)
                    Response.Redirect((string)Session["refUrl"]);
                else
                    Response.Redirect("~/");
            }
            else
            {
                txtUsername.Focus();
                if (!IsPostBack)
                {
                    cboService.DataSource = MoodleUtilites.GetServiceTable(Server.MapPath("~") + "./App_Data/ServiceList.txt");
                    cboService.DataBind();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
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
    }
}