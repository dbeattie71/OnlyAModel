using System;

namespace Messages.ServerToClient
{
	public class CharacterInitFinished : IPayload
	{
		public byte MessageType => Messages.MessageType.ServerToClient.CharacterInitFinished;

		public int Length => 1;

		public void Marshal(Span<byte> span)
		{
			// DoL calls the payload "mobs", hardcodes 0x00
		}
	}
}
