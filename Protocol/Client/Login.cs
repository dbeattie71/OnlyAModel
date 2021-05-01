using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class Login
	{
		public string User { get; }
		public string Password { get; }

		private Login(string user, string password)
		{
			User = user;
			Password = password;
		}

		[Unmarshaller(MessageType.Client.Login)]
		public static Login Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			reader.Skip(7); // client version junk
			var user = reader.ReadDaocString();
			var pass = reader.ReadDaocString();
			return new Login(user, pass);
		}
	}
}
