using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundacion.Models;

public partial class Asistencia
{
    [Key]
    public int AsiId { get; set; }
    public int EsId { get; set; }
    [DataType(DataType.DateTime)]    
    [Required(ErrorMessage = "Debe ingresar una fecha y hora de ingreso.")]
    public DateTime? AsIngreso { get; set; }
    [DataType(DataType.DateTime)]
    [Required(ErrorMessage = "Debe ingresar una fecha y hora de egreso.")]
    public DateTime? AsEgreso { get; set; }

    public bool AsPresent { get; set; }

    public virtual Espacio? Es { get; set; } = null!;
}
