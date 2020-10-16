using System;

namespace Core.Event
{
	public class ConnectionEventArgs : EventArgs
	{
		public Session Session { get; }
		public bool Connected { get; }

		internal ConnectionEventArgs(Session session, bool connected)
		{
			Session = session;
			Connected = connected;
		}
	}
}
