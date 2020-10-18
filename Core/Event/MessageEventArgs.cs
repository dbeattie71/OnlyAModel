using System;

namespace Core.Event
{
	public class MessageEventArgs : EventArgs
	{
		public Session Session { get; }
		public IMessage Message { get; }

		internal MessageEventArgs(Session session, IMessage message)
		{
			Session = session;
			Message = message;
		}
	}
}
