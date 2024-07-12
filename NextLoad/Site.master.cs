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

				string name_suc = Session["NAME_CONN"].ToString();
                if (Session["BRANCH"] != null)
					name_suc += (" / Sucursal: " + (Session["BRANCH"] as saSucursal).sucur_des);

                LBL_DataEmp.Text = name_suc;
			}
            else
                Response.Redirect("/Login.aspx");
        }
    }
}