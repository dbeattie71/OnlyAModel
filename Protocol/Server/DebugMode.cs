using Core;
using System;

namespace Protocol.Server
{
	public class DebugMode : ISendable
	{
		public byte Type => MessageType.Server.DebugMode;

		public int Length(int protocolVersion) => 2;

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			// TODO send 0x01, 0x00 for debug mode
		}
	}
}
