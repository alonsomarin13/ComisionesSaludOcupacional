using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class ComisionTableViewModel
    {
        public int idComision { get; set; }
        public string nombre { get; set; }
        public int idRegion { get; set; }
        public string nombreRegion { get; set; }
        public string contacto { get; set; }
        public string contactoCorreo { get; set; }
        public string contactoTelefono { get; set; }
        public string jefatura { get; set; }
        public string jefaturaCorreo { get; set; }
        public string jefaturaTelefono { get; set; }
        public string numeroRegistro { get; set; }
        public DateTime? fechaDeRegistro { get; set; }

    }

    public class AdminComisionViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicial"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fechaInicial { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Final"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fechaFinal { get; set; }
        public List<ComisionTableViewModel> comisiones { get; set; }

    }

    public class ComisionTableUserChooser
    {
        public int idComision { get; set; }
        public string nombre { get; set; }

    }

    public class ComisionUserTableViewModel
    {
        public int idComision { get; set; }
        public string nombre { get; set; }
        public string contacto { get; set; }
        public string contactoCorreo { get; set; }
        public string contactoTelefono { get; set; }
        public string jefatura { get; set; }
        public string jefaturaCorreo { get; set; }
        public string jefaturaTelefono { get; set; }
        public string numeroRegistro { get; set; }
        public DateTime? fechaDeRegistro { get; set; }

        public List<RepresentanteTableViewModel> representantes { get; set; }

}
}