using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models;

public partial class Espacio
{
    [Key]
    public int EsId { get; set; }
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$", ErrorMessage = "Ingrese un espacio válido.")]
    [Display(Name = "Espacio")]
    [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
    [Required(ErrorMessage = "Debe ingresar un espacio")]
    public string EsDescripcion { get; set; } = null!;
    [Display(Name = "Turno")]
    [Required(ErrorMessage = "Debe elegir un turno")]
    public int TuId { get; set; }
    [Display(Name = "Usuario")]
    [Required(ErrorMessage = "Debe elegir un usuario")]
    public int UsId { get; set; }
    [Display(Name = "Estado")]
    public bool EsActivo { get; set; }
    [Display(Name = "Categoría")]
    [Required(ErrorMessage = "Debe elegir una categoría")]
    public int CaId { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual Categoria? Ca { get; set; }

    public virtual ICollection<Subespacio> Subespacios { get; set; } = new List<Subespacio>();

    public virtual Turno? Tu { get; set; }

    public virtual Usuario? Us { get; set; }
}
