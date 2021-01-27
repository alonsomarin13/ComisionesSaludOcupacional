
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ComisionesSaludOcupacional
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //check for the "file is too big" exception if thrown at the IIS level
            if (Response.StatusCode == 413 && Response.SubStatusCode == 1)
            {
                Response.Redirect("~/AdminNoticia/Error");
                /*Response.Write("El Archivo es demasiado grande. \nEl límite es 10MB, por favor inténtelo de nuevo"); //just an example
                Response.End();*/
            }
        }
    }




}
