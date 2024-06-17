using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
     public class EmpresaBLL:IEmpresaBLL
     {
        private UnitOfWork unitOfWork;
        public List<Empresa> ListarEspresas()
         {
            try
            {
                List<Empresa> resultado;
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    resultado = unitOfWork.empresaDAL.GetAll().ToList();

                    unitOfWork.Complete();
                }



                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
         }


          public bool AgregarEspresa(Empresa DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.empresaDAL.Add(DTO);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

          public Empresa BuscarEspresaId(string id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Empresa empresa = dbContext.Empresas.Where(a => a.IDEMPRESA == id).FirstOrDefault();

                    return empresa;
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }

          public Empresa BuscarPrimeraEspresaId()
          {
              try
              {
                  using (var dbContext = new PrograVEntities())
                  {
                      Empresa empresa = dbContext.Empresas.OrderBy(a => a.IDEMPRESA).FirstOrDefault();

                      return empresa;
                  }

              }
              catch (Exception)
              {

                  throw;
              }

          }

        public bool BorrarEspresaId(string id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Empresa emp = dbContext.Empresas.Where(a => a.IDEMPRESA == id).FirstOrDefault();
                    dbContext.Empresas.Remove(emp);
                    dbContext.SaveChanges();           
                }
               return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EditarEspresa(Empresa DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.empresaDAL.Update(DTO);
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
