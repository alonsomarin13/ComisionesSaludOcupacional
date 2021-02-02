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
        /* Función de controlador tipo GET que abre la vista principal del módulo de
         * noticias, por el lado del usuario de comisión. Permite ver todas las noticias subidas, 
         * así como filtrar por título
         Parámetros: título a filtrar.*/
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

        /* Función de controlador tipo GET que abre la vista de ver noticia, donde
         * se puede visualizar el título, contenido y archivo de la noticia seleccionada
         Parámetros: Id de la noticia.*/
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

        /* Función de controlador tipo GET que se alza cada vez que el usuario presione
         * el botón de "Descargar Archivo". Permite descargar el archivo seleccionado.
         Parámetros: Id del archivo*/
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