using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.AddModels;
using Proyecto_Web_Ingenieria_de_Software.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        [PermisosModulos(moduloId: 8)]
        public ActionResult Index()
        {
            List<UserViewModel> list = null;

            using (BeautySalonEntities db = new BeautySalonEntities())
            {
                var e = db.Employee.Join(db.Users,
                    em => em.UserID,
                    us => us.ID, (em, us) => new { em, us }).ToList();


                list = e.ConvertAll(d =>
                {
                    return new UserViewModel()
                    {
                        ID = d.em.UserID,
                        nombre = d.em.FirstName + " " + d.em.LastName,
                        userActive = d.us.UserActive,
                        userName = d.us.UserName,
                        dateCreate = d.us.UserCreateDate.ToString("dd-MM-yyyy")
                    };
                });    
            }

            return View(list);
        }

        // GET: Agregar
        [PermisosModulos(moduloId: 8)]
        [HttpGet]
        public ActionResult Agregar()
        {
            List<SkillViewModel> skills = null;
            using (BeautySalonEntities db = new BeautySalonEntities())
            {
                skills = (from d in db.Skill
                          select new SkillViewModel
                          {
                              ID = d.ID,
                              SkillName = d.SkillName
                          }).ToList();
            }

            List<SelectListItem> items = skills.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.SkillName.ToString(),
                    Value = d.ID.ToString(),
                    Selected = false
                };
            });

            ViewData["Skills"] = items;
            return View();
        }

        // POST: Agregar
        [PermisosModulos(moduloId: 8)]
        [HttpPost]
        public ActionResult Agregar(UserModel model, int skill)
        {
            if (!ModelState.IsValid)
            {
                List<SkillViewModel> skills = null;
                using (BeautySalonEntities db = new BeautySalonEntities())
                {
                    skills = (from d in db.Skill
                              select new SkillViewModel
                              {
                                  ID = d.ID,
                                  SkillName = d.SkillName
                              }).ToList();
                }

                List<SelectListItem> items = skills.ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.SkillName.ToString(),
                        Value = d.ID.ToString(),
                        Selected = false
                    };
                });

                ViewData["Skills"] = items;
                return View(model);
            }

            using (var db = new BeautySalonEntities())
            {
                //Se crear el usuarios
                Users oUser = new Users();
                oUser.UserActive = true;
                oUser.UserCreateDate = DateTime.Today;
                oUser.UserName = model.usuario;
                oUser.UserEmail = model.correo;
                oUser.UserPassword = model.contraseña;

                var user = db.Users.Add(oUser);
                db.SaveChanges();

                //Se le asignan los permisos
                Permissions oPermiso = new Permissions();
                oPermiso.UserID = user.ID;

                //Ventas
                oPermiso.ModuleID = 1;
                oPermiso.estado = model.ventas;
                db.Permissions.Add(oPermiso);
                db.SaveChanges();

                //Servicos
                oPermiso.ModuleID = 2;
                oPermiso.estado = model.servicios;
                db.Permissions.Add(oPermiso);
                db.SaveChanges();

                //Productos
                oPermiso.ModuleID = 3;
                oPermiso.estado = model.productos;
                db.Permissions.Add(oPermiso);
                db.SaveChanges();

                //Citas
                oPermiso.ModuleID = 4;
                oPermiso.estado = model.citas;
                db.Permissions.Add(oPermiso);
                db.SaveChanges();

                //Reportes
                oPermiso.ModuleID = 5;
                oPermiso.estado = model.reportes;
                db.Permissions.Add(oPermiso);
                db.SaveChanges();

                //Horarios
                oPermiso.ModuleID = 6;
                oPermiso.estado = model.horarios;
                db.Permissions.Add(oPermiso);
                db.SaveChanges();

                //General
                oPermiso.ModuleID = 7;
                oPermiso.estado = model.general;
                db.Permissions.Add(oPermiso);
                db.SaveChanges();

                //Usuarios
                oPermiso.ModuleID = 8;
                oPermiso.estado = model.usuarios;
                db.Permissions.Add(oPermiso);
                db.SaveChanges();

                //Se crea el empleado
                Employee oEmployee = new Employee();
                oEmployee.FirstName = model.nombre;
                oEmployee.LastName = model.apellido;
                oEmployee.DNI = model.DNI;
                oEmployee.Gender = model.sexo;
                oEmployee.PhoneNumber = model.telefono;
                oEmployee.UserID = user.ID;
                oEmployee.SkillID = skill;

                db.Employee.Add(oEmployee);

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Usuarios");
        }

        // GET: Editar
        [PermisosModulos(moduloId: 8)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditUserModel eModel = new EditUserModel();
            using (var db = new BeautySalonEntities())
            {
                var empleado = db.Employee.Join(db.Users, em => em.UserID, us => us.ID, (em, us) => new { em, us }).FirstOrDefault(x => x.em.UserID == id);
                eModel.nombre = empleado.em.FirstName;
                eModel.apellido = empleado.em.LastName;
                eModel.DNI = empleado.em.DNI;
                eModel.sexo = empleado.em.Gender;
                eModel.telefono = empleado.em.PhoneNumber;
                eModel.usuario = empleado.us.UserName;
                eModel.correo = empleado.us.UserEmail;
                eModel.skillId = empleado.em.SkillID;
                eModel.id = empleado.us.ID;
                eModel.idEmpleado = empleado.em.ID;

                //Mapeando permisos
                List<Permissions> permisos = (from d in db.Permissions where d.UserID == id select d).ToList();
                foreach (var p in permisos)
                {
                    if (p.ModuleID == 1) { p.estado = eModel.ventas; }
                    else if (p.ModuleID == 2) { eModel.servicios = p.estado; }
                    else if (p.ModuleID == 3) { eModel.productos = p.estado; ; }
                    else if (p.ModuleID == 4) { eModel.citas = p.estado; ; }
                    else if (p.ModuleID == 5) { eModel.reportes = p.estado; ; }
                    else if (p.ModuleID == 6) { eModel.horarios = p.estado; ; }
                    else if (p.ModuleID == 7) { eModel.general = p.estado; ; }
                    else if (p.ModuleID == 8) { eModel.usuarios = p.estado; ; }

                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

            List<Skill> items = null;
            using (BeautySalonEntities db = new BeautySalonEntities())
            {
                items = db.Skill.ToList();
            }
            ViewData["Skills"] = items;

            return View(eModel);
        }

        [PermisosModulos(moduloId: 8)]
        [HttpPost]
        public ActionResult Edit(EditUserModel model, int idSkill)
        {
            if (!ModelState.IsValid)
            {
                List<Skill> items = null;
                using (BeautySalonEntities db = new BeautySalonEntities())
                {
                    items = db.Skill.ToList();
                }
                ViewData["Skills"] = items;

                return View(model);
            }

            using (var db = new BeautySalonEntities())
            {
                //Editando usuario
                Users u = db.Users.Find(model.id);
                u.UserEmail = model.correo;
                u.UserName = model.usuario;
                if (model.contraseña != null && model.contraseña.Trim() != "")
                {
                    u.UserPassword = model.contraseña;
                }
                db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //Editando Empleado
                Employee e = db.Employee.Find(model.idEmpleado);
                e.FirstName = model.nombre;
                e.LastName = model.apellido;
                e.PhoneNumber = model.telefono;
                e.DNI = model.DNI;
                e.Gender = model.sexo;
                e.SkillID = Convert.ToInt32(idSkill);
                db.Entry(e).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //Editando permisos
                List<Permissions> pD = (from d in db.Permissions where d.UserID == model.id select d).ToList();
                foreach (var p in pD)
                {
                    if(p.ModuleID == 1) { p.estado = model.ventas; }
                    else if (p.ModuleID == 2) { p.estado = model.servicios; }
                    else if (p.ModuleID == 3) { p.estado = model.productos; }
                    else if (p.ModuleID == 4) { p.estado = model.citas; }
                    else if (p.ModuleID == 5) { p.estado = model.reportes; }
                    else if (p.ModuleID == 6) { p.estado = model.horarios; }
                    else if (p.ModuleID == 7) { p.estado = model.general; }
                    else if (p.ModuleID == 8) { p.estado = model.usuarios; }

                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Usuarios");
        }

        [PermisosModulos(moduloId: 8)]
        public ActionResult CambiarEstado(int id)
        {
            using(var db = new BeautySalonEntities())
            {
                Users u = db.Users.Find(id);
                if(u.UserActive == true)
                {
                    u.UserActive = false;
                } else
                {
                    u.UserActive = true;
                }
                db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}