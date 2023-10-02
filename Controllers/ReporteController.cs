using Microsoft.AspNetCore.Mvc;
using Fundacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fundacion.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Net;
using ClosedXML.Excel;
using System.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Rotativa;
using Rotativa.AspNetCore;

namespace Fundacion.Controllers
{
    public class ReporteController : Controller
    {
        private readonly FundacionContext _context;
        private readonly IConfiguration _configuration;
        //private readonly UsuarioDTO _user;

        public ReporteController(FundacionContext context, IConfiguration configuration/*, UsuarioDTO user*/)
        {
            _context = context;
            _configuration = configuration;
            //_user = user;
        }
        // GET: Home
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias.ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "CaId", "CaDescripcion");
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

        [HttpPost]
        public async Task<IActionResult> ExportToExcel(int? mes, int? year, int? idcategoria)
        {
            //var categorias = await _context.Categorias.ToListAsync();
            //ViewBag.Categorias = new SelectList(categorias, "CaId", "CaDescripcion");
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

            IQueryable<Asistencia> asistenciasQuery = _context.Asistencias
                .Include(a => a.Es)
                .Include(a => a.Es.Tu)
                .Include(a => a.Es.Us)
                .Include(a => a.Es.Ca);

            if (ROL == "Usuario")
            {
                // Filtrar las asistencias por el ID del usuario actual
                asistenciasQuery = asistenciasQuery.Where(a => a.Es.Us.UsDni == dni);
            }

            List<Asistencia> asistencias = null;

            if (mes.HasValue)
            {
                if (mes.Value != 0)
                {
                    asistenciasQuery = asistenciasQuery.Where(a => a.AsIngreso.Month == mes);
                }
                if (year.HasValue)
                {
                    if (year.Value != 0)
                    {
                        asistenciasQuery = asistenciasQuery.Where(a => a.AsIngreso.Year == year);
                    }
                    if (idcategoria.HasValue)
                    {
                        if (idcategoria.Value != 0)
                        {
                            asistenciasQuery = asistenciasQuery.Where(a => a.Es.Ca.CaId == idcategoria);
                        }
                    }
                }
            }

            asistencias = await asistenciasQuery.ToListAsync();
            
            string Espacio = "";
            string Turno = "";
            string Usuario = "";
            string Categoria = "";
            string Ingreso = "";
            string Egreso = "";
            double CantidadHoras = 0;
            double ValorHora = 0;
            double Subtotal = 0;


            DataTable dt = new DataTable("Asistencias");
            dt.Columns.AddRange(new DataColumn[9]
            {
             new DataColumn("Espacio"),
             new DataColumn("Turno"),
             new DataColumn("Usuario"),
             new DataColumn("Categoría"),
             new DataColumn("Ingreso"),
             new DataColumn("Egreso"),
             new DataColumn("Cantidad Horas"),
             new DataColumn("Valor Hora"),
             new DataColumn("Subtotal")
            });

            var dateForXcellSheet = DateTime.Now;
            //var worksheet = wb.Worksheets.Add("Hoja1");
            
            foreach (var asistencia in asistencias)
            {
                Espacio = asistencia.Es.EsDescripcion;
                Turno = asistencia.Es.Tu.TuDescripcion;
                Usuario = asistencia.Es.Us.UsDni.ToString() + " " + asistencia.Es.Us.UsApellido.ToString() + ", " + asistencia.Es.Us.UsNombre.ToString();
                Categoria = asistencia.Es.Ca.CaDescripcion;
                Ingreso = asistencia.AsIngreso.ToString();
                Egreso = asistencia.AsEgreso.ToString();
                CantidadHoras = asistencia.AsCantHsRedondeo;
                ValorHora = asistencia.Es.Ca.CaValorHora;
                Subtotal = asistencia.Es.Ca.CaValorHora * asistencia.AsCantHsRedondeo;

                dt.Rows.Add(Espacio, Turno, Usuario, Categoria, Ingreso, Egreso, CantidadHoras, ValorHora, Subtotal);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Asistencias-" + dateForXcellSheet + ".xlsx");
                }
            }
        }

        // GET: Customers
        public async Task<IActionResult> ImprimirPDF(int? mes, int? year, int? idcategoria)
        {
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

            IQueryable<Asistencia> asistenciasQuery = _context.Asistencias
                .Include(a => a.Es)
                .Include(a => a.Es.Tu)
                .Include(a => a.Es.Us)
                .Include(a => a.Es.Ca);

            if (ROL == "Usuario")
            {
                // Filtrar las asistencias por el ID del usuario actual
                asistenciasQuery = asistenciasQuery.Where(a => a.Es.Us.UsDni == dni);
            }

            List<Asistencia> asistencias = null;

            if (mes.HasValue)
            {
                if (mes.Value != 0)
                {
                    asistenciasQuery = asistenciasQuery.Where(a => a.AsIngreso.Month == mes);
                }
                if (year.HasValue)
                {
                    if (year.Value != 0)
                    {
                        asistenciasQuery = asistenciasQuery.Where(a => a.AsIngreso.Year == year);
                    }
                    if (idcategoria.HasValue)
                        {
                        if (idcategoria.Value != 0)
                        {
                            asistenciasQuery = asistenciasQuery.Where(a => a.Es.Ca.CaId == idcategoria);
                        }
                    }
                }
            }

            asistencias = await asistenciasQuery.ToListAsync();

            var dateForXcellSheet = DateTime.Now;

            // Define la URL de la Cabecera 
            string _headerUrl = Url.Action("HeaderPDF", "Reporte", null, "https");
            // Define la URL del Pie de página
            string _footerUrl = Url.Action("FooterPDF", "Reporte", null, "https");


            return new ViewAsPdf("ImprimirPDF", asistencias)
            {
                // Establece la Cabecera y el Pie de página
                CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 13 " +
                             "--footer-html " + _footerUrl + " --footer-spacing 0",
                PageMargins = { Left = 10, Right = 10, Top = 40, Bottom = 10 }, // Define los márgenes
                PageSize = Rotativa.AspNetCore.Options.Size.A4, // Define el tamaño de página
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait, // Opcional: Orientación de página (Portrait o Landscape)
                FileName = "Asistencias-" + dateForXcellSheet + ".pdf" // Opcional: Nombre del archivo de salida
            };
        }
        public IActionResult HeaderPDF()
        {
            return View("HeaderPDF");
        }
        public IActionResult FooterPDF()
        {
            return View("FooterPDF");
        }
        public async Task<IActionResult> CargarCategorias()
        {
            var categorias = await _context.Categorias.ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "CaId", "CaDescripcion");
            return View(categorias);
        }
    }
}