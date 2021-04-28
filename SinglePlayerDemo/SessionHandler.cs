using Core;
using Core.Event;
using Messages;
using Messages.Client;
using Messages.Server;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Messages.Models;

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
			InitCharacter(request.Character);
			// send CharacterOverview if we assign a location, provide starter gear, change the character's level, etc.
			OnCharacterOverviewRequest(server, args, new CharacterOverviewRequest(request.Character.Classification.Realm));
		}

		[AutowiredHandler]
		public void OnRegionListRequest(Server server, MessageEventArgs args, RegionListRequest request)
		{
			SelectedCharacter = Characters[request.CharacterSlot];
			if (SelectedCharacter == null)
			{
				// TODO send RegionList
				throw new System.NotImplementedException();
			}
			else
			{
				var region = GetRegion(args.Session, SelectedCharacter.Region);
				var response = new CharacterRegion(region);
				args.Session.Send(response);
			}
		}

		[AutowiredHandler]
		public void OnGameOpenRequest(Server server, MessageEventArgs args, GameOpenRequest request)
		{
			// DoL records the current time as the last UDP ping time
			var response = new GameOpenResponse(request.ConfirmUdp);
			args.Session.Send(response);
			var status = new CharacterStatus(SelectedCharacter);
			args.Session.Send(status);
			var points = new CharacterPoints(SelectedCharacter.Points);
			args.Session.Send(points);
			// TODO send DisableSkills whenever we have any skills
			// DoL sends nothing if no skills are on cooldown
		}

		[AutowiredHandler]
		public void OnWorldInitRequest(Server server, MessageEventArgs args, WorldInitRequest request)
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

			var position = new PositionAndObjectId(SelectedCharacter);
			args.Session.Send(position);
			var debug = new DebugMode();
			args.Session.Send(debug);
		}

		[AutowiredHandler]
		public void OnCharacterInitRequest(Server server, MessageEventArgs args, CharacterInitRequest request)
		{
			var finished = new CharacterInitFinished();
			args.Session.Send(finished);
		}

		[AutowiredHandler]
		public void OnSlashCommand(Server server, MessageEventArgs args, SlashCommand command)
		{
			// client sends "&gc info 1" on entering the world
			var split = command.Command.Split(' ');
			switch(split[0].ToLower(CultureInfo.InvariantCulture))
			{
				case "&quit":
					args.Session.Send(new Quit(false));
					break;
				case "&exit":
					args.Session.Send(new Quit(true));
					break;
				case "&speed":
					if(split.Length > 1 && ushort.TryParse(split[1], out ushort value))
					{
						var speed = new CharacterSpeed(value, 100, false);
						args.Session.Send(speed);
					}
					break;
				case "&up":
					if (split.Length > 1 && ushort.TryParse(split[1], out ushort height))
					{
						var coords = SelectedCharacter.Coordinates;
						SelectedCharacter.Coordinates = new Coordinates(coords.X, coords.Y, coords.Z + height, coords.Heading);
						var position = new PositionAndObjectId(SelectedCharacter);
						args.Session.Send(position);
					}
					break;
			}
		}

		[AutowiredHandler]
		public void OnPositionUpdate(Server server, MessageEventArgs args, PositionUpdate update)
		{
			SelectedCharacter.Coordinates = update.Coordinates;
		}

		// temporary hacks

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

		private Region GetRegion(Session session, ushort id)
		{
			// TODO make region addresses part of the server configuration
			// a server behind a NAT firewall does not know what the client thinks its address is
			var ep = session.LocalEndPoint;
			return new Region(id, ep.Address, ep.Port);
		}
	}
}
