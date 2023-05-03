using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.ViewModels
{
    public class ViewDetalleCita
    {
        public string nameService { get; set; }
        public string descripcionService { get; set; }
        public decimal precioService { get; set; }
        public string horaService { get; set; }
    }
}