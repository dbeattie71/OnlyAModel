using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.Data;
using Messages.ServerToClient;

namespace SinglePlayerDemo
{
	public class SessionHandler
	{
		[AutowiredHandler]
		public void OnCryptKeyRequest(Server server, MessageEventArgs args, Handshake handshake)
		{
			var response = new HandshakeResponse(args.Session.Version.ToString(), args.Session.Version.Build);
			args.Session.Send(response);
		}

		[AutowiredHandler]
		public void OnLoginRequest(Server server, MessageEventArgs args, LoginRequest login)
		{
			var response = new LoginGranted(login.User, "Only A Model");
			args.Session.Send(response);

		}

		[AutowiredHandler]
		public void OnPingRequest(Server server, MessageEventArgs args, PingRequest ping)
		{
			var response = new PingReply(args.Message.Sequence, ping.Timestamp);
			args.Session.Send(response);
		}

		[AutowiredHandler]
		public void OnCharacterSelectRequest(Server server, MessageEventArgs args, CharacterSelectRequest request)
		{
			// notes in DoL say live sends LoginGranted again, but it doesn't seem to be necessary
			// TODO does the ID we give to the client even matter?
			var response = new SessionId((ushort)args.Session.Id);
			args.Session.Send(response);

			// TODO how does the client react if we pretend it requested quick login?
		}

		[AutowiredHandler]
		public void OnCharacterOverviewRequest(Server server, MessageEventArgs args, CharacterOverviewRequest request)
		{
			if(request.Realm == Realm.None)
			{
				// respond with the player's selected realm, or Realm.None if the player has
				// not selected a realm or players can create characters in multiple realms
				var response = new PlayerRealm(Realm.None);
				args.Session.Send(response);
			}
			else
			{
				// client is requesting character details for a particular realm
				var response = new CharacterOverview();
				args.Session.Send(response);
			}
		}
	}
}
