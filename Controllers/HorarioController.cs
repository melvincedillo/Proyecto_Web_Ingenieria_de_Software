using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.AddModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class HorarioController : Controller
    {
        // GET: Horario
        [PermisosModulos(moduloId: 6)]
        [HttpGet]
        public ActionResult Index()
        {
            List<Horario> listHorario = null;
            List<Holiday> listHoliday = null;

            using (var db = new BeautySalonEntities())
            {
                listHorario = (from d in db.Horario
                               where d.Weekday == true
                               select d).ToList();

                listHoliday = (from d in db.Holiday
                              where d.Date >= DateTime.Today
                              select d).ToList();
            }

            ViewBag.Horarios = listHorario;
            ViewBag.Holidays = listHoliday;
            return View();
        }

        // POST: Horarios
        [PermisosModulos(moduloId: 6)]
        [HttpPost]
        public ActionResult AddHoliday(DateTime Date, string motivo)
        {
            try
            {
                if(Date != null && motivo != null)
                {
                    using (var db = new BeautySalonEntities())
                    {
                        var holiday = new Holiday();
                        holiday.Date = Date;
                        holiday.Description = motivo;
                        db.Holiday.Add(holiday);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Horario");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Horario");
            }
            
        }

        [PermisosModulos(moduloId: 6)]
        [HttpPost]
        public ActionResult DeleteHoliday(int id)
        {
            using (var db = new BeautySalonEntities())
            {
                Holiday holiday = db.Holiday.Find(id);
                db.Holiday.Remove(holiday);
                db.SaveChanges();
            }

            return Content("1");
        }
    }
}