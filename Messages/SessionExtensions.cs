using Core;

namespace Messages
{
	public static class SessionExtensions
	{
		public static void Send(this Session session, IServerMessage message)
		{
			session.Send(message.Type, message);
		}
	}
}
