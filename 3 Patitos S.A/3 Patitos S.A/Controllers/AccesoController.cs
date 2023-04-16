using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;

namespace _3_Patitos_S.A.Controllers
{
    public class AccesoController : Controller
    {
        private readonly Db_Context _context;

        public AccesoController(Db_Context context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string correo, string contrasena)
        {
            JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            if (contrasena != null && correo != null)
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(contrasena);
                SHA256 sha256 = SHA256.Create();
                byte[] password = sha256.ComputeHash(passwordBytes);
                contrasena = Convert.ToBase64String(password);

                var user = _context.Persona.Where(p => p.Correo == correo && p.Contrasena == contrasena).SingleOrDefault();
                if (user != null)
                {
                    user.Contrasena = "Vacio";
                    var userBytes = JsonSerializer.SerializeToUtf8Bytes(user, options);
                    HttpContext.Session.Set("User", userBytes);

                    return RedirectToAction("IndexU", "Productos");
                }
                else
                {
                    ViewData["Error"] = "El correo o la contraseña son incorrectos.";
                    return View();
                }
            }
            else
            {
                ViewData["Error"] = "El correo o la contraseña son incorrectos.";
                return View();
            }
                
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Login", "Acceso");
        }
    }
}
