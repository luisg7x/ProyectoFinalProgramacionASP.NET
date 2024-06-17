using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PrograVEntities context;


        public IAdminDAL adminDAL { get; set; }
        public IClienteDAL clienteDAL { get; set; }
        public IDetalleFacturaDAL dfDAL { get; set; }
        public IEmpresaDAL empresaDAL { get; set; }
        public IFacturaDAL facDAL { get; set; }
        public IProductoDAL productoDAL { get; set; }
        public ITipoProductoDAL tpDAL { get; set; }

        public UnitOfWork(PrograVEntities _context)
        {
            context = _context;
            adminDAL = new AdminDALImpl(context);
            clienteDAL = new ClienteDALImpl(context);
            dfDAL = new DetalleFacturaDALImpl(context);
            empresaDAL = new EmpresaDALImpl(context);
            facDAL = new FacturaDALImpl(context);
            productoDAL = new ProductoDALImpl(context);
            tpDAL = new TipoProductoDALImpl(context);

            //Authors = new AuthorRepository(_context);
        }






        public void Complete()
        {
            context.SaveChanges();
        }







        public void Dispose()
        {
            context.Dispose();
        }
    }
}


