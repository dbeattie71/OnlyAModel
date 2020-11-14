using Core;

namespace MultiPlayer
{
	internal static class SessionExtensions
	{
		internal static SessionState GetState(this Session session)
		{
			return (SessionState)session.UserData;
		}

		internal static void SetState(this Session session, SessionState state)
		{
			session.UserData = state;
		}
	}
}
