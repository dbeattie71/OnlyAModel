using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class NameCheck
	{
		public string Name { get; }
		public string User { get; }

		private NameCheck(string name, string user)
		{
			Name = name;
			User = user;
		}

		[Unmarshaller(MessageType.Client.NameCheck)]
		public static NameCheck Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			var name = reader.ReadFixedString(30);
			var user = reader.ReadFixedString(24);
			return new NameCheck(name, user);
		}
	}
}
