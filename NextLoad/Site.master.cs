using System;
using NextLoad.Models;

namespace NextLoad
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {
                Usuario user = Session["USER"] as Usuario;
                LBL_User.Text = user.des_usuario;

                if (!user.admin)
                    item_users.Visible = false;
            }
            else
                Response.Redirect("/Login.aspx");
        }
    }
}