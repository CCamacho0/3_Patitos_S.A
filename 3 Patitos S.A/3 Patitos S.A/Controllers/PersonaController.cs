using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Security.Cryptography;

namespace _3_Patitos_S.A.Controllers
{
    public class PersonaController : Controller
    {
        private readonly Db_Context _context;

        public PersonaController(Db_Context context)
        {
            _context = context;
        }

        private object GetListaPersonas()
        {
            var listPersona = from p in _context.Persona
                              join r in _context.Rol on p.Id_Rol equals r.Id_Rol
                              join eu in _context.Estado_Usuario on p.Id_Categoria equals eu.Id_estado_usuario
                              join c in _context.Categoria on p.Id_Categoria equals c.Id_categoria
                             // where eu.Id_estado_usuario != 2 //Filtro para las personas eliminadas
                              select new
                              {
                                  p.Id_Persona,
                                  r.Nombre_Rol,
                                  p.Nombre_Persona,
                                  p.Direccion,
                                  p.Telefono,
                                  p.Correo,
                                  eu.Nombre_estado,
                                  c.Nombre_categoria
                              };

            return listPersona;
        }

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
        public IActionResult Delete(Persona persona)
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

        public JsonResult Update(int id)
        {
            var persona = _context.Persona.Find(id);
            if (persona == null)
            {
                Response.StatusCode = 500;
                return Json(new { message = "No se encontró la persona solicitada" });
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
                return RedirectToAction("IndexPersonas");
            }
            return View(persona);
            
        }

        public IActionResult Registarse()
        {
            ViewBag.ListRol = new SelectList(_context.Rol, "Id_Rol", "Nombre_Rol");
            ViewBag.EstadoUsuario = new SelectList(_context.Estado_Usuario, "Id_estado_usuario", "Nombre_estado");
            ViewBag.Categoria = new SelectList(_context.Categoria, "Id_categoria", "Nombre_categoria");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registarse(Persona persona)
        {
            persona.Id_Rol = 1;
            persona.Id_Estado_Usuario = 1;
            persona.Id_Categoria = 1;

            if (ModelState.IsValid)
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(persona.Contrasena);
                SHA256 sha256 = SHA256.Create();
                byte[] password = sha256.ComputeHash(passwordBytes);
                persona.Contrasena = Convert.ToBase64String(password);

                _context.Persona.Add(persona);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(persona);
        } 
    }
}
