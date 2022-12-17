using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Net
{
	public class ProxyService : IProxyInterface
	{
		private IProxyState? _state;
		private bool _continue;
		private TcpClient _tcpClient = new TcpClient();

		public TcpClient ValidateConnection(string username)
		{
			SetState(new ValidateIsConnected());
			_continue = _state.HandleRequest(_tcpClient, username);

			if (_continue)
			{
				SetState(new ValidateUsernameState());
				_continue = _state.HandleRequest(_tcpClient, username);
			}

			if (_continue)
			{
				SetState(new ValidateIsRestriced());
				_continue = _state.HandleRequest(_tcpClient ,username);
			}

			if (_continue)
				SetState(new GotSuccessState());
			else
				SetState(new GotFailState());
			_continue = _state.HandleRequest(_tcpClient, username);
			
			if(_continue)
			{
				return _tcpClient;
			}

			return null;
		}
		private void SetState(IProxyState newState)
		{
			_state = newState;
		}
	}
}
