using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models.VentasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Web_Ingenieria_de_Software.Models.ViewModels;
using Proyecto_Web_Ingenieria_de_Software.Models;
using Proyecto_Web_Ingenieria_de_Software.Models.AddModels;
using Newtonsoft.Json;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    [ValideSession]
    public class VentasController : Controller
    {
        // GET: Ventas
       // [PermisosModulos(moduloId: 1)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: CrearVenta
       // [PermisosModulos(moduloId: 1)]
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



        //public void Agregar(FacturaViewModel factura, List <FacturaDetalleViewModel> detalle)
      //  [PermisosModulos(moduloId: 1)]
        [HttpPost]
        public JsonResult Agregar(FacturaModel factura)
        {   

            using (var db = new BeautySalonEntities())
            {
                Factura oFactura = new Factura();
                

                oFactura.Time = DateTime.Now;
                oFactura.Total = (decimal)factura.total;
                oFactura.Tax = (decimal)factura.tax;
                oFactura.ClientName = factura.clientName;
                oFactura.SalonID = 1;
                oFactura.EmployeeID = 7;

                var lastSave = db.Factura.Add(oFactura);
                db.SaveChanges();

                int idFactura = lastSave.FacturaNumero;

                //detallefactura
                FacturaDetalle oDetalle = null;                
                foreach (var i in factura.detalleFactura)
                {
                    oDetalle= new FacturaDetalle();

                    oDetalle.FacturaNumero = idFactura;                    
                    oDetalle.Price = i.precio;
                    oDetalle.Quantity = (int)i.cantidad;
                    oDetalle.itemSale = i.Sku;
                    
                    var detalleFact = db.FacturaDetalle.Add(oDetalle);
                    db.SaveChanges();
                }
            }

            return Json("Servicio agregado con exito", JsonRequestBehavior.AllowGet);
        }



        public ActionResult BuscarVenta()
        {
            List<FacturaViewModel> lst = null;
            using (Models.BeautySalonEntities db = new Models.BeautySalonEntities())
            {
                lst = (from d in db.Factura
                       select new FacturaViewModel
                       {
                           FacturaNumero = d.FacturaNumero,
                           ClientName = d.ClientName,
                           Total = (double)d.Total,
                           
                       }).ToList();

            }


            ViewBag.listaVentas = lst;
            return View();

        }

    }
}