using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundacion.Models.ViewModel
{
    public class RecoveryViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string? UsEmail { get; set; }

    }
}
