using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TestEmpresaBLL
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new PrograVEntities());
        private IEmpresaBLL empBLL;
        [TestMethod]
        public void AgregarEmpresa()
        {
            Empresa emp = new Empresa();
            emp.IDEMPRESA = "3";
            emp.NOMBRE = "Gamer";
            emp.TELEFONO = "22343234";
            emp.CORREO = "gamergg@gmail.com";
            emp.DIRECCION = "Sto Domingo Centro";


            empBLL = new EmpresaBLL();
            empBLL.AgregarEspresa(emp);
        }

        [TestMethod]
        public void ModificarEnterprise()
        {
            Empresa empresa = new Empresa()
            {
                NOMBRE = "Dark"
            };
            EmpresaBLL emp = new EmpresaBLL();
            emp.EditarEspresa(empresa);
        }

        [TestMethod]
        public void EliminarEmpresa()
        {
            empBLL = new EmpresaBLL();
            empBLL.BorrarEspresaId("3");
        }
        [TestMethod]
        public void ConsultarEmpresa()
        {
            IEnumerable<Empresa> lista = unitOfWork.empresaDAL.GetAll();

            unitOfWork.Complete();
        }
    }
}
