using ComisionesSaludOcupacional.Models.ET01;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class NoticiaViewModel
    {
        public int idNoticia { get; set; }
        [Required]
        [Display(Name = "Título")]
        public string titulo { get; set; }

        [Display(Name = "Texto")]
        public string texto { get; set; }

        [MaxFileSize(10485760)]
        public HttpPostedFileBase archivo { get; set; }

    }

    public class NoticiaTableViewModel
    {
        public int idNoticia { get; set; }
        public string titulo { get; set; }
        public DateTime fecha { get; set; }
    }

    public class VerNoticiaViewModel
    {
        public int idNoticia { get; set; }
        public string titulo { get; set; }
        public string texto { get; set; }
        public Archivo archivo { get; set; }
    }

    public class EditNoticiaViewModel
    {
        public int idNoticia { get; set; }
        [Required]
        [Display(Name = "Título")]
        public string titulo { get; set; }

        [Display(Name = "Texto")]
        public string texto { get; set; }

        [MaxFileSize(10485760)]
        public HttpPostedFileBase archivoNuevo { get; set; }
        public int? idArchivoActual { get; set; }
    }
}