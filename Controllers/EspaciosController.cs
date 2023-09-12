using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fundacion.Data;
using Fundacion.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fundacion.Controllers
{
    [Authorize(Roles = "Super Admin, Admin")]
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
            var fundacionContext = _context.Espacios.Include(e => e.Ca).Include(e => e.Tu).Include(e => e.Us);
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
            ViewData["UsId"] = new SelectList(
                _context.Set<Usuario>()
                .Where(usuario => usuario.RoId == 2)
                .Select(usuario => new
                {
                    usuario.UsId, UsDni = $"({usuario.UsDni}) {usuario.UsApellido}, {usuario.UsNombre}"
                }),
                "UsId", "UsDni");
            return View();
        }

        // POST: Espacios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EsId,EsDescripcion,TuId,UsId,EsActivo,CaId")] Espacio espacio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(espacio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaId"] = new SelectList(_context.Categorias, "CaId", "CaDescripcion", espacio.CaId);
            ViewData["TuId"] = new SelectList(_context.Turnos, "TuId", "TuDescripcion", espacio.TuId);
            ViewData["UsId"] = new SelectList(
                _context.Set<Usuario>()
                .Where(usuario => usuario.RoId == 2)
                .Select(usuario => new
                {
                    usuario.UsId,
                    UsDni = $"({usuario.UsDni}) {usuario.UsApellido}, {usuario.UsNombre}"
                }),
                "UsId", "UsDni");
            return View(espacio);
        }

        public IActionResult RedirectToCreateByIdEspacio(int id)
        {
            // Redirige al método "CreateByIdEspacio" del controlador "Subespacios" con el ID como parámetro.
            return RedirectToAction("CreateByIdEspacio", "Subespacios", new { id = id });
        }

        public IActionResult RedirectToDetailsByEsId(int? EsId)
        {
            if (EsId == null)
            {
                return NotFound();
            }

            // Redirige a la acción Details del controlador de Subespacios
            return RedirectToAction("IndexById", "Subespacios", new { EsId });
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
            ViewData["CaId"] = new SelectList(_context.Categorias, "CaId", "CaDescripcion", espacio.CaId);
            ViewData["TuId"] = new SelectList(_context.Turnos, "TuId", "TuDescripcion", espacio.TuId);
            ViewData["UsId"] = new SelectList(
                _context.Set<Usuario>()
                .Where(usuario => usuario.RoId == 2)
                .Select(usuario => new
                {
                    usuario.UsId,
                    UsDni = $"({usuario.UsDni}) {usuario.UsApellido}, {usuario.UsNombre}"
                }),
                "UsId", "UsDni");
            return View(espacio);
        }

        // POST: Espacios/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EsId,EsDescripcion,TuId,UsId,EsActivo,CaId")] Espacio espacio)
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
            ViewData["CaId"] = new SelectList(_context.Categorias, "CaId", "CaDescripcion", espacio.CaId);
            ViewData["TuId"] = new SelectList(_context.Turnos, "TuId", "TuDescripcion", espacio.TuId);
            ViewData["UsId"] = new SelectList(
                _context.Set<Usuario>()
                .Where(usuario => usuario.RoId == 2)
                .Select(usuario => new
                {
                    usuario.UsId,
                    UsDni = $"({usuario.UsDni}) {usuario.UsApellido}, {usuario.UsNombre}"
                }),
                "UsId", "UsDni");
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
