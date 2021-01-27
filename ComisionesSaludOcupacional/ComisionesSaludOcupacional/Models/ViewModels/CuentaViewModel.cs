using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class CuentaViewModel
    {
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "La contraseña debe ser de mínimo 5 caracteres y máximo 40")]
        public string contrasena { get; set; }
        [Compare("contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirme su contraseña")]
        public string confirmarContrasena { get; set; }
    }

    public class CuentaModificadaViewModel
    {
        [Display(Name = "Nombre de Usuario")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string contrasena { get; set; }
        [Required]
        [Display(Name = "Contraseña nueva")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "La contraseña debe ser de mínimo 5 caracteres y máximo 40")]
        public string contrasenaNueva { get; set; }
        [Compare("contrasenaNueva", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirme su contraseña nueva")]
        public string confirmarContrasena { get; set; }
    }

    public class CuentaPopupViewModel
    {
        public string nombre { get; set; }
        public string password { get; set; }
        [Required]
        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string useremail { get; set; }
    }
}