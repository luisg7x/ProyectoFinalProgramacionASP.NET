using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class TipoProductoBLL:ITipoProductoBLL
    {

        private UnitOfWork unitOfWork;
        public List<TipoProducto> ListarTipoProducto()
        {
            try
            {

                List<TipoProducto> resultado;
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    resultado = unitOfWork.tpDAL.GetAll().ToList();

                    unitOfWork.Complete();
                }



                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AgregarTipoProducto(TipoProducto DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.tpDAL.Add(DTO);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public TipoProducto BuscarTipoProductoId(int id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    TipoProducto ad = dbContext.TipoProductos.Where(a => a.IDTIPOPRODUCTO == id).FirstOrDefault();

                    return ad;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool BorrarTipoProductoId(int id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    TipoProducto ad = dbContext.TipoProductos.Where(a => a.IDTIPOPRODUCTO == id).FirstOrDefault();
                    dbContext.TipoProductos.Remove(ad);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EditarTipoProducto(TipoProducto DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.tpDAL.Update(DTO);
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
