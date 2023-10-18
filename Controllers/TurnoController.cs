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
using DocumentFormat.OpenXml.InkML;

namespace Fundacion.Controllers
{
    [Authorize(Roles = "Super Admin, Admin")]
    public class TurnoController : Controller
    {
        private readonly FundacionContext _context;

        public TurnoController(FundacionContext context)
        {
            _context = context;
        }

        // GET: Turno
        public async Task<IActionResult> Index()
        {
              return _context.Turnos != null ? 
                          View(await _context.Turnos.ToListAsync()) :
                          Problem("Entity set 'FundacionContext.Turnos'  is null.");
        }

        // GET: Turno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .FirstOrDefaultAsync(m => m.TuId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TuId,TuDescripcion")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                // Convertir la descripción proporcionada a minúsculas
                string descripcionMinusculas = turno.TuDescripcion.ToLower();

                // Recuperar los registros de la base de datos
                var turnos = await _context.Turnos
                    .ToListAsync();

                // Realizar la comparación en memoria
                bool turnoExiste = turnos.Any(e => LevenshteinDistance(e.TuDescripcion.ToLower(), descripcionMinusculas) <= 3);

                if (!turnoExiste)
                {
                    _context.Add(turno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var turnoSimilar = turnos.FirstOrDefault(e => LevenshteinDistance(e.TuDescripcion.ToLower(), descripcionMinusculas) <= 3);
                    ModelState.AddModelError("TuDescripcion", $"El turno '{turno.TuDescripcion}' ya existe o tiene una nombre similar: '{turnoSimilar?.TuDescripcion}'.");
                }

                //if (!TurnoExists(turno.TuId, turno.TuDescripcion))
                //{
                //    _context.Add(turno);
                //    _context.Add(turno);
                //    await _context.SaveChangesAsync();
                //    return RedirectToAction(nameof(Index));
                //};
                //TempData["Mensaje"] = turno.TuDescripcion;
                //return RedirectToAction("ErrorTurno");
            }
            return View(turno);
        }
        //public ActionResult ErrorTurno()
        //{
        //    return View();
        //}
        // GET: Turno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            return View(turno);
        }

        // POST: Turno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TuId,TuDescripcion")] Turno turno)
        {
            if (id != turno.TuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Cargar el turno desde la base de datos
                    var turnoActual = await _context.Turnos.FindAsync(id);

                    if (turnoActual != null)
                    {
                        // Convertir la descripción proporcionada a minúsculas
                        string descripcionMinusculas = turno.TuDescripcion.ToLower();

                        // Recuperar los registros de la base de datos
                        var turnos = await _context.Turnos
                            .ToListAsync();

                        // Realizar la comparación en memoria
                        bool turnoExiste = turnos.Any(e => LevenshteinDistance(e.TuDescripcion.ToLower(), descripcionMinusculas) <= 3);

                        if (!turnoExiste)
                        {
                            // Aplicar las actualizaciones necesarias al espacio existente
                            turnoActual.TuDescripcion = turno.TuDescripcion;

                            // Guardar los cambios
                            //_context.Update(turno);
                            await _context.SaveChangesAsync();

                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            var turnoSimilar = turnos.FirstOrDefault(e => LevenshteinDistance(e.TuDescripcion.ToLower(), descripcionMinusculas) <= 3);
                            ModelState.AddModelError("TuDescripcion", $"El turno '{turno.TuDescripcion}' ya existe o tiene una nombre similar: '{turnoSimilar?.TuDescripcion}'.");
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("TuDescripcion", $"Se produjo un error al editar el Turno: {ex.Message}");
                }
            }



            //try
            //    {
            //        _context.Update(turno);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!TurnoExists(turno.TuId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}


            return View(turno);
        }

        // GET: Turno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .FirstOrDefaultAsync(m => m.TuId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Turnos == null)
            {
                return Problem("Entity set 'FundacionContext.Turnos'  is null.");
            }
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Función para calcular la distancia de Levenshtein entre dos cadenas
        public int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            for (int i = 0; i <= n; i++)
                d[i, 0] = i;

            for (int j = 0; j <= m; j++)
                d[0, j] = j;

            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }

        //private bool TurnoExists(int id, string descripcion = null)
        //{
        //  return (_context.Turnos?.Any(e => e.TuId == id || e.TuDescripcion== descripcion)).GetValueOrDefault();
        //}
    }
}
