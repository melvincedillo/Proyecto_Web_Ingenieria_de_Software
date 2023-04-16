using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models.VentasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Web_Ingenieria_de_Software.Models.ViewModels;


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

        public ActionResult Agregar()
        {

        }


    }
}