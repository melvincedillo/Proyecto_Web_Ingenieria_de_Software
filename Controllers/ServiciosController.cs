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
            ViewBag.code = "S4567";
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Servicio servicio)
        {
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
        public JsonResult BuscarProducto(string codigo)
        {
            Products p = null;
            using (var db = new BeautySalonEntities()) {
                p = (from d in db.Products where d.Sku == codigo select d).FirstOrDefault();
            }

            if(p != null)
            {
                Product product = new Product();
                product.ID = p.ID;
                product.ProductName = p.ProductName;
                product.Price = p.Price;
                product.Sku = p.Sku;
                product.encontrado = true;

                return Json(product, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Product product = new Product();
                product.encontrado = false;
                return Json(product, JsonRequestBehavior.AllowGet);
            }
        }
    }
}