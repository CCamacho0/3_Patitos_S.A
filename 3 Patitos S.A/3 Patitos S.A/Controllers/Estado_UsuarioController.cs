using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Models;
using _3_Patitos_S.A.Filtros;

namespace _3_Patitos_S.A
{
    [FiltroAutenticacion]
    public class Estado_UsuarioController : Controller
    {
        private readonly Db_Context _context;

        public Estado_UsuarioController(Db_Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ListEstados = _context.Estado_Usuario.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estado_Usuario estado_Usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estado_Usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estado_Usuario);
        }


        public JsonResult Update(int id)
        {
            var eUsuario = _context.Estado_Usuario.Find(id);
            if (eUsuario == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró la Estado usuario solicitada");
            }
            return Json(eUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Estado_Usuario estado_Usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(estado_Usuario).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado_Usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estado_Usuarios = await _context.Estado_Usuario.FindAsync(id);
            if (estado_Usuarios != null)
            {
                if (_context.Persona.Where(p => p.Id_Estado_Usuario == id).Count() == 0)
                    _context.Estado_Usuario.Remove(estado_Usuarios);
                else
                {
                    ViewData["Error"] = "No se puede eliminar porque hay productos con este estado.";
                    return View();
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
