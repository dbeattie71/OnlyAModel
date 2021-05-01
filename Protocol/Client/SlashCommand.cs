using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class SlashCommand
	{
		public string Command { get; }

		private SlashCommand(string command)
		{
			Command = command;
		}

		[Unmarshaller(MessageType.Client.SlashCommand)]
		public static SlashCommand Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			reader.Skip(8);
			// this is basically what DoL does
			// not sure how long the string can actually be
			var command = reader.ReadFixedString(255);
			return new SlashCommand(command);
		}

	}
}
