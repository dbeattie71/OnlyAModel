using Core;

namespace MultiPlayer
{
	internal static class SessionExtensions
	{
		internal static SessionData Data(this Session session)
		{
			return (SessionData)session.UserData;
		}
	}
}
