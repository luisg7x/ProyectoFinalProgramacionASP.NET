using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoP5.Models
{
    public class AdminCRUDModel
    {
        [Display(Name = "ID Admin")]
        [Required]
        public string IdAdmin { get; set; }
        [Display(Name = "Nombre Admin")]
        [Required]
        public string NombreAdmin { get; set; }
        [Display(Name = "Apellido Admin")]
        [Required]
        public string ApellidoAdmin { get; set; }
        [Display(Name = "Nombre de Usuario")]
        [Required]
        public string Usuario { get; set; }
        [Display(Name = "Contrasena de Usuario")]
        [Required]
        public string Contraseña { get; set; }
    }

   
}