using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Controllers
{
    public class AdminCentroDeTrabajoController : Controller
    {
        // GET: AdminCentrosDeTrabajo
        public ActionResult Index(string nombre, int? region)
        {
            List<CentroDeTrabajoTableViewModel> lista = null;

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

                var centrosDeTrabajo = from d in db.CentroDeTrabajo
                                       join r in db.Region on d.idRegion equals r.idRegion
                                       orderby d.idCentroDeTrabajo
                                       select new CentroDeTrabajoTableViewModel
                                       {
                                           nombre = d.nombreCentroDeTrabajo,
                                           nombreRegion = r.nombreRegion,
                                           idRegion = d.idRegion,
                                           idCentroDeTrabajo = d.idCentroDeTrabajo
                                       };

                if (!String.IsNullOrEmpty(nombre))
                {
                    centrosDeTrabajo = centrosDeTrabajo.Where(d => d.nombre.Contains(nombre));
                }

                if (region != null)
                {
                    centrosDeTrabajo = centrosDeTrabajo.Where(d => d.idRegion == region);
                }

                lista = centrosDeTrabajo.ToList();

                return View(lista);
            }
        }

        public ActionResult AddCentroDeTrabajo()
        {
            CentroDeTrabajoViewModel model = new CentroDeTrabajoViewModel();

            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                List<SelectListItem> regiones = (from d in db.Region
                                                 orderby d.numeroRegion
                                                 select new SelectListItem
                                                 {
                                                     Value = d.idRegion.ToString(),
                                                     Text = d.nombreRegion,
                                                 }).ToList();

                model.listaDeRegiones = new SelectList(regiones, "Value", "Text");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddCentroDeTrabajo(CentroDeTrabajoViewModel model)
        {
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                List<SelectListItem> regiones = (from d in db.Region
                                                 orderby d.numeroRegion
                                                 select new SelectListItem
                                                 {
                                                     Value = d.idRegion.ToString(),
                                                     Text = d.nombreRegion,
                                                 }).ToList();

                model.listaDeRegiones = new SelectList(regiones, "Value", "Text");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                int idRegion = int.Parse(model.idRegion);
                var nombreExistente = db.CentroDeTrabajo.Any(x => (x.nombreCentroDeTrabajo == model.nombre) && (x.idRegion == idRegion));
                if (nombreExistente)
                {
                    ModelState.AddModelError("nombre", "Este Centro de Trabajo ya está registrado en esta región");
                    return View(model);
                }

                CentroDeTrabajo oCentroDeTrabajo = new CentroDeTrabajo();

                oCentroDeTrabajo.nombreCentroDeTrabajo = model.nombre;
                oCentroDeTrabajo.idRegion = idRegion;

                db.CentroDeTrabajo.Add(oCentroDeTrabajo);

                db.SaveChanges();

                TempData["Success"] = "Centro de Trabajo creado correctamente";
            }

            return View(model);
        }

        public ActionResult EditCentroDeTrabajo(int id)
        {
            CentroDeTrabajoEditViewModel model = new CentroDeTrabajoEditViewModel();
            using (var db = new SaludOcupacionalEntities())
            {
                var oCentroDeTrabajo = db.CentroDeTrabajo.Find(id);
                model.idCentroDeTrabajo = id;
                model.nombre = oCentroDeTrabajo.nombreCentroDeTrabajo;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditCentroDeTrabajo(CentroDeTrabajoEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                var oCentroDeTrabajo = db.CentroDeTrabajo.Find(model.idCentroDeTrabajo);
                oCentroDeTrabajo.nombreCentroDeTrabajo = model.nombre;
                db.Entry(oCentroDeTrabajo).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                TempData["Success"] = "Centro de Trabajo modificado correctamente";
            }

            return View(model);
        }
    }
}