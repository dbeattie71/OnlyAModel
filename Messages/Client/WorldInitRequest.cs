using System;

namespace Messages.Client
{
	public class WorldInitRequest
	{
		[AutowiredFactory(MessageType.Client.WorldInitRequest)]
		public static WorldInitRequest Unmarshal(ReadOnlyMemory<byte> payload)
		{
			return new WorldInitRequest();
		}
	}
}
