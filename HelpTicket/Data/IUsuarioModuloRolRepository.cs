using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IUsuarioModuloRolRepository:ICrudRepository<Usuario_Modulo_Rol>
    {
        int obtenerIDUserModRol(string codigo_usuario, string modulo, string rol);
    }
}
