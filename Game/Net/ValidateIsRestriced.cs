using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameClient.Net
{
	public class ValidateIsRestriced : IProxyState
	{
		public bool HandleRequest(TcpClient tcpClient, string username)
		{
			List<string> restricted = GetRestriced();
			if (restricted.Contains(username.ToLower()))
			{
				MessageBox.Show("Jūsų naudotojo vardas yra užblokuotas!");
				return false;
			}
			return true;
		}

		private List<string> GetRestriced()
		{
			List<string> ret = new List<string>
			{
				"edvardas",
				"nojus"
			};
			return ret;
		}
	}
}
