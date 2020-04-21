using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IModuloRepository : ICrudRepository<Modulo>
    {
        bool AccesoModulo(string usuario_code, string modulo);
    }
}
