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
    public class SkillService : ISkillService
    {
        private ISkillRepository skill = new SkillRepository();
		

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

		public bool ExistByTopico(int topico_id, string identificador)
		{
			return skill.ExistByTopico(topico_id, identificador);
		}

        public bool ExistByTrabajador(int topico_id, string usuario)
        {
            return skill.ExistByTrabajador(topico_id, usuario);
        }

        public List<Skill> FindAll()
        {
            throw new NotImplementedException();
        }

        public Skill FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Skill t)
        {
            return skill.Insert(t);
        }

        public List<string> SkillXtrabajadorSimple(string codigo)
        {
            return skill.SkillXtrabajadorSimple(codigo);
        }

        public bool Update(Skill t)
        {
            throw new NotImplementedException();
        }
    }
}
