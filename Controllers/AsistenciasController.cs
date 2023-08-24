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
using Microsoft.Extensions.Configuration;

namespace Fundacion.Controllers
{
    [Authorize]
    public class AsistenciasController : Controller
    {
        private readonly FundacionContext _context;
        private readonly IConfiguration _configuration;

        public AsistenciasController(FundacionContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Asistencias
        public async Task<IActionResult> Index()
        {
            var fundacionContext = _context.Asistencias.Include(a => a.Es);
            return View(await fundacionContext.ToListAsync());
        }

        // GET: Asistencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asistencias == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.Es)
                .FirstOrDefaultAsync(m => m.AsiId == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // GET: Asistencias/Create
        public IActionResult Create()
        {
            ViewBag.CentroLatitud = _configuration.GetSection("Ubicacion")["Latitud"];
            ViewBag.CentroLongitud = _configuration.GetSection("Ubicacion")["Longitud"];
            ViewData["EsId"] = new SelectList(_context.Set<Espacio>().Where(espacio => espacio.Us.RoId == 2), "EsId", "EsDescripcion");
            return View();
        }

        // POST: Asistencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsiId,EsId,AsIngreso,AsEgreso,AsPresent")] Asistencia asistencia)
        {
            ViewBag.CentroLatitud = _configuration.GetSection("Ubicacion")["Latitud"];
            ViewBag.CentroLongitud = _configuration.GetSection("Ubicacion")["Longitud"];
            if (ModelState.IsValid)
            {
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EsId"] = new SelectList(_context.Set<Espacio>().Where(espacio => espacio.Us.RoId == 2), "EsId", "EsDescripcion", asistencia.EsId);
            return View(asistencia);
        }

        // GET: Asistencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.CentroLatitud = _configuration.GetSection("Ubicacion")["Latitud"];
            ViewBag.CentroLongitud = _configuration.GetSection("Ubicacion")["Longitud"];
            if (id == null || _context.Asistencias == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }
            ViewData["EsId"] = new SelectList(_context.Set<Espacio>().Where(espacio => espacio.Us.RoId == 2), "EsId", "EsDescripcion", asistencia.EsId);
            return View(asistencia);
        }

        // POST: Asistencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsiId,EsId,AsIngreso,AsEgreso,AsPresent")] Asistencia asistencia)
        {
            ViewBag.CentroLatitud = _configuration.GetSection("Ubicacion")["Latitud"];
            ViewBag.CentroLongitud = _configuration.GetSection("Ubicacion")["Longitud"];
            if (id != asistencia.AsiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaExists(asistencia.AsiId))
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
            ViewData["EsId"] = new SelectList(_context.Set<Espacio>().Where(espacio => espacio.Us.RoId == 2), "EsId", "EsDescripcion", asistencia.EsId);
            return View(asistencia);
        }

        // GET: Asistencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asistencias == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.Es)
                .FirstOrDefaultAsync(m => m.AsiId == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // POST: Asistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asistencias == null)
            {
                return Problem("Entity set 'FundacionContext.Asistencia'  is null.");
            }
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia != null)
            {
                _context.Asistencias.Remove(asistencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciaExists(int id)
        {
            return (_context.Asistencias?.Any(e => e.AsiId == id)).GetValueOrDefault();
        }
    }
}
