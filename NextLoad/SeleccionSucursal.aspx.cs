using NextLoad.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NextLoad
{
	public partial class SeleccionSucursal : System.Web.UI.Page
	{
		public List<saSucursal> sucs;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["USER"] != null)
			{
				if (Request.QueryString["conn"] != null)
				{
					int connect = int.Parse(Request.QueryString["conn"].ToString());
					Empresa conn = Connection.GetConnByID(connect);
					string connectionString = string.Format("Server={0};Database={1};User Id={2};Password={3}",
						conn.server, conn.database, conn.username, conn.password);
					SqlConnection connection = new SqlConnection(connectionString);

					Session["CONNECT"] = connectionString;
					Session["DB"] = connection.Database;
					Session["NAME_CONN"] = conn.des_con;
					Session["ID_CONN"] = conn.ID;

					bool useBranchs = new Branch().UseBranchs();
					if (!useBranchs)
						Response.Redirect("/Dashboard.aspx");

					sucs = new Branch().GetAllBranchs();
				}
				else
					Response.Redirect("/Login.aspx");
			}
			else
				Response.Redirect("/Login.aspx");
		}

        protected void BTN_Send_Click(object sender, EventArgs e)
        {
			Session["BRANCH"] = new Branch().GetBranchByID(HDD_Connect.Value);
			Response.Redirect("/Dashboard.aspx");
		}
    }
}