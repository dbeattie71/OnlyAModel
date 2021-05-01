using Core.Event;
using Protocol.Autowire;

namespace Protocol.Client
{
	public class Handshake
	{
		[Unmarshaller(MessageType.Client.Handshake)]
		public static Handshake Unmarshall(MessageEventArgs _)
		{
			return new Handshake();
		}
	}
}
