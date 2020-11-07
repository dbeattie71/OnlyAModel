using System;

namespace Messages.ClientToServer
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

		[AutowiredFactory(MessageType.ClientToServer.LoginRequest)]
		public static LoginRequest Unmarshall(ReadOnlyMemory<byte> payload)
		{
			var reader = new SpanReader(payload.Span);
			reader.Skip(7); // client version junk
			var user = reader.ReadDaocString();
			var pass = reader.ReadDaocString();
			return new LoginRequest(user, pass);
		}
	}
}
