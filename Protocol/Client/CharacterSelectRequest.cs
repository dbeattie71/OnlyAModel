using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
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

		[Unmarshaller(MessageType.Client.CharacterSelectRequest)]
		public static CharacterSelectRequest Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			reader.Skip(2); // DoL reads this as a byte or ushort called "type", but never uses it
			var name = reader.ReadFixedString(24);
			var language = reader.ReadFixedString(6);
			return new CharacterSelectRequest(name, language);
		}
	}
}
