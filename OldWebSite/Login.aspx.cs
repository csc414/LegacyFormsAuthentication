using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OldWebSite
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                var type = Request.Form["type"];
                if (type == "Login")
                {
                    FormsAuthentication.SetAuthCookie(Request.Form["name"], false);
                    Response.Redirect(Request.Url.AbsolutePath);
                }
                else if(type == "Logout")
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect(Request.Url.AbsolutePath);
                }
            }
        }
    }
}