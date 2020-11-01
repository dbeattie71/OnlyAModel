using Core;
using System;

namespace Messages.ClientToServer
{
	public class CharacterSelectRequest
	{
		public string Name { get; }
		public string Language { get; }

		private CharacterSelectRequest(string name, string language)
		{
			Name = name;
			Language = language;
		}

		[AutowiredFactory(MessageType.ClientToServer.CharacterSelectRequest)]
		public static CharacterSelectRequest Unmarshall(ReadOnlyMemory<byte> payload)
		{
			var reader = new SpanReader(payload.Span);
			reader.Skip(2); // DoL reads this as a byte or ushort called "type", but never uses it
			var name = reader.ReadFixedString(24);
			var language = reader.ReadFixedString(6);
			return new CharacterSelectRequest(name, language);
		}
	}
}
