using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Controllers
{
    public class CierreController : Controller
    {
        public ActionResult Logout()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}