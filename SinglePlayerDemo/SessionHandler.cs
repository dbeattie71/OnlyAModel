using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.Models;
using Messages.Models.Character;
using Messages.ServerToClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinglePlayerDemo
{
	public class SessionHandler
	{
		private List<Character> Characters { get; } = new List<Character>(new Character[30]);

		[AutowiredHandler]
		public void OnCryptKeyRequest(Server server, MessageEventArgs args, Handshake handshake)
		{
			var response = new HandshakeResponse(args.Session.Version.ToString(), args.Session.Version.Build);
			args.Session.Send(response);
		}

		[AutowiredHandler]
		public void OnLoginRequest(Server server, MessageEventArgs args, LoginRequest login)
		{
			var response = new LoginGranted(login.User, "Only A Model", PvPMode.PvE);
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
				int offset = ((int)request.Realm - 1) * 10;
				var response = new CharacterOverview(Characters.Skip(offset).Take(10));
				args.Session.Send(response);
			}
		}

		[AutowiredHandler]
		public void OnNameCheck(Server server, MessageEventArgs args, NameCheck request)
		{
			var status = NameStatus.Available;
			// the name "noname" has special meaning in CharacterSelectRequest
			if(request.Name.EqualsIgnoreCase("noname"))
			{
				status = NameStatus.Prohibited;
			}
			else if(Characters.Any(o => request.Name.EqualsIgnoreCase(o?.Name)))
			{
				status = NameStatus.Unavailable;
			}
			var response = new NameCheckResponse(request.Name, request.User, status);
			args.Session.Send(response);
		}

		[AutowiredHandler]
		public void OnCharacterCreateRequest(Server server, MessageEventArgs args, CharacterCreateRequest request)
		{
			Characters[request.Slot] = request.Character;
			// the client automatically displays the character it just created, but doesn't know its location.
			// we can send CharacterOverview here if we want to assign it before first login.
			//request.Character.LocationDescription = "Constantine's Sound";
			//OnCharacterOverviewRequest(server, args, new CharacterOverviewRequest(request.Character.Classification.Realm));
		}
	}
}
