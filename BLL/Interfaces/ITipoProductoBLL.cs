using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public interface ITipoProductoBLL:IDisposable
    {
        List<TipoProducto> ListarTipoProducto();
        bool AgregarTipoProducto(TipoProducto DTO);
        TipoProducto BuscarTipoProductoId(int id);
        bool BorrarTipoProductoId(int id);
        bool EditarTipoProducto(TipoProducto DTO);
    }
}
