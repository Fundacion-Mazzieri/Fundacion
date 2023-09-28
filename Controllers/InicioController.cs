using Fundacion.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fundacion.Controllers
{
    public class InicioController : Controller
    {
        [Authorize(Roles = "Super Admin, Admin,Usuario")]
        public IActionResult Index()
        {
            var dniClaim = User.FindFirstValue("DNI");
            var nombreClaim = User.FindFirstValue("Nombre");
            var apellidoClaim = User.FindFirstValue("Apellido");
            var rolClaim = User.FindFirstValue(ClaimTypes.Role);
            var bag= new UsuarioDTO { UsDni= Int32.Parse(dniClaim),rol=rolClaim,UsNombre=nombreClaim,UsApellido=apellidoClaim};
            return View(bag);
        }
    }
}
