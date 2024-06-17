using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TestAdminBLL
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new PrograVEntities());
        private IAdminBLL adminBLL;
        [TestMethod]
        public void AgregarAdministrador()
        {
            Admin admin = new Admin();
            admin.IDADMIN = "2";
            admin.NOMBRE = "Jose";
            admin.APELLIDOS = "Loaiza";
            admin.USUARIO = "JJLJ";
            admin.CONTRASEÑA = "WP";


            adminBLL = new AdminBLL();
            adminBLL.AgregarAdmin(admin);
        }

        [TestMethod]
        public void ModificarAdmin()
        {
            Admin admin = new Admin()
            {
                NOMBRE = "Joaquin"
            };
            AdminBLL ad = new AdminBLL();
            ad.EditarAdmin(admin);
        }

        [TestMethod]
        public void EliminarAdmin()
        {
            adminBLL = new AdminBLL();
            adminBLL.BorrarAdminId("2");
        }
        [TestMethod]
        public void ConsultarAdmin()
        {
            IEnumerable<Admin> lista = unitOfWork.adminDAL.GetAll();

            unitOfWork.Complete();
        }
    }
}
