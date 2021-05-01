using System;

namespace Core.Event
{
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>
		/// Null if the error originated in the server.
		/// </summary>
		public Session Session { get; }
		public Exception Error { get; }

		internal ErrorEventArgs(Session session, Exception error)
		{
			Session = session;
			Error = error;
		}
	}
}
