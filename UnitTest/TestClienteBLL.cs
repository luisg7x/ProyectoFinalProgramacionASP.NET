using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TestClienteBLL
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new PrograVEntities());
        private IClienteBLL clienteBLL;
        [TestMethod]
        public void AgregarCliente()
        {
            Cliente cliente = new Cliente();
            cliente.IDCLIENTE = "129";
            cliente.NOMBRE = "JJJ";
            cliente.APELLIDOS = "GG";
            cliente.SEXO = "Masculino";
            cliente.NUMTARJETA = "23324";
            cliente.CORREO = "GGLAG@OP.com";
            cliente.USUARIO = "GG";
            cliente.CONTRASEÑA = "WP";

            clienteBLL = new ClienteBLL();
            clienteBLL.AgregarCliente(cliente);
        }

        [TestMethod]
        public void ModificarCliente()
        {
            Cliente cliente = new Cliente()
            {
                NOMBRE = "Jose"
            };
            ClienteBLL cli = new ClienteBLL();
            cli.EditarCliente(cliente);
        }

        [TestMethod]
        public void EliminarCliente()
        {
            clienteBLL = new ClienteBLL();           
            clienteBLL.BorrarClienteId("129");
        }
        [TestMethod]
        public void ConsultarCliente()
        {
            IEnumerable<Cliente> lista = unitOfWork.clienteDAL.GetAll();

            unitOfWork.Complete();
        }
    }
}
