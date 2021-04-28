using System;

namespace Messages.Server
{
	public class DebugMode : IServerMessage
	{
		public byte Type => MessageType.Server.DebugMode;

		public int Length => 2;

		public void Marshal(Span<byte> span)
		{
			// TODO send 0x01, 0x00 for debug mode
		}
	}
}
