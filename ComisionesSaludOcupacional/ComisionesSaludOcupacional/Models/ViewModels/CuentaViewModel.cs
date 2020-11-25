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
        public string contrasena { get; set; }
        [Compare("contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirme su contraseña")]
        public string confirmarContrasena { get; set; }
    }
}