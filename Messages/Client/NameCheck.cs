using System;

namespace Messages.Client
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

		[AutowiredFactory(MessageType.Client.NameCheck)]
		public static NameCheck Unmarshall(ReadOnlyMemory<byte> payload)
		{
			var reader = new SpanReader(payload.Span);
			var name = reader.ReadFixedString(30);
			var user = reader.ReadFixedString(24);
			return new NameCheck(name, user);
		}
	}
}
