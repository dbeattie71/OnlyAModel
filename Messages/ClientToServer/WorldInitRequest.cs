using System;

namespace Messages.ClientToServer
{
	public class WorldInitRequest
	{
		[AutowiredFactory(MessageType.ClientToServer.WorldInitRequest)]
		public static WorldInitRequest Unmarshal(ReadOnlyMemory<byte> payload)
		{
			return new WorldInitRequest();
		}
	}
}
