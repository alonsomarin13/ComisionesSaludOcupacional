using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComisionesSaludOcupacional.Models.ViewModels
{
    public class ComisionTableViewModel
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