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
		bool logeo(string codigo,string contraseña);
<<<<<<< HEAD
=======
        bool VerificarToken(string token, out string codigo);
        bool ActualizarContraseña(string codigo, string contra, out string msm);
>>>>>>> 3d87a9705ccfca685b47ff329697db7141d27bc9
    }
}