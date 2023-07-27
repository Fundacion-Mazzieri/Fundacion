using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fundacion.Models;

public partial class Usuario
{
    public int UsId { get; set; }
    [Display(Name = "Dni")]
    [Range(1000000,99999999, ErrorMessage = "Debe ingresar los 7-8 dígitos del DNI")]
    [Required(ErrorMessage = "Debe ingresar un numero de DNI válido (8 dígitos)")]        
    public long UsDni { get; set; }
    [Display(Name = "Apellido")]
    [Required(ErrorMessage = "Debe ingresar un Apellido")]
    public string UsApellido { get; set; } = null!;
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "Debe ingresar un Nombre")]
    public string UsNombre { get; set; } = null!;
    [Display(Name = "Direccion")]
    public string? UsDireccion { get; set; }
    [Display(Name = "Localidad")]
    public string? UsLocalidad { get; set; }
    [Display(Name = "Provincia")]
    public string? UsProvincia { get; set; }
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Por favor verifique su dirección de email")]
    public string? UsEmail { get; set; }
    [Display(Name = "Telefono")]
    [Range(1000000000,9999999999,ErrorMessage = "Ingrese un número telefónico sin espacios ni guiones y sin 0 ni 15")]
    public long? UsTelefono { get; set; }
    [Display(Name = "Contraseña")]
    [PasswordPropertyText(true)]
    [Required(ErrorMessage = "Debe ingresar una contraseña")]
    public string UsContrasena { get; set; } = null!;
    [Display(Name = "Rol")]
    [Required(ErrorMessage = "Debe elegir un rol")]
    public int RoId { get; set; }
    [Display(Name = "Activo")]
    public bool UsActivo { get; set; }

    public virtual ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();

    public virtual Role? Ro { get; set; } = null!;
}