using System;

namespace Messages.ClientToServer
{
	public class CharacterInitRequest
	{
		[AutowiredFactory(MessageType.ClientToServer.PlayerInitRequest)]
		public static CharacterInitRequest Unmarshal(ReadOnlyMemory<byte> payload)
		{
			// DoL doesn't read anything from this message
			return new CharacterInitRequest();
		}
	}
}
