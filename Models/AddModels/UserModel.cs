using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Models.AddModels
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Required]
        [Display(Name = "DNI")]
        public string DNI { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string sexo { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string usuario { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string contraseña { get; set; }

        [Required]
        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("contraseña", ErrorMessage = "Las contraseñas no son iguales")]
        public string confContraseña { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        public string correo { get; set; }

        [Display(Name = "Citas")]
        public bool citas { get; set; }

        [Display(Name = "Ventas")]
        public bool ventas { get; set; }

        [Display(Name = "Reportes")]
        public bool reportes { get; set; }

        [Display(Name = "Horarios/Feriados")]
        public bool horarios { get; set; }

        [Display(Name = "Administración de usuarios")]
        public bool usuarios { get; set; }

        [Display(Name = "Configuración General")]
        public bool general { get; set; }

        [Display(Name = "Servicios")]
        public bool servicios { get; set; }

        [Display(Name = "Productos")]
        public bool productos { get; set; }
    }

    public class EditUserModel
    {
        public int id { get; set; }
        public int idEmpleado { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Required]
        [Display(Name = "DNI")]
        public string DNI { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string sexo { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string usuario { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string contraseña { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("contraseña", ErrorMessage = "Las contraseñas no son iguales")]
        public string confContraseña { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        public string correo { get; set; }

        [Display(Name = "Citas")]
        public bool citas { get; set; }

        [Display(Name = "Ventas")]
        public bool ventas { get; set; }

        [Display(Name = "Reportes")]
        public bool reportes { get; set; }

        [Display(Name = "Horarios/Feriados")]
        public bool horarios { get; set; }

        [Display(Name = "Administración de usuarios")]
        public bool usuarios { get; set; }

        [Display(Name = "Configuración General")]
        public bool general { get; set; }

        [Display(Name = "Servicios")]
        public bool servicios { get; set; }

        [Display(Name = "Productos")]
        public bool productos { get; set; }

        public int skillId { get; set; }
    }
}