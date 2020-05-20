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
    public class DepartamentoService : IDepartamentoService
    {
        private IDepartamentoRepository departamento = new DepartamentoRepository();

        public bool Delete(int id)
        {
            return departamento.Delete(id);
        }

        public List<Departamento> FindAll()
        {
            return departamento.FindAll();
        }

        public Departamento FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Departamento t)
        {
            return departamento.Insert(t);
        }

        public bool Update(Departamento t)
        {
            throw new NotImplementedException();
        }
    }
}
