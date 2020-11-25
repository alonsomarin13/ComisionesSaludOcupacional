using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using ComisionesSaludOcupacional.Models;
using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System.Text;
using System.Web.Security;

namespace ComisionesSaludOcupacional.Controllers
{
    public class AdminComisionController : Controller
    {
        public int regionIDCombo;
        // GET: AdminComision
        public ActionResult Index(string nombre, int? region, DateTime? fechaInicial, DateTime? fechaFinal)
        {
            AdminComisionViewModel model = new AdminComisionViewModel();

            model.fechaInicial = fechaInicial;
            model.fechaFinal = fechaFinal;

            model.comisiones = null;
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {

                List<SelectListItem> regiones = (from d in db.Region
                                orderby d.numeroRegion
                                select new SelectListItem
                                {
                                    Value = d.idRegion.ToString(),
                                    Text = d.nombreRegion,
                                }).ToList();

                ViewBag.Regiones = new SelectList(regiones, "Value", "Text");

                Debug.WriteLine(fechaInicial.ToString());

                var comisiones = from d in db.Comision
                                 join c in db.CentroDeTrabajo on d.idCentroDeTrabajo equals c.idCentroDeTrabajo
                                 join r in db.Region on c.idRegion equals r.idRegion
                                 orderby d.idComision
                                 select new ComisionTableViewModel
                                 {
                                     idComision = d.idComision,
                                     nombre = c.nombreCentroDeTrabajo,
                                     idRegion = r.idRegion,
                                     nombreRegion = r.nombreRegion,
                                     contacto = d.contacto,
                                     contactoCorreo = d.contactoCorreo,
                                     contactoTelefono = d.contactoTelefono,
                                     jefatura = d.jefatura,
                                     jefaturaCorreo = d.jefaturaCorreo,
                                     jefaturaTelefono = d.jefaturaTelefono,
                                     numeroRegistro = d.numeroDeRegistro,
                                     fechaDeRegistro = d.fechaDeRegistro
                                 };

                if (!String.IsNullOrEmpty(nombre)) {
                    comisiones = comisiones.Where(d => d.nombre.Contains(nombre));
                }

                if (region != null)
                {
                    comisiones = comisiones.Where(d => d.idRegion == region);
                }

                if (fechaInicial != null)
                {
                    comisiones = comisiones.Where(d => d.fechaDeRegistro >= fechaInicial);
                }

                if (fechaFinal != null)
                {
                    comisiones = comisiones.Where(d => d.fechaDeRegistro <= fechaFinal);
                }

                model.comisiones = comisiones.ToList();

                return View(model);
            }

            
        }
        [HttpGet]
        public ActionResult AddComisionUser()
        {

            ComisionViewModel model = new ComisionViewModel();
            List<SelectListItem> regionesTemp;
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
            regionesTemp = (from d in db.Region
                         orderby d.numeroRegion
                         select new SelectListItem
                         {
                             Value = d.idRegion.ToString(),
                             Text = d.nombreRegion,
                         }).ToList();
            }
            model.listaDeRegiones = new SelectList(regionesTemp, "Value", "Text");
            return View(model);
        }

        [HttpPost]
        public ActionResult AddComisionUser(ComisionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> regionesTemp;
                using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
                {
                    regionesTemp = (from d in db.Region
                                    orderby d.numeroRegion
                                    select new SelectListItem
                                    {
                                        Value = d.idRegion.ToString(),
                                        Text = d.nombreRegion,
                                    }).ToList();
                }
                model.listaDeRegiones = new SelectList(regionesTemp, "Value", "Text");
                return View(model);
            }

            Debug.WriteLine(model.idCentroDeTrabajo);

            using (var db = new SaludOcupacionalEntities())
            {

                CentroDeTrabajo oCentroDeTrabajo = db.CentroDeTrabajo.Find(model.idCentroDeTrabajo);
                if (oCentroDeTrabajo == null)
                {
                    List<SelectListItem> regionesTemp;
                    regionesTemp = (from d in db.Region
                                    orderby d.numeroRegion
                                    select new SelectListItem
                                    {
                                        Value = d.idRegion.ToString(),
                                        Text = d.nombreRegion,
                                    }).ToList();
                    model.listaDeRegiones = new SelectList(regionesTemp, "Value", "Text");
                    ModelState.AddModelError("nombreCentroDeTrabajo", "El Centro de Trabajo no Existe");
                    return View(model);
                }

                int valido = (from d in db.Comision
                              join ct in db.CentroDeTrabajo on d.idCentroDeTrabajo equals ct.idCentroDeTrabajo
                              where ct.idCentroDeTrabajo == model.idCentroDeTrabajo
                              select d.idCentroDeTrabajo).Count();

                if (valido != 0)
                {
                    List<SelectListItem> regionesTemp;
                    regionesTemp = (from d in db.Region
                                    orderby d.numeroRegion
                                    select new SelectListItem
                                    {
                                        Value = d.idRegion.ToString(),
                                        Text = d.nombreRegion,
                                    }).ToList();
                    model.listaDeRegiones = new SelectList(regionesTemp, "Value", "Text");
                    ModelState.AddModelError("nombreCentroDeTrabajo", "El Centro de Trabajo ya está asignado a otra comisión");
                    return View(model);
                }

                Comision oComision = new Comision();
                oComision.idCentroDeTrabajo = model.idCentroDeTrabajo;
                db.Comision.Add(oComision);

                string nombreComision = String.Concat(oCentroDeTrabajo.nombreCentroDeTrabajo.Where(c => !Char.IsWhiteSpace(c)));
                nombreComision = "comision" + nombreComision;
                nombreComision = nombreComision.ToLower();
                nombreComision = Regex.Replace(nombreComision.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

                string contrasena = Membership.GeneratePassword(10, 1);

                Cuenta oCuenta = new Cuenta();
                oCuenta.nombre = nombreComision;
                oCuenta.contrasena = contrasena;
                oCuenta.rol = 1;
                oCuenta.idComision = oComision.idComision;
                db.Cuenta.Add(oCuenta);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/AdminCuenta"));
        }

        [HttpPost]
        public JsonResult GetCentrosDeTrabajo(string Prefix, int regID) {
            Debug.WriteLine("Entre a la funcion de centros");
            
            Debug.WriteLine(regionIDCombo);
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities()) {
                var centros = (from d in db.CentroDeTrabajo
                               join c in db.Region on d.idRegion equals c.idRegion
                               where d.nombreCentroDeTrabajo.StartsWith(Prefix) && d.idRegion == regID
                               select new { nombreCentroDeTrabajo = d.nombreCentroDeTrabajo, idCentroDeTrabajo = d.idCentroDeTrabajo }
                               ).ToList();
                foreach (var x in centros) {
                    //Debug.WriteLine(x.idCentroDeTrabajo);
                }
                return Json(centros, JsonRequestBehavior.AllowGet);
            }
        }
    }
}