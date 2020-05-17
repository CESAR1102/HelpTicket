using Entity;
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
		Usuario logeo(string codigo,string contraseña);
        bool VerificarToken(string token, out string codigo);
        bool ActualizarContraseña(string codigo, string contra, out string msm);
        string ObtenerCorreo(string codigo);
        string ObtenerAdministrador(string codigo);
        List<Usuario> ObtenerTrabajadores();
    }
}