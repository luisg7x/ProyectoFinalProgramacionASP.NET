using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public interface IEmpresaBLL:IDisposable
    {
        List<Empresa> ListarEspresas();
        bool AgregarEspresa(Empresa DTO);
        Empresa BuscarEspresaId(string id);
        bool BorrarEspresaId(string id);
        bool EditarEspresa(Empresa DTO);
        Empresa BuscarPrimeraEspresaId();


    }
}
