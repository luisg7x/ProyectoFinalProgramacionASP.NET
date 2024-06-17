using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdminDALImpl : GenericRepository<Admin>, IAdminDAL
    {
        public AdminDALImpl(PrograVEntities context)
          : base(context)
        {

        }

        public PrograVEntities PrograContext
        {
            get { return Context as PrograVEntities; }
        }
    }
}
