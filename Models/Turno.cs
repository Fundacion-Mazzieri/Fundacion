using System;
using System.Collections.Generic;

namespace Fundacion.Models;

public partial class Turno
{
    public int TuId { get; set; }

    public string TuDescripcion { get; set; } = null!;

    public virtual ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();
}
