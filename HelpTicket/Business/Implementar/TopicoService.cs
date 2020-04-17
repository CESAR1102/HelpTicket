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
    public class TopicoService : ITopicoService
    {
        private ITopicoRepository topico = new TopicoRepository();

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Topico> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<Topico> FindByDepartamento(int departamento_id)
        {
            return topico.FindByDepartamento(departamento_id);
        }

        public Topico FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Topico t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Topico t)
        {
            throw new NotImplementedException();
        }
    }
}
