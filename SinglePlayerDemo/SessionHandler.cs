using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;

namespace SinglePlayerDemo
{
	public class SessionHandler
	{
		[AutowiredHandler]
		public void OnCryptKeyRequest(Server server, MessageEventArgs args, Handshake handshake)
		{
			args.Session.Send(new HandshakeResponse(args.Session.Version.ToString(), args.Session.Version.Build));
		}

		[AutowiredHandler]
		public void OnLoginRequest(Server server, MessageEventArgs args, LoginRequest login)
		{
			;
		}
	}
}
