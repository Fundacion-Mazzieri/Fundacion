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
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Fundacion.Controllers
{
    [Authorize(Roles="Super Admin,Admin,Usuario")]    
    public class AsistenciasController : Controller
    {
        private readonly FundacionContext _context;
        private readonly IConfiguration _configuration;
        //private readonly UsuarioDTO _user;

        public AsistenciasController(FundacionContext context, IConfiguration configuration/*, UsuarioDTO user*/)
        {
            _context = context;
            _configuration = configuration;
            //_user = user;
        }
        // GET: Asistencias
        public async Task<IActionResult> Index()
        {
            
            // En AsistenciasController
            var DNI = User.FindFirstValue("DNI");
            if (string.IsNullOrEmpty(DNI))
            {
                return RedirectToAction("Index", "Login");
            }
            //Convertir DNI string a dni long
            long.TryParse(DNI, out var dni);

            //Obtener el rol del usuario logueado
            var ROL = User.FindFirstValue("ROL");
            if (string.IsNullOrEmpty(ROL))
            {
                return RedirectToAction("Index", "Login");
            }            
            if (ROL == "Usuario")
            {
                // Filtrar las asistencias por el ID del usuario actual
                var asistencias = await _context.Asistencias
                    .Include(a => a.Es)
                    .Include(a => a.Es.Tu)
                    .Include(a => a.Es.Us)
                    .Include(a => a.Es.Ca)
                    .Where(a => a.Es.Us.UsDni == dni) // Filtrar por el ID del usuario actual
                    .ToListAsync();                
                return View(asistencias);
            }
            else
            {
                var asistencias = await _context.Asistencias
                    .Include(a => a.Es)
                    .Include(a => a.Es.Tu)
                    .Include(a => a.Es.Us)
                    .Include(a => a.Es.Ca)
                    .ToListAsync();                
                return View(asistencias);
            }            
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
            // En AsistenciasController
            var DNI = User.FindFirstValue("DNI");
            if (string.IsNullOrEmpty(DNI))
            {
                return RedirectToAction("Index", "Login");
            }
            //Convertir DNI string a dni long
            long.TryParse(DNI, out var dni);
            ViewData["AsIngreso"] = DateTime.Now;
            //Obtener el rol del usuario logueado
            var ROL = User.FindFirstValue("ROL");
            if (string.IsNullOrEmpty(ROL))
            {
                return RedirectToAction("Index", "Login");
            }
            
            if(ROL == "Usuario")
            {
                
                ViewData["EsId"] = new SelectList(
                    _context.Set<Espacio>()
                    .Where(espacio => espacio.Us.RoId == 2)
                    .Where(espacio => espacio.Us.UsDni == dni)
                    .Select(espacio => new
                    {
                        espacio.EsId,
                        EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                    }),
                    "EsId", "EsDescripcion");
            } 
            else
            {
                ViewData["EsId"] = new SelectList(
                    _context.Set<Espacio>()
                    .Where(espacio => espacio.Us.RoId == 2)
                    .Select(espacio => new
                    {
                        espacio.EsId,
                        EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                    }),
                    "EsId", "EsDescripcion");
            }
                      
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
            // Capturar DNI de usuario logueado
            var DNI = User.FindFirstValue("DNI");
            if (string.IsNullOrEmpty(DNI))
            {
                return RedirectToAction("Index", "Login");
            }
            // Convertir DNI string a dni long
            long.TryParse(DNI, out var dni);

            // Obtener ROL de usuario logueado
            var ROL = User.FindFirstValue("ROL");
            if (string.IsNullOrEmpty(ROL))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                asistencia.AsIngreso = DateTime.Now; // Establece el tiempo de inicio
                asistencia.AsEgreso = asistencia.AsIngreso;
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = asistencia.AsiId });
            }            
            if (ROL == "Usuario")
            {
                ViewData["EsId"] = new SelectList(
                _context.Set<Espacio>()
                .Where(espacio => espacio.Us.RoId == 2)
                .Where(espacio => espacio.Us.UsDni == dni)
                .Select(espacio => new
                {
                    espacio.EsId,
                    EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                }),
                "EsId", "EsDescripcion");
            }
            else
            {
                ViewData["EsId"] = new SelectList(
                _context.Set<Espacio>()
                .Where(espacio => espacio.Us.RoId == 2)
                .Select(espacio => new
                {
                    espacio.EsId,
                    EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                }),
                "EsId", "EsDescripcion");
            }
            

            
            return View(asistencia);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.CentroLatitud = _configuration.GetSection("Ubicacion")["Latitud"];
            ViewBag.CentroLongitud = _configuration.GetSection("Ubicacion")["Longitud"];
            // Capturar DNI de usuario logueado
            var DNI = User.FindFirstValue("DNI");
            if (string.IsNullOrEmpty(DNI))
            {
                return RedirectToAction("Index", "Login");
            }
            //Convertir DNI string a dni long
            long.TryParse(DNI, out var dni);

            // Obtener ROL de usuario logueado
            var ROL = User.FindFirstValue("ROL");
            if (string.IsNullOrEmpty(ROL))
            {
                return RedirectToAction("Index", "Login");
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.Es)
                .Include(a => a.Es.Tu)
                .Include(a => a.Es.Us)
                .Include(a => a.Es.Ca)
                .FirstOrDefaultAsync(m => m.AsiId == id);
            if (asistencia == null)
            {
                return NotFound();
            }
            if (ROL == "Usuario")
            {
                ViewData["EsId"] = new SelectList(
                _context.Set<Espacio>()
                .Where(espacio => espacio.Us.RoId == 2)
                .Where(espacio => espacio.Us.UsDni == dni)
                .Select(espacio => new
                {
                    espacio.EsId,
                    EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                }),
                "EsId", "EsDescripcion");
            }
            else
            {
                ViewData["EsId"] = new SelectList(
                _context.Set<Espacio>()
                .Where(espacio => espacio.Us.RoId == 2)
                .Select(espacio => new
                {
                    espacio.EsId,
                    EsDescripcion = $"{espacio.EsDescripcion} - {espacio.Tu.TuDescripcion} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                }),
                "EsId", "EsDescripcion");
            }
                     
            return View(asistencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("AsiId,EsId,AsIngreso,AsEgreso,AsPresent")] Asistencia asistencia)
        {
            // En AsistenciasController
            var DNI = User.FindFirstValue("DNI");
            if (string.IsNullOrEmpty(DNI))
            {
                return RedirectToAction("Index", "Login");
            }
            //Convertir DNI string a dni long
            long.TryParse(DNI, out var dni);

            // Obtener ROL de usuario logueado
            var ROL = User.FindFirstValue("ROL");
            if (string.IsNullOrEmpty(ROL))
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != asistencia.AsiId)
            {
                return NotFound();
            }            
            if (ModelState.IsValid)
            {                
                asistencia.AsEgreso = DateTime.Now; // Establece el tiempo de finalización
                if (asistencia.AsEgreso > asistencia.AsIngreso)
                {
                    // Calcula la diferencia de tiempo entre AsEgreso y AsIngreso   
                    TimeSpan tiempoTranscurrido = asistencia.AsEgreso - asistencia.AsIngreso;
                    // Convierte la diferencia en minutos
                    double minutos = tiempoTranscurrido.TotalMinutes;
                    // Redondea la diferencia al entero más cercano considerando que 50 minutos o más se cuentan como una hora
                    int horasTrabajadas = (int)Math.Round(minutos / 60.0, MidpointRounding.AwayFromZero);
                    // Agrego la cantidad de horas trabajadas a AsCantHsRedondeo
                    asistencia.AsCantHsRedondeo = horasTrabajadas;
                }
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

            if(ROL == "Usuario")
            {
                ViewData["EsId"] = new SelectList(
                _context.Set<Espacio>()
                .Where(espacio => espacio.Us.RoId == 2)
                .Where(espacio => espacio.Us.UsDni == dni)
                .Select(espacio => new
                {
                    espacio.EsId,
                    EsDescripcion = $"{espacio.EsDescripcion} - Turno: {espacio.Tu.TuDescripcion} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                }),
                "EsId", "EsDescripcion");
            }
            else
            {
                ViewData["EsId"] = new SelectList(
                _context.Set<Espacio>()
                .Where(espacio => espacio.Us.RoId == 2)
                .Select(espacio => new
                {
                    espacio.EsId,
                    EsDescripcion = $"{espacio.EsDescripcion} - Turno: {espacio.Tu.TuDescripcion} - {espacio.Us.UsApellido}, {espacio.Us.UsNombre} ({espacio.Us.UsDni})"
                }),
                "EsId", "EsDescripcion");
            }

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
