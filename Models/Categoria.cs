using System;
using System.Collections.Generic;

namespace Fundacion.Models;

public partial class Categoria
{
    public int CaId { get; set; }

    public string CaDescripcion { get; set; } = null!;

    public double CaValorHora { get; set; }

    public virtual ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();
}
