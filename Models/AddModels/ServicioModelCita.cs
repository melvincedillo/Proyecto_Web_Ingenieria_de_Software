using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class ServicioModelCita
    {
        public int id { get; set; }
        public string name { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
    }
}