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
        /* Función de controlador tipo GET que abre la vista principal del módulo de Noticias del
         * lado del administrador. Permite ver todas las noticias creadas con título, fecha, así como
         * el filtrado por medio del título
         Parámetros: título a filtrar*/
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

        /* Función de controlador tipo GET que abre la vista de creación de noticias,
         * permite ponerle título, texto y añadir un archivo.*/
        public ActionResult AddNoticia()
        {
            return View();
        }

        /* Función de controlador tipo POST que realiza la creación de la noticia, 
         * extrae del modelo los datos ingresados por la persona y los guarda en la base.
         Parámetros: modelo que envía la vista*/
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
                oNoticia.fecha = DateTime.Now; // Fecha y hora del momento actual.

                db.Noticia.Add(oNoticia);

                if (model.archivo != null)
                {
                    Archivo oArchivo = new Archivo();

                    oArchivo.nombre = model.archivo.FileName;
                    oArchivo.tipo = model.archivo.ContentType;

                    /* Las rutas del archivo se crean mediante la fecha y el nombre del archivo, así evitando que haya
                     * conflictos con archivos que se llamen igual.*/
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

        /* Función de controlador tipo GET que abre la vista de editar una noticia,
         * permite cambiar tanto el título, como el texto, como el archivo añadido.
         Parámetros: Id de la noticia elegida*/
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
                    // Este mensaje es para hacerle saber a la vista que existen archivos añadidos a la noticia.
                    ViewBag.Mensaje = "archivos";

                    model.idArchivoActual = archivo.First().idArchivo;
                }
            }
            
            return View(model);
        }

        /* Función de controlador tipo POST que realiza la edición de la noticia, 
         * extrae del modelo los datos ingresados por la persona y los guarda en la base.
         Parámetros: modelo que envía la vista*/
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
                    // Si no existía archivo anexado a la noticia, se añade
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
                    // Si se encuentra un archivo nuevo, le cae encima al que existía antes.
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

        /* Función de controlador tipo GET que abre la vista de ver una noticia, 
         * permite observar los contenidos completos de la noticia que se seleccione.
         Parámetros: Id de la noticia que se desea ver.*/
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
                    // Este mensaje es para hacerle saber a la vista si hay archivos.
                    ViewBag.Mensaje = "archivos";

                    Archivo oArchivo = archivo.First();

                    Session["filePath"] = oArchivo.filePath;

                    // Dependiendo del tipo de archivo, se hará una acción u otra en la vista. 
                    Session["tipo"] = oArchivo.tipo;

                    model.archivo = oArchivo;
                }
            }

            return View(model);
        }

        /* Función de controlador tipo GET que permite la descarga de un archivo, 
         * se llama cada vez que la alguien presiona un botón de "Descargar Archivo".
         Parámetros: Id del archivo a descargar.*/
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

        /* Función de controlador tipo GET que permite eliminar un archivo del sistema.
         Parámetros: Id del archivo a eliminar.*/
        public ActionResult Eliminar(int id)
        {
            string filePath = "";
            int idNoticia = 0;
            using (var db = new SaludOcupacionalEntities())
            {
                var oArchivo = db.Archivo.Find(id);
                filePath = oArchivo.filePath;
                idNoticia = oArchivo.idNoticia; // Esto es para poder volver a la pantalla de la noticia correcta.
                db.Archivo.Remove(oArchivo);

                db.SaveChanges();
            }

            string fullPath = Request.MapPath(filePath);
            System.IO.File.Delete(fullPath);

            return Redirect(Url.Content("~/AdminNoticia/EditarNoticia/" + idNoticia));
        }

        /* Función de controlador tipo GET que permite borrar una noticia del sistema.
         Parámetros: Id de la noticia a eliminar.*/
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

        /* Función de controlador tipo POST que alza la vista de error, cuando sea necesario.
         * Se utiliza para manejar el error HTML que salta cuando se intentan subir archivos demasiado grandes,
         * el cual es un error muy específico detectado en el sistema.*/
        public ActionResult Error()
        {
            return View();
        }
    }
}