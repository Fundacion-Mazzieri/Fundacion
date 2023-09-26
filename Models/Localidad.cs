using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models
{
    public class Localidad
    {
        [Key]
        public int LcId { get; set; }
        public int PvId { get; set; }
        public string? LcDescripcion { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public virtual Provincia? Pv { get; set; }
    }
}
