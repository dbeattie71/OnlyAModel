using Core.Event;
using System;

namespace Messages
{
	public class MessageHandlers
	{
		private EventHandler<MessageEventArgs>[] _handlers = new EventHandler<MessageEventArgs>[256];

		public EventHandler<MessageEventArgs> this[MessageType.ClientToServer type]
		{
			get { return _handlers[(int)type]; }
			set { _handlers[(int)type] = value; }
		}

		public void OnMessageReceived(object server, MessageEventArgs args)
		{
				_handlers[args.Message.Type]?.Invoke(server, args);
		}
	}
}
