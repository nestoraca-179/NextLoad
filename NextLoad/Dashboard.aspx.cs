using System;

namespace NextLoad
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {

            }
            else
                Response.Redirect("/Login.aspx");
        }
    }
}