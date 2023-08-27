//using AspNetCore;
using Fundacion.Data;
using Fundacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fundacion.Controllers
{
    [Authorize]
    public class UserAsistenciasController : Controller
    {
        private readonly FundacionContext _context;
        private readonly IConfiguration _configuration;

        public UserAsistenciasController(FundacionContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // GET: UserAsistenciasController
        public async Task<IActionResult> Index()
        {
            var fundacionContext = _context.Asistencias.Include(a => a.Es);
            return View(await fundacionContext.ToListAsync());
        }

        //// GET: UserAsistenciasController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: UserAsistenciasController/Create
        public ActionResult UserCreate()
        {
            ViewBag.CentroLatitud = _configuration.GetSection("Ubicacion")["Latitud"];
            ViewBag.CentroLongitud = _configuration.GetSection("Ubicacion")["Longitud"];
            ViewData["EsId"] = new SelectList(_context.Set<Espacio>(), "EsId", "EsDescripcion");
            return View();
        }

        // POST: UserAsistenciasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate([Bind("AsiId,EsId,AsIngreso,AsEgreso,AsPresent")] Asistencia asistencia, Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EsId"] = new SelectList(_context.Set<Espacio>().Where(espacio => espacio.UsId == usuario.UsId), "EsId", "EsDescripcion", asistencia.EsId);
            return View(asistencia);
        }

        // GET: UserAsistenciasController/Edit/5       
        public async Task<IActionResult> UserEdit(int? id)
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
            ViewData["EsId"] = new SelectList(_context.Set<Espacio>(), "EsId", "EsDescripcion", asistencia.EsId);
            return View(asistencia);
        }

        // POST: Asistencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(int id, [Bind("AsiId,EsId,AsIngreso,AsEgreso,AsPresent")] Asistencia asistencia, Usuario usuario)
        {
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
        //// GET: UserAsistenciasController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: UserAsistenciasController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        private bool AsistenciaExists(int id)
        {
            return (_context.Asistencias?.Any(e => e.AsiId == id)).GetValueOrDefault();
        }
    }
}
