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
        public async Task<IActionResult> ExportToExcel(int? mes)
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

            if (mes.HasValue)
            {
                asistenciasQuery = asistenciasQuery.Where(a => a.AsIngreso.Month == mes);
            }

            var asistencias = await asistenciasQuery.ToListAsync();

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



        // Método para obtener datos desde una fuente (por ejemplo, una base de datos)
        private List<Asistencia> GetYourDataFromDatabase()
        {
            // Implementa la lógica para obtener los datos de tu fuente de datos.
            // Reemplaza YourDataModel con el tipo de modelo real.
            var data = new List<Asistencia>
        {
            new Asistencia { /* Propiedades de datos */ },
            // Agregar más elementos según tus necesidades
        };

            return data;
        }
    }
}