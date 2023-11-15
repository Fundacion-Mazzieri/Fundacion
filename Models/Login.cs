using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models
{
    public class Login
    {
        public int idUsuario { get; set; }
        public string Correo { get; set; }
        [Display(Name = "Contraseña")]
        [PasswordPropertyText(true)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d']).+$", ErrorMessage = "La contraseña debe tener al menos una minúscula, una mayúscula, un número y un símbolo.")]
        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        [MinLength(8, ErrorMessage = "No se permiten menos de 8 caracteres.")]
        [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
        public string Clave{ get; set; }
        [Display(Name = "Contraseña")]
        [PasswordPropertyText(true)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d']).+$", ErrorMessage = "La contraseña debe tener al menos una minúscula, una mayúscula, un número y un símbolo.")]
        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        [MinLength(8, ErrorMessage = "No se permiten menos de 8 caracteres.")]
        [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
        public string ConfirmarClave { get; set; }
        
    }
}
