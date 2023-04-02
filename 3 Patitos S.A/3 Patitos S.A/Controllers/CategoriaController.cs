using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Filtros;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3_Patitos_S.A.Controllers
{
    [FiltroAutenticacion]
    public class CategoriaController : Controller
    {
        private readonly Db_Context _context;

        public CategoriaController(Db_Context context)
        {
            _context = context;
        }

        private object GetListaCategoria()
        {
            var listCategorias = from c in _context.Categoria
                                 select new
                                 {
                                     c.Id_categoria,
                                     c.Nombre_categoria,
                                     Descuento = c.Descuento * 100,
                                 };
            return listCategorias;
        }
        public IActionResult CategoriaIndex()
        {
            ViewBag.ListCategoria = GetListaCategoria();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            categoria.Descuento = Math.Round(categoria.Descuento / 100, 2);

            if (ModelState.IsValid)
            {
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
                ViewBag.ListCategoria = GetListaCategoria();
                return RedirectToAction("CategoriaIndex");
            }
            return View(categoria);
        }

        public JsonResult Update(int id)
        {
            var categoria = _context.Categoria.Find(id);
            if (categoria != null)
            {
                categoria.Descuento = categoria.Descuento * 100;
                return Json(categoria);
            }
            else
            {
                Response.StatusCode = 500;
                return Json(new { message = "No se encontró la persona solicitada" });
            } 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Categoria categoria)
        {
            categoria.Descuento = Math.Round(categoria.Descuento / 100, 2);

            if (ModelState.IsValid)
            {
                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                ViewBag.ListPersona = GetListaCategoria();
                return RedirectToAction("CategoriaIndex");
            }
            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Categoria categoria)
        {
            var cat = _context.Categoria.Find(categoria.Id_categoria);
            if (cat != null)
            {
                _context.Remove(cat);
                _context.SaveChanges();
                ViewBag.ListPersona = GetListaCategoria();
            }
            return RedirectToAction("CategoriaIndex");
        }
    }
}
