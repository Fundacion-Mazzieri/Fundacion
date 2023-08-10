﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fundacion.Data;
using Fundacion.Models;

namespace Fundacion.Controllers
{
    public class EspaciosController : Controller
    {
        private readonly FundacionContext _context;

        public EspaciosController(FundacionContext context)
        {
            _context = context;
        }

        // GET: Espacios
        public async Task<IActionResult> Index()
        {
            var fundacionContext = _context.Espacios.Include(e => e.Au).Include(e => e.Ca).Include(e => e.Tu).Include(e => e.Us);
            return View(await fundacionContext.ToListAsync());
        }

        // GET: Espacios/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Espacios == null)
            {
                return NotFound();
            }

            var espacio = await _context.Espacios
                .Include(e => e.Au)
                .Include(e => e.Ca)
                .Include(e => e.Tu)
                .Include(e => e.Us)
                .FirstOrDefaultAsync(m => m.EsId == id);
            if (espacio == null)
            {
                return NotFound();
            }

            return View(espacio);
        }

        // GET: Espacios/Create
        public IActionResult Create()
        {
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion");
            ViewData["CaId"] = new SelectList(_context.Categorias, "CaId", "CaDescripcion");
            ViewData["TuId"] = new SelectList(_context.Turnos, "TuId", "TuDescripcion");
            ViewData["UsId"] = new SelectList(_context.Usuarios, "UsId", "UsNombre");
            return View();
        }

        // POST: Espacios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EsId,EsDescripcion,AuId,EsDia,EsHora,EsCantHs,TuId,UsId,EsActivo,CaId")] Espacio espacio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(espacio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion", espacio.AuId);
            ViewData["CaId"] = new SelectList(_context.Categorias, "CaId", "CaDescripcion", espacio.CaId);
            ViewData["TuId"] = new SelectList(_context.Turnos, "TuId", "TuDescripcion", espacio.TuId);
            ViewData["UsId"] = new SelectList(_context.Usuarios, "UsId", "UsNombre", espacio.UsId);
            return View(espacio);
        }

        // GET: Espacios/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Espacios == null)
            {
                return NotFound();
            }

            var espacio = await _context.Espacios.FindAsync(id);
            if (espacio == null)
            {
                return NotFound();
            }
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion", espacio.AuId);
            ViewData["CaId"] = new SelectList(_context.Categorias, "CaId", "CaDescripcion", espacio.CaId);
            ViewData["TuId"] = new SelectList(_context.Turnos, "TuId", "TuDescripcion", espacio.TuId);
            ViewData["UsId"] = new SelectList(_context.Usuarios, "UsId", "UsNombre", espacio.UsId);
            return View(espacio);
        }

        // POST: Espacios/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EsId,EsDescripcion,AuId,EsDia,EsHora,EsCantHs,TuId,UsId,EsActivo,CaId")] Espacio espacio)
        {
            if (id != espacio.EsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(espacio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspacioExists(espacio.EsId))
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
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion", espacio.AuId);
            ViewData["CaId"] = new SelectList(_context.Categorias, "CaId", "CaDescripcion", espacio.CaId);
            ViewData["TuId"] = new SelectList(_context.Turnos, "TuId", "TuDescripcion", espacio.TuId);
            ViewData["UsId"] = new SelectList(_context.Usuarios, "UsId", "UsNombre", espacio.UsId);
            return View(espacio);
        }

        // GET: Espacios/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Espacios == null)
            {
                return NotFound();
            }

            var espacio = await _context.Espacios
                .Include(e => e.Au)
                .Include(e => e.Ca)
                .Include(e => e.Tu)
                .Include(e => e.Us)
                .FirstOrDefaultAsync(m => m.EsId == id);
            if (espacio == null)
            {
                return NotFound();
            }

            return View(espacio);
        }

        // POST: Espacios/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Espacios == null)
            {
                return Problem("Entity set 'FundacionContext.Espacios'  is null.");
            }
            var espacio = await _context.Espacios.FindAsync(id);
            if (espacio != null)
            {
                _context.Espacios.Remove(espacio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspacioExists(int id)
        {
          return (_context.Espacios?.Any(e => e.EsId == id)).GetValueOrDefault();
        }
    }
}