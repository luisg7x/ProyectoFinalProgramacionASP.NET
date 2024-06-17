using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminDAL adminDAL { get; set; }
        IClienteDAL clienteDAL { get; set; }
        IDetalleFacturaDAL dfDAL { get; set; }
        IEmpresaDAL empresaDAL { get; set; }
        IFacturaDAL facDAL { get; set; }
        IProductoDAL productoDAL { get; set; }
        ITipoProductoDAL tpDAL { get; set; }
        void Complete();
    }
}
