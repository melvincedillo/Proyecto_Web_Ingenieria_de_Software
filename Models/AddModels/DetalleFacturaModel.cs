using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class DetalleFacturaModel
    {

        public int numeroFactura { set; get; }


        public int salonId { set; get; }


        public int productId { set; get; }


        public decimal price { set; get; }


        public int quantity { set; get; }


        public decimal discount { set; get; }


        public decimal salesTax { set; get; }


    }
}