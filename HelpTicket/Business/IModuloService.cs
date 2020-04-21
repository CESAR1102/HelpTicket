using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IModuloService : ICrudService<Modulo>
    {
        bool AccesoModulo(string usuario_code, string modulo);
    }
}
