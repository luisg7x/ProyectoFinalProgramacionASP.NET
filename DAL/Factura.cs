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
    
    public partial class Factura
    {
        public Factura()
        {
            this.DetalleFacturas = new HashSet<DetalleFactura>();
        }
    
        public int NUMFACTURA { get; set; }
        public string IDEMPRESA { get; set; }
        public decimal PRECIOTOTAL { get; set; }
        public string IDCLIENTE { get; set; }
        public Nullable<System.DateTime> FECHA { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
