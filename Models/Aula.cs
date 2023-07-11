using System;
using System.Collections.Generic;

namespace Fundacion.Models;

public partial class Aula
{
    public int AuId { get; set; }

    public string AuDescripcion { get; set; } = null!;

    public virtual ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();

}






