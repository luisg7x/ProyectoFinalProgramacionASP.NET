using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoP5.Models
{
    public class ProductoCRUDModel
    {
        [Display(Name = "ID Producto")]
        [Required]
        public string IdProducto { get; set; }
        [Display(Name = "Marca")]
        [Required]
        public string MarcaProducto { get; set; }
        [Display(Name = "Modelo")]
        [Required]
        public string ModeloProducto { get; set; }
        [Display(Name = "ID Tipo Producto")]
        [Required]
        public int? IdTipoProducto { get; set; }
        [Display(Name = "Tipo Producto")]
        public string NTipoProducto { get; set; }
        public IEnumerable<SelectListItem> TipoProducto { get; set; }
        [Display(Name = "Descripción")]
        [Required]
        public string DescripcionProducto { get; set; }
        [Display(Name = "Foto")]
        [Required]
        public byte[] FotoProducto { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        [Display(Name = "Precio Unitario")]
        [Required]
        public decimal? PrecioUnitario { get; set; }
        [Display(Name = "Cantidad")]
        [Required]
        public int? Cantidad { get; set; }
    
    }
}