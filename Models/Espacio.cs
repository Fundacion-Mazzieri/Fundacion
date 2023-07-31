using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models;

public partial class Espacio
{
    [Key]
    public int EsId { get; set; }

    public string EsDescripcion { get; set; } = null!;

    public int AuId { get; set; }

    public string EsDia { get; set; } = null!;

    public string EsHora { get; set; } = null!;

    public double EsCantHs { get; set; }

    public int TuId { get; set; }

    public int UsId { get; set; }

    //public string EsActivo { get; set; }

    public bool EsActivo { get; set; }

    public int CaId { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual Aula? Au { get; set; } = null!;

    public virtual Categoria? Ca { get; set; }

    public virtual Turno? Tu { get; set; } = null!;

    public virtual Usuario? Us { get; set; } = null!;
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models;

public partial class Espacio
{
    [Key]
    public int EsId { get; set; }

    public string EsDescripcion { get; set; } = null!;

    public int AuId { get; set; }

    public string EsDia { get; set; } = null!;

    public string EsHora { get; set; } = null!;

    public double EsCantHs { get; set; }

    public int TuId { get; set; }

    public int UsId { get; set; }

    //public string EsActivo { get; set; }
    //string por bool (bin en Base de datos)

    public bool EsActivo { get; set; }

    public int CaId { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual Aula? Au { get; set; } = null!;

    public virtual Categoria? Ca { get; set; }

    public virtual Turno? Tu { get; set; } = null!;

    public virtual Usuario? Us { get; set; } = null!;
}
