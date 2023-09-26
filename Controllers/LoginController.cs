using Fundacion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;
using Fundacion.Data.DTO;
using Fundacion.Data;
using Microsoft.EntityFrameworkCore;

namespace Fundacion.Controllers
{
    public class LoginController : Controller
    {
        private readonly FundacionContext _context;
        public LoginController(FundacionContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ingresar(UsuarioDTO usuarioDTO)
        {
            var clave = Encrypt.GetMD5(usuarioDTO.UsContrasena.ToString());
            var usuario = _context.Usuarios.Where(item => item.UsDni == usuarioDTO.UsDni && item.UsContrasena == clave).FirstOrDefault();
            var roles = _context.Usuarios.Include(u => u.Ro).Where(item => item.UsDni == usuarioDTO.UsDni)
               .FirstOrDefault();
            
            Console.WriteLine(usuarioDTO.rol);
            if (usuario != null)
            {
                usuarioDTO.rol = roles.Ro.RoDenominacion;
                var claims = new List<Claim>
                {
                    new Claim("DNI", usuarioDTO.UsDni.ToString()),
                    new Claim("Nombre", usuario.UsNombre.ToString()),
                    new Claim("ROL", usuarioDTO.rol),
                    new Claim(ClaimTypes.Role, usuarioDTO.rol)
                    //new Claim("Clave",clave)

                };
                //foreach (var rol in usuarioDTO.rol)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, rol.ToString()));
                //}
                //claims.Add(new Claim(ClaimTypes.Role, usuarioDTO.rol));
                var claimsIdentity= new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
                usuarioDTO.Autenticado = true;
                return RedirectToAction("Index", "Inicio");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }
            
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

    }
}
