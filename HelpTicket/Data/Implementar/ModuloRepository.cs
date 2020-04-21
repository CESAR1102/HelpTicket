using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Implementar
{
    public class ModuloRepository : IModuloRepository
    {
        public bool AccesoModulo(string usuario_code, string modulo)
        {
            return true;
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
