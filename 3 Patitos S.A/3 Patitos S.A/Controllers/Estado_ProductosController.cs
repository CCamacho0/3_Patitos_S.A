using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3_Patitos_S.A.Data;
using _3_Patitos_S.A.Models;

namespace _3_Patitos_S.A
{
    public class Estado_ProductosController : Controller
    {
        private readonly Db_Context _context;

        public Estado_ProductosController(Db_Context context)
        {
            _context = context;
        }

        // GET: Estado_Productos
        public async Task<IActionResult> Index()
        {
              return _context.Estado_Productos != null ? 
                          View(await _context.Estado_Productos.ToListAsync()) :
                          Problem("Entity set 'Db_Context.Estado_Productos'  is null.");
        }

        // GET: Estado_Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estado_Productos == null)
            {
                return NotFound();
            }

            var estado_Productos = await _context.Estado_Productos
                .FirstOrDefaultAsync(m => m.ID_Estado == id);
            if (estado_Productos == null)
            {
                return NotFound();
            }

            return View(estado_Productos);
        }

        // GET: Estado_Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estado_Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Estado,Nombre_estado")] Estado_Productos estado_Productos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estado_Productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estado_Productos);
        }

        // GET: Estado_Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estado_Productos == null)
            {
                return NotFound();
            }

            var estado_Productos = await _context.Estado_Productos.FindAsync(id);
            if (estado_Productos == null)
            {
                return NotFound();
            }
            return View(estado_Productos);
        }

        // POST: Estado_Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Estado,Nombre_estado")] Estado_Productos estado_Productos)
        {
            if (id != estado_Productos.ID_Estado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estado_Productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Estado_ProductosExists(estado_Productos.ID_Estado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estado_Productos);
        }

        // GET: Estado_Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estado_Productos == null)
            {
                return NotFound();
            }

            var estado_Productos = await _context.Estado_Productos
                .FirstOrDefaultAsync(m => m.ID_Estado == id);
            if (estado_Productos == null)
            {
                return NotFound();
            }

            return View(estado_Productos);
        }

        // POST: Estado_Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estado_Productos == null)
            {
                return Problem("Entity set 'Db_Context.Estado_Productos'  is null.");
            }
            var estado_Productos = await _context.Estado_Productos.FindAsync(id);
            if (estado_Productos != null)
            {
                _context.Estado_Productos.Remove(estado_Productos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Estado_ProductosExists(int id)
        {
          return (_context.Estado_Productos?.Any(e => e.ID_Estado == id)).GetValueOrDefault();
        }
    }
}
