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
                           contactoCorreo = d.contactoCorreo,
                           contactoTelefono = d.contactoTelefono,
                           jefatura = d.jefatura,
                           jefaturaCorreo = d.jefaturaCorreo,
                           jefaturaTelefono = d.jefaturaTelefono
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
                oComision.contactoCorreo = model.contactoCorreo;
                oComision.contactoTelefono = model.contactoTelefono;
                oComision.jefatura = model.jefatura;
                oComision.jefaturaCorreo = model.jefaturaCorreo;
                oComision.jefaturaTelefono = model.jefaturaTelefono;
                db.Comision.Add(oComision);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Comision/Index"));
        }

        public ActionResult Edit(int id)
        {
            EditComisionViewModel model = new EditComisionViewModel();

            using (var db = new SaludOcupacionalEntities())
            {
                var oComision = db.Comision.Find(id);
                model.contacto = oComision.contacto;
                model.contactoCorreo = oComision.contactoCorreo;
                model.contactoTelefono = oComision.contactoTelefono;
                model.jefatura = oComision.jefatura;
                model.jefaturaCorreo = oComision.jefaturaCorreo;
                model.jefaturaTelefono = oComision.jefaturaTelefono;
                model.idComision = oComision.idComision;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditComisionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                var oComision = db.Comision.Find(model.idComision);
                oComision.contacto = model.contacto;
                oComision.contactoCorreo = model.contactoCorreo;
                oComision.contactoTelefono = model.contactoTelefono;
                oComision.jefatura = model.jefatura;
                oComision.jefaturaCorreo = model.jefaturaCorreo;
                oComision.jefaturaTelefono = model.jefaturaTelefono;
                db.Entry(oComision).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Comision/Index"));
        }
    }
}