using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Data.Implementar;
using Data;

namespace Business.Implementar
{
    public class UsuarioModuloRolService : IUsuarioModuloRolService
    {
        IUsuarioModuloRolRepository usuarioModuloRol = new UsuarioModuloRolRepository();

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario_Modulo_Rol> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario_Modulo_Rol FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Usuario_Modulo_Rol t)
        {
            throw new NotImplementedException();
        }

        public int obtenerIDUserModRol(string codigo_usuario, string modulo, string rol)
        {
            return usuarioModuloRol.obtenerIDUserModRol(codigo_usuario, modulo, rol);
        }

        public bool Update(Usuario_Modulo_Rol t)
        {
            throw new NotImplementedException();
        }
    }
}
