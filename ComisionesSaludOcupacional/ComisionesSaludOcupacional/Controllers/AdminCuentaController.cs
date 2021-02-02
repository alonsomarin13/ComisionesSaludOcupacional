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
        /* Función de controlador tipo GET que abre la vista principal del módulo de cuentas,
         * permite ver todas las cuentas de comisión que existen, filtrarlas y recuperar la contraseña.
         Parámetros: nombre de la comisión a filtrar*/
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

        /* Función de controlador tipo GET que abre la vista de creación de cuentas de
         * administrador. */
        [HttpGet]
        public ActionResult AddAdminUser()
        {
            return View();
        }

        /* Función de controlador tipo POST que realiza la creación de la cuenta de administrador, 
         * extrae del modelo los datos ingresados por la persona y los guarda en la base.
         Parámetros: modelo que envía la vista*/
        [HttpPost]
        public ActionResult AddAdminUser(CuentaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new SaludOcupacionalEntities())
            {
                // Revisa si el nombre de usuario ya existe
                var usernameExists = db.Cuenta.Any(x => x.nombre == model.nombre);
                if(usernameExists)
                {
                    ModelState.AddModelError("nombre", "Este nombre de usuario ya existe");
                    return View(model);
                }

                Cuenta oCuenta = new Cuenta();

                oCuenta.nombre = model.nombre;
                oCuenta.contrasena = CryptoEngine.Encrypt(model.contrasena);
                oCuenta.rol = 0; // Rol 0 significa "administrador"
                db.Cuenta.Add(oCuenta);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/AdminCuenta"));
        }

        /* Función de controlador tipo GET que abre la vista para enviar un correo de recuperación de
         * contraseña. Permite enviarle al correo especificado de un usuario, la contraseña de su cuenta.
         Parámetros: Id de la cuenta que se desea recuperar*/
        [HttpGet]
        public ActionResult EnviarCorreo(int Id)
        {
            CuentaPopupViewModel model = new CuentaPopupViewModel();
            using (var db = new SaludOcupacionalEntities())
            {
                Cuenta oCuenta = db.Cuenta.Find(Id);
                model.password = CryptoEngine.Decrypt(oCuenta.contrasena);
                model.nombre = oCuenta.nombre;
            }

            return View(model);
        }

        /* Función de controlador tipo POST que realiza el envío del correo electrónico
         * a una dirección especificada, con la contraseña y el usuario de la cuenta solicitada.
         Parámetros: modelo que envía la vista*/
        [HttpPost]
        public ActionResult EnviarCorreo(CuentaPopupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Motivo del correo
            string subject = "Ministerio de Seguridad Pública | Sistema de Comisiones de Salud Ocupacional | Credenciales de " + model.nombre;
           
            // Contenido del correo
            string mensaje = "Usted ha solitado las credenciales del sistema para el usuario: " + model.nombre + "\r\n\r\nUsuario: " + model.nombre + "\nContraseña: " + model.password + "\r\n\r\nSi ha recibido este correo por error, por favor ignórelo.";
            
            // Se reemplazan espacios por "<br />" para que aparezcan con saltos de línea en el correo
            string body = mensaje.Replace("\n", "<br />");

            // Para enviar el correo se utilizan funciones de "SmtpClient" clase de C# que permite enviar correos.
            SmtpClient cliente = new SmtpClient("smtp-mail.outlook.com");
            cliente.Port = 587;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.UseDefaultCredentials = false;

            /* Correo y contraseña de donde se quiere enviar la notificación. Es importante que se deshabilite las opciones de seguridad pertinentes
             * en las configuraciones de la cuenta, ya sea en Gmail, Hotmail, etc...*/ 
            System.Net.NetworkCredential credencial = new System.Net.NetworkCredential("comisionessaludocupacional@outlook.com","lastword1");
            cliente.EnableSsl = true;
            cliente.Credentials = credencial;

            MailMessage correo = new MailMessage("comisionessaludocupacional@outlook.com",model.useremail);
            correo.Subject = subject;
            correo.Body = body;
            correo.IsBodyHtml = true;

            // Acá se envía el correo.
            cliente.Send(correo);
            ViewBag.mensaje = "Correo enviado satisfactoriamente.";

            return View(model);
        }
    }
}