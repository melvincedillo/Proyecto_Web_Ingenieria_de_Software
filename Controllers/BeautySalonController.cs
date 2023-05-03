using Proyecto_Web_Ingenieria_de_Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    public class BeautySalonController : Controller
    {
        // GET: BeautySalon
        public ActionResult Index()
        {
            List<Services> servicios = null;
            using (var db = new BeautySalonEntities())
            {
                servicios = db.Services.ToList();
            }
            ViewBag.Servicios = servicios;
            return View();
        }

        public ActionResult Cita()
        {
            List<Services> servicios = null;
            using (var db = new BeautySalonEntities())
            {
                servicios = db.Services.ToList();
            }

            ViewBag.Servicios = servicios;
            return View();
        }
    }
}