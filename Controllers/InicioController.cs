using Microsoft.AspNetCore.Mvc;

namespace Fundacion.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
