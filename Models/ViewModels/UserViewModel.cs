using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public System.DateTime dateCreate { get; set; }
        public string userName { get; set; }
        public bool userActive { get; set; }
    }
}