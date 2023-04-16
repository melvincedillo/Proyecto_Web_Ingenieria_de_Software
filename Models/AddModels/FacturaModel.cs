using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class FacturaModel
    {
        public DateTime time { get; set; }

        public decimal total { get; set; }

        public decimal tax { get; set; }

        public string clientName { get; set; }


    }
}