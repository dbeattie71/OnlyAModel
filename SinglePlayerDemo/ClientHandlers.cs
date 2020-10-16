using Core.Event;
using Messages;
using System;
using static Messages.MessageType.ClientToServer;

namespace SinglePlayerDemo
{
	class ClientHandlers
	{
		public EventHandler<MessageEventArgs> OnMessageReceived { get => Handlers.OnMessageReceived; }

		private MessageHandlers Handlers { get; } = new MessageHandlers();

		public ClientHandlers()
		{
			Handlers[CryptKeyRequest] += (server, args) => {
				; // set breakpoint to examine session and message
			};
		}
	}
}
