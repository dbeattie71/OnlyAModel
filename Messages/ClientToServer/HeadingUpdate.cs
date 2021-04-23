using System;

namespace Messages.ClientToServer
{
	public class HeadingUpdate
	{
		public ushort Heading { get; }
		public bool TargetInView { get; }
		public bool GroundTargetInView { get; }
		public byte SteedSlot { get; }
		public byte RidingFlag { get; }

		public HeadingUpdate(ushort heading, bool targetInView, bool groundTargetInView, byte steedSlot, byte ridingFlag)
		{
			Heading = heading;
			TargetInView = targetInView;
			GroundTargetInView = groundTargetInView;
			SteedSlot = steedSlot;
			RidingFlag = ridingFlag;
		}

		[AutowiredFactory(MessageType.ClientToServer.PlayerHeadingUpdate)]
		public static HeadingUpdate Unmarshal(ReadOnlyMemory<byte> payload)
		{
			// based on DoL's PlayerHeadingUpdateHandler
			var reader = new SpanReader(payload.Span);
			var sessionId = reader.ReadUInt16LittleEndian();
			var head = reader.ReadUInt16LittleEndian();
			reader.ReadByte(); // unknown
			var flags = reader.ReadByte();
			var steed = reader.ReadByte();
			var riding = reader.ReadByte();

			// FIXME this heading is at odds with the value from PositionUpdate
			// PositionUpdate is trusted, and this value is nonsensical and never changes
			var heading = (ushort)(head * 0x0FFF);
			var tgt = (flags & 0x08) != 0;
			var gtgt = (flags & 0x10) != 0;
			return new HeadingUpdate(heading, tgt, gtgt, steed, riding);
		}
	}
}
