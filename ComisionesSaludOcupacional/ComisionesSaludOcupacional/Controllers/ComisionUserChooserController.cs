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
    public class ComisionUserChooserController : Controller
    {
        // GET: ComisionUserChooser
        public ActionResult Index()
        {
            List<ComisionTableUserChooser> lista = null;
            using (SaludOcupacionalEntities db = new SaludOcupacionalEntities())
            {
                lista = (from d in db.Comision
                         orderby d.idComision
                         select new ComisionTableUserChooser
                         {
                             idComision = d.idComision,
                             nombre = d.nombre,
                         }).ToList();
            }

            return View(lista);
        }
    }
}