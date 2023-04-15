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
        public ActionResult Agregar(UserModel model)
        {
            if (!ModelState.IsValid)
            {
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
                /*
                Permissions oPermiso = new Permissions();
                oPermiso.UserID = user.ID;
                if(model.ventas == true)
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
                */

                //Se crea el empleado
                Employee oEmployee = new Employee();
                oEmployee.FirstName = model.nombre;
                oEmployee.LastName = model.apellido;
                oEmployee.DNI = model.DNI;
                oEmployee.Gender = model.sexo;
                oEmployee.PhoneNumber = model.telefono;
                oEmployee.UserID = user.ID;
                oEmployee.SkillID = 2;

                db.Employee.Add(oEmployee);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index", "Usuarios");
        }
    }
}