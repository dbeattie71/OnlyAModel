using Core;
using Protocol.Models;
using System;

namespace Protocol.Server
{
	public class CharacterRegion : ISendable
	{
		private readonly Region _region;

		public CharacterRegion(Region region)
		{
			_region = region;
		}

		public byte Type => MessageType.Server.CharacterRegion;

		public int Length(int protocolVersion) => 52;

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			var writer = new SpanWriter(span);
			// DoL writes byte 0x00 and then region ID as a byte
			// but region is represented elsewhere as a ushort
			writer.WriteUInt16BigEndian(_region.Id);
			writer.Skip(20);
			writer.WriteFixedString(_region.Port.ToString(), 5);
			writer.WriteFixedString(_region.Port.ToString(), 5); // yeah, twice
			writer.WriteFixedString(_region.Address.ToString(), 20);
		}
	}
}
