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
    public class ComisionUserController : Controller
    {
        // GET: ComisionUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InformacionPrincipal(int id) {
            ComisionUserTableViewModel model = new ComisionUserTableViewModel();
            using (var db = new SaludOcupacionalEntities())
            {
                var oComision = db.Comision.Find(id);
                model.nombre = oComision.nombre;
                model.contacto = oComision.contacto;
                model.contactoCorreo = oComision.contactoCorreo;
                model.contactoTelefono = oComision.contactoTelefono;
                model.jefatura = oComision.jefatura;
                model.jefaturaCorreo = oComision.jefaturaCorreo;
                model.jefaturaTelefono = oComision.jefaturaTelefono;
                model.idComision = oComision.idComision;
                model.fechaDeRegistro = oComision.fechaDeRegistro;
                model.numeroRegistro = oComision.numeroDeRegistro;
            }
            if (model.contacto == null || model.contactoCorreo == null || model.contactoTelefono == null || model.jefatura == null ||
                model.jefaturaCorreo == null || model.jefaturaTelefono == null || model.fechaDeRegistro == null || model.numeroRegistro == null) {
                return Redirect(Url.Content("~/ComisionUser/LlenarInformacion/" + model.idComision));
            }
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                model.representantes = (from d in db.Representante
                         where d.idComision == id && d.estado == 1
                         orderby d.idRepresentante
                         select new RepresentanteTableViewModel
                         {
                             idRepresentante = d.idRepresentante,
                             nombre = d.nombre,
                             correo = d.correo,
                             telefono = d.telefono,
                             idComision = d.idComision
                         }).ToList();
            }
            return View(model);
        }

        public ActionResult LlenarInformacion(int id) {
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
                model.fechaDeRegistro = oComision.fechaDeRegistro;
                model.numeroRegistro = oComision.numeroDeRegistro;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult LlenarInformacion(EditComisionViewModel model) {
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
                oComision.numeroDeRegistro = model.numeroRegistro;
                oComision.fechaDeRegistro = model.fechaDeRegistro;
                db.Entry(oComision).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/ComisionUser/InformacionPrincipal/" + model.idComision));
        }
    }
}