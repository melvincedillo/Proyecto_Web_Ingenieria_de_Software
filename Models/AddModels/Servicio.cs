using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class Servicio
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal total { get; set; }
        public int skill { get; set; }
        public decimal precio { get; set; }
        public string descripcion { get; set; } 

        public bool encontrado  { get; set; }
        public List<Productos> products { get; set; }
    }
}