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
    
    public partial class MedioPagoDetalle
    {
        public int ID { get; set; }
        public int SalonID { get; set; }
        public int FacturaNumero { get; set; }
        public int MedioPagoID { get; set; }
        public string Description { get; set; }
        public string CreditCardNumber { get; set; }
        public decimal Amount { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual MedioPago MedioPago { get; set; }
        public virtual Salon Salon { get; set; }
    }
}