using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TestFacturaBLL
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new PrograVEntities());
        private IFacturaBLL facturaBLL;
        [TestMethod]
        public void AgregarFactura()
        {
            Factura factura = new Factura();
            factura.NUMFACTURA = 124;
            factura.IDEMPRESA = "3";
            factura.FECHA = DateTime.Now;
            factura.PRECIOTOTAL = 200000;
            factura.IDCLIENTE = "129";

            facturaBLL = new FacturaBLL();
            facturaBLL.AgregarFacturas(factura);
        }

        [TestMethod]
        public void ModificarFactura()
        {
            Factura factura = new Factura()
            {
                PRECIOTOTAL = 190000
            };
            FacturaBLL fac = new FacturaBLL();
            fac.EditarFactura(factura);
        }

        [TestMethod]
        public void EliminarFactura()
        {
            facturaBLL = new FacturaBLL();
            facturaBLL.BorrarFacturaId(1);
        }
        [TestMethod]
        public void ConsultarFactura()
        {
            IEnumerable<Factura> lista = unitOfWork.facDAL.GetAll();

            unitOfWork.Complete();
        }
    }
}
