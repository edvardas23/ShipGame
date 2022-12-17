using System.Net.Sockets;

namespace GameClient.Net
{
	public interface IProxyInterface
	{
		public TcpClient ValidateConnection(string username);
	}
}
