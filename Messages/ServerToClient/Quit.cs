using System;

namespace Messages.ServerToClient
{
	public class Quit : IPayload
	{
		private readonly byte _exitClient;

		public Quit(bool exitClient)
		{
			_exitClient = (byte)(exitClient ? 0x01 : 0x00);
		}

		public byte MessageType => Messages.MessageType.ServerToClient.Quit;

		public int Length => 2;

		public void Marshal(Span<byte> span)
		{
			span[0] = _exitClient;
		}
	}
}
