using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.VentasModel
{
    public class ProductsViewModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Sku { get; set; }
        public int TaxID { get; set; }

    }
}