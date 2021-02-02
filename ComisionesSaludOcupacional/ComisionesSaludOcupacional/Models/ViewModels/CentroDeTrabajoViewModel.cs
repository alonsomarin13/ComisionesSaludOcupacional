using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class CentroDeTrabajoViewModel
    {
        [Required]
        [Display(Name = "Nombre del Centro de Trabajo")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Region")]
        public string idRegion { get; set; }
        public IEnumerable<SelectListItem> listaDeRegiones { get; set; }

    }

    public class CentroDeTrabajoEditViewModel
    {
        public int idCentroDeTrabajo { get; set; }
        [Required]
        [Display(Name = "Nombre del Centro de Trabajo")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Region")]
        public string idRegion { get; set; }
        public IEnumerable<SelectListItem> listaDeRegiones { get; set; }
    }

    public class CentroDeTrabajoTableViewModel
    {
        public string nombre { get; set; }
        public string nombreRegion { get; set; }
        public int idRegion { get; set; }
        public int idCentroDeTrabajo { get; set; }
    }
}