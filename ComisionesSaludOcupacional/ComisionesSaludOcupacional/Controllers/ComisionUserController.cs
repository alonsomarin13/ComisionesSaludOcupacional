using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComisionesSaludOcupacional.Models;
using ComisionesSaludOcupacional.Models.ClasesUtilidad;
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
                var oCentroDeTrabajo = db.CentroDeTrabajo.Find(oComision.idCentroDeTrabajo);
                model.nombre = oCentroDeTrabajo.nombreCentroDeTrabajo;
                model.contacto = oComision.contacto;
                model.contactoCorreo = oComision.contactoCorreo;
                model.contactoTelefono = oComision.contactoTelefono;
                model.jefatura = oComision.jefatura;
                model.jefaturaCorreo = oComision.jefaturaCorreo;
                model.jefaturaTelefono = oComision.jefaturaTelefono;
                model.idComision = oComision.idComision;
                model.fechaDeRegistro = oComision.fechaDeRegistro;
                model.numeroRegistro = oComision.numeroDeRegistro;
                model.ultimoInforme = oComision.ultimoInforme;
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

            DateTime hoy = DateTime.Today;
            DateTime? fechaInforme = model.ultimoInforme;

            if (/*hoy.Month == 2 &&*/ (fechaInforme == null || fechaInforme?.Year < hoy.Year))
            {
                ViewBag.Mensaje = "Recuerde por favor entregar el informe Anual\n Ya lo ha entregado?";
            }

            DateTime? fechaRegistro = model.fechaDeRegistro;
            DateTime? fechaVencimiento = fechaRegistro?.AddYears(3);
            DateTime? fechaVencimientoTemp = fechaVencimiento?.AddMonths(-2);

            if (hoy < fechaVencimiento && hoy >= fechaVencimientoTemp)
            {
                var cantDias = (fechaVencimiento - hoy)?.TotalDays;
                ViewBag.Vencimiento = "Su comisión está a " + cantDias.ToString() + " días de vencerse. Por favor, renuévela.";
            } else if (hoy >= fechaVencimiento)
            {
                ViewBag.Vencimiento = "Su comisión está vencida desde el "+ fechaVencimiento?.ToShortDateString() + ". Por favor, renuévela.";
            }

            int cantPatronos, cantTrabajadores;

            using (var db = new SaludOcupacionalEntities())
            {
                cantPatronos = (from d in db.Comision
                                join r in db.Representante on d.idComision equals r.idComision
                                where d.idComision == model.idComision && r.tipo == 0 && r.estado == 1
                                select d.idCentroDeTrabajo).Count();

                cantTrabajadores = (from d in db.Comision
                                join r in db.Representante on d.idComision equals r.idComision
                                where d.idComision == model.idComision && r.tipo == 1 && r.estado == 1
                                    select d.idCentroDeTrabajo).Count();
            }

            if (cantPatronos != cantTrabajadores)
            {
                ViewBag.RepresentantesWarning = "Su comisión no tiene cantidades iguales de Patronos y Trabajadores, por favor asegúrese de que esto se cumpla. porfis porfis bro";
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
                model.fechaDeRegistro = oComision.fechaDeRegistro;
                model.numeroRegistro = oComision.numeroDeRegistro;
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
                oComision.numeroDeRegistro = model.numeroRegistro;
                oComision.fechaDeRegistro = model.fechaDeRegistro;
                db.Entry(oComision).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/ComisionUser/InformacionPrincipal/" + model.idComision));
        }

        public ActionResult Cuenta()
        {
            CuentaModificadaViewModel model = new CuentaModificadaViewModel();

            Cuenta oCuenta = (Cuenta)Session["Usuario"];
            model.nombre = oCuenta.nombre;

            return View(model);
        }

        [HttpPost]
        public ActionResult Cuenta(CuentaModificadaViewModel model)
        {
            Cuenta oCuenta = (Cuenta)Session["Usuario"];
            model.nombre = oCuenta.nombre;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (oCuenta.contrasena != CryptoEngine.Encrypt(model.contrasena, "sxlw-3jn8-sqoy12"))
            {
                ModelState.AddModelError("contrasena", "Contraseña incorrecta");
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                string contrasenaNueva = CryptoEngine.Encrypt(model.contrasenaNueva, "sxlw-3jn8-sqoy12");
                oCuenta.contrasena = contrasenaNueva;

                db.Entry(oCuenta).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                TempData["Success"] = "Contraseña cambiada correctamente";
            }

            return View(model);
        }


        public ActionResult InformeEntregado(int id)
        {
            using (var db = new SaludOcupacionalEntities())
            {
                var oComision = db.Comision.Find(id);

                oComision.ultimoInforme = DateTime.Today;

                db.Entry(oComision).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
            return Redirect(Url.Content("~/ComisionUser/InformacionPrincipal/" + id));
        }
    }
}