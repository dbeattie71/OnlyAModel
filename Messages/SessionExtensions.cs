using Core;

namespace Messages
{
	public static class SessionExtensions
	{
		public static void Send(this Session session, IPayload payload)
		{
			session.Send((byte)payload.Type, payload);
		}
	}
}
