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

            return View();
        }
    }
}