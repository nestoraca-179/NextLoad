using System;
using NextLoad.Models;

namespace NextLoad.Controllers
{
    public class IncidentController
    {
        public static void CreateIncident(string titulo, Exception ex)
        {
            Incidente error = new Incidente();

            error.Titulo = titulo;
            error.Fecha = DateTime.Now;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            error.Descripcion = string.Format("{0} -> {1} -> {2}", ex.Message, ex.StackTrace, ex.Source);

            using (NextLoadEntities context = new NextLoadEntities())
            {
                context.Incidente.Add(error);
                context.SaveChanges();
            }
        }
    }
}