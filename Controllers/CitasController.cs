using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class CitasController : Controller
    {
        // GET: Citas
        [PermisosModulos(moduloId: 4)]
        public ActionResult Index()
        {
            List<Appointment> citas = null;

            using(var db = new BeautySalonEntities())
            {
                citas = (from d in db.Appointment select d).ToList();
            }
            ViewBag.Citas = citas;

            return View();
        }

        [PermisosModulos(moduloId: 4)]
        public ActionResult Agregar()
        {
            return View();
        }
    }
}