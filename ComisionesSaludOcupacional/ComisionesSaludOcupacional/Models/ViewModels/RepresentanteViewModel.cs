﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class RepresentanteViewModel
    {
        public int idRepresentante { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string correo { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El teléfono del representante debe ser un número de télefono válido")]
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        [Required]
        public DateTime ingreso { get; set; }

        public string tipo { get; set; }

        public string idComision { get; set; }

    }

    public class EditRepresentanteViewModel
    {
        public int idRepresentante { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string correo { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El teléfono del representante debe ser un número de télefono válido")]
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
    }
}