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
using System.Configuration;

namespace Fundacion.Controllers
{
    [Authorize(Roles="Super Admin,Admin,Usuario")]
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
            var fundacionContext = _context.Asistencias
                .Include(a => a.Es)
                .Include(a => a.Es.Au)
                .Include(a => a.Es.Tu)
                .Include(a => a.Es.Us);
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
            ViewData["AsIngreso"] = DateTime.Now;
            ViewData["EsId"] = new SelectList(
                _context.Set<Espacio>()
                .Where(espacio => espacio.Us.RoId == 2)
                .Select(espacio => new
                {
                    espacio.EsId,
                    EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Au.AuDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.EsDia} {espacio.EsHora} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                }),
                "EsId", "EsDescripcion");
            return View();
        }
        //public IActionResult Create()
        //{
        //    ViewBag.CentroLatitud = _configuration.GetSection("Ubicacion")["Latitud"];
        //    ViewBag.CentroLongitud = _configuration.GetSection("Ubicacion")["Longitud"];

        //    // Establecer valores de AsIngreso y AsEgreso
        //    
        //    ViewData["AsEgreso"] = DateTime.Now;
        //    //ViewData["EsId"] = new SelectList(_context.Set<Espacio>().Where(espacio => espacio.Us.RoId == 2), "EsId", "EsDescripcion");

        //    ViewData["EsId"] = new SelectList(
        //        _context.Set<Espacio>()
        //        .Where(espacio => espacio.Us.RoId == 2)
        //        .Select(espacio => new
        //        {
        //            espacio.EsId, EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Au.AuDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.EsDia} {espacio.EsHora} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"}),
        //        "EsId", "EsDescripcion");
        //    return View();
        //}

        // POST: Asistencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Create([Bind("EsId,AsIngreso")] Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                asistencia.AsIngreso = DateTime.Now; // Establece el tiempo de inicio
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = asistencia.AsiId });
            }

            ViewData["EsId"] = new SelectList(_context.Set<Espacio>().Where(espacio => espacio.Us.RoId == 2), "EsId", "EsDescripcion", asistencia.EsId);
            return View(asistencia);
        }

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

            // Si la asistencia ya tiene tiempo de egreso, redirigir a la página de detalles
            if (asistencia.AsEgreso != null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["EsId"] = new SelectList(
                _context.Set<Espacio>()
                .Where(espacio => espacio.Us.RoId == 2)
                .Select(espacio => new
                {
                    espacio.EsId,
                    EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Au.AuDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.EsDia} {espacio.EsHora} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                }),
                "EsId", "EsDescripcion");
            return View(asistencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsiId,EsId,AsIngreso,AsEgreso,AsPresent")] Asistencia asistencia)
        {
            if (id != asistencia.AsiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                asistencia.AsEgreso = DateTime.Now; // Establece el tiempo de egreso
                _context.Update(asistencia);
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> ActivarEgreso()
        {
            await Task.Delay(TimeSpan.FromMinutes(30)); // Esperar 30 minutos
            ViewBag.ActivarEgreso = true;
            return RedirectToAction(nameof(Create));
        }
    }
}
