using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Controllers;

namespace ComisionesSaludOcupacional.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var oUsuario = (Cuenta)HttpContext.Current.Session["Usuario"];

            if (oUsuario == null)
            {
                if (filterContext.Controller is HomeController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home");
                }
            } 
            else {
                if (filterContext.Controller is HomeController == true)
                {
                    if (oUsuario.rol == 0)
                    {
                        filterContext.HttpContext.Response.Redirect("~/AdminComision", false);
                    } else
                    {
                        int idComision = (int)HttpContext.Current.Session["idComision"];
                        filterContext.HttpContext.Response.Redirect("~/ComisionUser/InformacionPrincipal/" + idComision, false);
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}