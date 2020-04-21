using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Implementar;
using Entity;

namespace Business.Implementar
{
    public class ModuloService : IModuloService
    {
        private IModuloRepository modulo = new ModuloRepository();

        public bool AccesoModulo(string usuario_code, string modulo)
        {
            return this.modulo.AccesoModulo(usuario_code, modulo);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Modulo> FindAll()
        {
            throw new NotImplementedException();
        }

        public Modulo FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Modulo t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Modulo t)
        {
            throw new NotImplementedException();
        }
    }
}
