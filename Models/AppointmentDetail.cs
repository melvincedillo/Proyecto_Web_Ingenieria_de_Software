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
    
    public partial class AppointmentDetail
    {
        public int ID { get; set; }
        public int AppointmentID { get; set; }
        public int ServicioID { get; set; }
        public Nullable<int> idHora { get; set; }
    
        public virtual Horas Horas { get; set; }
        public virtual Services Services { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
