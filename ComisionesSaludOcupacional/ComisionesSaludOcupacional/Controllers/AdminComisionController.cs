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
    public class AdminComisionController : Controller
    {
        // GET: AdminComision
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
                             contactoCorreo = d.contactoCorreo,
                             contactoTelefono = d.contactoTelefono,
                             jefatura = d.jefatura,
                             jefaturaCorreo = d.jefaturaCorreo,
                             jefaturaTelefono = d.jefaturaTelefono,
                             numeroRegistro = d.numeroDeRegistro,
                             fechaDeRegistro = d.fechaDeRegistro
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
                db.Comision.Add(oComision);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/AdminComision"));
        }
    }
}