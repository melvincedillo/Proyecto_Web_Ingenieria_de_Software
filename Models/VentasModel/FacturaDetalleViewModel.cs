using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.VentasModel
{
    public class FacturaDetalleViewModel
    {
        public int ID { get; set; }
        public int FacturaNumero { get; set; }
        public int SalonID { get; set; }
        public int ProductID { get; set; }
        public int ServiceID { get; set; }
        public int TaxID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double salesTax { get; set; }
    }
}