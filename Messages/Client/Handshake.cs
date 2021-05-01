using System;

namespace Messages.Client
{
	public class Handshake
	{
		[AutowiredFactory(MessageType.Client.Handshake)]
		public static Handshake Unmarshall(ReadOnlyMemory<byte> payload)
		{
			return new Handshake();
		}
	}
}
