//using Irony.Parsing;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Configuration;
//using Fundacion.Models.ViewModel;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Fundacion.Models;

//public partial class Usuario
//{
//    [Key]
//    public int UsId { get; set; }
//    [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Sólo se permiten números de DNI válidos.")]
//    [Display(Name = "DNI")]    
//    [Range(1000000,99999999, ErrorMessage = "Debe ingresar los 7-8 dígitos del DNI.")]
//    [Required(ErrorMessage = "Debe ingresar un número de DNI válido (8 dígitos).")]        
//    public long UsDni { get; set; }
//    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$",ErrorMessage ="Ingrese un apellido válido.")]
//    [Display(Name = "Apellido")]
//    [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
//    [Required(ErrorMessage = "Debe ingresar un Apellido")]
//    public string UsApellido { get; set; } = null!;
//    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]*$", ErrorMessage = "Ingrese un nombre válido.")]
//    [Display(Name = "Nombres")]
//    [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
//    [Required(ErrorMessage = "Debe ingresar un Nombre.")]
//    public string UsNombre { get; set; } = null!;
//    [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ0-9\s°/:;,.\-_\+()]+$", ErrorMessage = "Ingrese una dirección válida.")]
//    [Display(Name = "Dirección")]
//    [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
//    public string? UsDireccion { get; set; }    
//    [Display(Name = "Localidad")]    
//    public int UsLocalidad { get; set; }    
//    [Display(Name = "Provincia")]    
//    public int UsProvincia { get; set; }
//    [Display(Name = "Email")]
//    [EmailAddress(ErrorMessage = "Por favor verifique su dirección de email.")]
//    [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
//    public string? UsEmail { get; set; }
//    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Ingrese un número telefónico válido de 10 dígitos.")]
//    [Display(Name = "Teléfono")]
    
//    [Range(1000000000, 9999999999, ErrorMessage = "Ingrese un número telefónico válido sin espacios ni guiones y sin 0 ni 15.")]
//    public long? UsTelefono { get; set; }    
//    [Display(Name = "Contraseña")]
//    [PasswordPropertyText(true)]
//    //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d']).+$", ErrorMessage = "La contraseña debe tener al menos una minúscula, una mayúscula, un número y un símbolo.")]
//    [Required(ErrorMessage = "Debe ingresar una contraseña.")]
//    [MinLength(8, ErrorMessage = "No se permiten menos de 8 caracteres.")]
//    [MaxLength(50, ErrorMessage = "No se permiten más de 50 caracteres.")]
//    public string UsContrasena { get; set; } = null!;
//    [Display(Name = "Rol")]
//    [Required(ErrorMessage = "Debe elegir un rol.")]
//    public int RoId { get; set; }
//    [Display(Name = "Activo")]
//    public bool UsActivo { get; set; }


//    public string? token_recovery { get; set; } = "tokenbloqueado";
//    public DateTime date_created { get; set; } = DateTime.Now;

//    public virtual ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();

//    public virtual Role? Ro { get; set; } = null!;
//    public virtual Localidad? Lc { get; set; } = null!;
//    public virtual Provincia? Pv { get; set; } = null!;

//}