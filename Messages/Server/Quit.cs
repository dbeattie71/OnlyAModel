using System;

namespace Messages.Server
{
	public class Quit : IServerMessage
	{
		private readonly byte _exitClient;

		public Quit(bool exitClient)
		{
			_exitClient = (byte)(exitClient ? 0x01 : 0x00);
		}

		public byte Type => MessageType.Server.Quit;

		public int Length => 2;

		public void Marshal(Span<byte> span)
		{
			span[0] = _exitClient;
		}
	}
}
