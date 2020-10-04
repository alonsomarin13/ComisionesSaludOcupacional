using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using ComisionesSaludOcupacional.Models;
using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;

namespace ComisionesSaludOcupacional.Controllers
{
    public class RepresentanteController : Controller
    {
        // GET: Representante
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Representantes(int? pIdComision)
        {
            List<RepresentanteTableViewModel> lista = null;
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                lista = (from d in db.Representante
                         where d.idRepresentante == 1
                         orderby d.idRepresentante
                         select new RepresentanteTableViewModel
                         {
                             nombre = d.nombre,
                             correo = d.correo,
                             telefono = d.telefono,
                             ingreso = d.ingreso,
                             vencimiento = d.vencimiento,
                             idComision = d.idComision
                         }).ToList();
            }
            return View(lista);
        }

        [HttpGet]
        public ActionResult Add()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Add(RepresentanteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                Representante oRepresentante = new Representante();
                oRepresentante.nombre = model.nombre;
                oRepresentante.correo = model.correo;
                oRepresentante.telefono = model.telefono;
                oRepresentante.ingreso = DateTime.Today;
                oRepresentante.vencimiento = oRepresentante.ingreso.AddYears(3);
                oRepresentante.tipo = int.Parse(model.tipo);
                oRepresentante.estado = 1;

                db.Representante.Add(oRepresentante);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Home/Index"));
        }
    }
}