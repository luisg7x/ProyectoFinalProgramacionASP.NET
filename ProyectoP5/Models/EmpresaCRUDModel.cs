using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoP5.Models
{
    public class EmpresaCRUDModel
    {
        [Display(Name = "ID Empresa")]
        [Required]
        public string IdEmpresa { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string NombreEmpresa { get; set; }
        [Display(Name = "Telefono")]
        [Required]
        public string TelefonoEmpresa { get; set; }
        [Display(Name = "Correo")]
        [Required]
        public string CorreoEmpresa { get; set; }
        [Display(Name = "Direccion")]
        [Required]
        public string DireccionEmpresa { get; set; }
    }
}