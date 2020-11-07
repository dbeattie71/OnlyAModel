using System;

namespace Messages.ClientToServer
{
	public class SlashCommand
	{
		public string Command { get; }

		private SlashCommand(string command)
		{
			Command = command;
		}

		[AutowiredFactory(MessageType.ClientToServer.SlashCommand)]
		public static SlashCommand Unmarshal(ReadOnlyMemory<byte> payload)
		{
			var reader = new SpanReader(payload.Span);
			reader.Skip(8);
			// this is basically what DoL does
			// not sure how long the string can actually be
			var command = reader.ReadFixedString(255);
			return new SlashCommand(command);
		}
	}
}
