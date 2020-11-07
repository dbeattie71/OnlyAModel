using System;

namespace Messages.ClientToServer
{
	public class Handshake
	{
		[AutowiredFactory(MessageType.ClientToServer.Handshake)]
		public static Handshake Unmarshall(ReadOnlyMemory<byte> payload)
		{
			return new Handshake();
		}
	}
}
