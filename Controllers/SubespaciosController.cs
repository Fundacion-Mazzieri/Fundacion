﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fundacion.Data;
using Fundacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.ObjectPool;

namespace Fundacion.Controllers
{
    [Authorize(Roles = "Super Admin, Admin")]
    public class SubespaciosController : Controller
    {
        private readonly FundacionContext _context;

        public SubespaciosController(FundacionContext context)
        {
            _context = context;
        }

        // GET: Subespacios
        public async Task<IActionResult> Index()
        {
            var fundacionContext = _context.Subespacios.Include(s => s.Au).Include(s => s.Es);
            return View(await fundacionContext.ToListAsync());
        }

        // GET: Subespacios
        public async Task<IActionResult> IndexById(int EsId)
        {
            var fundacionContext = _context.Subespacios
                .Include(s => s.Au)
                .Include(s => s.Es)
                .Where(s => s.EsId == EsId);
            return View(await fundacionContext.ToListAsync());
        }

        public IActionResult RedirectToIndexEspacio()
        {
            // Redirige al método "CreateByIdEspacio" del controlador "Subespacios" con el ID como parámetro.
            return RedirectToAction("Index", "Espacios");
        }

        // GET: Subespacios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Subespacios == null)
            {
                return NotFound();
            }

            var subespacio = await _context.Subespacios
                .Include(s => s.Au)
                .Include(s => s.Es)
                .FirstOrDefaultAsync(m => m.SeId == id);

            if (subespacio == null)
            {
                return NotFound();
            }

            return View(subespacio);
        }

        // GET: Subespacios/Create
        public IActionResult Create()
        {
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion");
            ViewData["EsId"] = new SelectList(_context.Espacios, "EsId", "EsDescripcion");
            return View();
        }

        // POST: Subespacios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeId,EsId,AuId,SeDia,SeHora,SeCantHs")] Subespacio subespacio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subespacio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion", subespacio.AuId);
            ViewData["EsId"] = new SelectList(_context.Espacios, "EsId", "EsDescripcion", subespacio.EsId);
            return View(subespacio);
        }
        // GET: Subespacios/CreateByIdEspacio
        public IActionResult CreateByIdEspacio(int? id)
        {
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion");
            ViewData["EsId"] = new SelectList(_context.Espacios.Where(espacio => espacio.EsId == id), "EsId", "EsDescripcion");
            return View();
        }

        // POST: Subespacios/CreateByIdEspacio
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByIdEspacio(int id,[Bind("SeId,EsId,AuId,SeDia,SeHora,SeCantHs")] Subespacio subespacio)
        {
            double cantidadHoras = subespacio.SeCantHs/10.0;
            if (ModelState.IsValid)
            {
                subespacio.SeCantHs = cantidadHoras;
                _context.Add(subespacio);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(CreateByIdEspacio));
                return RedirectToAction("Index", "Espacios");
            }
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion", subespacio.AuId);
            ViewData["EsId"] = new SelectList(_context.Espacios.Where(espacio => espacio.EsId == id), "EsId", "EsDescripcion", subespacio.EsId);
            return View(subespacio);
        }

        // GET: Subespacios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subespacios == null)
            {
                return NotFound();
            }

            var subespacio = await _context.Subespacios.FindAsync(id);
            if (subespacio == null)
            {
                return NotFound();
            }
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion", subespacio.AuId);
            ViewData["EsId"] = new SelectList(_context.Espacios, "EsId", "EsDescripcion", subespacio.EsId);
            return View(subespacio);
        }

        // POST: Subespacios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeId,EsId,AuId,SeDia,SeHora,SeCantHs")] Subespacio subespacio)
        {
            if (id != subespacio.SeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subespacio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubespacioExists(subespacio.SeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexById", new { EsId = subespacio.EsId });
            }
            ViewData["AuId"] = new SelectList(_context.Aulas, "AuId", "AuDescripcion", subespacio.AuId);
            ViewData["EsId"] = new SelectList(_context.Espacios, "EsId", "EsDescripcion", subespacio.EsId);
            return View(subespacio);
        }

        // GET: Subespacios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subespacios == null)
            {
                return NotFound();
            }

            var subespacio = await _context.Subespacios
                .Include(s => s.Au)
                .Include(s => s.Es)
                .FirstOrDefaultAsync(m => m.SeId == id);
            if (subespacio == null)
            {
                return NotFound();
            }

            return View(subespacio);
        }

        // POST: Subespacios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subespacios == null)
            {
                return Problem("Entity set 'FundacionContext.Subespacios'  is null.");
            }
            var subespacio = await _context.Subespacios.FindAsync(id);
            if (subespacio != null)
            {
                _context.Subespacios.Remove(subespacio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexById", new { EsId = subespacio.EsId });
        }

        private bool SubespacioExists(int id)
        {
          return (_context.Subespacios?.Any(e => e.SeId == id)).GetValueOrDefault();
        }
    }
}
