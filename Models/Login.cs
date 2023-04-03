using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Web_Ingenieria_de_Software.Models
{
    public class Login
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}