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
    public class TrabajadorsController : Controller
    {
        private readonly PruebaContext _context;

        public TrabajadorsController(PruebaContext context)
        {
            _context = context;
        }

        // GET: Trabajadors
        public async Task<IActionResult> Index()
        {
              return View(await _context.Trabajadors.ToListAsync());
        }

        // GET: Trabajadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trabajadors == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadors
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // GET: Trabajadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trabajadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Apellido1,Apellido2,FechaDeNacimiento,Edad,Rol,TipoDePago,PasswordT,FechaDeIngreso")] Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trabajador);
        }

        // GET: Trabajadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trabajadors == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadors.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }
            return View(trabajador);
        }

        // POST: Trabajadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cedula,Nombre,Apellido1,Apellido2,FechaDeNacimiento,Edad,Rol,TipoDePago,PasswordT,FechaDeIngreso")] Trabajador trabajador)
        {
            if (id != trabajador.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadorExists(trabajador.Cedula))
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
            return View(trabajador);
        }

        // GET: Trabajadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trabajadors == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadors
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // POST: Trabajadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trabajadors == null)
            {
                return Problem("Entity set 'PruebaContext.Trabajadors'  is null.");
            }
            var trabajador = await _context.Trabajadors.FindAsync(id);
            if (trabajador != null)
            {
                _context.Trabajadors.Remove(trabajador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadorExists(int id)
        {
          return _context.Trabajadors.Any(e => e.Cedula == id);
        }
    }
}
