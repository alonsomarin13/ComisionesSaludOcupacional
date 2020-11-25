using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class ComisionViewModel
    {
        public int idComision { get; set; }
        [Required]
        [Display(Name ="Region")]
        public string nombreRegion { get; set; }
        public IEnumerable<SelectListItem> listaDeRegiones { get; set; }

        [Required]
        [Display(Name = "Nombre del Centro de Trabajo")]
        public string nombreCentroDeTrabajo { get; set; }
        public IEnumerable<SelectListItem> listaDeCentrosDeTrabajo { get; set; }
        [Required]
        public int idCentroDeTrabajo { get; set; }

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
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El campo debe ser un número de registro válido")]
        [Display(Name = "Número de Registro")]
        public string numeroRegistro { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Registro")]
        public DateTime? fechaDeRegistro { get; set; }
        
    }
}