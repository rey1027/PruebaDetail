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
    public class CitaController : Controller
    {
        private readonly PruebaContext _context;

        public CitaController(PruebaContext context)
        {
            _context = context;
        }

        // GET: Cita Insertar 
        public async Task<IActionResult> Index()
        {
            var pruebaContext = _context.Cita.Include(c => c.CedulaNavigation).Include(c => c.SucursalNavigation).Include(c => c.TipoNavigation);
            return View(await pruebaContext.ToListAsync());
        }

        // GET: Cita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita
                .Include(c => c.CedulaNavigation)
                .Include(c => c.SucursalNavigation)
                .Include(c => c.TipoNavigation)
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (citum == null)
            {
                return NotFound();
            }

            return View(citum);
        }

        // GET: Cita/Create  Crear
        public IActionResult Create()
        {
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula");
            ViewData["Sucursal"] = new SelectList(_context.Sucursals, "Nombre", "Nombre");
            ViewData["Tipo"] = new SelectList(_context.Lavados, "Tipo", "Tipo");
            return View();
        }

        // POST: Cita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Placa,Fecha,Sucursal,Tipo,Cedula,Nombre,ServicioBrindado,Puntos,Monto,Iva")] Cita citum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", citum.Cedula);
            ViewData["Sucursal"] = new SelectList(_context.Sucursals, "Nombre", "Nombre", citum.Sucursal);
            ViewData["Tipo"] = new SelectList(_context.Lavados, "Tipo", "Tipo", citum.Tipo);
            return View(citum);
        }

        // GET: Cita/Edit/5   Editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita.FindAsync(id);
            if (citum == null)
            {
                return NotFound();
            }
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", citum.Cedula);
            ViewData["Sucursal"] = new SelectList(_context.Sucursals, "Nombre", "Nombre", citum.Sucursal);
            ViewData["Tipo"] = new SelectList(_context.Lavados, "Tipo", "Tipo", citum.Tipo);
            return View(citum);
        }

        // POST: Cita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Placa,Fecha,Sucursal,Tipo,Cedula,Nombre,ServicioBrindado,Puntos,Monto,Iva")] Cita citum)
        {
            if (id != citum.Placa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitumExists(citum.Placa))
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
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", citum.Cedula);
            ViewData["Sucursal"] = new SelectList(_context.Sucursals, "Nombre", "Nombre", citum.Sucursal);
            ViewData["Tipo"] = new SelectList(_context.Lavados, "Tipo", "Tipo", citum.Tipo);
            return View(citum);
        }

        // GET: Cita/Delete/5  Eliminar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita
                .Include(c => c.CedulaNavigation)
                .Include(c => c.SucursalNavigation)
                .Include(c => c.TipoNavigation)
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (citum == null)
            {
                return NotFound();
            }

            return View(citum);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cita == null)
            {
                return Problem("Entity set 'PruebaContext.Cita'  is null.");
            }
            var citum = await _context.Cita.FindAsync(id);
            if (citum != null)
            {
                _context.Cita.Remove(citum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitumExists(int id)
        {
          return _context.Cita.Any(e => e.Placa == id);
        }
    }
}
