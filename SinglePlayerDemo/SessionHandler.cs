using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Models.Character;
using Messages.ServerToClient;
using System.Collections.Generic;
using System.Linq;
using Models.World;

namespace SinglePlayerDemo
{
	public class SessionHandler
	{
		private List<Character> Characters { get; } = new List<Character>(new Character[30]);
		private Character SelectedCharacter { get; set; }

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
			SelectedCharacter = Characters.SingleOrDefault(o => request.Name.EqualsIgnoreCase(o?.Name));
			// notes in DoL say live sends LoginGranted again, but it doesn't seem to be necessary
			// TODO does the ID we give to the client even matter?
			var response = new SessionId((ushort)args.Session.Id);
			args.Session.Send(response);
		}

		[AutowiredHandler]
		public void OnCharacterOverviewRequest(Server server, MessageEventArgs args, CharacterOverviewRequest request)
		{
			if (request.Realm == Realm.None)
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
			if (request.Name.EqualsIgnoreCase("noname"))
			{
				status = NameStatus.Prohibited;
			}
			else if (Characters.Any(o => request.Name.EqualsIgnoreCase(o?.Name)))
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
			// send CharacterOverview if we assign a location, provide starter gear, change the character's level, etc.
			//OnCharacterOverviewRequest(server, args, new CharacterOverviewRequest(request.Character.Classification.Realm));
		}

		[AutowiredHandler]
		public void OnRegionListRequest(Server server, MessageEventArgs args, RegionListRequest request)
		{
			SelectedCharacter = Characters[request.CharacterSlot];
			if (SelectedCharacter == null)
			{
				// TODO send RegionList
			}
			else
			{
				var region = GetRegion(args.Session, SelectedCharacter.Region);
				var response = new CharacterRegion(region);
				args.Session.Send(response);
			}
		}

		// temporary hacks

		private Region GetRegion(Session session, ushort id)
		{
			// TODO make region addresses part of the server configuration
			// a server behind a NAT firewall does not know what the client thinks its address is
			var ep = session.LocalEndPoint;
			return new Region(id, ep.Address, ep.Port);
		}
	}
}
