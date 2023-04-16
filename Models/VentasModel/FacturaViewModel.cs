using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.VentasModel
{
    public class FacturaViewModel
    {
        public int FacturaNumero { get; set; }
        public DateTime Time { get; set; }

        public double Total { get; set; }
        public double Tax { get; set; }
        public string ClientName { get; set; }
        public int SalonID { get; set; }
        public int EmployeeID { get; set; }

    }
}