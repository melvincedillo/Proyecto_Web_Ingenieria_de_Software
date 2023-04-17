using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models.VentasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Web_Ingenieria_de_Software.Models.ViewModels;
using Proyecto_Web_Ingenieria_de_Software.Models;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class VentasController : Controller
    {
        // GET: Ventas
        //[PermisosModulos(moduloId: 1)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: CrearVenta
        //[PermisosModulos(moduloId: 1)]
        public ActionResult CrearVenta()
        {

            List<ProductsViewModel> lst = null;
            using (Models.BeautySalonEntities db = new Models.BeautySalonEntities())
            {
                lst = (from d in db.Products
                       select new ProductsViewModel
                       {
                           ID = d.ID,
                           ProductName = d.ProductName,
                           Price = (double)d.Price
                       }).ToList();

            }


            ViewBag.listaProductos = lst;

            //ViewBag.listaProductos = new SelectList(lst,"ID", "ProductName");

            return View();
        }


        [HttpPost]
        public void Agregar(FacturaViewModel factura, List <FacturaDetalleViewModel> detalle)
        {
            double subTotal=0;
            foreach(var i in detalle)
            {
                subTotal = i.Price + subTotal;
            }
            using (var db = new BeautySalonEntities())
            {
                Factura oFactura = new Factura();
                

                oFactura.Time = DateTime.Now;
                oFactura.Total = (decimal)subTotal;
                oFactura.Tax = (decimal)factura.Tax;
                oFactura.ClientName = factura.ClientName;
                oFactura.SalonID = 1;
                oFactura.EmployeeID = 2;

                db.Factura.Add(oFactura);
                db.SaveChanges();

                int idFactura = db.Factura.Max(x => x.FacturaNumero);

                //detallefactura
                FacturaDetalle oDetalle = null;
                foreach (var i in detalle)
                {
                   oDetalle= new FacturaDetalle();

                    oDetalle.FacturaNumero = idFactura;
                    oDetalle.SalonID = oDetalle.SalonID;
                    oDetalle.ProductID = oDetalle.ProductID;
                    oDetalle.ServiceID = oDetalle.ServiceID;
                    oDetalle.TaxID = oDetalle.TaxID;
                    oDetalle.Price = oDetalle.Price;
                    oDetalle.Quantity = oDetalle.Quantity;
                    oDetalle.Discount = oDetalle.Discount;
                    oDetalle.SalesTax = oDetalle.SalesTax;

                    var detalleFact = db.FacturaDetalle.Add(oDetalle);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }


    }
}