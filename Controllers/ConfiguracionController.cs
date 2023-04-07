using Proyecto_Web_Ingenieria_de_Software.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class ConfiguracionController : Controller
    {
        // GET: Usuarios
        [PermisosModulos(moduloId:8)]
        public ActionResult Usuarios()
        {
            return View();
        }

        // GET: Horarios
        [PermisosModulos(moduloId: 6)]
        public ActionResult Horarios()
        {
            return View();
        }

        // GET: General
        [PermisosModulos(moduloId: 7)]
        public ActionResult General()
        {
            return View();
        }
    }
}