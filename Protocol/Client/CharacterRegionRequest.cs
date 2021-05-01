using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class CharacterRegionRequest
	{
		public int CharacterSlot { get; }

		private CharacterRegionRequest(int characterSlot)
		{
			CharacterSlot = characterSlot;
		}

		[Unmarshaller(MessageType.Client.CharacterRegionRequest)]
		public static CharacterRegionRequest Unmarshall(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			var slot = reader.ReadByte();
			return new CharacterRegionRequest(slot);
		}
	}
}
