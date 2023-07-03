using System;
using System.Collections.Generic;

namespace Fundacion.Models;

public partial class Role
{
    public int RoId { get; set; }

    public string RoDenominacion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
