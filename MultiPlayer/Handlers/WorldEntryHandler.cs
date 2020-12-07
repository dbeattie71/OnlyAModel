using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;
using Microsoft.Extensions.Logging;
using Models.World;
using MultiPlayer.Services;
using System;

namespace MultiPlayer.Handlers
{
	public class WorldEntryHandler : IHandler
	{
		private readonly CharacterService _characters;
		private readonly ILogger _log;

		public WorldEntryHandler(CharacterService characters, ILogger<WorldEntryHandler> log)
		{
			_characters = characters;
			_log = log;
		}

		[AutowiredHandler]
		public void OnRegionListRequest(Server server, MessageEventArgs args, RegionListRequest request)
		{
			var data = args.Session.Data();
			// TODO if this never throws, we don't need the character service
			var ch = _characters.GetBySlot(data.User, request.CharacterSlot);
			if (data.SelectedCharacter != ch)
			{
				throw new Exception("Apparently we do need to set SelectedCharacter in WorldEntryHandler...");
				//data.SelectedCharacter = ch;
			}
			if (data.SelectedCharacter == null)
			{
				// TODO send RegionList?
				throw new System.NotImplementedException();
			}
			else
			{
				var region = GetRegion(args.Session);
				var response = new CharacterRegion(region);
				args.Session.Send(response);
			}
		}

		private Region GetRegion(Session session)
		{
			// TODO make region addresses part of the server configuration
			// a server behind a NAT firewall does not know what the client thinks its address is
			var ep = session.LocalEndPoint;
			return new Region(session.Data().SelectedCharacter.Region, ep.Address, ep.Port);
		}

		[AutowiredHandler]
		public void OnGameOpenRequest(Server server, MessageEventArgs args, GameOpenRequest request)
		{
			// DoL records the current time as the last UDP ping time
			var response = new GameOpenResponse(request.ConfirmUdp);
			args.Session.Send(response);
			var ch = args.Session.Data().SelectedCharacter;
			var status = new CharacterStatus(ch);
			args.Session.Send(status);
			var points = new CharacterPoints(ch.Points);
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

			var position = new PositionAndObjectId(args.Session.Data().SelectedCharacter);
			args.Session.Send(position);
			var debug = new DebugMode();
			args.Session.Send(debug);
		}

		[AutowiredHandler]
		public void OnCharacterInitRequest(Server server, MessageEventArgs args, CharacterInitRequest request)
		{
			// called when the progress bar completes on the loading screen
			// TODO this might fit better in PositionHandler
			var finished = new CharacterInitFinished();
			args.Session.Send(finished);
		}
	}
}
