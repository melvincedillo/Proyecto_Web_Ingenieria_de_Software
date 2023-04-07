using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Citas()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Horarios()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Usuarios()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}