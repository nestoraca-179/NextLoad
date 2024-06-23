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
            error.Descripcion = string.Format("{0} -> {1} -> {2}", ex.Message, ex.StackTrace, ex.Source);
            error.Fecha = DateTime.Now;

            using (NextLoadEntities context = new NextLoadEntities())
            {
                context.Incidente.Add(error);
                context.SaveChanges();
            }
        }
    }
}