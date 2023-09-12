using System;
using System.Collections.Generic;

namespace Fundacion.Models;

public partial class Asistencia
{
    public int AsiId { get; set; }

    public int EsId { get; set; }

    public DateTime AsIngreso { get; set; }

    public DateTime? AsEgreso { get; set; }

    public bool AsPresent { get; set; }

    public double AsCantHsRedondeo { get; set; }

    public virtual Espacio Es { get; set; } = null!;
}
