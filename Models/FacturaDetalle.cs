//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_Web_Ingenieria_de_Software.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FacturaDetalle
    {
        public int ID { get; set; }
        public int FacturaNumero { get; set; }
        public int SalonID { get; set; }
        public int ProductID { get; set; }
        public int ServiceID { get; set; }
        public int TaxID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal SalesTax { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual Products Products { get; set; }
        public virtual Services Services { get; set; }
        public virtual Tax Tax { get; set; }
        public virtual Salon Salon { get; set; }
    }
}
