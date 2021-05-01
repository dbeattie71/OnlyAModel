using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class LoginRequest
	{
		public string User { get; }
		public string Password { get; }

		private LoginRequest(string user, string password)
		{
			User = user;
			Password = password;
		}

		[Unmarshaller(MessageType.Client.LoginRequest)]
		public static LoginRequest Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			reader.Skip(7); // client version junk
			var user = reader.ReadDaocString();
			var pass = reader.ReadDaocString();
			return new LoginRequest(user, pass);
		}
	}
}
