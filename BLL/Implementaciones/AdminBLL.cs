using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DAL;

namespace BLL
{
    public class AdminBLL:IAdminBLL
    {
        private UnitOfWork unitOfWork;

        public List<Admin> ListarAdmins()
        {
            try
            {
                List<Admin> resultado;
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    resultado = unitOfWork.adminDAL.GetAll().ToList();

                    unitOfWork.Complete();
                }



                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


          public bool AgregarAdmin(Admin DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.adminDAL.Add(DTO);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

         public Admin BuscarAdminId(string id)
         {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Admin admin = dbContext.Admins.Where(a => a.IDADMIN == id).FirstOrDefault();

                    return admin;
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }


         public Admin BuscarAdminUsuario(string user)
         {
             try
             {
                 using (var dbContext = new PrograVEntities())
                 {
                     Admin admin = dbContext.Admins.Where(a => a.USUARIO == user).FirstOrDefault();

                     return admin;
                 }

             }
             catch (Exception)
             {

                 throw;
             }

         }

        public bool BorrarAdminId(string id)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    Admin ad = dbContext.Admins.Where(a => a.IDADMIN == id).FirstOrDefault();
                    dbContext.Admins.Remove(ad);
                    dbContext.SaveChanges();           
                }
               return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EditarAdmin(Admin DTO)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new PrograVEntities()))
                {
                    unitOfWork.adminDAL.Update(DTO);
                    unitOfWork.Complete();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Admin LoginAdmin(string usuario, string contraseña)
        {
            try
            {
                using (var dbContext = new PrograVEntities())
                {
                    var admin = dbContext.Admins.Where(a => a.USUARIO == usuario && a.CONTRASEÑA == contraseña).FirstOrDefault();
                    if (admin != null)
                    {
                        return admin;
                    }
                    return admin;
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
