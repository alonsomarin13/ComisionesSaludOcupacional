using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Controllers;

namespace ComisionesSaludOcupacional.Filters
{
    /* Clase VerifySession
     * Se encarga de filtar si existe un usuario en el sistema, sino, no permite
     * que se pueda acceder a ninguna otra pantalla que no sea el login.*/ 
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Usuario que se encuentra logueado en este momento.
            var oUsuario = (Cuenta)HttpContext.Current.Session["Usuario"];

            if (oUsuario == null)
            {
                // Devuelve al login si el usuario es nulo y si el controlador es otro que no sea el del login
                if (filterContext.Controller is HomeController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home");
                }
            } 
            else {

                /* Devuelve a la pantalla respectiva, dependiendo del rol del usuario, siempre que el usuario no sea 
                 * nulo y que se intente acceder al login.*/
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