using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;
using Models.World;

namespace MultiPlayer.Handlers
{
	public class AuthHandler : IHandler
	{
		[AutowiredHandler]
		public void OnLoginRequest(Server server, MessageEventArgs args, LoginRequest login)
		{
			// TODO use auth service
			// TODO get server name and PvP mode from config
			// TODO notify session manager
			var response = new LoginGranted(login.User, "Only A Model", PvPMode.PvE);
			args.Session.Send(response);
		}
	}
}
