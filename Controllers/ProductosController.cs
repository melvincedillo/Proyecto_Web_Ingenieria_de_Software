using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.VentasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class ProductosController : Controller
    {
        // GET: Productos
        [PermisosModulos(moduloId: 3)]
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
        [PermisosModulos(moduloId: 3)]
        [HttpGet]
        public ActionResult Agregar()
        {
            cargarTaxes();
            return View();
        }


        [HttpPost]
        [PermisosModulos(moduloId: 3)]
        public ActionResult Agregar(Products producto)
        {
            try
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

                ViewBag.code = "1";
                ViewBag.messageSave = "Nuevo producto agregado con éxito";
            }
            catch(Exception ex)
            {
                ViewBag.code = "0";
                ViewBag.messageSave = ex.Message;
            }

            cargarTaxes();
            return View();

        }

        [HttpGet]
        [PermisosModulos(moduloId: 3)]
        public ActionResult Editar(int id)
        {
            Products product = null;
            cargarTaxes();            
            using (var db = new BeautySalonEntities())
            {
                product = db.Products.Find(id);
            }
                
            return View(product);
        }


        [HttpPost]
        [PermisosModulos(moduloId: 3)]
        public ActionResult Editar(Products productUpdate)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BeautySalonEntities())
                {
                   var product = db.Products.Find(productUpdate.ID);
                   product.ProductName = productUpdate.ProductName;
                   product.Price = productUpdate.Price;
                   product.Quantity = productUpdate.Quantity;
                   product.Sku = productUpdate.Sku;
                   product.TaxID = productUpdate.TaxID;

                   db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(productUpdate);
            }

            
        }



        [HttpGet]
        [PermisosModulos(moduloId: 3)]
        public ActionResult Delete(int id)
        {
            using (var db = new BeautySalonEntities())
            {
                var product = db.Products.Find(id);
                if(product == null)
                {
                    return HttpNotFound();
                }
                db.Products.Remove(product);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }





        private void cargarTaxes()
        {
            List<Tax> taxs = null;            

            using (var db = new BeautySalonEntities())
            {
                taxs = db.Tax.ToList();            
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

        }
    }
}