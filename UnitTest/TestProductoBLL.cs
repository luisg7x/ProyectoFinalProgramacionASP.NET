using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DAL;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TestProductoBLL
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new PrograVEntities());
        private IProductoBLL productoBLL;
        [TestMethod]
        public void AgregarProducto()
        {
            Producto producto = new Producto();
            producto.IDPRODUCTO = "12673";
            producto.MARCA = "Prueba";
            producto.MODELO = "Prueba";
            producto.IDTIPOPRODUCTO = 1;
            producto.DESCRIPCION = "Test";
            producto.FOTO = null;
            producto.PRECIOUNITARIO = 11111;
            producto.CANTIDADDISPONIBLE = 100;

            productoBLL = new ProductoBLL();
            productoBLL.AgregarProducto(producto);
        }

        //[TestMethod]
        //public void ModificarProducto()
        //{
        //    Producto producto = new Producto()
        //    {
        //       PRECIOUNITARIO= 100000
        //    };
        //    ProductoBLL pro = new ProductoBLL();
        //    pro.EditarProducto(producto);
        //}

        [TestMethod]
        public void EliminarProducto()
        {
            productoBLL = new ProductoBLL();
            productoBLL.BorrarProductoId("12673");
        }
        [TestMethod]
        public void ConsultarProducto()
        {
            IEnumerable<Producto> lista = unitOfWork.productoDAL.GetAll();

            unitOfWork.Complete();
        }
    }
}
