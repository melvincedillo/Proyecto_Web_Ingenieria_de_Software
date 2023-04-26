using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.VentasModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class ProductosController : Controller
    {
        // GET: Productos
        //[PermisosModulos(moduloId: 3)]
        public ActionResult Index()
        {
            List<Products> productList = null;
            using (var db = new BeautySalonEntities())
            {
                productList = db.Products.ToList();
            }
            ViewBag.productos = productList;

            return View();
        }

        // GET: Agregar
        //[PermisosModulos(moduloId: 2)]
        [HttpGet]
        public ActionResult Agregar()
        {
            int code;
            List<Tax> taxs = null;

            using (var db = new BeautySalonEntities())
            {
                taxs = db.Tax.ToList();
                code = db.Services.ToList().Count + 10;
            }

            List<SelectListItem> tax = taxs.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Value = d.ID.ToString(),
                    Text = d.TaxDescription.ToString()
                };
            });

            ViewBag.Tax = tax;
            ViewBag.code = code.ToString();

            return View();
        }

        [HttpPost]
        public void Agregar(Products producto)
        {
            using (var db = new BeautySalonEntities())
            {
                //Creando el nuevo producto
                Products nvoProducto = new Products();
                nvoProducto.Price = producto.Price;
                nvoProducto.ProductName = producto.ProductName;
                nvoProducto.Quantity = producto.Quantity;
                nvoProducto.Sku = producto.Sku;
                nvoProducto.TaxID = producto.TaxID;

                db.Products.Add(nvoProducto);
                db.SaveChanges();

            }
            

        }
    }
}