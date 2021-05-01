using Core.Event;
using Protocol.Autowire;
using Protocol.Client;
using Protocol.Server;

namespace Demo
{
	class Handlers
	{
		[Handler]
		public void OnHandshake(MessageEventArgs args, Handshake _)
		{
			args.Session.Send(new HandshakeResponse(args.Session.ClientInfo.Version, args.Session.ClientInfo.Build));
		}
	}
}
