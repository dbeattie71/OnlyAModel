using Core;
using System;

namespace Protocol.Server
{
	public class Quit : ISendable
	{
		private readonly byte _exitClient;

		public Quit(bool exitClient)
		{
			_exitClient = (byte)(exitClient ? 0x01 : 0x00);
		}

		public byte Type => MessageType.Server.Quit;

		public int Length(int protocolVersion) => 2;

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			span[0] = _exitClient;
		}
	}
}
