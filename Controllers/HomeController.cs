﻿using System;
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

        public ActionResult Agendar_cita()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ModuloCitas()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ModuloHorarios()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}