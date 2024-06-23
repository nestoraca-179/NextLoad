using NextLoad.Controllers;
using NextLoad.Models;
using System;

namespace NextLoad.Usuarios
{
    public partial class Editar : System.Web.UI.Page
    {
        private static string IDUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                Usuario user = (Session["USER"] as Usuario);

                if (!user.admin)
                {
                    PN_ContainerForm.Visible = false;
                    PN_Error.Visible = true;
                    LBL_Error.Text = "No tienes acceso al área de usuarios";
                }
                else
                {
                    Usuario edit_user = UsuarioController.GetByID(int.Parse(Request.QueryString["ID"].ToString()));

                    if (!IsPostBack)
                        CargarUsuario(edit_user);

                    IDUser = Request.QueryString["ID"].ToString();
                    LBL_IDUsuario.Text = "Editar Usuario " + edit_user.des_usuario;
                }
            }
            else
            {
                BTN_Guardar.Visible = false;
                BTN_Volver.Visible = false;
                PN_ContainerForm.Visible = false;
                PN_Error.Visible = true;
                LBL_Error.Text = "El ID del usuario no puede ser nulo";
            }
        }

        protected void BTN_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Usuarios/Index.aspx");
        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();

            try
            {
                user.ID = int.Parse(IDUser);
                user.username = TB_Username.Text;
                // user.password = SecurityController.Encrypt(TB_Password.Text);
                user.des_usuario = TB_Descrip.Text;
                user.email = TB_Email.Text;
                user.activo = CK_Activo.Checked;
                user.admin = CK_Admin.Checked;
                user.co_us_mo = (Session["USER"] as Usuario).username;
                user.fe_us_mo = DateTime.Now;

                int result = UsuarioController.Edit(user);

                if (result == 1)
                {
                    Response.Redirect("/Usuarios/Index.aspx?edit_user=1");
                }
                else
                {
                    PN_Error.Visible = true;
                    LBL_Error.Text = "Ha ocurrido un error al modificar el Usuario. Ver tabla de Incidentes";
                }
            }
            catch (Exception ex)
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
                IncidentController.CreateIncident(string.Format("ERROR PROCESANDO DATOS DEL USUARIO {0}", user.ID), ex);
            }
        }

        private void CargarUsuario(Usuario user)
        {
            TB_Username.Text = user.username;
            TB_Password.Text = SecurityController.GeneratePointPass(user.password);
            TB_Descrip.Text = user.des_usuario;
            TB_Email.Text = user.email;
            CK_Activo.Checked = user.activo;
            CK_Admin.Checked = user.admin;
        }
    }
}