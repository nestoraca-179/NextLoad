using NextLoad.Controllers;
using System;

namespace NextLoad
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] != null && Session["USER"] != null)
            {
                AccountController.LogOut();
                Response.Redirect("/Login.aspx");
            }

            Session.Clear();
        }

        protected void BTN_Login_Click(object sender, EventArgs e)
        {
            string message = "";
            int result = AccountController.LogIn(TB_Username.Text.Trim(), TB_Password.Text.Trim());

            switch (result)
            {
                case 0:
                    message = "Usuario o Clave incorrectos";
                    break;
                case 1:
                    Response.Redirect("/Dashboard.aspx");
                    break;
                case 2:
                    message = "Usuario Inactivo";
                    break;
                case 3:
                    message = "Se ha producido una excepción. Ver table de Incidentes";
                    break;
                case 4:
                    Response.Redirect("/CambioClave.aspx");
                    break;
            }

            if (result != 1 && result != 4)
            {
                LBL_Error.Visible = true;
                LBL_Error.Text = message;
            }
        }
    }
}