using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;
using Models.Character;
using MultiPlayer.Services;
using System.Linq;

namespace MultiPlayer.Handlers
{
	public class CharSelectHandler : IHandler
	{
		private readonly CharacterService _service;

		public CharSelectHandler(CharacterService service)
		{
			_service = service;
		}

		[AutowiredHandler]
		public void OnCharacterSelectRequest(Server server, MessageEventArgs args, CharacterSelectRequest request)
		{
			var data = args.Session.Data();
			data.SelectedCharacter = _service.GetByName(data.User, request.Name);
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
				var chars = _service.GetByRealm(args.Session.Data().User, request.Realm);
				var response = new CharacterOverview(chars);
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
			else if (_service.NameTaken(request.Name))
			{
				status = NameStatus.Unavailable;
			}
			var response = new NameCheckResponse(request.Name, request.User, status);
			args.Session.Send(response);
		}

		[AutowiredHandler]
		public void OnCharacterCreateRequest(Server server, MessageEventArgs args, CharacterCreateRequest request)
		{
			_service.Create(args.Session.Data().User, request.Character, request.Slot);
			// send CharacterOverview if we assign a location, provide starter gear, change the character's level, etc.
			OnCharacterOverviewRequest(server, args, new CharacterOverviewRequest(request.Character.Classification.Realm));
		}
	}
}
