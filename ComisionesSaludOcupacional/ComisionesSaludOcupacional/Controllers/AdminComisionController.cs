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
using ComisionesSaludOcupacional.Models.ClasesUtilidad;

namespace ComisionesSaludOcupacional.Controllers
{
    public class AdminComisionController : Controller
    {
        public int regionIDCombo;

        /* Función de controlador tipo GET que muestra todas las comisiones existentes en el sistema,
         * con todos los atributos que necesita ver el administrador. También, permite filtrar por nombre, región
         * fecha inicial o final, si entregaron el informe y si está vencida.
         Parámetros: nombre, región, fecha inicial, fecha final, informe, vencida (todos son datos de filtros)*/
        public ActionResult Index(string nombre, int? region, DateTime? fechaInicial, DateTime? fechaFinal, int? informe, int? vencida)
        {
            AdminComisionViewModel model = new AdminComisionViewModel();

            model.fechaInicial = fechaInicial;
            model.fechaFinal = fechaFinal;

            // Se declara y se crea la lista para el filtro de "informe entregado"
            List<SelectListItem> informeEntregado = new List<SelectListItem>
            {
                new SelectListItem{Text = "Sí", Value = "1"},
                new SelectListItem{Text = "No", Value = "0"}
            };

            ViewBag.listaInforme = new SelectList(informeEntregado, "Value", "Text");

            // Se declara y se crea la lista para el filtro de "comisión vencida"
            List<SelectListItem> comisionVencida = new List<SelectListItem>
            {
                new SelectListItem{Text = "Sí", Value = "1"},
                new SelectListItem{Text = "No", Value = "0"}
            };

            ViewBag.listaVencimiento = new SelectList(comisionVencida, "Value", "Text");

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
                                     fechaDeRegistro = d.fechaDeRegistro,
                                     ultimoInforme = d.ultimoInforme
                                 };

                // Filtro por nombre
                if (!String.IsNullOrEmpty(nombre)) {
                    comisiones = comisiones.Where(d => d.nombre.Contains(nombre));
                }
                
                // Filtro por región
                if (region != null)
                {
                    comisiones = comisiones.Where(d => d.idRegion == region);
                }

                // Filtro por fecha inicial
                if (fechaInicial != null)
                {
                    comisiones = comisiones.Where(d => d.fechaDeRegistro >= fechaInicial);
                }

                // Filtro por fecha final
                if (fechaFinal != null)
                {
                    comisiones = comisiones.Where(d => d.fechaDeRegistro <= fechaFinal);
                }

                // Filtro por si entregó el informe
                if (informe != null)
                {
                    DateTime hoy = DateTime.Today;
                    DateTime primerDiaDelAño = new DateTime(hoy.Year, 1, 1);
                    if (informe == 0)
                    {
                        comisiones = comisiones.Where(d => (d.ultimoInforme < primerDiaDelAño) || (d.ultimoInforme == null));
                    }
                    else
                    {
                        comisiones = comisiones.Where(d => d.ultimoInforme >= primerDiaDelAño);
                    }
                }

                // Filtro por si está vencida la comisión
                if (vencida != null)
                {
                    DateTime hoyHace3Annos = DateTime.Today.AddYears(-3);
                    if (vencida == 0)
                    {
                        comisiones = comisiones.Where(d => d.fechaDeRegistro >= hoyHace3Annos);
                    } else
                    {
                        comisiones = comisiones.Where(d => d.fechaDeRegistro < hoyHace3Annos);
                    }
                }

                model.comisiones = comisiones.ToList();

                return View(model);
            }

            
        }

        /* Función de controlador tipo GET que abre la vista de "Añadir Usuario de Comisión",
         * permite crear un usuario de comisión para una comisión nueva en el sistema.*/
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

        /* Función de controlador tipo POST que realiza la creación del usuario de comisión, 
         * extrae del modelo los datos ingresados por la persona y los guarda en la base.
         Parámetros: modelo que envía la vista*/
        [HttpPost]
        public ActionResult AddComisionUser(ComisionViewModel model)
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

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {

                // Revisa si el centro de trabajo existe en el sistema
                CentroDeTrabajo oCentroDeTrabajo = db.CentroDeTrabajo.Find(model.idCentroDeTrabajo);
                if (oCentroDeTrabajo == null)
                {
                    ModelState.AddModelError("nombreCentroDeTrabajo", "El Centro de Trabajo no Existe");
                    return View(model);
                }

                // Revisa si el centro de trabajo ya tiene una comisión asignada. Sólo puede haber una comisión por centro de trabajo
                int valido = (from d in db.Comision
                              join ct in db.CentroDeTrabajo on d.idCentroDeTrabajo equals ct.idCentroDeTrabajo
                              where ct.idCentroDeTrabajo == model.idCentroDeTrabajo
                              select d.idCentroDeTrabajo).Count();

                if (valido != 0)
                {
                    ModelState.AddModelError("nombreCentroDeTrabajo", "El Centro de Trabajo ya está asignado a otra comisión");
                    return View(model);
                }

                /* Crea el nombre de la comisión de manera predeterminada. Ej: "comisionhatillo" para el centro de trabajo "Hatillo"
                 * Quita los espacios en blanco*/
                string nombreComision = String.Concat(oCentroDeTrabajo.nombreCentroDeTrabajo.Where(c => !Char.IsWhiteSpace(c)));
                nombreComision = "comision" + nombreComision;

                /* Revisa si el nombre de la comisión ya existe. Esto es para cuentas duplicadas, centros de trabajo que se llamen igual
                 * pero pertenezcan a diferentes regiones, lo cual es válido*/
                var usernameExists = db.Cuenta.Any(x => x.nombre == nombreComision);

                if (usernameExists)
                {
                    // En caso de repetirse, añade el número de región. Ej "comisionsanrafael2" para San Rafael de Alajuela
                    nombreComision = nombreComision + oCentroDeTrabajo.Region.numeroRegion.ToString();
                }

                // Quita mayúsculas y guiones del nombre
                nombreComision = nombreComision.ToLower();
                nombreComision = Regex.Replace(nombreComision.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");

                // La contraseña se genera mediante una función de C#, que recibe la cantidad de caracteres y el número de caracteres no alfanuméricos
                string contrasena = Membership.GeneratePassword(10, 1);

                ViewBag.nombre = nombreComision;
                ViewBag.contra = contrasena;

                // Se encripta la contraseña
                contrasena = CryptoEngine.Encrypt(contrasena);

                Cuenta oCuenta = new Cuenta();
                oCuenta.nombre = nombreComision;
                oCuenta.contrasena = contrasena;
                oCuenta.rol = 1; // Rol 1 significa "usuario"
                db.Cuenta.Add(oCuenta);

                Comision oComision = new Comision();
                oComision.idCentroDeTrabajo = model.idCentroDeTrabajo;
                oComision.idCuenta = oCuenta.idCuenta;
                db.Comision.Add(oComision);

                db.SaveChanges();
            }

            return View(model);
        }

        /* Función de controlador tipo POST que retorna en un JsonResult, la lista de centros de trabajo
         * anexados a cierta región. Esto se utiliza en la parte de autocompletar, en la creación de comisiones.
         Parámetros: palabra que ha ingresado el usuario hasta el momento, Id de región*/
        [HttpPost]
        public JsonResult GetCentrosDeTrabajo(string Prefix, int regID) {

            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities()) {
                var centros = (from d in db.CentroDeTrabajo
                               join c in db.Region on d.idRegion equals c.idRegion
                               where d.nombreCentroDeTrabajo.StartsWith(Prefix) && d.idRegion == regID
                               select new { nombreCentroDeTrabajo = d.nombreCentroDeTrabajo, idCentroDeTrabajo = d.idCentroDeTrabajo }
                               ).ToList();
                return Json(centros, JsonRequestBehavior.AllowGet);
            }
        }
    }
}