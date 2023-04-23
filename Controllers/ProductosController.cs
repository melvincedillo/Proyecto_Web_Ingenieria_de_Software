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
    }
}