using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ISkillRepository : ICrudRepository<Skill>
    {
        List<string> SkillXtrabajadorSimple(string codigo);
		bool ExistByTopico(int topico_id, string identificador);
	}
}
