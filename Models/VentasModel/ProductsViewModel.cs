using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.VentasModel
{
    public class ProductsViewModel
    {
        public int ID { get; set; }
        public String ProductName { get; set; }
        public String Price { get; set; }
        public int Quantity { get; set; }
        public String Sku { get; set; }
        public int TaxID { get; set; }

    }
}