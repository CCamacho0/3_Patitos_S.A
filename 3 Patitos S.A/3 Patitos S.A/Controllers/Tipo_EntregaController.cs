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
    public class Tipo_EntregaController : Controller
    {
        private readonly Db_Context _context;

        public Tipo_EntregaController(Db_Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ListTEntregas = _context.Tipo_Entrega.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tipo_Entrega tipo_Entrega)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipo_Entrega);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo_Entrega);
        }

        public JsonResult Update(int id)
        {
            var tEntrega = _context.Tipo_Entrega.Find(id);
            if (tEntrega == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró el tipo de entrega solicitada");
            }
            return Json(tEntrega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Tipo_Entrega tipo_Entrega)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(tipo_Entrega).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Entrega);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipo_entrega = await _context.Tipo_Entrega.FindAsync(id);
            if (tipo_entrega != null)
            {
                if (_context.Pedidos.Where(p => p.ID_Entrega == id).Count() == 0)
                    _context.Tipo_Entrega.Remove(tipo_entrega);
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
