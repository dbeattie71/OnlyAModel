using System;

namespace Messages.Server
{
	public class CharacterInitFinished : IServerMessage
	{
		public byte Type => MessageType.Server.CharacterInitFinished;

		public int Length => 1;

		public void Marshal(Span<byte> span)
		{
			// DoL calls the payload "mobs", hardcodes 0x00
		}
	}
}
