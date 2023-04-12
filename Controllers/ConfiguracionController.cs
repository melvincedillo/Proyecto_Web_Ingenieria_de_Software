using Proyecto_Web_Ingenieria_de_Software.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.ViewModels;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class ConfiguracionController : Controller
    {
        // GET: Usuarios
        [PermisosModulos(moduloId:8)]
        public ActionResult Usuarios()
        {
            List<UserViewModel> UserList = null;

            using (BeautySalonEntities db = new BeautySalonEntities())
            {
                UserList = (from d in db.Users
                       select new UserViewModel
                       {
                           ID = d.ID,
                           dateCreate = d.UserCreateDate,
                           userName = d.UserName,
                           userActive = d.UserActive
                       }).ToList();
            }

            return View(UserList);
        }

        // GET: Horarios
        [PermisosModulos(moduloId: 6)]
        public ActionResult Horarios()
        {
            return View();
        }

        // GET: General
        [PermisosModulos(moduloId: 7)]
        public ActionResult General()
        {
            return View();
        }
    }
}