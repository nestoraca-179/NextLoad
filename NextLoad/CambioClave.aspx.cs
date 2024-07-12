using NextLoad.Controllers;
using NextLoad.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace NextLoad
{
    public partial class CambioClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Session["USER"] as Usuario;
            TB_Username.Text = user.username;
        }

        protected void BTN_CambiarClave_Click(object sender, EventArgs e)
        {
            string username = TB_Username.Text;
            string old_pass = TB_OldPassword.Text;
            string new_pass = TB_NewPassword.Text;

            string message = AccountController.ChangePass(username, old_pass, new_pass);   

            if (message == "OK")
            {
				Response.Redirect("/Dashboard.aspx");
            }
            else
            {
				LBL_Error.Visible = true;
				LBL_Error.Text = message;
			}
        }
    }
}