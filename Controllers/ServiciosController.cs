using Proyecto_Web_Ingenieria_de_Software.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class ServiciosController : Controller
    {
        // GET: Servicios
        [PermisosModulos(moduloId: 2)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Agregar
        [PermisosModulos(moduloId: 2)]
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        // GET: Editar
        [PermisosModulos(moduloId: 2)]
        [HttpGet]
        public ActionResult Editar()
        {
            return View();
        }
    }
}