using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Filtros;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3_Patitos_S.A.Controllers
{
    [FiltroAutenticacion]
    public class RolController : Controller
    {
        private readonly Db_Context _context;

        public RolController(Db_Context context)
        {
            _context = context;
        }
        public IActionResult RolIndex()
        {
            ViewBag.ListRoles = _context.Rol.ToList();
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        public JsonResult Update(int id)
        {
            var rol = _context.Rol.Find(id);
            if (rol == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró el rol solicitado");
            }
            return Json(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(rol).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _context.Rol.FindAsync(id);
            if (rol != null)
            {
                if (_context.Persona.Where(p => p.Id_Rol == id).Count() == 0)
                    _context.Rol.Remove(rol);
                else
                {
                    ViewData["Error"] = "No se puede eliminar porque hay personas con este rol.";
                    return RedirectToAction("RolIndex");
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("RolIndex");
        }
    }
}
