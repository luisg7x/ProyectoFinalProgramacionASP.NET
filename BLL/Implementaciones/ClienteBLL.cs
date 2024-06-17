using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ClienteBLL:IClienteBLL
    {
        private UnitOfWork unitOfWork;
        public List<Cliente> ListarClientes()
        {
            try
            {
                List<Cliente> resultado;
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    resultado = unitOfWork.clienteDAL.GetAll().ToList();

                    unitOfWork.Complete();
                }



                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AgregarCliente(Cliente DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.clienteDAL.Add(DTO);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Cliente BuscarClienteId(string id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Cliente cliente = dbContext.Clientes.Where(a => a.IDCLIENTE == id).FirstOrDefault();

                    return cliente;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public Cliente BuscarClienteUsuario(string user)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Cliente cliente = dbContext.Clientes.Where(a => a.USUARIO == user).FirstOrDefault();

                    return cliente;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public Cliente BuscarClienteCorreo(string correo)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Cliente cliente = dbContext.Clientes.Where(a => a.CORREO == correo).FirstOrDefault();

                    return cliente;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }


        public bool BorrarClienteId(string id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Cliente ad = dbContext.Clientes.Where(a => a.IDCLIENTE == id).FirstOrDefault();
                    dbContext.Clientes.Remove(ad);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EditarCliente(Cliente DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.clienteDAL.Update(DTO);
                    unitOfWork.Complete();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Cliente LoginCliente(string usuario, string contraseña)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    var cliente = dbContext.Clientes.Where(a => a.USUARIO == usuario && a.CONTRASEÑA == contraseña).FirstOrDefault();
                    if (cliente != null)
                    {
                        return cliente;
                    }
                    return cliente;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
