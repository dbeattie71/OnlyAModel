using Core.Event;
using Protocol;
using Protocol.Autowire;
using Protocol.Client;
using Protocol.Models;
using Protocol.Server;
using System.Linq;
using System.Net;

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
		public void OnCharacterSelect(MessageEventArgs args, CharacterSelect request)
		{
			_selected = _characters.SingleOrDefault(o => request.Name.EqualsIgnoreCase(o?.Name));
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

		[Handler]
		public void OnNameCheck(MessageEventArgs args, NameCheck request)
		{
			var status = NameStatus.Available;
			// the name "noname" has a special meaning in CharacterSelect
			if (request.Name.EqualsIgnoreCase("noname"))
			{
				status = NameStatus.Prohibited;
			}
			else if (_characters.Any(o => request.Name.EqualsIgnoreCase(o?.Name)))
			{
				status = NameStatus.Unavailable;
			}
			var response = new NameCheckResponse(request.Name, request.User, status);
			args.Session.Send(response);
		}

		[Handler]
		public void OnCharacterCreate(MessageEventArgs args, CharacterCreate request)
		{
			_characters[request.Slot] = request.Character;
			InitCharacter(request.Character);
			// send CharacterOverview to update the client if we assign a location, provide starter gear, change the character's level, etc.
			OnCharacterOverviewRequest(args, new CharacterOverviewRequest(request.Character.Classification.Realm));
		}

		[Handler]
		public void OnCharacterRegionRequest(MessageEventArgs args, CharacterRegionRequest request)
		{
			_selected = _characters[request.CharacterSlot];
			if (_selected == null)
			{
				// TODO send RegionList
				throw new System.NotImplementedException();
			}
			else
			{
				// TODO make region addresses part of server configuration
				// a server behind a NAT firewall does not know what a client thinks its address is
				var ep = args.Session.LocalEndPoint;
				var region = new Region(_selected.Region, ep.Address, ep.Port);
				var response = new CharacterRegion(region);
				args.Session.Send(response);
			}
		}

		// hacks

		private void InitCharacter(Character c)
		{
			c.Status = new Status()
			{
				Health = 100,
				MaxHealth = 100,
				Mana = 100,
				MaxMana = 100,
				Endurance = 100,
				MaxEndurance = 100,
				Concentration = 100,
				MaxConcentration = 100
			};
			// cotswold
			c.Region = 1;
			c.Coordinates = new Coordinates(560467, 511652, 2344, 3398);
		}
	}
}
