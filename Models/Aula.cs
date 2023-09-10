using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models;

public partial class Aula
{
    [Key]
    public int AuId { get; set; }
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ0-9\s°/:;,.\-_\+()]+$", ErrorMessage = "Ingrese un aula válido.")]
    [Display(Name = "Aulas")]
    [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
    public string AuDescripcion { get; set; } = null!;

    public virtual ICollection<Subespacio> Subespacios { get; set; } = new List<Subespacio>();
}
