using Proyecto_Web_Ingenieria_de_Software.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class AdministracionController : Controller
    {
        // GET: Citas
        [PermisosModulos(moduloId: 4)]
        public ActionResult Citas()
        {
            return View();
        }

        // GET: Ventas
        [PermisosModulos(moduloId: 1)]
        public ActionResult Ventas()
        {
            return View();
        }

        // GET: Productos
        [PermisosModulos(moduloId: 3)]
        public ActionResult Productos()
        {
            return View();
        }

        // GET: Servicios
        [PermisosModulos(moduloId: 2)]
        public ActionResult Servicios()
        {
            return View();
        }
    }
}