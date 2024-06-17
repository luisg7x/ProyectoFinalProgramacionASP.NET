using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoP5.Models
{
    public class HistorialComprasModel
    {
        [Display(Name = "Producto")]
        [Required]
        public string IDPRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        [Display(Name = "Numero de factura")]
        [Required]
        public int NUMFACTURA { get; set; }
        [Display(Name = "Cantidad")]
        [Required]
        public int? CANTIDADPRODUCTO { get; set; }
        [Display(Name = "Precio parcial")]
        [Required]
        public decimal? PRECIOPARCIAL { get; set; }

        [Display(Name = "Fecha")]
        [Required]
        public Nullable<System.DateTime> FECHA { get; set; }
        [Display(Name = "Precio total")]
        [Required]
        public decimal? PRECIOTOTAL { get; set; }

        [Display(Name = "ID Cliente")]
        [Required]
        public string IDCLIENTE { get; set; }

        [Display(Name = "Marca")]
        [Required]
        public string MARCA { get; set; }
        [Display(Name = "Modelo")]
        [Required]
        public string MODELO { get; set; }
      
        [Display(Name = "Foto")]
        [Required]
        public byte[] FOTO { get; set; }

        [Display(Name = "ID Empresa")]
        [Required]
        public string IDEMPRESA { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string NOMBREEMPRESA { get; set; }
        [Display(Name = "Telefono")]
        [Required]
        public string TELEFONOEMPRESA { get; set; }
        [Display(Name = "Correo")]
        [Required]
        public string CORREOEMPRESA { get; set; }
        [Display(Name = "Direccion")]
        [Required]
        public string DIRECCIONEMPRESA { get; set; }


    }
}