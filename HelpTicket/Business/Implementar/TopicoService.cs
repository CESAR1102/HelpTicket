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

        public bool ExistByDepartamento(int departamento_id, string identificador)
        {
            return topico.ExistByDepartamento(departamento_id, identificador);
        }

        public List<Topico> FindAll()
        {
			return topico.FindAll();
		}

        public List<Topico> FindByDepartamento(int departamento_id, string identificador)
        {
            return topico.FindByDepartamento(departamento_id, identificador);
        }

        public Topico FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Topico t)
        {
			return topico.Insert(t);
        }

        public bool Update(Topico t)
        {
            throw new NotImplementedException();
        }
    }
}
