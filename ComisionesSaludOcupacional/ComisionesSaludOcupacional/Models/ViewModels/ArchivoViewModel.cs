using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class ArchivoViewModel
    {
        public string nombre { get; set; }
        public string filePath { get; set; }

        public int idNoticia { get; set; }
        public string tipo { get; set; }
    }


}