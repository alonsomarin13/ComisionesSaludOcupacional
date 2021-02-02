using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Controllers
{
    public class CierreController : Controller
    {
        /* Función de controlador tipo GET que se llama cuando el botón de
         * "Cerrar sesión" es presionado. Se encarga de borrar del parámetro "Session" 
         * el usuario actual.*/
        public ActionResult Logout()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}