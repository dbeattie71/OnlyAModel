using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;

namespace MultiPlayer.Handlers
{
	public class PositionHandler : IHandler
	{
		[AutowiredHandler]
		public void OnPositionUpdate(Server server, MessageEventArgs args, PositionUpdate update)
		{
			// TODO notify session manager
			args.Session.GetState().SelectedCharacter.Coordinates = update.Coordinates;
		}

		// TODO handle heading update
	}
}
