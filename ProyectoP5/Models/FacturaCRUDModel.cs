using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoP5.Models
{
    public class FacturaCRUDModel
    {
        [Display(Name = "Numero de factura")]
        [Required]
        public int? NUMFACTURA { get; set; }
        [Display(Name = "Empresa ID")]
        [Required]
        public string IDEMPRESA { get; set; }
        public IEnumerable<SelectListItem> EMPRESALIST { get; set; }
        [Display(Name = "Empresa")]
        [Required]
        public string EMPRESA { get; set; }
        [Display(Name = "Fecha")]
        [Required]
        public Nullable<System.DateTime> FECHA { get; set; }
        [Display(Name = "Precio total")]
        [Required]
        public decimal? PRECIOTOTAL { get; set; }
        [Display(Name = "Cliente ID")]
        [Required]
        public string IDCLIENTE { get; set; }
    
    }
}