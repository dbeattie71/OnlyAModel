using Models.Character;
using System;

namespace Messages.ServerToClient
{
	public class PositionAndObjectId : IPayload
	{
		private readonly Character _character;

		public PositionAndObjectId(Character character)
		{
			_character = character;
		}

		public byte MessageType => Messages.MessageType.ServerToClient.PositionAndObjectID;

		public int Length => 32;

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			var coords = _character.Coordinates;
			writer.WriteFloat(coords.X);
			writer.WriteFloat(coords.Y);
			writer.WriteFloat(coords.Z);
			writer.WriteUInt16LittleEndian(1); // arbitrary object id
			writer.WriteUInt16LittleEndian(coords.Heading);
			// next two shorts have something to do with dungeons
			writer.WriteUInt16LittleEndian(0);
			writer.WriteUInt16LittleEndian(0);
			// unsure about byte order here
			// might have missed something in the DoL version
			//writer.WriteUInt16LittleEndian(_character.Region);
			writer.WriteUInt16BigEndian(_character.Region);
			writer.WriteByte(0x80); // 0x80 = diving enabled - TODO get from region
			// DoL sends server name if current region is housing, otherwise 0x00
			writer.WriteByte(0);
			// 4 more 0x00 bytes
		}
	}
}
