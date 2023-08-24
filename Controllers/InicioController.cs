using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fundacion.Controllers
{
    public class InicioController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
