using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;
using Models.World;
using MultiPlayer.Services;

namespace MultiPlayer.Handlers
{
	public class AuthHandler : IHandler
	{
		private readonly AuthService _service;

		public AuthHandler(AuthService service)
		{
			_service = service;
		}

		[AutowiredHandler]
		public void OnLoginRequest(Server server, MessageEventArgs args, LoginRequest login)
		{
			if (_service.Authenticate(login.User, login.Password))
			{
				args.Session.Data().User = login.User;
				// TODO get server name and PvP mode from config
				var response = new LoginGranted(login.User, "Only A Model", PvPMode.PvE);
				args.Session.Send(response);
			}
			else
			{
				var response = new LoginDenied(LoginError.WrongPassword);
				args.Session.Send(response);
			}
		}
	}
}
