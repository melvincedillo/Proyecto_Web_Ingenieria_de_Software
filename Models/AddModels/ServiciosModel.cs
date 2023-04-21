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
        [Display(Name = "Codigo del servicio")]
        public string codigo { get; set; }
        [Required]
        [Display(Name = "Nombre del servicio")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        [Required]
        [Display(Name = "Precio del servicio")]
        public decimal precio { get; set; }
        [Required]
        [Display(Name = "Precio Total")]
        public decimal precioTotal { get; set; }
        [Required]
        [Display(Name = "Impuesto aplicado")]
        public decimal impuesto { get; set; }
        [Required]
        public IEnumerable<SelectListItem> skill { get; set; }
    }
}