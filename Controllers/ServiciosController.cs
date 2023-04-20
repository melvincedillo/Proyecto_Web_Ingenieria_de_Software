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
    //[ValideSession]
    public class ServiciosController : Controller
    {
        // GET: Servicios
        //[PermisosModulos(moduloId: 2)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Agregar
        //[PermisosModulos(moduloId: 2)]
        [HttpGet]
        public ActionResult Agregar()
        {
            List<Skill> skills = null;
            using(var db = new BeautySalonEntities())
            {
                skills = db.Skill.ToList();
            }
            List<SelectListItem> skill = skills.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Value = d.ID.ToString(),
                    Text = d.SkillName.ToString()
                };
            });
            ViewBag.Skill = skill;

            return View();
        }

        // POST: Agregar
        //[PermisosModulos(moduloId: 2)]
        [HttpPost]
        public ActionResult Agregar(ServiciosModel model)
        {
            if (!ModelState.IsValid)
            {
                List<Skill> skills = null;
                using (var db = new BeautySalonEntities())
                {
                    skills = db.Skill.ToList();
                }
                List<SelectListItem> skill = skills.ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Value = d.ID.ToString(),
                        Text = d.SkillName.ToString()
                    };
                });
                ViewBag.Skill = skill;
                return View(model);
            }

            return View();
        }

        // GET: Editar
        //[PermisosModulos(moduloId: 2)]
        [HttpGet]
        public ActionResult Editar()
        {
            return View();
        }

        // POST: Buscar Producto
        //[PermisosModulos(moduloId: 2)]
        [HttpPost]
        public ActionResult BuscarProducto(string search)
        {
            Products p = null;
            using (var db = new BeautySalonEntities()) {
                p = (from d in db.Products where d.Sku == search.ToString() select d).FirstOrDefault();
            }

            return RedirectToAction("Agregar", p);
        }
    }
}