using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models
{
    public class Login
    {
        public int idUsuario { get; set; }
        public string Correo { get; set; }
        [Required]
        public string Clave{ get; set; }

        public string ConfirmarClave { get; set; }
        
    }
}
