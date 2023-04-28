using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.ViewModels
{
    public class HorarioViewModel
    {
        public int id { get; set; }
        public string day { get; set; }
        public int open { get; set; }
        public int close { get; set; }
        public bool laborable { get; set; }
        public string nameOpen { get; set; }
        public string nameClose { get; set; }
    }
}