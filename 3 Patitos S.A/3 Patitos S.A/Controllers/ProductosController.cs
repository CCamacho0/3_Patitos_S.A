using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3_Patitos_S.A.Models;
using _3_Patitos_S.A.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3_Patitos_S.A.Filtros;
using Microsoft.Extensions.Options;

namespace _3_Patitos_S.A.Controllers
{
    public class ProductosController : Controller
    {
        private readonly Db_Context _context;

        public ProductosController(Db_Context context)
        {
            _context = context;
        }

        [FiltroAutenticacion]
        public ActionResult Index()
        {
            ViewBag.ProductosList = _context.Productos.ToList();
            ViewBag.Ubicacion = new SelectList(_context.Ubicacion, "Id_Ubicacion", "Nombre_Ubicacion");
            ViewBag.Estado = new SelectList(_context.Estado_Productos, "ID_Estado", "Nombre_estado");

            return View();
        }
        public ActionResult IndexU()
        {
            ViewBag.ProductosList = _context.Productos.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [FiltroAutenticacion]
        public IActionResult Create(Productos productos)
        {
            try
            {
                if (productos.Img != null)
                {
                    using Stream fs = productos.Img.OpenReadStream();
                    BinaryReader binaryReader = new(fs);
                    using BinaryReader br = binaryReader;
                    productos.Imagen = br.ReadBytes((int)fs.Length);
                }
                if (ModelState.IsValid)
                {
                    _context.Add(productos);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        [FiltroAutenticacion]
        public JsonResult Update(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró la persona solicitada");
            }
            return Json(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [FiltroAutenticacion]
        public IActionResult Update(Productos productos)
        {
            try
            {
                if (productos.Img != null)
                {
                    using Stream fs = productos.Img.OpenReadStream();
                    BinaryReader binaryReader = new(fs);
                    using BinaryReader br = binaryReader;
                    productos.Imagen = br.ReadBytes((int)fs.Length);
                }

                if (ModelState.IsValid)
                {
                    _context.Entry(productos).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(productos);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [FiltroAutenticacion]
        public IActionResult Delete(Productos? productos)
        {
            var producto = _context.Productos.Find(productos.ID_Producto);

            if (producto != null)
            {
                producto.ID_Estado = 4;
                _context.Entry(producto).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

