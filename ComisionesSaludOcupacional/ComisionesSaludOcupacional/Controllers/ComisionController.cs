using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComisionesSaludOcupacional.Models;
using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;

namespace ComisionesSaludOcupacional.Controllers
{
    public class ComisionController : Controller
    {
        // GET: Comision
        public ActionResult Index()
        {
            List<ComisionTableViewModel> lista = null;
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                lista = (from d in db.Comision
                       orderby d.idComision
                       select new ComisionTableViewModel
                       {
                           idComision = d.idComision,
                           nombre = d.nombre,
                           contacto = d.contacto,
                           correo = d.correo,
                           telefono = d.telefono
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
        public ActionResult Add(ComisionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                Comision oComision = new Comision();
                oComision.nombre = model.nombre;
                oComision.contacto = model.contacto;
                oComision.correo = model.correo;
                oComision.telefono = model.telefono;

                db.Comision.Add(oComision);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Home/Index"));
        }
    }
}