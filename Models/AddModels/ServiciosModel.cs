using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class ServiciosModel
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public decimal precioTotal { get; set; }
        public decimal impuesto { get; set; }
        public IEnumerable<SelectListItem> skill { get; set; }
        public List<InsumosProducto> insumos { get; set; }
    }
}