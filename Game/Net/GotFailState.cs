using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Net
{
	public class GotFailState : IProxyState
	{
		public bool HandleRequest(TcpClient tcpClient, string username)
		{
			return false;
		}
	}
}
