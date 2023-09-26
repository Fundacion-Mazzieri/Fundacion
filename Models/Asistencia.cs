using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models;

public partial class Asistencia
{
    [Key]
    public int AsiId { get; set; }
    [Required(ErrorMessage = "Debe elegir un espacio")]
    [Display(Name = "Espacio")]
    public int EsId { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM - HH:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Debe cargarse un horario de ingreso")]
    [Display(Name = "Ingreso")]    
    public DateTime AsIngreso { get; set; }
    [DisplayFormat(DataFormatString = "{0: HH:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Debe cargarse un horario de egreso")]
    [Display(Name = "Egreso")]
    public DateTime AsEgreso { get; set; }
    [Display(Name = "Presente")]
    public bool AsPresent { get; set; }
    [Display(Name = "Horas trabajadas")]
    public double AsCantHsRedondeo { get; set; }
    [Display(Name = "Espacio")]
    public virtual Espacio? Es { get; set; }
}
