using System;

namespace Messages.ServerToClient
{
	public class LoginDenied : IPayload
	{
		private readonly LoginError _error;

		public LoginDenied(LoginError error)
		{
			_error = error;
		}

		public byte MessageType => Messages.MessageType.ServerToClient.LoginDenied;

		public int Length => 5;

		public void Marshal(Span<byte> span)
		{
			span[0] = (byte)_error;
			// DoL sends client version in the remaining bytes
		}
	}
}
