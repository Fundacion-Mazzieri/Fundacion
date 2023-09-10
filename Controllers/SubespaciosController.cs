using System;
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

        // GET: Subespacios/Details/5
        public async Task<IActionResult> Details(int? id)
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool SubespacioExists(int id)
        {
          return (_context.Subespacios?.Any(e => e.SeId == id)).GetValueOrDefault();
        }
    }
}
