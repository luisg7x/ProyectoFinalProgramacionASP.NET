using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public interface IFacturaBLL:IDisposable
    {
        List<Factura> ListarFacturas();
        bool AgregarFacturas(Factura DTO);
        Factura BuscarFacturaId(int id);
        bool BorrarFacturaId(int id);
        bool EditarFactura(Factura DTO);
        Factura BuscarUltimoId();
        List<Factura> BuscarClienteFacturaId(string id);
    }
}
