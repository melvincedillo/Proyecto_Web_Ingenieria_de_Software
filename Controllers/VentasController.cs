using Proyecto_Web_Ingenieria_de_Software.Filters;
using Proyecto_Web_Ingenieria_de_Software.Models.VentasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
  // [ValideSession]
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
            
            using ( Models.BeautySalonEntities db = new Models.BeautySalonEntities())
            {
                List<ProductsViewModel> lst =
                   (from d in db.Products
                    select new ProductsViewModel { }
                    ).ToList();


            }
            return View();
        }
    }
}