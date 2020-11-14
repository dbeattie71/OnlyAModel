using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;
using Messages.ServerToClient;
using Models.World;
using System.Globalization;

namespace MultiPlayer.Handlers
{
	public class CommandHandler : IHandler
	{
		[AutowiredHandler]
		public void OnSlashCommand(Server server, MessageEventArgs args, SlashCommand command)
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
						var ch = args.Session.GetState().SelectedCharacter;
						var coords = ch.Coordinates;
						ch.Coordinates = new Coordinates(coords.X, coords.Y, coords.Z + height, coords.Heading);
						var position = new PositionAndObjectId(ch);
						args.Session.Send(position);
					}
					break;
			}
		}
	}
}
