using System;

namespace Messages.Client
{
	public class RegionListRequest
	{
		public int CharacterSlot { get; }

		private RegionListRequest(int slot)
		{
			CharacterSlot = slot;
		}

		[AutowiredFactory(MessageType.Client.RegionListRequest)]
		public static RegionListRequest Unmarshal(ReadOnlyMemory<byte> payload)
		{
			var reader = new SpanReader(payload.Span);
			var slot = reader.ReadByte();
			return new RegionListRequest(slot);
		}
	}
}
