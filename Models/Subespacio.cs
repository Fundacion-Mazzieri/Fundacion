using System;
using System.Collections.Generic;

namespace Fundacion.Models;

public partial class Subespacio
{
    public int SeId { get; set; }

    public int EsId { get; set; }

    public int AuId { get; set; }

    public string SeDia { get; set; } = null!;

    public TimeSpan? SeHora { get; set; }

    public string? SeCantHs { get; set; }

    public virtual Aula Au { get; set; } = null!;

    public virtual Espacio Es { get; set; } = null!;
}
