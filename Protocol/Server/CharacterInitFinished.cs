using Core;
using System;

namespace Protocol.Server
{
	public class CharacterInitFinished : ISendable
	{
		public byte Type => MessageType.Server.CharacterInitFinished;

		public int Length(int protocolVersion) => 1;

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			// DoL calls the payload "mobs", hardcodes 0x00
		}
	}
}
