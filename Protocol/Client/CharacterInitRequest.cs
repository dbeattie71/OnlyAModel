using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class CharacterInitRequest
	{
		private CharacterInitRequest() { }

		[Unmarshaller(MessageType.Client.CharacterInitRequest)]
		public static CharacterInitRequest Unmarshall(MessageEventArgs args)
		{
			return new CharacterInitRequest();
		}
	}
}
