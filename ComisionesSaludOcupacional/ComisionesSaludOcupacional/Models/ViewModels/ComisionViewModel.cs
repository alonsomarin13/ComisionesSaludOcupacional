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
    }

    public class EditComisionViewModel
    {
        public int idComision { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string contacto { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string contactoCorreo { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe ser un número de teléfono válido")]
        [Display(Name = "Teléfono")]
        public string contactoTelefono { get; set; }
        [Required]
        [Display(Name = "Nombre de Jefatura")]
        public string jefatura { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico de la Jefatura")]
        public string jefaturaCorreo { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe ser un número de teléfono válido")]
        [Display(Name = "Teléfono de la Jefatura")]
        public string jefaturaTelefono { get; set; }
    }
}