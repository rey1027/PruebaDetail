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
    public class LavadoesController : Controller
    {
        private readonly PruebaContext _context;

        public LavadoesController(PruebaContext context)
        {
            _context = context;
        }

        // GET: Lavadoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Lavados.ToListAsync());
        }

        // GET: Lavadoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Lavados == null)
            {
                return NotFound();
            }

            var lavado = await _context.Lavados
                .FirstOrDefaultAsync(m => m.Tipo == id);
            if (lavado == null)
            {
                return NotFound();
            }

            return View(lavado);
        }

        // GET: Lavadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lavadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Costo,Duracion,PuntosRequeridos,PuntosOrtorgados,Precio,Lavador,Pulidor")] Lavado lavado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lavado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lavado);
        }

        // GET: Lavadoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Lavados == null)
            {
                return NotFound();
            }

            var lavado = await _context.Lavados.FindAsync(id);
            if (lavado == null)
            {
                return NotFound();
            }
            return View(lavado);
        }

        // POST: Lavadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Tipo,Costo,Duracion,PuntosRequeridos,PuntosOrtorgados,Precio,Lavador,Pulidor")] Lavado lavado)
        {
            if (id != lavado.Tipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lavado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LavadoExists(lavado.Tipo))
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
            return View(lavado);
        }

        // GET: Lavadoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Lavados == null)
            {
                return NotFound();
            }

            var lavado = await _context.Lavados
                .FirstOrDefaultAsync(m => m.Tipo == id);
            if (lavado == null)
            {
                return NotFound();
            }

            return View(lavado);
        }

        // POST: Lavadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Lavados == null)
            {
                return Problem("Entity set 'PruebaContext.Lavados'  is null.");
            }
            var lavado = await _context.Lavados.FindAsync(id);
            if (lavado != null)
            {
                _context.Lavados.Remove(lavado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LavadoExists(string id)
        {
          return _context.Lavados.Any(e => e.Tipo == id);
        }
    }
}
