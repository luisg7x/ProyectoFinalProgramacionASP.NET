using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TipoProductoDALImpl : GenericRepository<TipoProducto>, ITipoProductoDAL
    {
        public TipoProductoDALImpl(PrograVEntities context)
          : base(context)
        {

        }

        public PrograVEntities PrograContext
        {
            get { return Context as PrograVEntities; }
        }
    }
}
