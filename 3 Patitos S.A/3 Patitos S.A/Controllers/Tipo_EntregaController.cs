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
    public class Tipo_EntregaController : Controller
    {
        private readonly Db_Context _context;

        public Tipo_EntregaController(Db_Context context)
        {
            _context = context;
        }

        // GET: Tipo_Entrega
        public async Task<IActionResult> Index()
        {
              return _context.Tipo_Entrega != null ? 
                          View(await _context.Tipo_Entrega.ToListAsync()) :
                          Problem("Entity set 'Db_Context.Tipo_Entrega'  is null.");
        }

        // GET: Tipo_Entrega/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tipo_Entrega == null)
            {
                return NotFound();
            }

            var tipo_Entrega = await _context.Tipo_Entrega
                .FirstOrDefaultAsync(m => m.ID_Entrega == id);
            if (tipo_Entrega == null)
            {
                return NotFound();
            }

            return View(tipo_Entrega);
        }

        // GET: Tipo_Entrega/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Entrega/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Entrega,Nombre_Entrega")] Tipo_Entrega tipo_Entrega)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipo_Entrega);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo_Entrega);
        }

        // GET: Tipo_Entrega/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tipo_Entrega == null)
            {
                return NotFound();
            }

            var tipo_Entrega = await _context.Tipo_Entrega.FindAsync(id);
            if (tipo_Entrega == null)
            {
                return NotFound();
            }
            return View(tipo_Entrega);
        }

        // POST: Tipo_Entrega/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Entrega,Nombre_Entrega")] Tipo_Entrega tipo_Entrega)
        {
            if (id != tipo_Entrega.ID_Entrega)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo_Entrega);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tipo_EntregaExists(tipo_Entrega.ID_Entrega))
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
            return View(tipo_Entrega);
        }

        // GET: Tipo_Entrega/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tipo_Entrega == null)
            {
                return NotFound();
            }

            var tipo_Entrega = await _context.Tipo_Entrega
                .FirstOrDefaultAsync(m => m.ID_Entrega == id);
            if (tipo_Entrega == null)
            {
                return NotFound();
            }

            return View(tipo_Entrega);
        }

        // POST: Tipo_Entrega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tipo_Entrega == null)
            {
                return Problem("Entity set 'Db_Context.Tipo_Entrega'  is null.");
            }
            var tipo_Entrega = await _context.Tipo_Entrega.FindAsync(id);
            if (tipo_Entrega != null)
            {
                _context.Tipo_Entrega.Remove(tipo_Entrega);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Tipo_EntregaExists(int id)
        {
          return (_context.Tipo_Entrega?.Any(e => e.ID_Entrega == id)).GetValueOrDefault();
        }
    }
}
