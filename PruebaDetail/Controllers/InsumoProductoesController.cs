using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaDetail.Models;

namespace PruebaDetail.Controllers
{
    public class InsumoProductoesController : Controller
    {
        private readonly PruebaContext _context;

        public InsumoProductoesController(PruebaContext context)
        {
            _context = context;
        }

        // GET: InsumoProductoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.InsumoProductos.ToListAsync());
        }

        // GET: InsumoProductoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.InsumoProductos == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (insumoProducto == null)
            {
                return NotFound();
            }

            return View(insumoProducto);
        }

        // GET: InsumoProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsumoProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Marca,Costo")] InsumoProducto insumoProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insumoProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insumoProducto);
        }

        // GET: InsumoProductoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.InsumoProductos == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos.FindAsync(id);
            if (insumoProducto == null)
            {
                return NotFound();
            }
            return View(insumoProducto);
        }

        // POST: InsumoProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Marca,Costo")] InsumoProducto insumoProducto)
        {
            if (id != insumoProducto.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insumoProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsumoProductoExists(insumoProducto.Nombre))
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
            return View(insumoProducto);
        }

        // GET: InsumoProductoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.InsumoProductos == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (insumoProducto == null)
            {
                return NotFound();
            }

            return View(insumoProducto);
        }

        // POST: InsumoProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.InsumoProductos == null)
            {
                return Problem("Entity set 'PruebaContext.InsumoProductos'  is null.");
            }
            var insumoProducto = await _context.InsumoProductos.FindAsync(id);
            if (insumoProducto != null)
            {
                _context.InsumoProductos.Remove(insumoProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsumoProductoExists(string id)
        {
          return _context.InsumoProductos.Any(e => e.Nombre == id);
        }
    }
}
