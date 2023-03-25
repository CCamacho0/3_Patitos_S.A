using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace _3_Patitos_S.A.Controllers
{
    public class PersonaController : Controller
    {
        private readonly Db_Context _context;

        public PersonaController(Db_Context context)
        {
            _context = context;
        }
        public IActionResult IndexPersonas()
        {
            var listPersona = from p in _context.Persona
                              join r in _context.Rol on p.Rol equals r.Id_Rol
                              join eu in _context.Estado_Usuario on p.Id_Categoria equals eu.Id_estado_usuario
                              join c in _context.Categoria on p.Id_Categoria equals c.Id_categoria
                              select new {
                                   p.Id_Persona,
                                   r.Nombre_Rol,
                                   p.Nombre_Persona,
                                   p.Direccion,
                                   p.Telefono,
                                   p.Correo,
                                   eu.Nombre_estado,
                                   c.Nombre_categoria};

            ViewBag.ListPersona = listPersona.ToList();
            ViewBag.ListRol = new SelectList(_context.Rol, "Id_Rol", "Nombre_Rol");
            ViewBag.EstadoUsuario = new SelectList(_context.Estado_Usuario, "Id_estado_usuario", "Nombre_estado");
            ViewBag.Categoria = new SelectList(_context.Categoria, "Id_categoria", "Nombre_categoria");
            return View();
        }

        public JsonResult DeletePersona(int id)
        {
            bool result = false;
            var persona = _context.Persona.Find(id);

            if (persona != null)
            {
                result = true;
                _context.Persona.Remove(persona);
                _context.SaveChanges();
            }
            return Json(result);
        }

        public IActionResult GetPersona(int id)
        {
            var persona = _context.Persona.Find(id);
            if (persona == null)
                return RedirectToAction("IndexPersonas");
            
            return Json(persona);
        }

        [HttpPost]
        public IActionResult Update(Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(persona).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("IndexPersonas");
            }

            return View(persona);
            
        }

        [HttpPost]
        public IActionResult Registarse(Persona persona)
        {
            persona.Rol = 1;
            persona.Id_Estado_Usuario = 1;
            persona.Id_Categoria = 1;

            if (ModelState.IsValid)
            {
                _context.Persona.Add(persona);
                _context.SaveChanges();
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
    }
}
