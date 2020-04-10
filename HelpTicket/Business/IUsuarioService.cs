﻿using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IUsuarioService : ICrudService<Usuario>
    {
        bool AsignarToken(string correo, out string msm);
        bool EnviarEmailRecuperarContra(string correo);
    }
}