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
	public class UsuarioService : IUsuarioService
	{
		private IUsuarioRepository usuario = new UsuarioRepository();

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<Usuario> FindAll()
		{
			
			return usuario.FindAll();
		}
		
		public Usuario FindById(int? id)
		{
			throw new NotImplementedException();
		}

		public bool Insert(Usuario t)
		{
			throw new NotImplementedException();
		}

		public bool Update(Usuario t)
		{
			throw new NotImplementedException();
		}
	}
}
