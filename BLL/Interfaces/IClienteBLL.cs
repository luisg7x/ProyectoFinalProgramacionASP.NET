using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IClienteBLL:IDisposable
    {
        List<Cliente> ListarClientes();
        bool AgregarCliente(Cliente DTO);
        Cliente BuscarClienteId(string id);
        bool BorrarClienteId(string id);
        bool EditarCliente(Cliente DTO);
        Cliente BuscarClienteUsuario(string user);
        Cliente BuscarClienteCorreo(string correo);
        
    }
}
