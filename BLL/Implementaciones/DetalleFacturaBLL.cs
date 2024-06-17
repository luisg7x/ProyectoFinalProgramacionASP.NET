using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class DetalleFacturaBLL:IDetalleFacturaBLL
    {
        private UnitOfWork unitOfWork;
        public List<DetalleFactura> ListarDetalleFactura()
        {
            try
            {
                List<DetalleFactura> resultado;
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    resultado = unitOfWork.dfDAL.GetAll().ToList();

                    unitOfWork.Complete();
                }



                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AgregarDetalleFactura(DetalleFactura DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.dfDAL.Add(DTO);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool MultipleAgregarDetalleFactura(List<DetalleFactura> lista)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    foreach (DetalleFactura detalle in lista)
                    {
                        dbContext.DetalleFacturas.Add(detalle);
                    }
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }



        public DetalleFactura BuscarDetalleFacturaId(int id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    DetalleFactura detallefacturas = dbContext.DetalleFacturas.Where(a => a.IDDETALLEFACTURA == id).FirstOrDefault();

                    return detallefacturas;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<DetalleFactura> BuscarDetalleFacturaNumFactura(int id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    List<DetalleFactura> Query = dbContext.DetalleFacturas.Where(a => a.NUMFACTURA == id).ToList();
                    return Query;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool BorrarDetalleFacturaId(int id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    DetalleFactura ad = dbContext.DetalleFacturas.Where(a => a.IDDETALLEFACTURA == id).FirstOrDefault();
                    dbContext.DetalleFacturas.Remove(ad);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EditarDetalleFactura(DetalleFactura DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.dfDAL.Update(DTO);
                    unitOfWork.Complete();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
