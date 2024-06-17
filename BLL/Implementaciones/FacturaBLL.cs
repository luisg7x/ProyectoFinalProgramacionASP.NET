using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class FacturaBLL:IFacturaBLL
    {

        private UnitOfWork unitOfWork;
        public List<Factura> ListarFacturas()
        {
            try
            {
                List<Factura> resultado;
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    resultado = unitOfWork.facDAL.GetAll().ToList();

                    unitOfWork.Complete();
                }



                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AgregarFacturas(Factura DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.facDAL.Add(DTO);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Factura BuscarFacturaId(int id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Factura factura = dbContext.Facturas.Where(a => a.NUMFACTURA == id).FirstOrDefault();
                    return factura;
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Factura> BuscarClienteFacturaId(string id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    List<Factura> Query = dbContext.Facturas.Where(a => a.IDCLIENTE == id).ToList();
                    return Query;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }




        public bool BorrarFacturaId(int id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Factura ft = dbContext.Facturas.Where(a => a.NUMFACTURA == id).FirstOrDefault();

                    dbContext.Facturas.Remove(ft);
        
                    dbContext.SaveChanges();           
                }
               return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EditarFactura(Factura DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.facDAL.Update(DTO);
                    unitOfWork.Complete();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        
        public Factura BuscarUltimoId()
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Factura factura = dbContext.Facturas.OrderByDescending(a => a.NUMFACTURA).FirstOrDefault();
                    return factura;
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
