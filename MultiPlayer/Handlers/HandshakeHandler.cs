using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;

namespace MultiPlayer.Handlers
{
	public class HandshakeHandler : IHandler
	{
		[AutowiredHandler]
		public void OnHandshake(Server server, MessageEventArgs args, Handshake handshake)
		{
			// TODO restrict client versions?
			var response = new HandshakeResponse(args.Session.Version.ToString(), args.Session.Version.Build);
			args.Session.Send(response);
		}
	}
}
