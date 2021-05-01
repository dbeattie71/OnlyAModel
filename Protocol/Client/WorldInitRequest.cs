using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class WorldInitRequest
	{
		private WorldInitRequest() { }

		[Unmarshaller(MessageType.Client.WorldInitRequest)]
		public static WorldInitRequest Unmarshal(MessageEventArgs _)
		{
			return new WorldInitRequest();
		}
	}
}
