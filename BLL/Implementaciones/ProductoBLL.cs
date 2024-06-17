using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{
    public class ProductoBLL : IProductoBLL
    {

        private UnitOfWork unitOfWork;
        public List<Producto> ListarProductos()
        {
            try
            {
                List<Producto> resultado;
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    resultado = unitOfWork.productoDAL.GetAll().ToList();

                    unitOfWork.Complete();
                }



                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AgregarProducto(Producto DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.productoDAL.Add(DTO);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Producto BuscarProductoId(string id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Producto producto = dbContext.Productos.Where(a => a.IDPRODUCTO == id).FirstOrDefault();

                    return producto;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Producto> BuscarListaPorTipoProductoId(int id)
        {
            try
            {
                using (PrograVEntities dbContext = new PrograVEntities())
                {
                    List<Producto> Query = dbContext.Productos.Where(a => a.IDTIPOPRODUCTO == id).ToList();
                    return Query;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool BorrarProductoId(string id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Producto pd = dbContext.Productos.Where(a => a.IDPRODUCTO == id).FirstOrDefault();
                    dbContext.Productos.Remove(pd);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public bool EditarProducto(Producto DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.productoDAL.Update(DTO);
                    unitOfWork.Complete();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool MultipleActualizarStock(List<DetalleFactura> lista)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    foreach (DetalleFactura detalle in lista)
                    {
                        Producto pd = dbContext.Productos.Where(a => a.IDPRODUCTO == detalle.IDPRODUCTO).FirstOrDefault();
                        pd.CANTIDADDISPONIBLE = (pd.CANTIDADDISPONIBLE - detalle.CANTIDADPRODUCTO);
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
