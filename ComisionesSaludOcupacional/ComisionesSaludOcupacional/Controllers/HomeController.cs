using ComisionesSaludOcupacional.Models.ClasesUtilidad;
using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ComisionesSaludOcupacional.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {

                model.contrasena = CryptoEngine.Encrypt(model.contrasena, "sxlw-3jn8-sqoy12");

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