using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.ViewModels
{
    public class HorarioView
    {
        public int id { get; set; }
        public string day { get; set; }
        public string open { get; set; }
        public string close { get; set; }
    }
}