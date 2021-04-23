using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Microsoft.Extensions.Logging;

namespace MultiPlayer.Handlers
{
	public class PositionHandler : IHandler
	{
		private readonly ILogger _log;

		public PositionHandler(ILogger<PositionHandler> log)
		{
			_log = log;
		}

		[AutowiredHandler]
		public void OnPositionUpdate(Server server, MessageEventArgs args, PositionUpdate update)
		{
			args.Session.Data().SelectedCharacter.Coordinates = update.Coordinates;
			// TODO notify session manager
			_log.LogDebug("Heading: {0}", update.Coordinates.Heading);
		}

		[AutowiredHandler]
		public void OnHeadingUpdate(Server server, MessageEventArgs args, HeadingUpdate update)
		{
			args.Session.Data().SelectedCharacter.Heading = update.Heading;
			// TODO update other properties
			// TODO notify session manager
			_log.LogDebug("Heading: {0}", update.Heading);
		}
	}
}
