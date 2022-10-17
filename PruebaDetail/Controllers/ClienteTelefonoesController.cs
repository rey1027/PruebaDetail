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
    public class ClienteTelefonoesController : Controller
    {
        private readonly PruebaContext _context;

        public ClienteTelefonoesController(PruebaContext context)
        {
            _context = context;
        }

        // GET: ClienteTelefonoes
        public async Task<IActionResult> Index()
        {
            var pruebaContext = _context.ClienteTelefonos.Include(c => c.CedulaNavigation);
            return View(await pruebaContext.ToListAsync());
        }

        // GET: ClienteTelefonoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteTelefonos == null)
            {
                return NotFound();
            }

            var clienteTelefono = await _context.ClienteTelefonos
                .Include(c => c.CedulaNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (clienteTelefono == null)
            {
                return NotFound();
            }

            return View(clienteTelefono);
        }

        // GET: ClienteTelefonoes/Create
        public IActionResult Create()
        {
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula");
            return View();
        }

        // POST: ClienteTelefonoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Telefono")] ClienteTelefono clienteTelefono)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteTelefono);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteTelefono.Cedula);
            return View(clienteTelefono);
        }

        // GET: ClienteTelefonoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteTelefonos == null)
            {
                return NotFound();
            }

            var clienteTelefono = await _context.ClienteTelefonos.FindAsync(id);
            if (clienteTelefono == null)
            {
                return NotFound();
            }
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteTelefono.Cedula);
            return View(clienteTelefono);
        }

        // POST: ClienteTelefonoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cedula,Telefono")] ClienteTelefono clienteTelefono)
        {
            if (id != clienteTelefono.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteTelefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteTelefonoExists(clienteTelefono.Cedula))
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
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteTelefono.Cedula);
            return View(clienteTelefono);
        }

        // GET: ClienteTelefonoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteTelefonos == null)
            {
                return NotFound();
            }

            var clienteTelefono = await _context.ClienteTelefonos
                .Include(c => c.CedulaNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (clienteTelefono == null)
            {
                return NotFound();
            }

            return View(clienteTelefono);
        }

        // POST: ClienteTelefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteTelefonos == null)
            {
                return Problem("Entity set 'PruebaContext.ClienteTelefonos'  is null.");
            }
            var clienteTelefono = await _context.ClienteTelefonos.FindAsync(id);
            if (clienteTelefono != null)
            {
                _context.ClienteTelefonos.Remove(clienteTelefono);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteTelefonoExists(int id)
        {
          return _context.ClienteTelefonos.Any(e => e.Cedula == id);
        }
    }
}
