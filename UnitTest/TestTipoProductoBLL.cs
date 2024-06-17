using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TestTipoProductoBLL
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new PrograVEntities());
        private ITipoProductoBLL tipoBLL;
        [TestMethod]
        public void AgregarTipoProducto()
        {
            TipoProducto tip = new TipoProducto();
            tip.IDTIPOPRODUCTO = 2;
            tip.NOMBRETIPOPRODUCTO = "Tarjeta de Video";

            tipoBLL = new TipoProductoBLL();
            tipoBLL.AgregarTipoProducto(tip);
        }

        [TestMethod]
        public void ModificarTipoProducto()
        {
            TipoProducto tipo = new TipoProducto()
            {
                NOMBRETIPOPRODUCTO = "Procesador"
            };
            TipoProductoBLL tp = new TipoProductoBLL();
            tp.EditarTipoProducto(tipo);
        }

        [TestMethod]
        public void EliminarTipoProducto()
        {
           
            tipoBLL = new TipoProductoBLL();
            tipoBLL.BorrarTipoProductoId(2);
          
        }
        [TestMethod]
        public void ConsultarTipoProducto()
        {
            IEnumerable<TipoProducto> lista = unitOfWork.tpDAL.GetAll();

            unitOfWork.Complete();
        }
    }
}
