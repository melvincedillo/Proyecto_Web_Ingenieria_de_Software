using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class Productos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }
        public string Sku { get; set; }

        public decimal total { get; set; }
    }
}