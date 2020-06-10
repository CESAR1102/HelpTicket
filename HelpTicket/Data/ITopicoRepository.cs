using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ITopicoRepository:ICrudRepository<Topico>
    {
        List<Topico> FindByDepartamento(int departamento_id, string identificador);
        bool ExistByDepartamento(int departamento_id, string identificador);

		List<Topico> Departamento_x_tickets();
	}
}
