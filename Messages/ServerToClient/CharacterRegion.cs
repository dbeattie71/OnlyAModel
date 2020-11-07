using Models.World;
using System;

namespace Messages.ServerToClient
{
	public class CharacterRegion : IPayload
	{
		private readonly Region _region;

		public CharacterRegion(Region region)
		{
			_region = region;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.CharacterRegion;

		public int Length => 52;

		public void Marshal(Span<byte> span)
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
