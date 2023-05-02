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
    [ValideSession]
    public class ServiciosController : Controller
    {
        // GET: Servicios
        [PermisosModulos(moduloId: 2)]
        public ActionResult Index()
        {
            List<Services> servicios = null;
            using(var db = new BeautySalonEntities())
            {
                servicios = db.Services.ToList();
            }
            ViewBag.Servicios = servicios;
            return View();
        }

        // GET: Agregar
        [PermisosModulos(moduloId: 2)]
        [HttpGet]
        public ActionResult Agregar()
        {
            List<Skill> skills = null;
            int code;
            using(var db = new BeautySalonEntities())
            {
                skills = db.Skill.ToList();
                code = db.Services.ToList().Count + 10;
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
            ViewBag.code = code.ToString();
            return View();
        }

        // POST: Agregar
        [PermisosModulos(moduloId: 2)]
        [HttpPost]
        public JsonResult Agregar(Servicio servicio)
        {
            using(var db = new BeautySalonEntities())
            {
                //Creando el servicio
                Services service = new Services();
                service.TaxID = 2;
                service.SkillID = servicio.skill;
                service.ServiceCode = servicio.codigo;
                service.ServiceName = servicio.nombre;
                service.Price = servicio.precio;
                service.PrecioTotal = servicio.total;
                service.Description = servicio.descripcion;
                var s = db.Services.Add(service);
                db.SaveChanges();

                //Agregando los productos
                foreach(var p in servicio.products)
                {
                    ServiceDetail sD = new ServiceDetail();
                    sD.ServiceID = s.ID;
                    sD.ProductID = p.id;
                    sD.Cantidad = p.cantidad;
                    db.ServiceDetail.Add(sD);
                    db.SaveChanges();
                }
            }

            return Json("Servicio agregado con exito", JsonRequestBehavior.AllowGet);
        }

        // GET: Editar
        [PermisosModulos(moduloId: 2)]
        [HttpGet]
        public ActionResult Editar(int id)
        {
            List<Skill> skills = null;
            Services servicio;
            using (var db = new BeautySalonEntities())
            {
                skills = db.Skill.ToList();
                servicio = db.Services.Find(id);
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
            return View(servicio);
        }

        // POST: Editar
        [PermisosModulos(moduloId: 2)]
        [HttpPost]
        public JsonResult Editar(Servicio servicio, int id)
        {
            using (var db = new BeautySalonEntities())
            {
                //Editando el servicio
                Services service = db.Services.Find(id);
                service.SkillID = servicio.skill;
                service.ServiceCode = servicio.codigo;
                service.ServiceName = servicio.nombre;
                service.Price = servicio.precio;
                service.PrecioTotal = servicio.total;
                service.Description = servicio.descripcion;
                db.Entry(service).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //Eliminando el detalle de servicio
                var dsp = (from d in db.ServiceDetail where d.ServiceID == id select d).ToList();
                foreach(var pd in dsp)
                {
                    db.ServiceDetail.Remove(pd);
                    db.SaveChanges();
                }

                //Agregando el detalle del servicio
                foreach (var p in servicio.products)
                {
                    ServiceDetail sD = new ServiceDetail();
                    sD.ServiceID = id;
                    sD.ProductID = p.id;
                    sD.Cantidad = p.cantidad;
                    db.ServiceDetail.Add(sD);
                    db.SaveChanges();
                }
            }

            return Json("Servicio Editado con exito", JsonRequestBehavior.AllowGet);
        }

        // POST: Buscar Producto
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


        public JsonResult buscarServicio(int codigo)
        {
            Services s = null;
            using (var db = new BeautySalonEntities())
            {
                s = (from d in db.Services where d.ID == codigo select d).FirstOrDefault();
            }


            if (s != null)
            {
                /*Services service = new Services();
                service.ID = s.ID;
                service.ServiceName = s.ServiceName;
                service.Price = s.Price;*/

                Servicio srv = new Servicio();
                srv.encontrado = true;
                srv.codigo = s.ID.ToString();
                srv.nombre = s.ServiceName;
                srv.precio = s.Price;


                return Json(srv, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Servicio srv = new Servicio();
                srv.encontrado = false;
                return Json(srv, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Obtener productos
        public JsonResult GetAllProducts(int id)
        {
            List<Productos> productos = new List<Productos>();

            using(var db = new BeautySalonEntities())
            {
                var produ = db.ServiceDetail.Join(db.Products, sd => sd.ProductID, p => p.ID, (sd, p) => new { sd, p }).Where(x => x.sd.ServiceID == id).ToList();

                foreach(var pr in produ)
                {
                    productos.Add(new Productos()
                    {
                        id = pr.p.ID,
                        nombre = pr.p.ProductName,
                        cantidad = pr.sd.Cantidad,
                        precio = pr.p.Price,
                        total = pr.p.Price * pr.sd.Cantidad
                    });
                }
            }

            return Json(productos, JsonRequestBehavior.AllowGet);
        }
    }
}