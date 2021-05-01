using Core.Event;
using Protocol;
using Protocol.Autowire;
using Protocol.Client;
using Protocol.Models;
using Protocol.Server;
using System.Linq;

namespace Demo
{
	class Handlers
	{
		[Handler]
		public void OnHandshake(MessageEventArgs args, Handshake _)
		{
			args.Session.Send(new HandshakeResponse(args.Session.ClientInfo.Version, args.Session.ClientInfo.Build));
		}

		[Handler]
		public void OnLogin(MessageEventArgs args, Login login)
		{
			args.Session.Send(new LoginGranted(login.User, "Only A Model", PvPMode.PvE));
		}

		[Handler]
		public void OnPing(MessageEventArgs args, Ping ping)
		{
			args.Session.Send(new PingResponse(args.Message.Sequence, ping.Timestamp));
		}

		private Character[] _characters = new Character[30];
		private Character _selected = null;

		[Handler]
		public void OnCharacterSelect(MessageEventArgs args, CharacterSelect select)
		{
			_selected = _characters.SingleOrDefault(o => select.Name.EqualsIgnoreCase(o?.Name));
			// notes in DoL say live sends LoginGranted again, but it doesn't seem to be necessary
			// does it even matter what the client thinks its session ID is?
			args.Session.Send(new SessionId((ushort)args.Session.Id));
		}

		[Handler]
		public void OnCharacterOverviewRequest(MessageEventArgs args, CharacterOverviewRequest request)
		{
			if (request.Realm == Realm.None)
			{
				// client is testing which realm the player is in
				// respond with the player's selected realm, or Realm.None if the player has
				// not selected a realm or PvP mode allows characters in multiple realms
				var response = new PlayerRealm(Realm.None);
				args.Session.Send(response);
			}
			else
			{
				// client is requesting character details for a particular realm
				int offset = ((int)request.Realm - 1) * 10;
				var response = new CharacterOverview(_characters.Skip(offset).Take(10));
				args.Session.Send(response);
			}
		}
	}
}
