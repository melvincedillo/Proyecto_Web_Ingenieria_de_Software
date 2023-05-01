﻿using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.AddModels;
using Proyecto_Web_Ingenieria_de_Software.Models.HorarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    //[ValideSession]
    public class CitasController : Controller
    {
        // GET: Citas
        //[PermisosModulos(moduloId: 4)]
        [HttpGet]
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

        //[PermisosModulos(moduloId: 4)]
        [HttpGet]
        public ActionResult Agregar()
        {
            List<Services> servicios = null;
            using(var db = new BeautySalonEntities())
            {
                servicios = db.Services.ToList();
            }

            ViewBag.Servicios = servicios;
            return View();
        }

        public JsonResult BuscarServicio(int id)
        {
            Services s = null;
            using (var db = new BeautySalonEntities())
            {
                s = db.Services.Find(id);
            }

            if (s != null)
            {
                ServicioModelCita service = new ServicioModelCita();
                service.id = s.ID;
                service.name = s.ServiceName;
                service.descripcion = s.Description;
                service.precio = s.PrecioTotal;
                service.idSkill = s.SkillID;

                return Json(service, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ServicioModelCita service = new ServicioModelCita();
                return Json(service, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ComprobarFecha(DateTime fecha)
        {
            Holiday esFeriado = null;
            bool esLaborable = true;
            int dia = (int)fecha.DayOfWeek;
            List<Horario> horario = null;
            RespDate resp = new RespDate();

            using (var db = new BeautySalonEntities())
            {
                esFeriado = (from d in db.Holiday where d.Date == fecha select d).FirstOrDefault();
                horario = db.Horario.ToList();
            }

            foreach(var h in horario)
            {
                if(h.Day == "Lunes" && dia == 1) { esLaborable = h.Weekday; resp.idDay = h.ID; }
                if(h.Day == "Martes" && dia == 2) { esLaborable = h.Weekday; resp.idDay = h.ID; }
                if(h.Day == "Miercoles" && dia == 3) { esLaborable = h.Weekday; resp.idDay = h.ID; }
                if(h.Day == "Jueves" && dia == 4) { esLaborable = h.Weekday; resp.idDay = h.ID; }
                if(h.Day == "Viernes" && dia == 5) { esLaborable = h.Weekday; resp.idDay = h.ID; }
                if(h.Day == "Sabado" && dia == 6) { esLaborable = h.Weekday; resp.idDay = h.ID; }
                if(h.Day == "Domingo" && dia == 0) { esLaborable = h.Weekday; resp.idDay = h.ID; }
            }

            if(esFeriado != null || esLaborable == false)
            {
                resp.disponible = false;
            }
            else
            {
                resp.disponible = true;
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHoras(int id)
        {
            List<HorasDisponibles> horas = null;
            using (var db = new BeautySalonEntities())
            {
                Horario x = db.Horario.Find(id);
                if(x != null)
                {
                    List<Horas> hora = (from d in db.Horas
                                        where d.ID >= x.OpenTime && d.ID <= x.CloseTime
                                        select d).ToList();

                    horas = hora.ConvertAll(h =>
                    {
                        return new HorasDisponibles()
                        {
                            id = h.ID,
                            hora = h.Hora
                        };
                    });
                }
            }

            return Json(horas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHoraLibreSkill(int idSkill, int idHorario, DateTime fecha)
        {
            int userSkill = 0;
            List<HorasDisponibles> horas = null;
            List<int> horasReservadas = new List<int>();

            using (var db = new BeautySalonEntities())
            {
                //Contando los empreados por skill
                var users = db.Employee.Join(db.Users, em => em.UserID, us => us.ID, (em, us) => new { em, us }).Where(x => x.em.SkillID == idSkill && x.us.UserActive == true).ToList();
                if(users != null) { userSkill = users.Count(); }

                //Obteniendo el horario completo del dia
                Horario hx = db.Horario.Find(idHorario);
                if (hx != null)
                {
                    List<Horas> hora = (from d in db.Horas
                                        where d.ID >= hx.OpenTime && d.ID <= hx.CloseTime
                                        select d).ToList();

                    horas = hora.ConvertAll(h =>
                    {
                        return new HorasDisponibles()
                        {
                            id = h.ID,
                            hora = h.Hora
                        };
                    });
                }

                //Obteniendo 
                var serviciosReservados = (from cita in db.Appointment
                                           join detalle in db.AppointmentDetail on cita.ID equals detalle.AppointmentID
                                           join service in db.Services on detalle.ServicioID equals service.ID
                                           where cita.AppointmentDate == fecha && cita.Status == "Pendiente" && service.SkillID == idSkill
                                           select new
                                           {
                                               cliente = cita.ClientName,
                                               service = service.ServiceName,
                                               hora = detalle.idHora
                                           }).ToList();
            }

            return Json(userSkill, JsonRequestBehavior.AllowGet);
        }
    }
}