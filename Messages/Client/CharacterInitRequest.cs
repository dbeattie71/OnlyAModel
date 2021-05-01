using System;

namespace Messages.Client
{
	public class CharacterInitRequest
	{
		[AutowiredFactory(MessageType.Client.PlayerInitRequest)]
		public static CharacterInitRequest Unmarshal(ReadOnlyMemory<byte> payload)
		{
			// DoL doesn't read anything from this message
			return new CharacterInitRequest();
		}
	}
}
