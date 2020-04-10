using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public interface IUsuarioRepository:ICrudRepository<Usuario>
	{
        string ValidarCorreo(string correo, out string msm);
        bool AsignarToken(string codigo, string token, out string msm);
        string GetTokenByEmail(string correo);
    }
}
