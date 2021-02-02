using ComisionesSaludOcupacional.Models.ClasesUtilidad;
using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Controllers
{
    public class HomeController : Controller
    {
        /* Función de controlador tipo GET que se corre al iniciar el sistema, 
         * alza la pantalla principal de login con la imagen del Ministerio.*/
        public ActionResult Index()
        {
            ViewBag.route = Server.MapPath("~") + "\\Views\\Home\\logo.png";
            return View();
        }

        /* Función de controlador tipo POST que realiza la autenticación del usuario, 
         * extrae del modelo los datos ingresados por la persona y revisa que estén correctos con
         * los que están guardados en la base También, realiza la redirección dependiendo si el usuario
         * es administrador, o usuario de comisión.
         Parámetros: modelo que envía la vista */
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {

                model.contrasena = CryptoEngine.Encrypt(model.contrasena);

                var cuenta = from d in db.Cuenta
                             where d.nombre == model.username
                             select d;

                if (cuenta.Count() == 0)
                {
                    ModelState.AddModelError("username", "Usuario inválido");
                    return View(model);
                }

                Cuenta oCuenta = cuenta.First();

                if (oCuenta.contrasena != model.contrasena)
                {
                    ModelState.AddModelError("contrasena", "Contraseña incorrecta");
                    return View(model);
                }

                Session["Usuario"] = oCuenta;

                if (oCuenta.rol == 0)
                {
                    return Redirect(Url.Content("~/AdminComision"));
                } else
                {
                    var comision = (from d in db.Comision
                                    join c in db.Cuenta on d.idCuenta equals oCuenta.idCuenta
                                    select d.idComision);

                    int idComision = comision.First();

                    Session["idComision"] = idComision;
                    return Redirect(Url.Content("~/ComisionUser/InformacionPrincipal/" + idComision));
                }
            }
        }
    }
}