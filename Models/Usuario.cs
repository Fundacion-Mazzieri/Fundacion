using System;
using System.Collections.Generic;

namespace Fundacion.Models;

public partial class Usuario
{
    public int UsId { get; set; }


    public long UsDni { get; set; }

    public string UsApellido { get; set; } = null!;

    public string UsNombre { get; set; } = null!;

    public string? UsDireccion { get; set; }

    public string? UsLocalidad { get; set; }

    public string? UsProvincia { get; set; }

    public string? UsEmail { get; set; }

    public long? UsTelefono { get; set; }

    public string UsContrasena { get; set; } = null!;

    public int RoId { get; set; }

    public bool? UsActivo { get; set; }

    public virtual ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();

    public virtual Role Ro { get; set; } = null!;
}
