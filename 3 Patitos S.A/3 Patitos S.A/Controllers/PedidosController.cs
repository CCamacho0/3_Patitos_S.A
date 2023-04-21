using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Filtros;
using _3_Patitos_S.A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace _3_Patitos_S.A.Controllers
{
    [FiltroAutenticacion]
    public class PedidosController : Controller
    {
        private readonly Db_Context _context;
        private readonly JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private Persona? user = null;

        public PedidosController(Db_Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var pedidos = from p in _context.Pedidos
                              join psna in _context.Persona on p.ID_Persona equals psna.Id_Persona
                              join te in _context.Tipo_Entrega on p.ID_Entrega equals te.ID_Entrega
                              join ep in _context.Estado_pedido on p.ID_Estado_Pedido equals ep.ID_Estado_Pedido
                              join pdcto in _context.Productos on p.ID_Producto equals pdcto.ID_Producto
                              select new
                              {
                                  p.Id_pedido,
                                  pdcto.Nombre_producto,
                                  psna.Nombre_Persona,
                                  te.Nombre_Entrega,
                                  p.Cantidad,
                                  p.Precio,
                                  ep.Nombre_Estado_Pedido
                              };

            if (pedidos.Count() > 0)
                ViewBag.ListPedidos = pedidos;
            else
                ViewData["Nodata"] = "No hay pedidos";

            ViewBag.Entrega = new SelectList(_context.Tipo_Entrega, "ID_Entrega", "Nombre_Entrega");

            var estados = _context.Estado_pedido.Where(e => e.ID_Estado_Pedido != 2 && e.ID_Estado_Pedido != 1 && e.ID_Estado_Pedido != 5).ToList();
            ViewBag.Estado = new SelectList(estados, "ID_Estado_Pedido", "Nombre_Estado_Pedido");
            return View();
        }

        public IActionResult IndexU()
        {
            string? userJson = HttpContext.Session.GetString("User");
            user = JsonSerializer.Deserialize<Persona>(userJson, options);

            var pedidos = from p in _context.Pedidos
                          join psna in _context.Persona on p.ID_Persona equals psna.Id_Persona
                          join te in _context.Tipo_Entrega on p.ID_Entrega equals te.ID_Entrega
                          join ep in _context.Estado_pedido on p.ID_Estado_Pedido equals ep.ID_Estado_Pedido
                          join pdcto in _context.Productos on p.ID_Producto equals pdcto.ID_Producto
                          where psna.Id_Persona == user.Id_Persona && p.ID_Estado_Pedido == 2
                          select new
                          {
                              p.Id_pedido,
                              pdcto.Nombre_producto,
                              psna.Nombre_Persona,
                              te.Nombre_Entrega,
                              p.Cantidad,
                              p.Precio,
                              ep.Nombre_Estado_Pedido
                          };

            if (pedidos.Count() > 0)
                ViewBag.ListPedidos = pedidos;
            else
                ViewData["Nodata"] = "No hay pedidos";

            ViewBag.Entrega = new SelectList(_context.Tipo_Entrega, "ID_Entrega", "Nombre_Entrega");
            ViewBag.Estado = new SelectList(_context.Estado_pedido, "ID_Estado_Pedido", "Nombre_Estado_Pedido");
            return View();
        }

        public IActionResult Compras()
        {
            string? userJson = HttpContext.Session.GetString("User");
            user = JsonSerializer.Deserialize<Persona>(userJson, options);

            var pedidos = from p in _context.Pedidos
                          join psna in _context.Persona on p.ID_Persona equals psna.Id_Persona
                          join te in _context.Tipo_Entrega on p.ID_Entrega equals te.ID_Entrega
                          join ep in _context.Estado_pedido on p.ID_Estado_Pedido equals ep.ID_Estado_Pedido
                          join pdcto in _context.Productos on p.ID_Producto equals pdcto.ID_Producto
                          where psna.Id_Persona == user.Id_Persona && p.ID_Estado_Pedido != 2 && p.ID_Estado_Pedido != 5
                          select new
                          {
                              p.Id_pedido,
                              pdcto.Nombre_producto,
                              psna.Nombre_Persona,
                              te.Nombre_Entrega,
                              p.Cantidad,
                              p.Precio,
                              ep.Nombre_Estado_Pedido
                          };

            if (pedidos.Count() > 0)
                ViewBag.ListPedidos = pedidos;
            else
                ViewData["Nodata"] = "No hay pedidos";

            return View();
        }

        public IActionResult Create(int id)
        {
            var pedidos = new Pedidos();
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {

                var va3 = 0.13;
                var iva = (int)producto.Precio * va3;

                pedidos.ID_Producto = id;
                ViewBag.Nombre = producto.Nombre_producto.ToString();
                ViewBag.Img = producto.Imagen;
                ViewBag.Precio = (int)producto.Precio+iva;
            }


            ViewBag.Entrega = new SelectList(_context.Tipo_Entrega, "ID_Entrega", "Nombre_Entrega");
            return View(pedidos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedidos pedidos)
        {
            string? userJson = HttpContext.Session.GetString("User");
            if (userJson != null)
            {
                user = JsonSerializer.Deserialize<Persona>(userJson, options);

                if (_context.Persona.Find(user.Id_Persona) == null)
                    return NotFound();

                pedidos.ID_Persona = user.Id_Persona;
            }

            pedidos.ID_Estado_Pedido = 2;

            var producto = _context.Productos.Find(pedidos.ID_Producto);
            producto.Cantidad -= pedidos.Cantidad;

            if (producto.Cantidad < 0)
            {
                //ViewData["Error"] = "La cantidad pedida es mayor a la cantidad disponible";
                //return View(pedidos);
                return RedirectToAction("Create", "Pedidos");
            }
            
            else
            {
                UpdateProductos(producto);

                if (ModelState.IsValid)
                {
                    _context.Add(pedidos);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("IndexU", "Productos");
                }
            }

            return View(pedidos);
        }

        private void UpdateProductos(Productos productos)
        {
            _context.Productos.Update(productos);
            _context.SaveChanges();
        }

        public IActionResult RCompra(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            pedido.ID_Estado_Pedido = 1;

            _context.Pedidos.Update(pedido);
            _context.SaveChanges();

            return RedirectToAction("IndexU");
        }

        public JsonResult Update(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            if (pedido == null)
            {
                Response.StatusCode = 500;
                return Json("No se encontró la Estado usuario solicitada");
            }
            pedido.Precio /= pedido.Cantidad;
            return Json(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Pedidos pedidos)
        {
            if(pedidos.ID_Estado_Pedido == 2)
            {
                var producto = _context.Productos.Find(pedidos.ID_Producto);
                int oldcantidad = _context.Pedidos.AsNoTracking().FirstOrDefault(p => p.Id_pedido == pedidos.Id_pedido)?.Cantidad ?? 0;

                if (oldcantidad > pedidos.Cantidad)
                    producto.Cantidad += oldcantidad - pedidos.Cantidad;

                else if (oldcantidad == pedidos.Cantidad)
                    producto.Cantidad -= pedidos.Cantidad;

                else
                    producto.Cantidad -= pedidos.Cantidad - oldcantidad;

                if (producto.Cantidad < 0)
                {
                    ViewData["Error"] = "La cantidad pedida es mayor a la cantidad disponible";
                    return RedirectToAction("Index");
                }
                else
                    UpdateProductos(producto);
                
            }
            if (ModelState.IsValid)
            {
                _context.Entry(pedidos).State = EntityState.Modified;
                _context.SaveChanges();
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateU(Pedidos pedidos)
        {
            if (pedidos.ID_Estado_Pedido == 2)
            {
                var producto = _context.Productos.Find(pedidos.ID_Producto);
                int oldcantidad = _context.Pedidos.AsNoTracking().FirstOrDefault(p => p.Id_pedido == pedidos.Id_pedido)?.Cantidad ?? 0;

                if (oldcantidad > pedidos.Cantidad)
                    producto.Cantidad += oldcantidad - pedidos.Cantidad;

                else if (oldcantidad == pedidos.Cantidad)
                    producto.Cantidad -= pedidos.Cantidad;

                else
                    producto.Cantidad -= pedidos.Cantidad - oldcantidad;

                if (producto.Cantidad < 0)
                {
                    ViewData["Error"] = "La cantidad pedida es mayor a la cantidad disponible";
                    return RedirectToAction("IndexU");
                }
                else
                    UpdateProductos(producto);

            }
            if (ModelState.IsValid)
            {
                _context.Entry(pedidos).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("IndexU");
            }
            return RedirectToAction("IndexU");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var pedido = _context.Pedidos.Find(id);

            var producto = _context.Productos.Find(pedido.ID_Producto);
            producto.Cantidad += pedido.Cantidad;

            if (pedido != null && pedido.ID_Estado_Pedido != 5)
            {
                UpdateProductos(producto);

                pedido.ID_Estado_Pedido = 5;
                _context.Entry(pedido).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
