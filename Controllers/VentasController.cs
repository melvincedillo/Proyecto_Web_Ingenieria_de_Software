using Proyecto_Web_Ingenieria_de_Software.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
  // [ValideSession]
    public class VentasController : Controller
    {
        // GET: Ventas
        //[PermisosModulos(moduloId: 1)]
        public ActionResult Index()
        {
            return View();
        }
    }
}