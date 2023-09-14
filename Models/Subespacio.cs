using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models;

public partial class Subespacio
{
    public int SeId { get; set; }
    [Display(Name = "Espacio")]
    public int EsId { get; set; }
    [Required(ErrorMessage = "Debe elegir un aula")]
    [Display(Name = "Aula")]
    public int AuId { get; set; }    
    [Display(Name = "Días")]
    [MaxLength(200, ErrorMessage = "No se permiten más de 200 caracteres.")]
    public string SeDia { get; set; } = null!;
    [Display(Name = "Hora")]    
    public TimeSpan? SeHora { get; set; }
    [Display(Name = "Cantidad de Horas")]
    public double SeCantHs { get; set; }

    public virtual Aula? Au { get; set; }

    public virtual Espacio? Es { get; set; }
}
