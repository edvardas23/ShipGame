using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameClient.Net
{
	public class ValidateIsConnected : IProxyState
	{
		public bool HandleRequest(TcpClient tcpClient, string username)
		{
			if (!tcpClient.Connected)
			{
				return true;
			}
			else
			{
				MessageBox.Show("Jūs jau esate prisijungęs!");
				return false;
			}
		}
	}
}
