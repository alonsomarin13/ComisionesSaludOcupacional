﻿using ComisionesSaludOcupacional.Models.ET01;
using ComisionesSaludOcupacional.Models.ViewModels;
using ComisionesSaludOcupacional.Models.ClasesUtilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                              join c in db.Comision on d.idComision equals c.idComision
                              join ct in db.CentroDeTrabajo on c.idCentroDeTrabajo equals ct.idCentroDeTrabajo
                              orderby d.idCuenta
                              select new CuentaTableViewModel
                              {
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

                Cuenta oCuenta = new Cuenta();

                oCuenta.nombre = model.nombre;
                oCuenta.contrasena = CryptoEngine.Encrypt(model.contrasena, "sxlw-3jn8-sqoy12");
                oCuenta.rol = 0;
                oCuenta.idComision = null;
                db.Cuenta.Add(oCuenta);

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/AdminCuenta"));
        }
    }
}