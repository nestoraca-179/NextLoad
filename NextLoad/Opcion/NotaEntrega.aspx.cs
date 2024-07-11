using NextLoad.Controllers;
using NextLoad.Models;
using System;
using System.IO;
using System.Web;

namespace NextLoad.Opcion
{
    public partial class NotaEntrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PN_Success.Visible = false;
            PN_Error.Visible = false;
        }

        protected void BTN_UploadFileExcel_Click(object sender, EventArgs e)
        {
            DataController dc = new DataController();
            Usuario user = (Session["USER"] as Usuario);

            bool is_error = false;
            string error = "", folder = Server.MapPath("~") + "Documents\\";
            string result = "";

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (FU_UploadFile.HasFile)
            {
                HttpPostedFile file = FU_UploadFile.PostedFile;

                // VALIDA SI EL ARCHIVO ES EXCEL
                if (dc.IsExcel(file))
                {
                    string filename = file.FileName;
                    string path = folder + filename;

                    try
                    {
                        file.SaveAs(path);

                        if (!CK_Exist.Checked)
                        {
							var rows = dc.ProcessExcelNote(path, true);
							dc.ProcessDataNote(rows, true);
							result = dc.InsertDataNote(rows, user.username, true);
						}
						else
						{
							var rows = dc.ProcessExcelDocExist(path, true);
							dc.ProcessExistDataNote(rows, true);
							result = dc.InsertExistDataNote(rows, user.username, true);
						}
					}
                    catch (Exception ex)
                    {
                        is_error = true;
                        error = ex.Message;

                        File.Delete(path);
                        IncidentController.CreateIncident("ERROR LEYENDO DOCUMENTO EXCEL", ex);
                    }
                }
                else
                {
                    is_error = true;
                    error = "El archivo debe ser .xls o .xlsx";
                }
            }
            else
            {
                is_error = true;
                error = "No has subido ningún archivo Excel";
            }

            if (!is_error)
            {
                PN_Success.Visible = true;
                LBL_Success.Text = string.Format("Archivo Excel procesado con éxito (Nota de Entrega Nro. {0})", result);
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = error;
            }
        }

        protected void BTN_DownloadTemplate_Click(object sender, EventArgs e)
        {
            if (!CK_Exist.Checked)
            {
				string path = Server.MapPath("~") + "Templates\\nota_entrega.xlsx";
				Response.ContentType = "application/vnd.ms-excel";
				Response.AppendHeader("Content-Disposition", "attachment; filename=TEMPLATE_nota_entrega.xlsx");
				Response.TransmitFile(path);
				Response.End();
            }
            else
            {
				string path = Server.MapPath("~") + "Templates\\nota_entrega_exists.xlsx";
				Response.ContentType = "application/vnd.ms-excel";
				Response.AppendHeader("Content-Disposition", "attachment; filename=TEMPLATE_nota_entrega_exists.xlsx");
				Response.TransmitFile(path);
				Response.End();
			}
        }

        protected void BTN_DownloadExample_Click(object sender, EventArgs e)
        {
            if (!CK_Exist.Checked)
            {
				string path = Server.MapPath("~") + "Files\\nota_entrega.xlsx";
				Response.ContentType = "application/vnd.ms-excel";
				Response.AppendHeader("Content-Disposition", "attachment; filename=EXAMPLE_nota_entrega.xlsx");
				Response.TransmitFile(path);
				Response.End();
            }
            else
            {
				string path = Server.MapPath("~") + "Files\\nota_entrega_exists.xlsx";
				Response.ContentType = "application/vnd.ms-excel";
				Response.AppendHeader("Content-Disposition", "attachment; filename=EXAMPLE_nota_entrega_exists.xlsx");
				Response.TransmitFile(path);
				Response.End();
			}
		}

		protected void CK_Exist_CheckedChanged(object sender, EventArgs e)
		{
			PN_CondsNormal.Visible = !CK_Exist.Checked;
			PN_CondsExists.Visible = CK_Exist.Checked;
			SP_Exist.Visible = CK_Exist.Checked;
		}
	}
}