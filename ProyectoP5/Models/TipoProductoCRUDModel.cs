using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoP5.Models
{
    public class TipoProductoCRUDModel
    {
        [Display(Name = "ID Tipo Producto")]
        [Required]
        public int? IdTipoProducto { get; set; }
        [Display(Name = "Nombre Tipo Producto")]
        [Required]
        public string NombreProducto { get; set; }
    }
}