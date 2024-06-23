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
            string message = "";

            string username = TB_Username.Text;
            string old_pass = TB_OldPassword.Text;
            string new_pass = TB_NewPassword.Text;

            if (!string.IsNullOrEmpty(old_pass) && !string.IsNullOrEmpty(new_pass))
            {
                string encryptedOldPass = SecurityController.Encrypt(old_pass);
                string encryptedNewPass = SecurityController.Encrypt(new_pass);

                using (NextLoadEntities context = new NextLoadEntities())
                {
                    Usuario user = context.Usuario.AsNoTracking().SingleOrDefault(u => u.username == username && u.password == encryptedOldPass);

                    if (user == null)
                    {
                        message = "Usuario o clave incorrectos";
                    }
                    else
                    {
                        if (old_pass.Trim() == new_pass.Trim())
                        {
                            message = "Debes ingresar una clave diferente a la anterior";
                        }
                        else
                        {
                            user.password = encryptedNewPass;
                            user.fec_camb = DateTime.Now.AddMonths(3);
                            context.Entry(user).State = EntityState.Modified;
                            context.SaveChanges();

                            Response.Redirect("/Dashboard.aspx");
                        }
                    }
                }
            }
            else
            {
                message = "Debes ingresar la clave anterior y la clave nueva";
            }

            if (message != "")
            {
                LBL_Error.Visible = true;
                LBL_Error.Text = message;
            }
        }
    }
}