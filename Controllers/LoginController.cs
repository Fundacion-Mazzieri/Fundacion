using Fundacion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.Security.Cryptography;
using System.Text;

namespace Fundacion.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ingresar(Login oLogin)
        {
            if (oLogin.Clave == "123" && oLogin.Correo == "admin")
            {
                return RedirectToAction("Index", "Inicio");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }
            
        }

        public ActionResult Registrar(Login oLogin) 
        {
            bool registrado;
            string mensaje;
            if (oLogin.Clave == oLogin.ConfirmarClave)
            {
                oLogin.Clave= ConvertirSha256(oLogin.Clave);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            return View();
        }

        public static string ConvertirSha256(string texto) //codigo para encriptar contraseñas usando SHA256
        {
            StringBuilder sb = new StringBuilder();
            using(SHA256 hash = SHA256.Create()) { 
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach(byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
