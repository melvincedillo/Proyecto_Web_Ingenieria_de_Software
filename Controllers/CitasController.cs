using PagedList;
using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.AddModels;
using Proyecto_Web_Ingenieria_de_Software.Models.HorarioModel;
using Proyecto_Web_Ingenieria_de_Software.Models.ViewModels;
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
        public ActionResult Index(int? numPage, DateTime? fecha, int? estado, string buscar)
        {
            numPage = (numPage ?? 1);
            estado = (estado ?? 1);
            IPagedList<Appointment> citas2 = null;
            using(var db = new BeautySalonEntities())
            {
                if(fecha == null)
                {
                    citas2 = (from d in db.Appointment 
                              where d.AppointmentDate >= DateTime.Today
                              where d.ClientName.Contains(buscar)
                              orderby d.AppointmentDate
                              select d).ToPagedList(numPage.Value, 2);
                }
                else
                {
                    citas2 = (from d in db.Appointment 
                              where d.AppointmentDate == fecha
                              where d.ClientName.Contains(buscar)
                              orderby d.AppointmentDate 
                              select d).ToPagedList(numPage.Value, 2);
                }
            }

            ViewBag.Estado = estado;
            if(fecha != null)
            {
                ViewBag.Fecha = DateTime.Parse(fecha.ToString()).ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.Fecha = "";
            }

            return View(citas2);
        }

        [HttpGet]
        //[PermisosModulos(moduloId: 4)]
        public ActionResult Detalles(int id)
        {
            Appointment cita = null;
            List <ViewDetalleCita> detalleCita = null;
            Decimal total = 0;

            using(var db = new BeautySalonEntities())
            {
                cita = db.Appointment.Find(id);

                detalleCita = (from detalle in db.AppointmentDetail
                                    join hora in db.Horas on detalle.idHora equals hora.ID
                                    join servicio in db.Services on detalle.ServicioID equals servicio.ID
                                    where detalle.AppointmentID == id
                                    select new ViewDetalleCita
                                    {
                                        nameService = servicio.ServiceName,
                                        descripcionService = servicio.Description,
                                        precioService = servicio.PrecioTotal,
                                        horaService = hora.Hora
                                    }).ToList();
            }

            foreach(var x in detalleCita)
            {
                total += x.precioService;
            }

            ViewBag.detalle = detalleCita;
            ViewBag.total = total;

            if(cita.Status == "Completada")
            {
                ViewBag.color = "green";
            } else if(cita.Status == "Pendiente")
            {
                ViewBag.color = "blue";
            } else
            {
                ViewBag.color = "red";
            }

            return View(cita);
        }

        public ActionResult Cancelar(int id)
        {
            using(var db = new BeautySalonEntities())
            {
                Appointment cita = db.Appointment.Find(id);
                cita.Status = "Cancelada";

                db.Entry(cita).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Citas");
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

        [HttpPost]
        //[PermisosModulos(moduloId: 4)]
        public JsonResult Agregar(Cita cita)
        {
            using(var db = new BeautySalonEntities())
            {
                Appointment nCita = new Appointment();
                nCita.ClientName = cita.name;
                nCita.PhoneNumber = cita.phone;
                nCita.Status = "Pendiente";
                nCita.AppointmentDate = cita.fecha;

                Appointment citaGuardada = db.Appointment.Add(nCita);
                db.SaveChanges();

                foreach(var dc in cita.servicios)
                {
                    AppointmentDetail detalleCita = new AppointmentDetail();
                    detalleCita.AppointmentID = citaGuardada.ID;
                    detalleCita.ServicioID = dc.id;
                    detalleCita.idHora = dc.hora;

                    db.AppointmentDetail.Add(detalleCita);
                    db.SaveChanges();
                }
            }
            return Json("Cita guardada con exito", JsonRequestBehavior.AllowGet);
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
                                        where d.ID >= hx.OpenTime && d.ID < hx.CloseTime
                                        select d).ToList();

                    horas = hora.ConvertAll(h =>
                    {
                        return new HorasDisponibles()
                        {
                            id = h.ID,
                            hora = h.Hora,
                            personal = userSkill
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

                if(serviciosReservados != null)
                {
                    foreach (var element in horas)
                    {
                        foreach (var sr in serviciosReservados)
                        {
                            if (sr.hora == element.id)
                            {
                                element.personal--;
                            }
                        }
                    }
                }

            }

            return Json(horas, JsonRequestBehavior.AllowGet);
        }
    }
}