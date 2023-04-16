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
using Microsoft.Extensions.Options;

namespace _3_Patitos_S.A
{
    [FiltroAutenticacion]
    public class Estado_ProductosController : Controller
    {
        private readonly Db_Context _context;

        public Estado_ProductosController(Db_Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ListEstados = _context.Estado_Productos.ToList();
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estado_Productos estado_Productos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estado_Productos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(estado_Productos);
        }

        public JsonResult Update(int id)
        {
            var eProducto = _context.Estado_Productos.Find(id);
            if (eProducto == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró la Estado producto solicitada");
            }
            return Json(eProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Estado_Productos estado_Productos)
        {   
            if (ModelState.IsValid)
            {
                _context.Entry(estado_Productos).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado_Productos);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var estado_Productos = await _context.Estado_Productos.FindAsync(id);
            if (estado_Productos != null)
            {
                if(_context.Productos.Where(p => p.ID_Producto == id).Count() == 0)
                    _context.Estado_Productos.Remove(estado_Productos);
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
