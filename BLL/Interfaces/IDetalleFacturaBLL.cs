using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public interface IDetalleFacturaBLL:IDisposable
    {
        List<DetalleFactura> ListarDetalleFactura();
        bool AgregarDetalleFactura(DetalleFactura DTO);
        DetalleFactura BuscarDetalleFacturaId(int id);
        bool BorrarDetalleFacturaId(int id);
        bool EditarDetalleFactura(DetalleFactura DTO);
        bool MultipleAgregarDetalleFactura(List<DetalleFactura> lista);
        List<DetalleFactura> BuscarDetalleFacturaNumFactura(int id);


    }
}
