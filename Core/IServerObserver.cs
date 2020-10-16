using Core.Event;

namespace Core
{
	public interface IServerObserver
	{
		void OnConnection(object server, ConnectionEventArgs args);
		void OnMessageSent(object server, MessageEventArgs args);
		void OnMessageReceived(object server, MessageEventArgs args);
		void OnError(object server, ErrorEventArgs args);
	}
}
