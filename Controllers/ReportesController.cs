using Microsoft.AspNetCore.Mvc;
using Fundacion.Models;


namespace Fundacion.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
