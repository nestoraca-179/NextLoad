using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using NextLoad.Models;

namespace NextLoad.Controllers
{
    public class AccountController
    {
        public static int LogIn(string username, string password)
        {
            int result = 0;

            try
            {
                string encrypted_pass = SecurityController.Encrypt(password);
                string decrypted_pass = SecurityController.Decrypt("SujxtFmjFBY0pyaVgcEjfA==");

                using (NextLoadEntities context = new NextLoadEntities())
                {
                    Usuario user = context.Usuario.SingleOrDefault(u => u.username == username && u.password == encrypted_pass);

                    if (user != null)
                    {
                        if (user.activo)
                        {
                            FormsAuthentication.SetAuthCookie(username, true);
                            HttpContext.Current.Session["USER"] = user;
                            LogController.CreateLog(user.username, "LOGIN", user.ID.ToString(), "L", null);

                            if (DateTime.Compare(user.fec_camb, DateTime.Now) < 0)
                                result = 4;
                            else
                                result = 1;
                        }
                        else
                        {
                            result = 2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = 3;
                IncidentController.CreateIncident("ERROR INICIANDO SESION " + username, ex);
            }

            return result;
        }

        public static void LogOut()
        {
            Usuario user = HttpContext.Current.Session["USER"] as Usuario;

            LogController.CreateLog(user.username, "LOGOUT", user.ID.ToString(), "D", null);
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
        }
    }
}