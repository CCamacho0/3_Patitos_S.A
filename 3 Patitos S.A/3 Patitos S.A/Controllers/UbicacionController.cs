using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Filtros;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3_Patitos_S.A.Controllers
{
    [FiltroAutenticacion]
    public class UbicacionController : Controller
    {
        private readonly Db_Context _context;

        public UbicacionController(Db_Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ListUbi = _context.Ubicacion.ToList();
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ubicacion);
        }

        public JsonResult Update(int id)
        {
            var ubicacion = _context.Ubicacion.Find(id);
            if (ubicacion == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró la Estado producto solicitada");
            }
            return Json(ubicacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(ubicacion).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ubicacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ubicacion = await _context.Ubicacion.FindAsync(id);
            if (ubicacion != null)
            {
                if (_context.Productos.Where(p => p.Id_Ubicacion == id).Count() == 0)
                    _context.Ubicacion.Remove(ubicacion);
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
