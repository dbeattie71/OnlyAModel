using System;

namespace Core.Event
{
	public class MessageEventArgs : EventArgs
	{
		public Session Session { get; }
		public Message Message { get; }

		internal MessageEventArgs(Session session, Message message)
		{
			Session = session;
			Message = message;
		}
	}
}
