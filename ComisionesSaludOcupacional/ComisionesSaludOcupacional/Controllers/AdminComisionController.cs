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
    public class AdminComisionController : Controller
    {
        public int regionIDCombo;
        // GET: AdminComision
        public ActionResult Index()
        {
            List<ComisionTableViewModel> lista = null;
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                lista = (from d in db.Comision
                         join c in db.CentroDeTrabajo on d.idCentroDeTrabajo equals c.idCentroDeTrabajo
                         orderby d.idComision
                         select new ComisionTableViewModel
                         {
                             idComision = d.idComision,
                             nombre = c.nombreCentroDeTrabajo,
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
            ComisionViewModel model = new ComisionViewModel();
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
            model.listaDeRegiones = (from d in db.Region
                         orderby d.numeroRegion
                         select new SelectListItem
                         {
                             Value = d.idRegion.ToString(),
                             Text = d.nombreRegion,
                         }).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ComisionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Debug.WriteLine(model.idCentroDeTrabajo);

            using (var db = new SaludOcupacionalEntities())
            {
                Comision oComision = new Comision();
                oComision.idCentroDeTrabajo = model.idCentroDeTrabajo;
                db.Comision.Add(oComision);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/AdminComision"));
        }

        [HttpPost]
        public JsonResult GetCentrosDeTrabajo(string Prefix, int regID) {
            //Debug.WriteLine("Entre a la funcion de centros");
            
            //Debug.WriteLine(regionIDCombo);
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