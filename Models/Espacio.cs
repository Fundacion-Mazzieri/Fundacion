using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace Fundacion.Models;

public partial class Espacio
{
    [Key]
    public int EsId { get; set; }


    //[Display(Name = "Descripcion")]
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ0-9\s]+[A-Za-záéíóúÁÉÍÓÚüÜñÑ0-9\s]*$", ErrorMessage = "Caracter especial no aceptado. Complete con letras y/o números.")]
    [StringLength(50)]
    [Required(ErrorMessage = "Debe ingresar una descripcion del espacio.")]
    public string EsDescripcion { get; set; } = null!;


    public int AuId { get; set; }


    //[Display(Name = "Ingrese una fecha")]
    [DataType (DataType.Date)]
    [DisplayFormat (DataFormatString ="{0:YYYY-MM-dd}")]
    [Required(ErrorMessage = "Debe ingresar una fecha correcta.")]
    public string EsDia { get; set; } = null!;


    //[Display(Name = "Ingrese una hora")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{00:00}")]
    [Required(ErrorMessage = "Debes ingresar una hora correcta.")]
    public string EsHora { get; set; } = null!;


    //[Display(Name = "Duracion del espacio en minutos")]
    [RegularExpression(@"^[1-9\s]+[0\s]*$", ErrorMessage = "Debes ingresar solo números. Valor 0 no aceptado al inicio.")]
    [Range(maximum: 59, minimum: 1, ErrorMessage ="La duracion del espacio tiene que ser entre 1 y 59 minutos.")]
    [Required(ErrorMessage = "Debes ingresar los minutos de duracion del espacio.")]
    public double EsCantHs { get; set; }


    public int TuId { get; set; }


    public int UsId { get; set; }


    //public string EsActivo { get; set; }
    //string por bool (bin en Base de datos)


    //[Display(Name = "Activo")]
    public bool EsActivo { get; set; }


    public int CaId { get; set; }


    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();
    public virtual Aula? Au { get; set; } = null!;
    public virtual Categoria? Ca { get; set; }
    public virtual Turno? Tu { get; set; } = null!;
    public virtual Usuario? Us { get; set; } = null!;
}
