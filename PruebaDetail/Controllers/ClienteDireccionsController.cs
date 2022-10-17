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
    public class ClienteDireccionsController : Controller
    {
        private readonly PruebaContext _context;

        public ClienteDireccionsController(PruebaContext context)
        {
            _context = context;
        }

        // GET: ClienteDireccions
        public async Task<IActionResult> Index()
        {
            var pruebaContext = _context.ClienteDireccions.Include(c => c.CedulaNavigation);
            return View(await pruebaContext.ToListAsync());
        }

        // GET: ClienteDireccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteDireccions == null)
            {
                return NotFound();
            }

            var clienteDireccion = await _context.ClienteDireccions
                .Include(c => c.CedulaNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (clienteDireccion == null)
            {
                return NotFound();
            }

            return View(clienteDireccion);
        }

        // GET: ClienteDireccions/Create
        public IActionResult Create()
        {
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula");
            return View();
        }

        // POST: ClienteDireccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Direccion")] ClienteDireccion clienteDireccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteDireccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteDireccion.Cedula);
            return View(clienteDireccion);
        }

        // GET: ClienteDireccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteDireccions == null)
            {
                return NotFound();
            }

            var clienteDireccion = await _context.ClienteDireccions.FindAsync(id);
            if (clienteDireccion == null)
            {
                return NotFound();
            }
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteDireccion.Cedula);
            return View(clienteDireccion);
        }

        // POST: ClienteDireccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cedula,Direccion")] ClienteDireccion clienteDireccion)
        {
            if (id != clienteDireccion.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteDireccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteDireccionExists(clienteDireccion.Cedula))
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
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteDireccion.Cedula);
            return View(clienteDireccion);
        }

        // GET: ClienteDireccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteDireccions == null)
            {
                return NotFound();
            }

            var clienteDireccion = await _context.ClienteDireccions
                .Include(c => c.CedulaNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (clienteDireccion == null)
            {
                return NotFound();
            }

            return View(clienteDireccion);
        }

        // POST: ClienteDireccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteDireccions == null)
            {
                return Problem("Entity set 'PruebaContext.ClienteDireccions'  is null.");
            }
            var clienteDireccion = await _context.ClienteDireccions.FindAsync(id);
            if (clienteDireccion != null)
            {
                _context.ClienteDireccions.Remove(clienteDireccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteDireccionExists(int id)
        {
          return _context.ClienteDireccions.Any(e => e.Cedula == id);
        }
    }
}
