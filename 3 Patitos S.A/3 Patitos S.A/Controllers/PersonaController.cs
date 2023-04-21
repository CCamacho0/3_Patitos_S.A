using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Filtros;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace _3_Patitos_S.A.Controllers
{
    public class PersonaController : Controller
    {
        private readonly Db_Context _context;

        private readonly JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private Persona? user = null;

        public PersonaController(Db_Context context)
        {
            _context = context;
        }

        private object GetListaPersonas()
        {
            var listPersona = from p in _context.Persona
                              join r in _context.Rol on p.Id_Rol equals r.Id_Rol
                              join eu in _context.Estado_Usuario on p.Id_Estado_Usuario equals eu.Id_estado_usuario
                              join c in _context.Categoria on p.Id_Categoria equals c.Id_categoria
                              select new
                              {
                                  p.Id_Persona,
                                  r.Nombre_Rol,
                                  p.Nombre_Persona,
                                  p.Fecha_Nacimiento,
                                  p.Direccion,
                                  p.Telefono,
                                  p.Correo,
                                  eu.Nombre_estado,
                                  c.Nombre_categoria
                              };

            return listPersona;
        }

        private static string ConvertContrasena(string contrasena)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(contrasena);
            SHA256 sha256 = SHA256.Create();
            byte[] computePassword = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(computePassword);
        }

        [FiltroAutenticacion]
        public IActionResult IndexPersonas()
        {
            ViewBag.ListPersona = GetListaPersonas();
            ViewBag.ListRol = new SelectList(_context.Rol, "Id_Rol", "Nombre_Rol");
            ViewBag.EstadoUsuario = new SelectList(_context.Estado_Usuario, "Id_estado_usuario", "Nombre_estado");
            ViewBag.Categoria = new SelectList(_context.Categoria, "Id_categoria", "Nombre_categoria");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Persona? persona)
        {
            var psona = _context.Persona.Find(persona.Id_Persona);

            if (psona != null)
            {
                psona.Id_Estado_Usuario = 2;
                _context.Entry(psona).State = EntityState.Modified;
                _context.SaveChanges();
                ViewBag.ListPersona = GetListaPersonas();
            }
            return RedirectToAction("IndexPersonas");
        }

        [FiltroAutenticacion]
        public JsonResult Update(int id)
        {
            var persona = _context.Persona.Find(id);
            if (persona == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró la persona solicitada");
            }
            return Json(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(persona).State = EntityState.Modified;
                _context.SaveChanges();
                ViewBag.ListPersona = GetListaPersonas();

                string? userJson = HttpContext.Session.GetString("User");
                if (userJson != null)
                {
                    user = JsonSerializer.Deserialize<Persona>(userJson, options);
                    if (user.Id_Rol == 1)
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectToAction("IndexPersonas");
                }
            }
            return View(persona);
            
        }

        [FiltroAutenticacion]
        public IActionResult UpdateClient(int id)
        {
            var persona = _context.Persona.Find(id);
            if (persona == null)
                return NotFound();

            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateClient(Persona persona, string password)
        {
            if (ModelState.IsValid)
            {

                if (persona.Contrasena.Equals(password))
                    persona.Contrasena = ConvertContrasena(password);

                else
                {
                    ViewData["Error"] = "Las contraseñas no coinciden";
                    return View(persona);
                }

                _context.Entry(persona).State = EntityState.Modified;
                _context.SaveChanges();
                ViewBag.ListPersona = GetListaPersonas();
                return RedirectToAction("Detalles");

            }
            return View(persona);
        }

        public IActionResult Registarse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registarse(Persona persona, string password)
        {
            persona.Id_Rol = 1;
            persona.Id_Estado_Usuario = 1;
            persona.Id_Categoria = 1;
            try
            {
                if (ModelState.IsValid && persona.Contrasena != null)
                {

                    if (persona.Contrasena.Equals(password))
                        persona.Contrasena = ConvertContrasena(password);

                    else
                    {
                        ViewData["Error"] = "Las contraseñas no coinciden";
                        return View(persona);
                    }

                    _context.Persona.Add(persona);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "Acceso");
                }
                return View(persona);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        [FiltroAutenticacion]
        public IActionResult Detalles()
        {
            string? userJson = HttpContext.Session.GetString("User");
            try
            {
                if (userJson != null)
                {
                    user = JsonSerializer.Deserialize<Persona>(userJson, options);

                    var getpersona = from p in _context.Persona
                                     join r in _context.Rol on p.Id_Rol equals r.Id_Rol
                                     join c in _context.Categoria on p.Id_Categoria equals c.Id_categoria
                                     where p.Id_Persona == user.Id_Persona
                                     select new
                                     {
                                         p.Id_Persona,
                                         r.Nombre_Rol,
                                         p.Nombre_Persona,
                                         p.Fecha_Nacimiento,
                                         p.Direccion,
                                         p.Telefono,
                                         p.Correo,
                                         c.Nombre_categoria
                                     };
                    ViewBag.Persona = getpersona.ToList();
                    return View();
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }
    }
}
