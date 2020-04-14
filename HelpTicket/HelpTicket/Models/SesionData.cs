using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.Models
{
	public class SesionData
	{
		private string session;
		public string getSession(string name)
		{
			this.session = Convert.ToString(HttpContext.Current.Session[name]);
			return session;
		}
		public void setSession(string name, string data)
		{
			HttpContext.Current.Session[name] = data;
		}
		public void destroySession()
		{
			HttpContext.Current.Session.Abandon();
		}
	}
}