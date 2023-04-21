using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Filtros;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3_Patitos_S.A.Controllers
{
    [FiltroAutenticacion]
    public class Estado_pedidoController : Controller
    {
        private readonly Db_Context _context;

        public Estado_pedidoController(Db_Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ListEstados = _context.Estado_pedido.ToList();
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estado_pedido estado_pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estado_pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(estado_pedido);
        }

        public JsonResult Update(int id)
        {
            var ePedido = _context.Estado_pedido.Find(id);
            if (ePedido == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró la Estado producto solicitada");
            }
            return Json(ePedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Estado_pedido estado_pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(estado_pedido).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado_pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var estado_pedido = await _context.Estado_pedido.FindAsync(id);
            if (estado_pedido != null)
            {
                if (_context.Pedidos.Where(p => p.ID_Estado_Pedido == id).Count() == 0)
                    _context.Estado_pedido.Remove(estado_pedido);
                else
                {
                    ViewData["Error"] = "No se puede eliminar porque hay peidos con este estado.";
                    return View();
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
