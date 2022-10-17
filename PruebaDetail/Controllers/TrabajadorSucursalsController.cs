﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaDetail.Models;

namespace PruebaDetail.Controllers
{
    public class TrabajadorSucursalsController : Controller
    {
        private readonly PruebaContext _context;

        public TrabajadorSucursalsController(PruebaContext context)
        {
            _context = context;
        }

        // GET: TrabajadorSucursals
        public async Task<IActionResult> Index()
        {
            var pruebaContext = _context.TrabajadorSucursals.Include(t => t.CedulaNavigation).Include(t => t.NombreNavigation);
            return View(await pruebaContext.ToListAsync());
        }

        // GET: TrabajadorSucursals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrabajadorSucursals == null)
            {
                return NotFound();
            }

            var trabajadorSucursal = await _context.TrabajadorSucursals
                .Include(t => t.CedulaNavigation)
                .Include(t => t.NombreNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (trabajadorSucursal == null)
            {
                return NotFound();
            }

            return View(trabajadorSucursal);
        }

        // GET: TrabajadorSucursals/Create
        public IActionResult Create()
        {
            ViewData["Cedula"] = new SelectList(_context.Trabajadors, "Cedula", "Cedula");
            ViewData["Nombre"] = new SelectList(_context.Sucursals, "Nombre", "Nombre");
            return View();
        }

        // POST: TrabajadorSucursals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,FechaDeInicio")] TrabajadorSucursal trabajadorSucursal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajadorSucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cedula"] = new SelectList(_context.Trabajadors, "Cedula", "Cedula", trabajadorSucursal.Cedula);
            ViewData["Nombre"] = new SelectList(_context.Sucursals, "Nombre", "Nombre", trabajadorSucursal.Nombre);
            return View(trabajadorSucursal);
        }

        // GET: TrabajadorSucursals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrabajadorSucursals == null)
            {
                return NotFound();
            }

            var trabajadorSucursal = await _context.TrabajadorSucursals.FindAsync(id);
            if (trabajadorSucursal == null)
            {
                return NotFound();
            }
            ViewData["Cedula"] = new SelectList(_context.Trabajadors, "Cedula", "Cedula", trabajadorSucursal.Cedula);
            ViewData["Nombre"] = new SelectList(_context.Sucursals, "Nombre", "Nombre", trabajadorSucursal.Nombre);
            return View(trabajadorSucursal);
        }

        // POST: TrabajadorSucursals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cedula,Nombre,FechaDeInicio")] TrabajadorSucursal trabajadorSucursal)
        {
            if (id != trabajadorSucursal.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajadorSucursal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadorSucursalExists(trabajadorSucursal.Cedula))
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
            ViewData["Cedula"] = new SelectList(_context.Trabajadors, "Cedula", "Cedula", trabajadorSucursal.Cedula);
            ViewData["Nombre"] = new SelectList(_context.Sucursals, "Nombre", "Nombre", trabajadorSucursal.Nombre);
            return View(trabajadorSucursal);
        }

        // GET: TrabajadorSucursals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrabajadorSucursals == null)
            {
                return NotFound();
            }

            var trabajadorSucursal = await _context.TrabajadorSucursals
                .Include(t => t.CedulaNavigation)
                .Include(t => t.NombreNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (trabajadorSucursal == null)
            {
                return NotFound();
            }

            return View(trabajadorSucursal);
        }

        // POST: TrabajadorSucursals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrabajadorSucursals == null)
            {
                return Problem("Entity set 'PruebaContext.TrabajadorSucursals'  is null.");
            }
            var trabajadorSucursal = await _context.TrabajadorSucursals.FindAsync(id);
            if (trabajadorSucursal != null)
            {
                _context.TrabajadorSucursals.Remove(trabajadorSucursal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadorSucursalExists(int id)
        {
          return _context.TrabajadorSucursals.Any(e => e.Cedula == id);
        }
    }
}
