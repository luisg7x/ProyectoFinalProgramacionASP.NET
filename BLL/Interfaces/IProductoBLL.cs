using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public interface IProductoBLL:IDisposable
    {
        List<Producto> ListarProductos();
        List<Producto> BuscarListaPorTipoProductoId(int id);
        bool AgregarProducto(Producto DTO);
        Producto BuscarProductoId(string id);
        bool BorrarProductoId(string id);
        bool EditarProducto(Producto DTO);
        bool MultipleActualizarStock(List<DetalleFactura> lista);
    }
}
