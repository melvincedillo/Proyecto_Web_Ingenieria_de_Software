using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.AddModels;
using Proyecto_Web_Ingenieria_de_Software.Models.ViewModels;
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
                              orderby d.Date
                              select d).ToList();
            }

            ViewBag.Horarios = listHorario;
            ViewBag.Holidays = listHoliday;
            return View();
        }

        // POST: Add Holiday
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

        // POST: Delete Holiday
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

        // GET: Congiguracion Horario
        [PermisosModulos(moduloId: 6)]
        [HttpGet]
        public ActionResult Configuracion()
        {
            List<HorarioViewModel> listHorario = null;
            List<Horas> horas = null;

            using (var db = new BeautySalonEntities())
            {
                listHorario = (from d in db.Horario
                               orderby d.ID 
                               select new HorarioViewModel { 
                                   id = d.ID,
                                   day = d.Day,
                                   open = d.OpenTime,
                                   close = d.CloseTime,
                                   laborable = d.Weekday,
                                   nameClose = d.Day + "C",
                                   nameOpen = d.Day + "O"
                               }).ToList();

                horas = db.Horas.ToList();
            }

            ViewBag.Horarios = listHorario;
            ViewBag.Horas = horas;
            return View();
        }

        // POST: Congiguracion Horario
        [PermisosModulos(moduloId: 6)]
        [HttpPost]
        public ActionResult Configuracion(
            string Lunes, string LunesO, string LunesC,
            string Martes, string MartesO, string MartesC,
            string Miercoles, string MiercolesO, string MiercolesC,
            string Jueves, string JuevesO, string JuevesC,
            string Viernes, string ViernesO, string ViernesC,
            string Sabado, string SabadoO, string SabadoC,
            string Domingo, string DomingoO, string DomingoC
            )
        {
            using (var db = new BeautySalonEntities())
            {
                // Lunes
                var lunes = db.Horario.Find(8);
                lunes.OpenTime = LunesO;
                lunes.CloseTime = LunesC;
                if(Lunes == "1") { lunes.Weekday = true; } else { lunes.Weekday = false; }
                db.Entry(lunes).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Martes
                var martes = db.Horario.Find(9);
                martes.OpenTime = MartesO;
                martes.CloseTime = MartesC;
                if (Martes == "1") { martes.Weekday = true; } else { martes.Weekday = false; }
                db.Entry(martes).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Miercoles
                var miercoles = db.Horario.Find(10);
                miercoles.OpenTime = MiercolesO;
                miercoles.CloseTime = MiercolesC;
                if (Miercoles == "1") { miercoles.Weekday = true; } else { miercoles.Weekday = false; }
                db.Entry(miercoles).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Jueves
                var jueves = db.Horario.Find(11);
                jueves.OpenTime = JuevesO;
                jueves.CloseTime = JuevesC;
                if (Jueves == "1") { jueves.Weekday = true; } else { jueves.Weekday = false; }
                db.Entry(martes).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Viernes
                var viernes = db.Horario.Find(12);
                viernes.OpenTime = ViernesO;
                viernes.CloseTime = ViernesC;
                if (Viernes == "1") { viernes.Weekday = true; } else { viernes.Weekday = false; }
                db.Entry(viernes).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Sabado
                var sabado = db.Horario.Find(13);
                sabado.OpenTime = SabadoO;
                sabado.CloseTime = SabadoC;
                if (Sabado == "1") { sabado.Weekday = true; } else { sabado.Weekday = false; }
                db.Entry(sabado).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Domingo
                var domingo = db.Horario.Find(14);
                domingo.OpenTime = DomingoO;
                domingo.CloseTime = DomingoC;
                if (Domingo == "1") { domingo.Weekday = true; } else { domingo.Weekday = false; }
                db.Entry(domingo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}