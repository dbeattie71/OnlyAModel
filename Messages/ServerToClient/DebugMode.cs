using System;

namespace Messages.ServerToClient
{
	public class DebugMode : IPayload
	{
		public byte MessageType => Messages.MessageType.ServerToClient.DebugMode;

		public int Length => 2;

		public void Marshal(Span<byte> span)
		{
			// TODO send 0x01, 0x00 for debug mode
		}
	}
}
