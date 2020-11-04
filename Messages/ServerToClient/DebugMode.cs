using Core;
using System;

namespace Messages.ServerToClient
{
	public class DebugMode : IPayload
	{
		public MessageType.ServerToClient Type => MessageType.ServerToClient.DebugMode;

		public int Length => 2;

		public void Marshal(Span<byte> span)
		{
			// TODO send 0x01, 0x00 for debug mode
		}
	}
}
