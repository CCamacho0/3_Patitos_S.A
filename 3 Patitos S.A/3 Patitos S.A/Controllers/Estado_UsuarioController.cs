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
    public class Estado_UsuarioController : Controller
    {
        private readonly Db_Context _context;

        public Estado_UsuarioController(Db_Context context)
        {
            _context = context;
        }

        // GET: Estado_Usuario
        public async Task<IActionResult> Index()
        {
              return _context.Estado_Usuario != null ? 
                          View(await _context.Estado_Usuario.ToListAsync()) :
                          Problem("Entity set 'Db_Context.Estado_Usuario'  is null.");
        }

        // GET: Estado_Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estado_Usuario == null)
            {
                return NotFound();
            }

            var estado_Usuario = await _context.Estado_Usuario
                .FirstOrDefaultAsync(m => m.Id_estado_usuario == id);
            if (estado_Usuario == null)
            {
                return NotFound();
            }

            return View(estado_Usuario);
        }

        // GET: Estado_Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estado_Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_estado_usuario,Nombre_estado")] Estado_Usuario estado_Usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estado_Usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estado_Usuario);
        }

        // GET: Estado_Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estado_Usuario == null)
            {
                return NotFound();
            }

            var estado_Usuario = await _context.Estado_Usuario.FindAsync(id);
            if (estado_Usuario == null)
            {
                return NotFound();
            }
            return View(estado_Usuario);
        }

        // POST: Estado_Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_estado_usuario,Nombre_estado")] Estado_Usuario estado_Usuario)
        {
            if (id != estado_Usuario.Id_estado_usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estado_Usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Estado_UsuarioExists(estado_Usuario.Id_estado_usuario))
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
            return View(estado_Usuario);
        }

        // GET: Estado_Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estado_Usuario == null)
            {
                return NotFound();
            }

            var estado_Usuario = await _context.Estado_Usuario
                .FirstOrDefaultAsync(m => m.Id_estado_usuario == id);
            if (estado_Usuario == null)
            {
                return NotFound();
            }

            return View(estado_Usuario);
        }

        // POST: Estado_Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estado_Usuario == null)
            {
                return Problem("Entity set 'Db_Context.Estado_Usuario'  is null.");
            }
            var estado_Usuario = await _context.Estado_Usuario.FindAsync(id);
            if (estado_Usuario != null)
            {
                _context.Estado_Usuario.Remove(estado_Usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Estado_UsuarioExists(int id)
        {
          return (_context.Estado_Usuario?.Any(e => e.Id_estado_usuario == id)).GetValueOrDefault();
        }
    }
}
