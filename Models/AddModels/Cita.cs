using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class Cita
    {
        public string name { get; set; }
        public string phone { get; set; }
        public DateTime fecha { get; set; }
        public List<Servicios> servicios { get; set; }
    }
}