using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClienteDALImpl : GenericRepository<Cliente>, IClienteDAL
    {
        public ClienteDALImpl(PrograVEntities context)
          : base(context)
        {

        }

        public PrograVEntities PrograContext
        {
            get { return Context as PrograVEntities; }
        }
    }
}
