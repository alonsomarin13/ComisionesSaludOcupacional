using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Controllers
{
    public class ComisionUserNoticiaController : Controller
    {
        // GET: ComisionUserNoticia
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
    }
}