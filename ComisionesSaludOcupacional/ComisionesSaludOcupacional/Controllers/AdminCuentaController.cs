using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;
using ComisionesSaludOcupacional.Models.ClasesUtilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Helpers;
using System.Net.Mail;

namespace ComisionesSaludOcupacional.Controllers
{
    public class AdminCuentaController : Controller
    {
        // GET: AdminCuenta
        public ActionResult Index(string nombreComision)
        {
            List<CuentaTableViewModel> lista = null;
            using (var db = new SaludOcupacionalEntities())
            {
                var cuentas = from d in db.Cuenta
                              join c in db.Comision on d.idCuenta equals c.idCuenta
                              join ct in db.CentroDeTrabajo on c.idCentroDeTrabajo equals ct.idCentroDeTrabajo
                              orderby d.idCuenta
                              select new CuentaTableViewModel
                              {
                                  idCuenta = d.idCuenta,
                                  nombre = d.nombre,
                                  nombreComision = ct.nombreCentroDeTrabajo
                              };

                if (!String.IsNullOrEmpty(nombreComision))
                {
                    cuentas = cuentas.Where(d => d.nombre.Contains(nombreComision));
                }

                lista = cuentas.ToList();
            }

            return View(lista);
        }

        [HttpGet]
        public ActionResult AddAdminUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdminUser(CuentaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {

                var usernameExists = db.Cuenta.Any(x => x.nombre == model.nombre);
                if(usernameExists)
                {
                    ModelState.AddModelError("nombre", "Este nombre de usuario ya existe");
                    return View(model);
                }

                Cuenta oCuenta = new Cuenta();

                oCuenta.nombre = model.nombre;
                oCuenta.contrasena = CryptoEngine.Encrypt(model.contrasena, "sxlw-3jn8-sqoy12");
                oCuenta.rol = 0;
                db.Cuenta.Add(oCuenta);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/AdminCuenta"));
        }

        [HttpGet]
        public ActionResult EnviarCorreo(int Id)
        {
            CuentaPopupViewModel model = new CuentaPopupViewModel();
            using (var db = new SaludOcupacionalEntities())
            {
                Cuenta oCuenta = db.Cuenta.Find(Id);
                model.password = CryptoEngine.Decrypt(oCuenta.contrasena, "sxlw-3jn8-sqoy12");
                model.nombre = oCuenta.nombre;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EnviarCorreo(CuentaPopupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string subject = "Credenciales de " + model.nombre;
            string mensaje = "Usted ha solitado las credenciales del sistema para el usuario: " + model.nombre + "\r\n\r\nUsuario: " + model.nombre + "\nContraseña: " + model.password + "\r\n\r\nSi ha recibido este correo por error, por favor ignórelo.";
            string body = mensaje.Replace("\n", "<br />");
            SmtpClient cliente = new SmtpClient("smtp-mail.outlook.com");
            cliente.Port = 587;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.UseDefaultCredentials = false;
            System.Net.NetworkCredential credencial = new System.Net.NetworkCredential("comisionessaludocupacional@outlook.com","lastword1");
            cliente.EnableSsl = true;
            cliente.Credentials = credencial;

            MailMessage correo = new MailMessage("comisionessaludocupacional@outlook.com",model.useremail);
            correo.Subject = subject;
            correo.Body = body;
            correo.IsBodyHtml = true;
            cliente.Send(correo);
            ViewBag.mensaje = "Correo enviado satisfactoriamente.";

            return View(model);
        }
    }
}