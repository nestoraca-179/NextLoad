using NextLoad.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace NextLoad.Controllers
{
    public class UsuarioController : Repository
    {
        public static Usuario GetByID(int id)
        {
            return db.Usuario.Single(u => u.ID == id);
        }

        public static int Add(Usuario user)
        {
            int result = 0;

            using (NextLoadEntities context = new NextLoadEntities())
            {
                using (DbContextTransaction tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        user.fec_camb = DateTime.Now.AddMinutes(-1);
                        Usuario u = context.Usuario.Add(user);
                        context.SaveChanges();
                        tran.Commit();

                        LogController.CreateLog(u.co_us_in, "USUARIO", u.ID.ToString(), "I", null);
                        result = 1;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        IncidentController.CreateIncident(string.Format("ERROR AGREGANDO USUARIO N° {0}", user.username), ex);
                    }
                }
            }

            return result;
        }

        public static int Edit(Usuario user)
        {
            int result = 0;

            try
            {
                Usuario existing = GetByID(user.ID);
                user.password = existing.password;
                user.co_us_in = existing.co_us_in;
                user.fe_us_in = existing.fe_us_in;
                user.fec_camb = existing.fec_camb;

                string campos = GetChanges(existing, user);

                db.Entry(existing).CurrentValues.SetValues(user);
                db.SaveChanges();

                LogController.CreateLog(user.co_us_mo, "USUARIO", user.ID.ToString(), "M", campos);
                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR MODIFICANDO USUARIO N° {0}", user.username), ex);
            }

            return result;
        }

        public static int Delete(int ID, string us)
        {
            int result = 0;
            Usuario user = GetByID(ID);

            try
            {
                Usuario u = db.Usuario.Remove(user);
                db.SaveChanges();

                LogController.CreateLog(us, "USUARIO", u.ID.ToString(), "E", null);
                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR ELIMINANDO USUARIO N° {0}", user.username), ex);
            }

            return result;
        }

        private static string GetChanges(Usuario user_v, Usuario user_n)
        {
            string campos = "";
            Type type = new Usuario().GetType();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.Name != "fe_us_in" && prop.Name != "fe_us_mo")
                {
                    string valor1 = prop.GetValue(user_v) == null ? "" : prop.GetValue(user_v).ToString();
                    string valor2 = prop.GetValue(user_n) == null ? "" : prop.GetValue(user_n).ToString();

                    if (valor1 != valor2)
                    {
                        campos += string.Format("{0}: {1} -> {2}; ", prop.Name, valor1, valor2);
                    }
                }
            }

            return campos;
        }
    }
}