using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;
using Models.Character;
using Models.World;
using System.Linq;

namespace MultiPlayer.Handlers
{
	public class CharSelectHandler : IHandler
	{
		[AutowiredHandler]
		public void OnCharacterSelectRequest(Server server, MessageEventArgs args, CharacterSelectRequest request)
		{
			var state = args.Session.Data();
			state.SelectedCharacter = state.Characters.SingleOrDefault(o => request.Name.EqualsIgnoreCase(o?.Name));
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
				var response = new CharacterOverview(args.Session.Data().Characters.Skip(offset).Take(10));
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
			// TODO check session manager (and eventually character service) for name availability
			else if (args.Session.Data().Characters.Any(o => request.Name.EqualsIgnoreCase(o?.Name)))
			{
				status = NameStatus.Unavailable;
			}
			var response = new NameCheckResponse(request.Name, request.User, status);
			args.Session.Send(response);
		}

		[AutowiredHandler]
		public void OnCharacterCreateRequest(Server server, MessageEventArgs args, CharacterCreateRequest request)
		{
			args.Session.Data().Characters[request.Slot] = request.Character;
			InitCharacter(request.Character);
			// send CharacterOverview if we assign a location, provide starter gear, change the character's level, etc.
			OnCharacterOverviewRequest(server, args, new CharacterOverviewRequest(request.Character.Classification.Realm));
		}

		private void InitCharacter(Character c)
		{
			// TODO real stats and starting locations
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
