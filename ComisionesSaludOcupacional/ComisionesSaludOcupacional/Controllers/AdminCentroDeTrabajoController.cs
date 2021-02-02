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
        /* Función de controlador tipo GET que muestra la lista de centros de trabajo en el sistema, 
         * permite ver y editar cada centro, y filtrarlos tanto por nombre como por región 
         Parámetros: Nombre del centro de trabajo y número de la región que se desea filtrar*/
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
                                       orderby d.idRegion descending
                                       select new CentroDeTrabajoTableViewModel
                                       {
                                           nombre = d.nombreCentroDeTrabajo,
                                           nombreRegion = r.nombreRegion,
                                           idRegion = d.idRegion,
                                           idCentroDeTrabajo = d.idCentroDeTrabajo
                                       };

                // Filtro por nombre
                if (!String.IsNullOrEmpty(nombre))
                {
                    centrosDeTrabajo = centrosDeTrabajo.Where(d => d.nombre.Contains(nombre));
                }

                // Filtro por región
                if (region != null)
                {
                    centrosDeTrabajo = centrosDeTrabajo.Where(d => d.idRegion == region);
                }

                lista = centrosDeTrabajo.ToList();

                return View(lista);
            }
        }

        /* Función de controlador tipo GET que abre la vista de "Agregar centros de trabajo", 
         * permite crear un nuevo centro de trabajo, y guardarlo en el sistema. */
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

        /* Función de controlador tipo POST que realiza la creación del centro de trabajo, 
         * extrae del modelo los datos ingresados por la persona y los guarda en la base.
         Parámetros: modelo que envía la vista*/
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

        /* Función de controlador tipo GET que abre la vista "Editar Centro de Trabajo, 
         * permite editar tanto el nombre como la región del centro seleccionado.
         Parámetros: Id del centro de trabajo*/
        public ActionResult EditCentroDeTrabajo(int id)
        {
            CentroDeTrabajoEditViewModel model = new CentroDeTrabajoEditViewModel();
            using (var db = new SaludOcupacionalEntities())
            {
                var oCentroDeTrabajo = db.CentroDeTrabajo.Find(id);
                model.idCentroDeTrabajo = id;
                model.nombre = oCentroDeTrabajo.nombreCentroDeTrabajo;

                List<SelectListItem> regiones = (from d in db.Region
                                                 orderby d.numeroRegion
                                                 select new SelectListItem
                                                 {
                                                     Value = d.idRegion.ToString(),
                                                     Text = d.nombreRegion,
                                                 }).ToList();

                model.listaDeRegiones = new SelectList(regiones, "Value", "Text", oCentroDeTrabajo.idRegion);
            }

            return View(model);
        }

        /* Función de controlador tipo POST que realiza la edición del centro de trabajo, 
         * extrae del modelo los nuevos datos ingresados por la persona y los guarda en la base.
         Parámetros: modelo que envía la vista*/
        [HttpPost]
        public ActionResult EditCentroDeTrabajo(CentroDeTrabajoEditViewModel model)
        {
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                var oCentroDeTrabajo = db.CentroDeTrabajo.Find(model.idCentroDeTrabajo);

                List<SelectListItem> regiones = (from d in db.Region
                                                 orderby d.numeroRegion
                                                 select new SelectListItem
                                                 {
                                                     Value = d.idRegion.ToString(),
                                                     Text = d.nombreRegion,
                                                 }).ToList();

                model.listaDeRegiones = new SelectList(regiones, "Value", "Text", oCentroDeTrabajo.idRegion);
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

                var oCentroDeTrabajo = db.CentroDeTrabajo.Find(model.idCentroDeTrabajo);
                oCentroDeTrabajo.nombreCentroDeTrabajo = model.nombre;
                oCentroDeTrabajo.idRegion = idRegion;

                db.Entry(oCentroDeTrabajo).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                TempData["Success"] = "Centro de Trabajo modificado correctamente";
            }

            return View(model);
        }
    }
}