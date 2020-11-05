using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class FiltroFechasViewModel
    {
        public DateTime? fechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
    }
}