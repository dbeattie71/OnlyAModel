using Core;
using Core.Event;
using Messages;
using Messages.ClientToServer;

namespace SinglePlayerDemo
{
	public class AutowiredHandlers
	{
		[AutowiredHandler]
		public void OnCryptKeyRequest(Server server, MessageEventArgs args, CryptKeyRequest payload)
		{
			args.Session.Send((byte)MessageType.ServerToClient.CryptKey, new byte[6]);
		}
	}
}
