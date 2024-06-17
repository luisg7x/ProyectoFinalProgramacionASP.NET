using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoP5.Models
{
    public class DetalleFacturaCRUDModel
    {
        [Display(Name = "ID")]
        [Required]
        public int? IDDETALLEFACTURA { get; set; }
        [Display(Name = "Producto")]
        [Required]
        public string IDPRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public IEnumerable<SelectListItem> PRODUCTOLIST { get; set; }
        [Display(Name = "Numero de factura")]
        [Required]
        public int? NUMFACTURA { get; set; }
        [Display(Name = "Cantidad")]
        [Required]
        public int? CANTIDADPRODUCTO { get; set; }
        [Display(Name = "Precio parcial")]
        [Required]
        public decimal? PRECIOPARCIAL { get; set; }
    }
}