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
    public class ComisionUserRepresentantesController : Controller
    {
        // GET: ComisionUserRepresentantes
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Add(RepresentanteViewModel model, int id)
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
                oRepresentante.tipo = int.Parse(model.tipo);
                oRepresentante.estado = 1;
                oRepresentante.idComision = id;

                db.Representante.Add(oRepresentante);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/ComisionUser/InformacionPrincipal/" + Session["ComisionUserID"]));
        }

        public ActionResult Edit(int id)
        {
            EditRepresentanteViewModel model = new EditRepresentanteViewModel();
            using (var db = new SaludOcupacionalEntities())
            {
                var oRepresentante = db.Representante.Find(id);
                model.nombre = oRepresentante.nombre;
                model.correo = oRepresentante.correo;
                model.telefono = oRepresentante.telefono;
                model.idRepresentante = oRepresentante.idRepresentante;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditRepresentanteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                var oRepresentate = db.Representante.Find(model.idRepresentante);
                oRepresentate.nombre = model.nombre;
                oRepresentate.correo = model.correo;
                oRepresentate.telefono = model.telefono;

                db.Entry(oRepresentate).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/ComisionUser/InformacionPrincipal/" + Session["ComisionUserID"]));
        }

        public ActionResult Delete(int? id)
        {
            using (var db = new SaludOcupacionalEntities())
            {
                var oRepresentate = db.Representante.Find(id);
                oRepresentate.estado = 0;

                db.Entry(oRepresentate).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/ComisionUser/InformacionPrincipal/" + Session["ComisionUserID"]));
        }
    }
}