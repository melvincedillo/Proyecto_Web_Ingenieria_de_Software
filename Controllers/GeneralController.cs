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
    public class GeneralController : Controller
    {
        // GET: General
        [PermisosModulos(moduloId: 7)]
        public ActionResult Index()
        {
            List<Salon> ListSalon = null;

            using (var db = new BeautySalonEntities())
            {
                ListSalon = db.Salon.ToList();
            }

            ViewBag.Salon = ListSalon;
            return View();


        }

        [HttpGet]
        [PermisosModulos(moduloId: 7)]
        public ActionResult Editar(int id)
        {
            Salon salon = null;
            using (var db = new BeautySalonEntities())
            {
                salon = db.Salon.Find(id);
            }


            return View(salon);


        }

        [HttpPost]
        [PermisosModulos(moduloId: 7)]
        public ActionResult Editar(Salon salonUpdate)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BeautySalonEntities())
                {
                    var salon = db.Salon.Find(salonUpdate.ID);
                    salon.Address = salonUpdate.Address;
                    salon.Email = salonUpdate.Email;
                    salon.Facebook = salonUpdate.Facebook;
                    salon.Instagram = salonUpdate.Instagram;
                    salon.Mision = salonUpdate.Mision;

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(salonUpdate);
            }

        }
    }
}