//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleFactura
    {
        public int IDDETALLEFACTURA { get; set; }
        public string IDPRODUCTO { get; set; }
        public int NUMFACTURA { get; set; }
        public int CANTIDADPRODUCTO { get; set; }
        public decimal PRECIOPARCIAL { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Factura Factura { get; set; }
    }
}
