using Fundacion.Models;
using Irony.Parsing;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Fundacion.Models.ViewModel;
using Fundacion.Data;
using Microsoft.EntityFrameworkCore;
using Fundacion.Data.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using NuGet.Common;
using Azure.Messaging;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using ADO.Net.Client.Annotations;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.SignalR;

namespace Fundacion.Controllers
{
    public class AccessControler : Controller
    {
        private readonly FundacionContext _context;
        public AccessControler(FundacionContext context)
        {
            _context = context;
        }

        string urlDomain = "http://localhost:5000/";


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StartRecovery()
        {
            RecoveryViewModel model = new RecoveryViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StartRecovery(RecoveryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var token = GetMD5(Guid.NewGuid().ToString());



                var usuario = await _context.Usuarios
                    .Where(e => e.UsEmail == model.UsEmail)
                    .ToListAsync();

                Usuario usuario1 = new Usuario();

                var oUser = usuario[0];

                if (oUser != null)
                {
                    oUser.token_recovery = token;
                    _context.Entry(oUser).State = EntityState.Modified;
                    _context.SaveChanges();

                    //enviar Email

                    Sendemail(oUser.UsEmail, token);
                }

                return View();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Recovery(string token)
        {
            RecoveryPasswordViewModel model = new RecoveryPasswordViewModel();
            model.token = token;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var usuario = await _context.Usuarios
                .Where(e => e.token_recovery == model.token)
                .FirstAsync();

            }

            catch (Exception ex)
            {
                return View("~/Views/Login/Index-ErrorToken.cshtml");
                throw new Exception(ex.Message);

            }

            ViewBag.Message = "Error de token";
            return View();


        }


        [HttpPost]
        public async Task<IActionResult> Recovery(RecoveryPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                var usuario = await _context.Usuarios
                    .Where(d => d.token_recovery == model.token)
                    .FirstAsync();
                    

                var oUser = usuario;

                if (oUser != null)
                {
                    oUser.UsContrasena = model.UsContrasena;
                    oUser.token_recovery = null;

                    // Encriptar la contraseña antes de guardarla
                    usuario.UsContrasena = Encrypt.GetMD5(usuario.UsContrasena);

                    //Cambiar el token

                    oUser.token_recovery = "tokenbloqueado";
                    //---------------------------------------------------
                    _context.Entry(oUser).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ViewBag.Message = "Contraseña modificada con éxito";
            return View("~/Views/Login/Index.cshtml");
        }

        #region Helpers
        private string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[]? stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void Sendemail(string EmailDestino, string token)
        {
            string EmailOrigen = "soportetecnicoit124@gmail.com";
            //string Contrasena = "Tercero2023";
            string Contrasena = "dczvcrtfsixgopyr";
            string url = urlDomain + "AccessControler/Recovery?token=" + token;

            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Fundacion Carlos A. Mazzieri - Recuperacion de Contraseña.",
                "<p>Estimado Usuario: Estás recibiendo este correo porque has solicitado restablecer tu contraseña para acceder al sistema de asistencia.</p><br>" +
                "<p>Puede hacer Click en el siguiente enlace para reestablecer su contraseña:</p><br>" +
                "<a href='" + url + "'> Restablecer Contraseña</a>" +
                "<p>En caso que no haya solicitado el restablecimiento de su contraseña, desestime este email y comuniquese con el administrador del sistema.</p><br>" +
                "<p>Cualquier consulta estamos a su disposicion.</p><br>" +
                "<p>Saludos.</p><br>" +
                "<p>Servicio Técnico - Fundacion Carlos A. Mazzieri -</p><br>");

            oMailMessage.IsBodyHtml = true;

            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contrasena);
            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }


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
                    new Claim("Apellido", usuario.UsApellido.ToString()),
                    new Claim("ROL", usuarioDTO.rol),
                    new Claim(ClaimTypes.Role, usuarioDTO.rol)

                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                usuarioDTO.Autenticado = true;
                return RedirectToAction("Index", "Inicio");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }
            //return View("~/Views/Inicio/Index.cshtml");
        }
        #endregion
    }
}
