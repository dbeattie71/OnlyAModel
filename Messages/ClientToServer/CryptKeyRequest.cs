using System;

namespace Messages.ClientToServer
{
	public class CryptKeyRequest
	{
		[AutowiredFactory(MessageType.ClientToServer.CryptKeyRequest)]
		public static CryptKeyRequest Unmarshall(ReadOnlyMemory<byte> payload)
		{
			return new CryptKeyRequest();
		}
	}
}
