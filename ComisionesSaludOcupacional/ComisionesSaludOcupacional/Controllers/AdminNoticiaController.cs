using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Controllers
{
    public class AdminNoticiaController : Controller
    {
        // GET: AdminNoticia
        public ActionResult Index(string titulo)
        {
            List<NoticiaTableViewModel> lista = null;

            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {

                var noticias = from d in db.Noticia
                               orderby d.fecha descending
                               select new NoticiaTableViewModel
                               {
                                   idNoticia = d.idNoticia,
                                   titulo = d.titulo,
                                   fecha = d.fecha
                               };

                if (!String.IsNullOrEmpty(titulo))
                {
                    noticias = noticias.Where(d => d.titulo.Contains(titulo));
                }

                lista = noticias.ToList();
            }
            return View(lista);
        }

        public ActionResult AddNoticia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNoticia(NoticiaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                Noticia oNoticia = new Noticia();

                oNoticia.titulo = model.titulo;
                oNoticia.texto = model.texto;
                oNoticia.fecha = DateTime.Now;

                db.Noticia.Add(oNoticia);

                if (model.archivo != null)
                {
                    Archivo oArchivo = new Archivo();

                    oArchivo.nombre = model.archivo.FileName;
                    oArchivo.tipo = model.archivo.ContentType;

                    string strDateTime = System.DateTime.Now.ToString("ddMMyyyHHMMss");
                    string filePath = "\\UploadedFiles\\" + strDateTime + model.archivo.FileName;

                    model.archivo.SaveAs(Server.MapPath("~") + filePath);

                    oArchivo.filePath = filePath;
                    oArchivo.idNoticia = oNoticia.idNoticia;

                    db.Archivo.Add(oArchivo);
                }

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/AdminNoticia"));
        }

        public ActionResult EditarNoticia(int id)
        {
            EditNoticiaViewModel model = new EditNoticiaViewModel();

            using (var db = new SaludOcupacionalEntities())
            {
                var oNoticia = db.Noticia.Find(id);
                model.idNoticia = oNoticia.idNoticia;
                model.titulo = oNoticia.titulo;
                model.texto = oNoticia.texto;

                var archivo = from d in db.Archivo
                              where d.idNoticia == id
                              select d;

                if (archivo.Count() != 0)
                {
                    ViewBag.Mensaje = "archivos";

                    model.idArchivoActual = archivo.First().idArchivo;
                }
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarNoticia(EditNoticiaViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                var oNoticia = db.Noticia.Find(model.idNoticia);
                oNoticia.titulo = model.titulo;
                oNoticia.texto = model.texto;

                if (model.archivoNuevo != null)
                {
                    if (model.idArchivoActual == null)
                    {
                        Archivo oArchivo = new Archivo();

                        oArchivo.nombre = model.archivoNuevo.FileName;
                        oArchivo.tipo = model.archivoNuevo.ContentType;

                        string strDateTime = System.DateTime.Now.ToString("ddMMyyyHHMMss");
                        string filePath = "\\UploadedFiles\\" + strDateTime + model.archivoNuevo.FileName;

                        model.archivoNuevo.SaveAs(Server.MapPath("~") + filePath);

                        oArchivo.filePath = filePath;
                        oArchivo.idNoticia = oNoticia.idNoticia;

                        db.Archivo.Add(oArchivo);
                    }
                    else
                    {
                        var oArchivo = db.Archivo.Find(model.idArchivoActual);

                        string fullPath = Request.MapPath(oArchivo.filePath);
                        System.IO.File.Delete(fullPath);

                        oArchivo.nombre = model.archivoNuevo.FileName;
                        oArchivo.tipo = model.archivoNuevo.ContentType;

                        string strDateTime = System.DateTime.Now.ToString("ddMMyyyHHMMss");
                        string filePath = "\\UploadedFiles\\" + strDateTime + model.archivoNuevo.FileName;

                        model.archivoNuevo.SaveAs(Server.MapPath("~") + filePath);

                        oArchivo.filePath = filePath;

                        db.Entry(oArchivo).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                

                db.SaveChanges();

            }

            return Redirect(Url.Content("~/AdminNoticia/VerNoticia/" + model.idNoticia));

        }

        public ActionResult VerNoticia(int id)
        {
            VerNoticiaViewModel model = new VerNoticiaViewModel();
            using (var db = new SaludOcupacionalEntities())
            {
                var oNoticia = db.Noticia.Find(id);
                model.idNoticia = oNoticia.idNoticia;
                model.titulo = oNoticia.titulo;
                model.texto = oNoticia.texto;

                var archivo = from d in db.Archivo
                              where d.idNoticia == id
                              select d;

                if (archivo.Count() != 0)
                {
                    ViewBag.Mensaje = "archivos";

                    Archivo oArchivo = archivo.First();

                    Session["filePath"] = oArchivo.filePath;
                    Session["tipo"] = oArchivo.tipo;

                    model.archivo = oArchivo;
                }
            }

            return View(model);
        }

        public FileResult Descargar(int id)
        {
            string filePath = "";
            string tipo = "";
            using (var db = new SaludOcupacionalEntities())
            {
                var oArchivo = db.Archivo.Find(id);
                filePath = oArchivo.filePath;
                tipo = oArchivo.tipo;
            }

            return File(filePath, tipo);
        }

        public ActionResult Eliminar(int id)
        {
            string filePath = "";
            int idNoticia = 0;
            using (var db = new SaludOcupacionalEntities())
            {
                var oArchivo = db.Archivo.Find(id);
                filePath = oArchivo.filePath;
                idNoticia = oArchivo.idNoticia;
                db.Archivo.Remove(oArchivo);

                db.SaveChanges();
            }

            string fullPath = Request.MapPath(filePath);
            System.IO.File.Delete(fullPath);

            return Redirect(Url.Content("~/AdminNoticia/EditarNoticia/" + idNoticia));
        }

        public ActionResult BorrarNoticia(int id)
        {
            string filePath = "";

            using (var db = new SaludOcupacionalEntities())
            {
                var oNoticia = db.Noticia.Find(id);

                var archivo = from d in db.Archivo
                              where d.idNoticia == id
                              select d;

                if (archivo.Count() != 0)
                {
                    Archivo oArchivo = archivo.First();

                    filePath = oArchivo.filePath;
                    db.Archivo.Remove(oArchivo);

                    string fullPath = Request.MapPath(filePath);
                    System.IO.File.Delete(fullPath);
                }

                db.Noticia.Remove(oNoticia);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/AdminNoticia"));
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}