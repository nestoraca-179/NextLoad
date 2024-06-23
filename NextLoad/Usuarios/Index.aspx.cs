using NextLoad.Controllers;
using NextLoad.Models;
using System;
using System.Data;
using System.Web.UI;

namespace NextLoad.Usuarios
{
    public partial class Index : System.Web.UI.Page
    {
        private static string IDSelected;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
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
                    if (Request.QueryString["new_user"] != null)
                    {
                        PN_Success.Visible = true;
                        LBL_Success.Text = "Usuario agregado con éxito";
                    }

                    if (Request.QueryString["edit_user"] != null)
                    {
                        PN_Success.Visible = true;
                        LBL_Success.Text = "Usuario modificado con éxito";
                    }
                }
            }
            else
                Response.Redirect("/Login.aspx");
        }

        protected void BTN_AgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Usuarios/Agregar.aspx");
        }

        protected void BTN_EliminarUsuario_Click(object sender, EventArgs e)
        {
            Usuario user = (Session["USER"] as Usuario);
            int result = UsuarioController.Delete(int.Parse(IDSelected), user.username);

            if (result == 1)
            {
                PN_Success.Visible = true;
                LBL_Success.Text = "Usuario eliminado con éxito";
                GV_Usuarios.DataBind();
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
            }
        }

        protected void GV_Usuarios_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            IDSelected = e.KeyValue.ToString();

            if (e.CommandArgs.CommandName == "Editar")
            {
                Response.Redirect("/Usuarios/Editar.aspx?ID=" + IDSelected);
            }
            else if (e.CommandArgs.CommandName == "Eliminar")
            {
                string username = (GV_Usuarios.GetRow(e.VisibleIndex) as DataRowView).Row.ItemArray[1].ToString();
                LBL_Delete.Text = string.Format("¿Desea eliminar el Usuario {0}?", username);

                ScriptManager.RegisterStartupScript(this, GetType(), "modal", "openModalDelete()", true);
            }
        }

        protected void GV_Usuarios_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                if (e.VisibleIndex % 2 == 0)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
                else
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#eeeeee");
                }
            }
        }
    }
}