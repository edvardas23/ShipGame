using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameClient.Net
{
	public class ValidateUsernameState : IProxyState
	{
		public bool HandleRequest(TcpClient tcpClient, string username)
		{
			if (username.Length >= 4)
			{
				return true;
			}
			else
			{
				MessageBox.Show("Jūsų naudotojo vardas per trumpas!\n Minimalus simbolių kiekis 4!");
				return false;
			}
		}
	}
}
