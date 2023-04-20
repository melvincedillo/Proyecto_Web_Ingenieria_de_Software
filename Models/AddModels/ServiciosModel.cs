using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class ServiciosModel
    {
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public decimal precioTotal { get; set; }
        [Required]
        public decimal impuesto { get; set; }
        [Required]
        public IEnumerable<SelectListItem> skill { get; set; }
        [Required]
        public List<InsumosProducto> insumos { get; set; }
    }
}