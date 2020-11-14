using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;

namespace MultiPlayer.Handlers
{
	public class PingHandler : IHandler
	{
		[AutowiredHandler]
		public void OnPingRequest(Server server, MessageEventArgs args, PingRequest ping)
		{
			var response = new PingReply(args.Message.Sequence, ping.Timestamp);
			args.Session.Send(response);
		}
	}
}
