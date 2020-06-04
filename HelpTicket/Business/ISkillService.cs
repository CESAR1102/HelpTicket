using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ISkillService: ICrudService<Skill>
    {
        List<string> SkillXtrabajadorSimple(string codigo);
		bool ExistByTopico(int topico_id, string identificador);
        bool ExistByTrabajador(int topico_id, string usuario);
    }
}
