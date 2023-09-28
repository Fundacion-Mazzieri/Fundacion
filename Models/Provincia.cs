using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models
{
    public class Provincia
    {
        [Key]
        public int PvId { get; set; }
        public string? PvDescripcion { get; set; }

        public virtual ICollection<Localidad> Localidades { get; set; } = new List<Localidad>();
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
