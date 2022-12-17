using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameClient.Net
{
	public class GotSuccessState : IProxyState
	{
		public bool HandleRequest(TcpClient tcpClient, string username)
		{
			tcpClient.Connect("127.0.0.1", 6969);
			if(tcpClient.Connected)
				MessageBox.Show($"Naudotojo {username} prisijungimas sėkmingas!");
			return true;
		}
	}
}
