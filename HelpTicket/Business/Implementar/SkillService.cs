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
            throw new NotImplementedException();
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
