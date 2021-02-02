using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComisionesSaludOcupacional.Models;
using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;

namespace ComisionesSaludOcupacional.Controllers
{
    public class AdminRepresentantesController : Controller
    {
        /* Función de controlador tipo GET que abre la vista principal de
         * visualización de representantes del lado del administrador. Permite ver
         * todos los representantes anexados a cierta comisión
         Parámetros: Id de la comisión*/
        public ActionResult Representantes(int id)
        {
            List<RepresentanteTableViewModel> lista = null;
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                lista = (from d in db.Representante
                         where d.idComision == id && d.estado == 1
                         orderby d.idRepresentante
                         select new RepresentanteTableViewModel
                         {
                             idRepresentante = d.idRepresentante,
                             nombre = d.nombre,
                             correo = d.correo,
                             telefono = d.telefono,
                             idComision = d.idComision,
                             tipo = d.tipo == 0 ? "Patrono" : "Trabajador"
                         }).ToList();
            }

            foreach (var obj in lista)
            {
                obj.sIngreso = obj.ingreso.ToShortDateString();
                obj.sVencimiento = obj.vencimiento.ToShortDateString();
            }
            return View(lista);
        }
    }
}