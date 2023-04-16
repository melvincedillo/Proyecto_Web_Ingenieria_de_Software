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
                list = (from d in db.Users
                        select new UserViewModel
                        {
                            ID = d.ID,
                            dateCreate = d.UserCreateDate,
                            userName = d.UserName,
                            userActive = d.UserActive
                        }).ToList();
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
                if (model.ventas == true)
                {
                    oPermiso.ModuleID = 1;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.servicios == true)
                {
                    oPermiso.ModuleID = 2;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.productos == true)
                {
                    oPermiso.ModuleID = 3;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.citas == true)
                {
                    oPermiso.ModuleID = 4;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.reportes == true)
                {
                    oPermiso.ModuleID = 5;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.horarios == true)
                {
                    oPermiso.ModuleID = 6;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.general == true)
                {
                    oPermiso.ModuleID = 7;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.usuarios == true)
                {
                    oPermiso.ModuleID = 8;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }

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

                eModel.ventas = false;
                eModel.servicios = false;
                eModel.productos = false;
                eModel.citas = false;
                eModel.reportes = false;
                eModel.horarios = false;
                eModel.general = false;
                eModel.usuarios = false;

                List<Permissions> modulos = (from d in db.Permissions where d.UserID == id select d).ToList();
                foreach (var element in modulos)
                {
                    if (element.ModuleID == 1) { eModel.ventas = true; }
                    if (element.ModuleID == 2) { eModel.servicios = true; }
                    if (element.ModuleID == 3) { eModel.productos = true; }
                    if (element.ModuleID == 4) { eModel.citas = true; }
                    if (element.ModuleID == 5) { eModel.reportes = true; }
                    if (element.ModuleID == 6) { eModel.horarios = true; }
                    if (element.ModuleID == 7) { eModel.general = true; }
                    if (element.ModuleID == 8) { eModel.usuarios = true; }
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

                //Editando permisos
                List<Permissions> pD = (from d in db.Permissions where d.UserID == model.id select d).ToList();
                foreach (var p in pD)
                {
                    db.Permissions.Remove(p);
                    db.SaveChanges();
                }

                Permissions oPermiso = new Permissions();
                oPermiso.UserID = model.id;
                if (model.ventas == true)
                {
                    oPermiso.ModuleID = 1;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.servicios == true)
                {
                    oPermiso.ModuleID = 2;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.productos == true)
                {
                    oPermiso.ModuleID = 3;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.citas == true)
                {
                    oPermiso.ModuleID = 4;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.reportes == true)
                {
                    oPermiso.ModuleID = 5;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.horarios == true)
                {
                    oPermiso.ModuleID = 6;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.general == true)
                {
                    oPermiso.ModuleID = 7;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }
                if (model.usuarios == true)
                {
                    oPermiso.ModuleID = 8;
                    db.Permissions.Add(oPermiso);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Usuarios");
        }
    }
}