using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public bool encontrado { get; set; }
    }
}