using System.Net.Sockets;

namespace GameClient.Net
{
	public interface IProxyState
	{
		public bool HandleRequest(TcpClient tcpClient, string username);
	}
}
