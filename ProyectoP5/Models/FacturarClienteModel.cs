using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoP5.Models
{
    public class FacturarClienteModel
    {
        [Display(Name = "Numero de factura")]
        [Required]
        public int? NUMFACTURA { get; set; }
        [Display(Name = "Fecha")]
        [Required]
        public Nullable<System.DateTime> FECHA { get; set; }
        [Display(Name = "Precio total")]
        [Required]
        public string IDCLIENTE { get; set; }
        [Display(Name = "Precio total")]
        [Required]
        public decimal? PRECIOTOTAL { get; set; }



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