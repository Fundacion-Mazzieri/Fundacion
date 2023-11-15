using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundacion.Models.ViewModel
{
    public class RecoveryPasswordViewModel
    {
     
        public string? token { get; set; }

        [Required]
        public string? UsContrasena { get; set; }

        //[Compare("Password")]
        [Required]
        public string? UsContrasena2 { get; set; }

    }
}
