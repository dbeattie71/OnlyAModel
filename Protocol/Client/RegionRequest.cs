using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class RegionRequest
	{
		public int CharacterSlot { get; }

		private RegionRequest(int characterSlot)
		{
			CharacterSlot = characterSlot;
		}

		[Unmarshaller(MessageType.Client.RegionRequest)]
		public static RegionRequest Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			var slot = reader.ReadByte();
			return new RegionRequest(slot);
		}
	}
}
