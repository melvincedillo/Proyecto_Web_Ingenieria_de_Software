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
            return View();
        }

        public ActionResult Cita()
        {
            return View();
        }
    }
}