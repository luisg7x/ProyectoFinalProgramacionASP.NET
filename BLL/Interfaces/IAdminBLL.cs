using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public interface IAdminBLL:IDisposable
    {
        List<Admin> ListarAdmins();
        bool AgregarAdmin(Admin DTO);
        Admin BuscarAdminId(string id);
        bool BorrarAdminId(string id);
        bool EditarAdmin(Admin DTO);
        Admin BuscarAdminUsuario(string user);
    }
}
