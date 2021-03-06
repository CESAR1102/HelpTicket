﻿using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ITopicoService: ICrudService<Topico>
    {
        List<Topico> FindByDepartamento(int departamento_id, string identificador);
        bool ExistByDepartamento(int departamento_id, string identificador);
		List<Topico> Departamento_x_tickets();
	}
}
