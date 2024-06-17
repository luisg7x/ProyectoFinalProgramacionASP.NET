using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TestDetalleFacBLL
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new PrograVEntities());
        private IDetalleFacturaBLL dfBLL;
        [TestMethod]
        public void AgregarDetalleFactura()
        {
            DetalleFactura df = new DetalleFactura();
            df.IDPRODUCTO = "12673";
            df.NUMFACTURA = 1;
            df.CANTIDADPRODUCTO = 350;
            df.PRECIOPARCIAL = 45000;


            dfBLL = new DetalleFacturaBLL();
            dfBLL.AgregarDetalleFactura(df);
        }

        [TestMethod]
        public void ModificarDetalleFactura()
        {
            DetalleFactura detalle = new DetalleFactura()
            {
                PRECIOPARCIAL = 50000
            };
            DetalleFacturaBLL df = new DetalleFacturaBLL();
            df.EditarDetalleFactura(detalle);
        }

        [TestMethod]
        public void EliminarDetalleFactura()
        {
            dfBLL = new DetalleFacturaBLL();
            dfBLL.BorrarDetalleFacturaId(1);
        }
        [TestMethod]
        public void ConsultarDetalleFactura()
        {
            IEnumerable<DetalleFactura> lista = unitOfWork.dfDAL.GetAll();

            unitOfWork.Complete();
        }
    }
}
