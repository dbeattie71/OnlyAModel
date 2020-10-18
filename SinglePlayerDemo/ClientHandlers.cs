using Core.Event;
using Messages;
using System;
using static Messages.MessageType.ClientToServer;
using static Messages.MessageType.ServerToClient;

namespace SinglePlayerDemo
{
	class ClientHandlers
	{
		public EventHandler<MessageEventArgs> OnMessageReceived { get => Handlers.OnMessageReceived; }

		private MessageHandlers Handlers { get; } = new MessageHandlers();

		public ClientHandlers()
		{
			Handlers[CryptKeyRequest] += SendCryptKey;
		}

		private void SendCryptKey(object server, MessageEventArgs args)
		{
			// live server sends the version string as the key
			//var payload = new byte[13];
			//payload[0] = 0x07;  // key length including terminator
			//payload[1] = 0x00;  // unknown
			//payload[2] = 0x00;  // unknown
			//payload[3] = 0x00;  // unknown
			//payload[4] = 0x31;  // '1'
			//payload[5] = 0x2E;  // '.'
			//payload[6] = 0x31;  // '1'
			//payload[7] = 0x32;  // '2'
			//payload[8] = 0x36;  // '6'
			//payload[9] = 0x62;  // 'b'
			//payload[10] = 0x00; // string terminator
			//payload[11] = 0x75; // build lsb
			//payload[12] = 0x05; // build msb

			// when launched with DAoCPortal, 1.125d accepts a zero-length key and build version zero
			var payload = new byte[7];
			args.Session.Send((byte)CryptKey, payload);
		}
	}
}
