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
        /* Función de controlador tipo GET que abre la vista de añadir representante,
         * donde se permite crear un nuevo representante anexado a la comisión*/
        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }

        /* Función de controlador tipo POST que realiza la creación del representante, 
         * extrae del modelo los datos ingresados por la persona y los guarda en la base.
         Parámetros: modelo que envía la vista, Id de la comisión*/
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

        /* Función de controlador tipo GET que abre la vista de editar representante,
         * donde se pueden editar todos los contenidos de un representante elegido
         Parámetros: Id del representante.*/
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

        /* Función de controlador tipo POST que realiza la edición del representante, 
         * extrae del modelo los datos ingresados por la persona y los guarda en la base.
         Parámetros: modelo que envía la vista*/
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

        /* Función de controlador tipo GET que permite el borrado lógico de un representante en
         * la base de datos, en caso de que sea necesario. 
         Parámetros: Id del representante*/
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