using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class ComisionViewModel
    {
        [Required]
        [Display(Name ="Nombre de la Comisión")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string contacto { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string correo { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }





    }
}