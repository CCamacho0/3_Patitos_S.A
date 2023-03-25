using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace _3_Patitos_S.A.Controllers
{
    public class AccesoController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        // GET: Acceso
        public ActionResult Enter(string Usuario, string Contrasena)
        {

            try
            {
                using (Db_Context db = new Db_Context())

                {

                    var lst = from d in db.Persona
                              where d.Correo == Usuario && d.Contrasena == Contrasena
                              select d;
                    if (lst.Count() > 0)
                    {

                        Usuario oUser = lst.First();
                        Session["User"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario invalido");
                    }


                }
            }

            
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }


        }
    }
}
