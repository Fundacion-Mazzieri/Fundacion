using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Fundacion.Models;

public partial class Usuario
{
    public int UsId { get; set; }
    [Display(Name = "Dni")]
    [Range(1000000,99999999, ErrorMessage = "Debe ingresar los 7-8 dígitos del DNI")]
    [Required(ErrorMessage = "Debe ingresar un numero de DNI válido (8 dígitos)")]        
    public long UsDni { get; set; }
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$",ErrorMessage ="Ingrese un apellido válido")]
    [Display(Name = "Apellido")]
    [StringLength(50)]
    [Required(ErrorMessage = "Debe ingresar un Apellido")]
    public string UsApellido { get; set; } = null!;
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$", ErrorMessage = "Ingrese un nombre válido")]
    [Display(Name = "Nombre")]
    [StringLength(50)]
    [Required(ErrorMessage = "Debe ingresar un Nombre")]
    public string UsNombre { get; set; } = null!;
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ0-9\s°]*$", ErrorMessage = "Ingrese una dirección válida")]
    [Display(Name = "Direccion")]
    [StringLength(50)]
    public string? UsDireccion { get; set; }
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$", ErrorMessage ="Ingrese una localidad válida")]
    [Display(Name = "Localidad")]
    [StringLength(50)]
    public string? UsLocalidad { get; set; }
    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$", ErrorMessage = "Ingrese una provincia válida")]
    [Display(Name = "Provincia")]
    [StringLength(50)]
    public string? UsProvincia { get; set; }
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Por favor verifique su dirección de email")]
    [StringLength(50)]
    public string? UsEmail { get; set; }
    [Display(Name = "Telefono")]
    [Range(1000000000,9999999999,ErrorMessage = "Ingrese un número telefónico sin espacios ni guiones y sin 0 ni 15")]
    public long? UsTelefono { get; set; }
    [Display(Name = "Contraseña")]
    [PasswordPropertyText(true)]
    [Required(ErrorMessage = "Debe ingresar una contraseña")]
    [StringLength(50)]
    public string UsContrasena { get; set; } = null!;
    [Display(Name = "Rol")]
    [Required(ErrorMessage = "Debe elegir un rol")]
    public int RoId { get; set; }
    [Display(Name = "Activo")]
    public bool UsActivo { get; set; }

    public virtual ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();

    public virtual Role? Ro { get; set; } = null!;
}