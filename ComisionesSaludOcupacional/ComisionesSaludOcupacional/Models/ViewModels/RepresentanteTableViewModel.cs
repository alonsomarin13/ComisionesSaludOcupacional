using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class RepresentanteTableViewModel
    {
        public int idRepresentante { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string tipo { get; set; }
        public DateTime ingreso { get; set; }
        public DateTime vencimiento { get; set; }
        public int? idComision { get; set; }
    }
}