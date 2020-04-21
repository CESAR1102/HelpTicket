using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.Models
{
	public class SesionData
	{
		//private string session;
        private Entity.Usuario session;
		public Entity.Usuario getSession(string name)
		{
            this.session = (Entity.Usuario)HttpContext.Current.Session[name];
            return session;
		}
		public void setSession(string name, Entity.Usuario data)
		{
			HttpContext.Current.Session[name] = data;
		}
		public void destroySession()
		{
			HttpContext.Current.Session.Abandon();
		}
	}
}