using Core.Event;
using Protocol;
using Protocol.Autowire;
using Protocol.Client;
using Protocol.Models;
using Protocol.Server;
using System.Globalization;
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
		public void OnLoginRequest(MessageEventArgs args, LoginRequest login)
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
		public void OnCharacterSelectRequest(MessageEventArgs args, CharacterSelectRequest request)
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
		public void OnCharacterCreateRequest(MessageEventArgs args, CharacterCreateRequest request)
		{
			_characters[request.Slot] = request.Character;
			// init character
			request.Character.Status = new Status()
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
			// everyone starts in cotswold
			request.Character.Region = 1;
			request.Character.Coordinates = new Coordinates(560467, 511652, 2344, 3398);
			// update the client with location, stats, level, starter gear, etc.
			OnCharacterOverviewRequest(args, new CharacterOverviewRequest(request.Character.Classification.Realm));
		}

		[Handler]
		public void OnRegionRequest(MessageEventArgs args, RegionRequest request)
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

		[Handler]
		public void OnGameOpenRequest(MessageEventArgs args, GameOpenRequest request)
		{
			// DoL records the current time as the last UDP ping time
			var response = new GameOpenResponse(request.ConfirmUdp);
			args.Session.Send(response);
			var status = new CharacterStatus(_selected);
			args.Session.Send(status);
			var points = new CharacterPoints(_selected.Points);
			args.Session.Send(points);
			// TODO send DisableSkills whenever we have any skills
			// DoL sends nothing if no skills are on cooldown
		}

		[Handler]
		public void OnWorldInit(MessageEventArgs args, WorldInitRequest request)
		{
			// DoL sends a ton of messages here
			// most of these are sent in WorldInitRequestHandler.cs
			// not sure where AddFriend and the last CharacterStatusUpdate come from

			// AddFriend - ???
			// PositionAndObjectId
			// Encumberance
			// MaxSpeed
			// MaxSpeed
			// CharacterStatusUpdate
			// InventoryUpdate (equipment)
			// InventoryUpdate (inventory)
			// VariousUpdate (skills)
			// VariousUpdate (crafting skills)
			// DelveInfo times a million ("update player")
			// VariousUpdate (apparently part of "update player")
			// MoneyUpdate
			// StatsUpdate
			// VariousUpdate (resists, icons, weapon and armor stats)
			// QuestEntry
			// CharacterStatusUpdate
			// CharacterPointsUpdate
			// Encumberance
			// ConcentrationList
			// CharacterStatusUpdate - ???
			// ObjectGuildId
			// DebugMode
			// MaxSpeed
			// ControlledHorse

			var position = new PositionAndObjectId(_selected);
			args.Session.Send(position);
			var debug = new DebugMode();
			args.Session.Send(debug);
		}

		[Handler]
		public void OnCharacterInitRequest(MessageEventArgs args, CharacterInitRequest _)
		{
			args.Session.Send(new CharacterInitFinished());
		}

		[Handler]
		public void OnPositionUpdate(MessageEventArgs args, PositionUpdate update)
		{
			_selected.Coordinates = update.Coordinates;
		}

		[Handler]
		public void OnSlashCommand(MessageEventArgs args, SlashCommand command)
		{
			// client sends "&gc info 1" on entering the world
			var split = command.Command.Split(' ');
			switch (split[0].ToLower(CultureInfo.InvariantCulture))
			{
				case "&quit":
					args.Session.Send(new Quit(false));
					break;
				case "&exit":
					args.Session.Send(new Quit(true));
					break;
				case "&speed":
					if (split.Length > 1 && ushort.TryParse(split[1], out ushort value))
					{
						var speed = new CharacterSpeed(value, 100, false);
						args.Session.Send(speed);
					}
					break;
				case "&up":
					if (split.Length > 1 && ushort.TryParse(split[1], out ushort height))
					{
						var coords = _selected.Coordinates;
						_selected.Coordinates = new Coordinates(coords.X, coords.Y, coords.Z + height, coords.Heading);
						var position = new PositionAndObjectId(_selected);
						args.Session.Send(position);
					}
					break;
			}
		}
	}
}
