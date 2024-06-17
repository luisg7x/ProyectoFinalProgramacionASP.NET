using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoP5.Models
{
    public class ClienteCRUDModel
    {
        [Display(Name = "ID Cliente")]
        [Required]
        public string IdCliente { get; set; }
           [Display(Name = "Nombre")]
           [Required]
        public string NombreCliente { get; set; }
           [Display(Name = "Apellido")]
           [Required]
        public string ApellidoCliente { get; set; }
           [Display(Name = "Sexo")]
           [Required]
        public string Sexo { get; set; }
           [Display(Name = "Numero de Tarjeta")]
           [Required]
        public string NumTarjeta { get; set; }
           [Display(Name = "Correo")]
           [Required]
        public string Correo { get; set; }
           [Display(Name = "Usuario")]
           [Required]
        public string Usuario { get; set; }
           [Display(Name = "Contrasena")]
           [Required]
        public string Password { get; set; }

    }
}