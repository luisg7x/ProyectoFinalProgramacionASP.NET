using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductoDALImpl : GenericRepository<Producto>, IProductoDAL
    {
        public ProductoDALImpl(PrograVEntities context)
          : base(context)
        {

        }

        public PrograVEntities PrograContext
        {
            get { return Context as PrograVEntities; }
        }
    }

}
